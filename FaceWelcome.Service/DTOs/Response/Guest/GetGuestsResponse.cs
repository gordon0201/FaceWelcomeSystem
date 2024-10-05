using System.Collections.Generic;

namespace FaceWelcome.Service.DTOs.Response.Guest
{
    public class GetGuestsResponse
    {


        // Danh sách các khách được trả về
        public List<GetGuestResponse> Guests { get; set; }

        // Thông báo chung về quá trình xử lý
        public string Message { get; set; }

        // Constructor để khởi tạo danh sách khách
        public GetGuestsResponse()
        {
            Guests = new List<GetGuestResponse>();
        }
    }
}
