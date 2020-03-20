using SYS_MODELS._ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespException
    {
        public RespException(bool error, string message, EKB_SYS_REQUEST requestType)
        {
            Error = error;
            Message = message;
            eop = requestType.ToString();
        }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string  eop { get; set; }
    }
}