using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EKANBAN_SYS_LIB;
using SYS_MODELS;
using EKB_WEBSERVER.API.RuntimeModels;


namespace EKB_WEBSERVER.API
{
    public class API_EKANBANController : ApiController
    {
        private IDbName Database = new RealDb();
        [HttpGet]
        [Route("api-getdata/{id}")]
        public IHttpActionResult EKanban_GetData(int id)
        {
            return Json(new EKanbanResponse(Database, id));
        }

        [HttpGet]
        [Route("api-confirm/{id}/{qty}")]
        public IHttpActionResult EKanban_Cofirm(int id, int qty)
        {
            return Json(new ConfirmResponse(Database, id, qty));
        }


    }
}
