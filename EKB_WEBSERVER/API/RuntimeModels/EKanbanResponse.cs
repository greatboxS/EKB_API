using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using EKANBAN_SYS_LIB;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class EKanbanResponse
    {
        public EKanbanResponse(IDbName _database, int _ekanbanDeviceId)
        {

            EKanbanTaskQuery eKanbanTaskQuery = new EKanbanTaskQuery(_database);
            SequenceQuery sequenceQuery = new SequenceQuery(_database);
            ComponentQuery componentQuery = new ComponentQuery(_database);

            var ekanbanDevice = eKanbanTaskQuery.GetEKanbanDevice(_ekanbanDeviceId);

            var _Interface = eKanbanTaskQuery.GetLastEKanbanInterface(_ekanbanDeviceId);

            SizeList = new List<SizeInfo>();
            PartList = new List<PartInfo>();
            List<OriginalSize> originalSizes = new List<OriginalSize>();

            foreach (var item in _Interface.EKanbanLoadings)
            {
                var Seq = sequenceQuery.GetOriginalSequence(ShareFuncs.GetInt(item.OriginalSequence_Id));
                TotalCartQty += ShareFuncs.GetInt(Seq.Quantity);
                foreach (var size in Seq.OriginalSizes)
                {
                    originalSizes.Add(size);
                }
            }

            foreach (var item in originalSizes)
            {
                var foundSizes = SizeList.Where(i => i.SizeId == item.SizeId);
                if (foundSizes.Count() > 0)
                {
                    foundSizes.First().SizeQty += ShareFuncs.GetInt(item.Quantity);
                }
                else
                {
                    SizeList.Add(new SizeInfo(ShareFuncs.GetInt(item.SizeId), ShareFuncs.GetInt(item.Quantity)));
                }
            }

            Interface = new InterfaceInfo(_Interface);

            foreach (var item in _Interface.EkanbanComponents)
            {
                var Component = componentQuery.GetShoeComponent(ShareFuncs.GetInt(item.ShoeComponent_Id));
                PartList.Add(new PartInfo(ShareFuncs.ConvertToUnSign(Component.Reference)));
            }

            //if (DateTime.Now.Hour == 17)
            //{
            //    if (DateTime.Now.Minute > 50)
            //    {
            //        ScreenOff = true;
            //        ekanbanDevice.ScreenOff = true;
            //    }
            //}
            //else
            //{
            //    if (DateTime.Now.Hour > 17)
            //    {
            //        ScreenOff = true;
            //        ekanbanDevice.ScreenOff = true;
            //    }
            //}

            //if (DateTime.Now.Hour == 6 && DateTime.Now.Minute < 45)
            //{
            //    ScreenOff = false;
            //    ekanbanDevice.ScreenOff = false;
            //}

            ekanbanDevice.NetworkStatus = (int)SYS_MODELS._ENUM.SysActionCode.ONLINE;
            ekanbanDevice.LastOnline = DateTime.Now;
            eKanbanTaskQuery.UpdateEKanbanDevice(ekanbanDevice);

            if (ekanbanDevice.ScreenOff == null)
                ScreenOff = false;
            else
                ScreenOff = (bool)ekanbanDevice.ScreenOff;
        }
        public int TotalCartQty { get; set; }
        public InterfaceInfo Interface { get; set; }
        public List<PartInfo> PartList { get; set; }
        public List<SizeInfo> SizeList { get; set; }
        public bool ScreenOff { get; set; }
        public string eop { get; set; } = SYS_MODELS._ENUM.EKB_SYS_REQUEST.EKANBAN_GET_INTERFACE.ToString();
    }

    public class InterfaceInfo
    {
        public InterfaceInfo(EKanbanInterface _interface)
        {
            id = _interface.id;
            PoNumber = _interface.PO;
            SequenceNo = _interface.SequenceNo;
            PoQty = _interface.POqty;
            CartQty = _interface.CartQty;
            Line = _interface.Line;
        }
        public int id { get; set; }
        public string PoNumber { get; set; }
        public string SequenceNo { get; set; }
        public string PoQty { get; set; }
        public string CartQty { get; set; }
        public string Line { get; set; }
    }

    public class PartInfo
    {
        public PartInfo(string _name)
        {
            Name = _name;
        }
        public string Name { get; set; }
    }

    public class SizeInfo
    {
        public SizeInfo(int _sizeId, int _sizeQty)
        {
            SizeId = _sizeId;
            SizeQty = _sizeQty;
        }
        public int SizeId { get; set; }
        public int SizeQty { get; set; }
    }

}