using FaceWelcome.Repository.Models;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Request.GuestImage;
using FaceWelcome.Service.Services.Interfaces;
using System.IO;

namespace FaceWelcome.Service.Services.Implementations
{
    public class GuestImageService : IGuestImageService
    {
        private readonly UnitOfWork _unitOfWork;

        public GuestImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

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
    }
}
