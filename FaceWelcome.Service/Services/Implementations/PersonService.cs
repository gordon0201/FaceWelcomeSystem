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
        #region Create Person
        public async Task CreatePersonAsync(PostPersonRequest postPersonRequest)
        {
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
                string imageUrl = await _unitOfWork.FirebaseStorageRepository.UploadImageToFirebase(tempFilePath, "persons");

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


    }
}
