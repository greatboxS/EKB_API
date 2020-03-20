using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class NewUser
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string RFID { get; set; }
        public string Dep { get; set; }
    }
}