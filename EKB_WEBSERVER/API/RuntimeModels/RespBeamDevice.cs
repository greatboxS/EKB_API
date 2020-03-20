using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EKANBAN_SYS_LIB;
using SYS_MODELS;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespBeamDevice
    {

        public RespBeamDevice(IDbName database, string sysCode, string name)
        {
            try
            {
                BeamCutQuery BeamCutQuery = new BeamCutQuery(database);
                var bdevice = BeamCutQuery.GetBeamCutDevice(sysCode, name);
                if (bdevice == null)
                {
                    Exception = new RespException(true, "Beam machine not found", SYS_MODELS._ENUM.EKB_SYS_REQUEST.GET_DEVICE_INFO);
                    return;
                }

                SystemCode = bdevice.MachineCode;
                Name = bdevice.Name;
                id = bdevice.id;
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, SYS_MODELS._ENUM.EKB_SYS_REQUEST.GET_DEVICE_INFO);
            }

        }

        public int id { get; set; }
        public string SystemCode { get; set; }
        public string Name { get; set; }
        public RespException Exception { get; set; } = new RespException(false, "Get device: OK", SYS_MODELS._ENUM.EKB_SYS_REQUEST.GET_DEVICE_INFO);
    }
}