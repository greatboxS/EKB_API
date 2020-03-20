using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS._ENUM;
namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class LocalTime
    {
        public LocalTime()
        {
            Day = DateTime.Now.Day;
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year % 100;
            Hour = DateTime.Now.Hour;
            Min = DateTime.Now.Minute;
            Sec = DateTime.Now.Second;
        }

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }

        public RespException Exception = new RespException(false, "Get Time: OK", EKB_SYS_REQUEST.GET_TIME);
    }
}