using AutoMapper;
using Azure.Core;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Response.Person;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }


        #region Update person
        public async Task UpdatePersonAsync(Guid id, UpdatePersonRequest updatePersonRequest)
        {
            string folderName = "persons";
            var existedPerson = await _unitOfWork.PersonRepository.GetPersonByIdAsync(id);
            if (existedPerson == null)
            {
                throw new Exception($"Person with ID {id} cannot be found.");
            }
            try
            {
                existedPerson.FullName = updatePersonRequest.FullName;
                existedPerson.Address = updatePersonRequest.Address;
                existedPerson.DateOfBirth = updatePersonRequest.DateOfBirth;
                existedPerson.Gender = updatePersonRequest.Gender;
                existedPerson.Email1 = updatePersonRequest.Email1;
                existedPerson.Email2 = updatePersonRequest.Email2;
                existedPerson.Phone = updatePersonRequest.Phone;
                existedPerson.Position = updatePersonRequest.Position;

                if (updatePersonRequest.Image != null)
                {
                    //Tạo tên file tạm thời
                    var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(updatePersonRequest.Image.FileName));

                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await updatePersonRequest.Image.CopyToAsync(stream);
                    }
                    var imageUrl = await _unitOfWork.FirebaseStorageRepository.UploadImageToFirebase(tempFilePath, folderName);

                    // xóa ảnh đã tồn tại trên Firebase nếu có
                    if (!string.IsNullOrEmpty(existedPerson.Image))
                    {
                        await _unitOfWork.FirebaseStorageRepository.DeleteImageFromFirebase(existedPerson.Image);
                    }
                    existedPerson.Image = imageUrl;

                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }

                await this._unitOfWork.PersonRepository.UpdatePersonAsync(existedPerson);
                await this._unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Create Person
        public async Task CreatePersonAsync(PostPersonRequest postPersonRequest)
        {
            string folderName = "persons";
            // Kiểm tra file có tồn tại không
            if (postPersonRequest.Image == null || postPersonRequest.Image.Length == 0)
            {
                throw new InvalidOperationException("No image file provided.");
            }

            // Tạo tên file tạm thời duy nhất
            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(postPersonRequest.Image.FileName));
            try
            {
                // Lưu file tạm thời
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await postPersonRequest.Image.CopyToAsync(stream); // Sử dụng ImageFile từ request
                }

                // Tải lên hình ảnh và lấy URL từ Firebase
                string imageUrl = await _unitOfWork.FirebaseStorageRepository.UploadImageToFirebase(tempFilePath, folderName);

                //Tạo đối tượng Person
                var person = new Person
                {
                    Image = imageUrl,
                    Address = postPersonRequest.Address,
                    Code = postPersonRequest.Code,
                    DateOfBirth = postPersonRequest.DateOfBirth,
                    Email1 = postPersonRequest.Email1,
                    Email2 = postPersonRequest.Email2,
                    FullName = postPersonRequest.FullName,
                    Gender = postPersonRequest.Gender,
                    Phone = postPersonRequest.Phone,
                    Position = postPersonRequest.Position,
                    OrganizationId = postPersonRequest.OrganizationId,
                };
                await _unitOfWork.PersonRepository.AddAsync(person);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get all people
        public async Task<GetPeopleResponse> GetAllPersonsAsync(GetPeopleRequest getPeopleRequest)
        {
            var people = await _unitOfWork.PersonRepository.GetPeopleAsync();
            if (people == null)
            {
                throw new Exception();
            }
            int totalRecords = people.Count;
            int totalPages = (int)System.Math.Ceiling((double)totalRecords / getPeopleRequest.PageSize);
            var list = people.Skip((getPeopleRequest.PageNumber - 1) * getPeopleRequest.PageSize)
               .Take(getPeopleRequest.PageSize)
               .ToList();
            var responseList = this._mapper.Map<List<GetPersonResponse>>(list);

            var response = new GetPeopleResponse
            {
                PageSize = getPeopleRequest.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = getPeopleRequest.PageNumber,
                Persons = responseList
            };
            return response;
        }
        #endregion

        #region Get person by id
        public async Task<GetPersonResponse> GetPersonByIdAsync(Guid id)
        {
            try
            {
                var person = await _unitOfWork.PersonRepository.GetPersonByIdAsync(id);
                if (person == null)
                {
                    throw new Exception();
                }
                var personResponse = this._mapper.Map<GetPersonResponse>(person);
                return personResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
