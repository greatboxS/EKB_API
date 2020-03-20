using EKB_WEBSERVER.API.RuntimeModels;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EKB_WEBSERVER.API
{
    public class BKanbanController : ApiController
    {
        //private IDbName Database = new FakeDb();
        private IDbName Database = new RealDb();
        [HttpGet]
        [Route("api-gettime")]
        public IHttpActionResult Get_Time()
        {
            return Json(new LocalTime());
        }

        [HttpGet]
        [Route("api-search_schedule/{line}/{month}/{po}")]
        public IHttpActionResult Beam_GetSchedule(string line, int month, string po)
        {
            return Json(new RespSchedule(Database, line, month, po));
        }

        [HttpGet]
        [Route("api-get_schedule/{line}/{month}/{startId}")]
        public IHttpActionResult Beam_GetSchedule(string line, int month, int startId)
        {
            return Json(new RespSchedule(Database, line, month, startId));
        }

        [HttpGet]
        [Route("api-get_order/{machineId}/{workerid}")]
        public IHttpActionResult Beam_GetSchedule(int machineId, int workerid)
        {
            return Json(new RespSchedule(Database, machineId, workerid));
        }

        [HttpGet]
        [Route("api-get_scheduleInfo/{scheduleId}/{originalPoId}/{componentId}")]
        public IHttpActionResult Beam_GetPoInfo(int scheduleId, int originalPoId,int  componentId)
        {
            return Json(new RespPoInfo(Database, scheduleId, originalPoId, componentId));
        }

        [HttpGet]
        [Route("api-get_component/{scheduleId}/{originalPoId}")]
        public IHttpActionResult Beam_GetComponent(int scheduleId, int originalPoId)
        {
            return Json(new RespComponent(Database, scheduleId, originalPoId));
        }

        [HttpGet]
        [Route("api-get_size/{seq1Id}/{seq2Id}")]
        public IHttpActionResult Beam_GetSize(int seq1Id, int seq2Id)
        {
            return Json(new RespSize(Database, seq1Id, seq2Id));
        }

        [HttpGet]
        [Route("api-start_cutting/{deviceId}/{workerId}/{clonePoId}/{seq1Id}/{seq2Id}/{binterfaceId}")]
        public IHttpActionResult Beam_StartCutting(int deviceId, int workerId, int clonePoId, int seq1Id, int seq2Id, int binterfaceId  )
        {
            return Json(new RespCutting(Database, deviceId, workerId, clonePoId, seq1Id, seq2Id, binterfaceId));
        }

        [HttpGet]
        [Route("api-stop_cutting/{binterfaceId}/{bcounter}")]
        public IHttpActionResult Beam_StopCutting(int binterfaceId, int bcounter)
        {
            return Json(new RespCutting(Database, binterfaceId, bcounter));
        }

        [HttpGet]
        [Route("api-confirm_size/{binterfaceId}/{sizeId}/{sizeQty}")]
        public IHttpActionResult Beam_ConfirmSize(int binterfaceId, int sizeId, int sizeQty)
        {
            return Json(new RespConfirmSize(Database, binterfaceId, sizeId, sizeQty));
        }

        [HttpGet]
        [Route("api-get_userinfo/{rfid}")]
        public IHttpActionResult Beam_GetUser(string rfid)
        {
            return Json(new RespUserInfo(Database, rfid));
        }

        [HttpGet]
        [Route("api-get_bdevice/{systemCode}/{BName}")]
        public IHttpActionResult Get_BeamDevice(string systemCode, string BName)
        {
            return Json(new RespBeamDevice(Database, systemCode, BName));
        }

        [HttpGet]
        [Route("api-get_lastcutting/{bdeviceId}/{binterfaceId}")]
        public IHttpActionResult Get_LastCutting(int bdeviceId, int binterfaceId)
        {
            return Json(new BLastCut(Database,bdeviceId, binterfaceId));
        }

        [HttpGet]
        [Route("api-bmachine_cuttime/{bmachineId}/{cuttime}")]
        public IHttpActionResult Record_CutTime(int bmachineId, int cuttime)
        {
            return Json(new BDeviceCutTime(Database, bmachineId, cuttime));
        }

        [HttpPost]
        [Route("api-new_user")]
        public IHttpActionResult AddNewUser([FromBody] NewUser newUser)
        {
            return Json(new RespUserInfo(Database, newUser));
        }
    }
}
