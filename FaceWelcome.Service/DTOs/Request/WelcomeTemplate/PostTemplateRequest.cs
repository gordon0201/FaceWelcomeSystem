using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Request.WelcomeTemplate
{
    public class PostTemplateRequest
    {
      

        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Content must not exceed 500 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Image number is required.")]
        public string Image { get; set; }

        //public DateTime? CreatedAt { get; set; }

        [Required(ErrorMessage = "Event id is required.")]
        public Guid? EventId { get; set; }
    }
}
