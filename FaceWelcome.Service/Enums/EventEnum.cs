using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Enums
{
    public class EventEnum
    {
        public enum Type
        {
            Conference,   // Hội nghị
            Workshop,     // Hội thảo
            Seminar,      // Chuyên đề
            Webinar,      // Hội thảo trực tuyến
            SocialEvent   // Sự kiện xã hội
        }

        public enum Status
        {
            ACTIVE,
            INACTIVE,
            DISABLE
        }
    }
}
