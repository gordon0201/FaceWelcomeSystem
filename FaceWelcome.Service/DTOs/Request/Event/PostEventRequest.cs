using System;
using System.ComponentModel.DataAnnotations;

namespace FaceWelcome.Service.DTOs.Request.Event
{
    public class PostEventRequest
    {
        [Required(ErrorMessage = "Tên sự kiện là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên sự kiện không được vượt quá 100 ký tự.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu là bắt buộc.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc là bắt buộc.")]
        public DateTime EndTime { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Địa điểm là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Địa điểm không được vượt quá 200 ký tự.")]
        public string Location { get; set; }

        // Có thể thêm các thuộc tính khác nếu cần
    }
}
