using FaceWelcome.Repository.Models;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Request.GuestImage;
using FaceWelcome.Service.Services.Interfaces;
using System.IO;
using FaceWelcome.Service.DTOs.Response.GuestImage;
using AutoMapper;

namespace FaceWelcome.Service.Services.Implementations
{
    public class GuestImageService : IGuestImageService
    {
        private readonly UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GuestImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }
        #region Create Guest Image
        public async Task CreateGuestImageAsync(PostGuestImageRequest request)
        {
            // Kiểm tra file có tồn tại không
            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                throw new InvalidOperationException("No image file provided.");
            }

            // Tạo tên file tạm thời duy nhất
            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(request.ImageFile.FileName));
            try
            {
                // Lưu file tạm thời
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await request.ImageFile.CopyToAsync(stream); // Sử dụng ImageFile từ request
                }

                // Tải lên hình ảnh và lấy URL từ Firebase
                string imageUrl = await _unitOfWork.FirebaseStorageRepository.UploadImageToFirebase(tempFilePath, "guest_images");

                // Tạo đối tượng GuestImage và lưu vào cơ sở dữ liệu
                var guestImage = new GuestImage
                {
                    Code = request.code,
                    Type = request.type,
                    Path = imageUrl,
                    Description = request.description,
                    CreatedAt = DateTime.UtcNow,
                    GuestId = request.guestId
                };

                await _unitOfWork.GuestImageRepository.AddAsync(guestImage); // Sử dụng GuestImageRepository từ UnitOfWork
                await _unitOfWork.CommitAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating guest image: " + ex.Message, ex);
            }
            finally
            {
                // Xóa file tạm thời sau khi tải lên hoặc xảy ra lỗi
                if (File.Exists(tempFilePath))
                {
                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch (IOException ioEx)
                    {
                        // Ghi log nếu không thể xóa tệp
                        Console.WriteLine($"Unable to delete temp file: {tempFilePath}, Error: {ioEx.Message}");
                    }
                }
            }
        }
        #endregion

        #region Delete Guest Image
        public async Task DeleteGuestImageAsync(Guid id)
        {
            // Lấy ảnh khách hiện tại từ cơ sở dữ liệu theo id
            var existedGuestImage = await _unitOfWork.GuestImageRepository.GetGuestImageByIdAsync(id);
            if (existedGuestImage == null)
            {
                throw new Exception($"Guest image with ID {id} cannot be found.");
            }
            try
            {
                await _unitOfWork.GuestImageRepository.DeleteGuestImageAsync(existedGuestImage);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the guest image: {ex.Message}");
            }
        }

        #endregion 

        #region Get all guest images
        public async Task<GetAllGuestImagesResponse> GetAllGuestImagesAsync(GetAllGuestImagesRequest getAllGuestImagesRequest)
        {
            // Lấy danh sách GuestImage từ repository
            var guestImages = await _unitOfWork.GuestImageRepository.GetAllGuestImagesAsync();
            if (guestImages == null)
            {
                throw new Exception("No guest images found.");
            }

            // Tính tổng số bản ghi và số trang
            int totalRecords = guestImages.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecords / getAllGuestImagesRequest.PageSize);

            // Phân trang cho danh sách GuestImages
            var paginatedList = guestImages.Skip((getAllGuestImagesRequest.PageNumber - 1) * getAllGuestImagesRequest.PageSize)
                                           .Take(getAllGuestImagesRequest.PageSize)
                                           .ToList();

            // Map danh sách GuestImages sang DTO response
            var responseList = _mapper.Map<List<GetGuestImageResponse>>(paginatedList);

            // Tạo đối tượng phản hồi chứa thông tin phân trang và danh sách GuestImages
            var response = new GetAllGuestImagesResponse
            {
                PageSize = getAllGuestImagesRequest.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = getAllGuestImagesRequest.PageNumber,
                GuestImages = responseList
            };

            return response;
        }
        #endregion

        #region Get Guest Image By ID
        public async Task<GetGuestImageResponse> GetGuestImageByIdAsync(Guid id)
        {
            try
            {
                var guestImage = await _unitOfWork.GuestImageRepository.GetGuestImageByIdAsync(id);
                if (guestImage is null)
                {
                    throw new Exception("Guest image not found.");
                }

                var guestImageResponse = this._mapper.Map<GetGuestImageResponse>(guestImage);
                return guestImageResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Update Guest Image
        public async Task UpdateGuestImageAsync(Guid id, UpdateGuestImageRequest updateGuestImageRequest)
        {
            // Lấy đối tượng GuestImage hiện tại từ cơ sở dữ liệu theo id
            var existedGuestImage = await _unitOfWork.GuestImageRepository.GetGuestImageByIdAsync(id);
            if (existedGuestImage == null)
            {
                throw new Exception($"Guest image with ID {id} cannot be found.");
            }

            // Kiểm tra file hình ảnh có tồn tại không
            if (updateGuestImageRequest.ImageFile != null && updateGuestImageRequest.ImageFile.Length > 0)
            {
                // Tạo tên file tạm thời duy nhất
                var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(updateGuestImageRequest.ImageFile.FileName));

                try
                {
                    // Lưu file tạm thời
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await updateGuestImageRequest.ImageFile.CopyToAsync(stream);
                    }

                    // Tải lên hình ảnh và lấy URL từ Firebase
                    string imageUrl = await _unitOfWork.FirebaseStorageRepository.UploadImageToFirebase(tempFilePath, "guest_images");

                    // Cập nhật thuộc tính Path của GuestImage
                    existedGuestImage.Path = imageUrl;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error uploading image: " + ex.Message, ex);
                }
                finally
                {
                    // Xóa file tạm thời sau khi tải lên hoặc xảy ra lỗi
                    if (File.Exists(tempFilePath))
                    {
                        try
                        {
                            File.Delete(tempFilePath);
                        }
                        catch (IOException ioEx)
                        {
                            // Ghi log nếu không thể xóa tệp
                            Console.WriteLine($"Unable to delete temp file: {tempFilePath}, Error: {ioEx.Message}");
                        }
                    }
                }
            }

            // Cập nhật các thuộc tính khác của GuestImage từ request
            existedGuestImage.Type = updateGuestImageRequest.type;
            existedGuestImage.Description = updateGuestImageRequest.description;
            existedGuestImage.GuestId = updateGuestImageRequest.guestId;

            // Cập nhật GuestImage trong cơ sở dữ liệu
            await _unitOfWork.GuestImageRepository.UpdateGuestImageAsync(existedGuestImage);
            await _unitOfWork.CommitAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }
        #endregion

    }
}
