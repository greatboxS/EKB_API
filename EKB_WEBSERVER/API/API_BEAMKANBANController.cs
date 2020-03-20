using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EKANBAN_SYS_LIB;
using EKB_WEBSERVER.API.RuntimeModels;
using SYS_MODELS;

namespace EKB_WEBSERVER.API
{
    public class API_BEAMKANBANController : ApiController
    {
        private IDbName Database = new RealDb();
        [HttpGet]
        [Route("api-gettime")]
        public IHttpActionResult Get_Time()
        {
            return Json(new LocalTime());
        }

        [HttpGet]
        [Route("api-searching/{line}/{po}")]
        public IHttpActionResult Beam_GetSchedule(string line, string po)
        {
            return Json(new RespSchedule(Database, line, po));
        }

        [HttpGet]
        [Route("api-getschedule/{line}/{month}/{startId}")]
        public IHttpActionResult Beam_GetSchedule(string line, int month, int startId)
        {
            return Json(new RespSchedule(Database, line, month, startId));
        }

        [HttpGet]
        [Route("api-getpoinfo/{scheduleId}/{componentId}")]
        public IHttpActionResult Beam_GetPoInfo(int scheduleId, int componentId)
        {
            return Json(new RespPoInfo(Database, scheduleId, componentId));
        }

        [HttpGet]
        [Route("api-getcomponent/{scheduleId}")]
        public IHttpActionResult Beam_GetComponent(int scheduleId)
        {
            return Json(new RespComponent(Database, scheduleId));
        }

        [HttpGet]
        [Route("api-getsize/{clonePoId}/{cloneSeqId}/{totalSeq}")]
        public IHttpActionResult Beam_GetSize(int clonePoId, int cloneSeqId, int totalSeq)
        {
            return Json(new RespSize(Database, clonePoId, cloneSeqId, totalSeq));
        }

        [HttpGet]
        [Route("api-startcutting/{deviceId}/{workerId}/{scheduleId}/{clonePoId}/{cloneSeqId}/{totalSeq}")]
        public IHttpActionResult Beam_StartCutting(int deviceId, int workerId, int scheduleId, int clonePoId, int cloneSeqId, int totalSeq)
        {
            return Json(new RespCutting(Database, deviceId, workerId, scheduleId, clonePoId, cloneSeqId, totalSeq));
        }

        [HttpGet]
        [Route("api-stopcutting/{binterfaceId}/{cutqty}/{bcounter}")]
        public IHttpActionResult Beam_StopCutting(int binterfaceId, int cutqty, int bcounter)
        {
            return Json(new RespCutting(Database, binterfaceId, cutqty, bcounter));
        }

        [HttpGet]
        [Route("api-confirmsize/{binterfaceId}/{sizeId}/{sizeQty}")]
        public IHttpActionResult Beam_ConfirmSize(int binterfaceId, int sizeId, int sizeQty)
        {
            return Json(new RespConfirmSize(Database, binterfaceId, sizeId, sizeQty));
        }

        [HttpGet]
        [Route("api-getuserinfo/{rfid}")]
        public IHttpActionResult Beam_GetUser(string rfid)
        {
            return Json(new RespUserInfo(Database, rfid));
        }

        [HttpGet]
        [Route("api-getbdevice/{systemCode}/{BName}")]
        public IHttpActionResult Get_BeamDevice(string systemCode, string BName)
        {
            return Json(new RespBeamDevice(Database, systemCode, BName));
        }

        [HttpGet]
        [Route("api-getlastcutting/{binterfaceId}")]
        public IHttpActionResult Get_LastCutting(int binterfaceId)
        {
            return Json(new RespCutting(Database, binterfaceId));
        }

        [HttpGet]
        [Route("api-finishallcutting/{binterfaceId}/{bcutcounter}")]
        public IHttpActionResult FinishAllCutting(int binterfaceId, int bcutcounter)
        {
            return Json(new RespFinishAll(Database, binterfaceId,bcutcounter));
        }
    }
}
