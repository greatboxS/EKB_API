using BEAMCUT_TASK;
using BUILDING;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEQUENCE;
using System.Data.Entity;

namespace EKANBAN_SYS_LIB
{
    public class BeamCutQuery
    {
        private IDbName database;
        public BeamCutContext BeamCutContext { get; set; }

        public BeamCutQuery(IDbName _database)
        {
            database = _database;
            BeamCutContext = new BeamCutContext(database);
        }

        public BeamCutInterface GetBeamInterfaceById(int? _bInterfaceId)
        {
            return GetBeamInterfaceById(ShareFuncs.GetInt(_bInterfaceId));
        }

        public BeamCutInterface GetLastInterface(int deviceId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutInterfaces
                        .Where(i => i.BeamCutDevice_Id == deviceId)
                        .OrderByDescending(i => i.id)
                        .FirstOrDefault();
                }
            }
            catch { return null; }
        }
        public BeamCutInterface GetBeamInterfaceById(int _bInterfaceId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutInterfaces
                        .Where(i => i.id == _bInterfaceId)
                        .First();
                }
            }
            catch { return null; }
        }
        public BeamCutInterface GetBeamInterfaceByOriginalPo(int? _originalPoId)
        {
            return GetBeamInterfaceByOriginalPo(ShareFuncs.GetInt(_originalPoId));
        }
        public BeamCutInterface GetBeamInterfaceByOriginalPo(int _originalPoId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutInterfaces
                        .Where(i => i.OriginalPo_Id == _originalPoId)
                        .First();
                }
            }
            catch { return null; }
        }
        public BeamCutInterface GetBeamInterfaceByScheduleId(int? _scheduleId)
        {
            return GetBeamInterfaceByScheduleId(ShareFuncs.GetInt(_scheduleId));
        }
        public BeamCutInterface GetBeamInterfaceByScheduleId(int _scheduleId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutInterfaces
                        .Where(i => i.Schedule_Id == _scheduleId)
                        .First();
                }
            }
            catch { return null; }
        }
        public BeamCutPo AddNewBeamCutPo(int? originalPoId, int? componentId)
        {
            return AddNewBeamCutPo(ShareFuncs.GetInt(originalPoId), ShareFuncs.GetInt(componentId));
        }
        public BeamCutPo AddNewBeamCutPo(int originalPoId, int componentId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var result = BeamCutContext.BeamCutPos.Add(new BeamCutPo
                    {
                        OriginalPo_Id = originalPoId,
                        Component_Id = componentId
                    });
                    BeamCutContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public BeamCutSeq AddNewBeamSeq(OriginalPOsequence originalPOsequence, int componentId, int BeamCutPoId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var result = BeamCutContext.BeamCutSeqs.Add(new BeamCutSeq
                    {
                        OriginalPo_Id = originalPOsequence.OriginalPO_Id,
                        Component_Id = componentId,
                        BeamCutPo_Id = BeamCutPoId,
                        SequenceNo = originalPOsequence.SequenceNo,
                        SequenceQty = originalPOsequence.Quantity,
                        Finish = false,
                        CutQty = 0,
                    });
                    BeamCutContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public BeamCutPo AddNewBeamCutPo(OriginalPO originalPo, ShoeComponent shoeComponent)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var result = BeamCutContext.BeamCutPos.Add(new BeamCutPo
                    {
                        OriginalPo_Id = originalPo.id,
                        PoNumber = originalPo.PoNumber,
                        PoQuantity = ShareFuncs.GetInt(originalPo.Quantity),
                        ProductionLine_Id = ShareFuncs.GetInt(originalPo.ProductionLine_Id),
                        Finish = false,
                        CuttingQuantity = 0,
                        ComponentRef = shoeComponent.Reference,
                        Component_Id = shoeComponent.Id
                    });
                    BeamCutContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public BeamCutPo CloneOriginalPo(OriginalPO originalPo, ShoeComponent shoeComponent)
        {
            try
            {
                SequenceQuery sequenceQuery = new SequenceQuery(database);
                ScheduleQuery scheduleQuery = new ScheduleQuery(database);
                ComponentQuery componentQuery = new ComponentQuery(database);

                // New BeamCutPo
                var beamCutPo = AddNewBeamCutPo(originalPo, shoeComponent);

                if (beamCutPo == null)
                {
                    Console.WriteLine("Can not add new beam cut po");
                    return null;
                }
                // make return value
                beamCutPo.BeamCutSeqs = new List<BeamCutSeq>();

                foreach (var seq in originalPo.OriginalPOsequences)
                {
                    var currentSeq = sequenceQuery.GetOriginalSequence(seq.id);

                    //New BeamCutSeq
                    var beamSeq = AddNewBeamSeq(currentSeq, shoeComponent.Id, beamCutPo.id);

                    if (beamSeq == null)
                        continue;
                    // make return value
                    beamSeq.BeamCutSizes = new List<BeamCutSize>();

                    foreach (var size in currentSeq.OriginalSizes)
                    {
                        var beamSize = AddNewBeamSize(size, beamSeq.id, ShareFuncs.GetInt(beamSeq.SequenceNo), shoeComponent.Id, beamCutPo.id);
                        if (beamSize == null)
                            continue;
                        // make return value
                        beamSeq.BeamCutSizes.Add(beamSize);
                    }
                    // make return value
                    beamCutPo.BeamCutSeqs.Add(beamSeq);
                }
                // make return value
                return beamCutPo;
            }
            catch { return null; }
        }

        public BeamCutPo CloneOriginalPo(int scheduleId, int componentId)
        {
            try
            {
                SequenceQuery sequenceQuery = new SequenceQuery(database);
                ScheduleQuery scheduleQuery = new ScheduleQuery(database);
                ComponentQuery componentQuery = new ComponentQuery(database);

                var schedule = scheduleQuery.GetSchedule(scheduleId);
                if (schedule == null)
                    return null;

                var originalPo = sequenceQuery.GetOriginalPo(schedule);

                if (originalPo == null)
                    return null;

                if (FindOriginalPo(originalPo.id, componentId))
                {
                    Console.WriteLine("Po is already added\r\nContinue.");
                    return null;
                }
                // New BeamCutPo
                var beamCutPo = AddNewBeamCutPo(originalPo.id, componentId);

                if (beamCutPo == null)
                {
                    Console.WriteLine("Can not add new beam cut po");
                    return null;
                }
                // make return value
                beamCutPo.BeamCutSeqs = new List<BeamCutSeq>();

                foreach (var seq in originalPo.OriginalPOsequences)
                {
                    var currentSeq = sequenceQuery.GetOriginalSequence(seq.id);

                    //New BeamCutSeq
                    var beamSeq = AddNewBeamSeq(currentSeq, componentId, beamCutPo.id);

                    if (beamSeq == null)
                        continue;
                    // make return value
                    beamSeq.BeamCutSizes = new List<BeamCutSize>();

                    foreach (var size in currentSeq.OriginalSizes)
                    {
                        var beamSize = AddNewBeamSize(size, beamSeq.id, ShareFuncs.GetInt(beamSeq.SequenceNo), componentId, beamCutPo.id);
                        if (beamSize == null)
                            continue;
                        // make return value
                        beamSeq.BeamCutSizes.Add(beamSize);
                    }
                    // make return value
                    beamCutPo.BeamCutSeqs.Add(beamSeq);
                }
                // make return value
                return beamCutPo;
            }
            catch { return null; }
        }

        public BeamCutSize AddNewBeamSize(OriginalSize size, int beamCutSeqId, int seqNo, int componentId, int beamCutPoId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var result = BeamCutContext.BeamCutSizes.Add(new BeamCutSize
                    {
                        BeamCutPo_Id = beamCutPoId,
                        Component_Id = componentId,
                        BeamCutSeq_Id = beamCutSeqId,
                        CutQty = 0,
                        SequenceNo = seqNo,
                        Finished = false,
                        SizeId = size.SizeId,
                        SizeQty = size.Quantity,
                    });
                    BeamCutContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public BeamCutPo GetBeamCutPo(int originalPoId, int componentId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutPos
                        .Where(i => i.OriginalPo_Id == originalPoId && i.Component_Id == componentId)
                        .First();
                }
            }
            catch { return null; }
        }

        public BeamCutPo GetBeamCutPo(int? clonePoId)
        {
            return GetBeamCutPo(ShareFuncs.GetInt(clonePoId));
        }
        public BeamCutPo GetBeamCutPo(int clonePoId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutPos
                        .Include("BeamCutSeqs")
                        .Where(i => i.id == clonePoId)
                        .First();
                }
            }
            catch { return null; }
        }

        public bool FindOriginalPo(int originalPoId, int componentId)
        {
            return GetBeamCutPo(originalPoId, componentId) == null ? false : true;
        }

        public BeamCutPo FindClonePo(int originalPoId, int componentId)
        {
            try
            {
                BeamCutPo Po = new BeamCutPo();
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var po = BeamCutContext.BeamCutPos
                        .Include("BeamCutSeqs")
                        .Where(i => i.OriginalPo_Id == originalPoId && i.Component_Id == componentId)
                        .First();

                    foreach (var item in po.BeamCutSeqs)
                    {
                        item.BeamCutSizes = new List<BeamCutSize>(BeamCutContext.BeamCutSeqs.Where(i => i.id == item.id).First().BeamCutSizes);
                    }

                    return po;
                }
            }
            catch { return null; }
        }

        public List<BeamCutPo> CloneOriginalPo(int? scheduleId)
        {
            return CloneOriginalPo(ShareFuncs.GetInt(scheduleId));
        }
        public List<BeamCutPo> CloneOriginalPo(int scheduleId)
        {
            try
            {
                SequenceQuery sequenceQuery = new SequenceQuery(database);
                ScheduleQuery scheduleQuery = new ScheduleQuery(database);
                ComponentQuery componentQuery = new ComponentQuery(database);

                var schedule = scheduleQuery.GetSchedule(scheduleId);
                if (schedule == null)
                    return null;

                var components = componentQuery.GetModelComponents(schedule.Model).Where(i => i.CuttingType.id == 2).ToList();

                if (components == null)
                    return null;

                var originalPo = sequenceQuery.GetOriginalPo(schedule);

                if (originalPo == null)
                    return null;

                // make return value
                List<BeamCutPo> PoList = new List<BeamCutPo>();

                foreach (var component in components)
                {
                    if (FindOriginalPo(originalPo.id, component.Id))
                    {
                        Console.WriteLine("Po is already added\r\nContinue.");
                        continue;
                    }
                    // New BeamCutPo
                    var beamCutPo = AddNewBeamCutPo(originalPo.id, component.Id);

                    if (beamCutPo == null)
                        continue;

                    // make return value
                    beamCutPo.BeamCutSeqs = new List<BeamCutSeq>();

                    foreach (var seq in originalPo.OriginalPOsequences)
                    {
                        var currentSeq = sequenceQuery.GetOriginalSequence(seq.id);

                        //New BeamCutSeq
                        var beamSeq = AddNewBeamSeq(currentSeq, component.Id, beamCutPo.id);

                        if (beamSeq == null)
                            continue;
                        // make return value
                        beamSeq.BeamCutSizes = new List<BeamCutSize>();

                        foreach (var size in currentSeq.OriginalSizes)
                        {
                            var beamSize = AddNewBeamSize(size, beamSeq.id, ShareFuncs.GetInt(beamSeq.SequenceNo), component.Id, beamCutPo.id);
                            if (beamSize == null)
                                continue;
                            // make return value
                            beamSeq.BeamCutSizes.Add(beamSize);
                        }
                        // make return value
                        beamCutPo.BeamCutSeqs.Add(beamSeq);
                    }
                    // make return value
                    PoList.Add(beamCutPo);
                }

                return PoList;
            }
            catch { return null; }
        }

        public List<BeamCutSize> GetBeamCutSizes(int clonePoId, int beamSeqId, int totalSeq)
        {
            try
            {
                List<BeamCutSize> Sizes = new List<BeamCutSize>();

                using (BeamCutContext = new BeamCutContext(database))
                {
                    var clonePo = GetBeamCutPo(clonePoId);
                    var currentSeq = GetBeamCutSeq(beamSeqId);


                    foreach (var seq in clonePo.BeamCutSeqs)
                    {
                        if (seq.id >= beamSeqId && (seq.SequenceNo - currentSeq.SequenceNo) < totalSeq)
                        {
                            var sizes = GetBeamCutSize(seq.id);
                            foreach (var size in sizes)
                            {
                                var ss = Sizes.Where(i => i.SizeId == size.SizeId);
                                if (ss.Count() > 0)
                                {
                                    ss.First().SizeQty += size.SizeQty;
                                    ss.First().CutQty += size.CutQty;
                                }
                                else
                                {
                                    Sizes.Add(size);
                                }
                            }
                        }
                    }
                }
                return Sizes;
            }
            catch { return null; }
        }

        public List<BeamCutSize> GetBeamCutSizes(int seq1Id, int seq2Id)
        {
            try
            {
                List<BeamCutSize> Sizes = new List<BeamCutSize>();

                using (BeamCutContext = new BeamCutContext(database))
                {
                    var currentSeq1 = GetBeamCutSeq(seq1Id);
                    var currentSeq2 = GetBeamCutSeq(seq2Id);
                    if (currentSeq1.BeamCutPo_Id != currentSeq2.BeamCutPo_Id)
                        return null;

                    var clonePo = GetBeamCutPo(ShareFuncs.GetInt(currentSeq1.BeamCutPo_Id));

                    foreach (var seq in clonePo.BeamCutSeqs)
                    {
                        if (seq.SequenceNo >= currentSeq1.SequenceNo && seq.SequenceNo <= currentSeq2.SequenceNo)
                        {
                            var sizes = GetBeamCutSize(seq.id);
                            foreach (var size in sizes)
                            {
                                var ss = Sizes.Where(i => i.SizeId == size.SizeId);
                                if (ss.Count() > 0)
                                {
                                    ss.First().SizeQty += size.SizeQty;
                                    ss.First().CutQty += size.CutQty;
                                }
                                else
                                {
                                    Sizes.Add(size);
                                }
                            }
                        }
                    }
                }
                return Sizes;
            }
            catch { return null; }
        }

        public BeamCutSeq GetBeamCutSeq(int? beamSeqId)
        {
            return GetBeamCutSeq(beamSeqId);
        }
        public BeamCutSeq GetBeamCutSeq(int beamSeqId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutSeqs
                         .Include("BeamCutSizes")
                         .Where(i => i.id == beamSeqId)
                         .First();
                }
            }
            catch { return null; }
        }

        public int[] GetTotalSelectSequenceQty(BeamCutSeq seq1, BeamCutSeq seq2)
        {
            int total = 0;
            int totalCut = 0;
            try
            {
                var po = GetBeamCutPo(ShareFuncs.GetInt(seq1.BeamCutPo_Id));
                if (po == null)
                    return null;
                foreach (var item in po.BeamCutSeqs)
                {
                    if (item.SequenceNo >= seq1.SequenceNo && item.SequenceNo <= seq2.SequenceNo)
                    {
                        total += ShareFuncs.GetInt(item.SequenceQty);
                        totalCut += ShareFuncs.GetInt(item.CutQty);
                    }
                }
                return new int[] { total, totalCut };
            }
            catch { return null; }

        }

        public List<BeamCutSize> GetBeamCutSize(int? beamSeqId)
        {
            return GetBeamCutSize(ShareFuncs.GetInt(beamSeqId));
        }
        public List<BeamCutSize> GetBeamCutSize(int beamSeqId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutSeqs
                         .Include("BeamCutSizes")
                         .Where(i => i.id == beamSeqId)
                         .First().BeamCutSizes;
                }
            }
            catch { return null; }
        }
        public BeamCutDevice GetBeamCutDevice(int? bdeviceId)
        {
            return GetBeamCutDevice(ShareFuncs.GetInt(bdeviceId));
        }
        public BeamCutDevice GetBeamCutDevice(int bdeviceId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutDevices.Find(bdeviceId);
                }
            }
            catch { return null; }
        }

        public BeamCutDevice GetBeamCutDevice(string machineCode /*System code*/, string name)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutDevices
                        .Where(i => i.MachineCode == machineCode || i.Name == name)
                        .First();
                }
            }
            catch { return null; }
        }

        public BeamCutInterface AddNewBeamCutInterface(BeamCutInterface binterface)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var result = BeamCutContext.BeamCutInterfaces.Add(binterface);
                    BeamCutContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public bool UpdateBeamInterface(BeamCutInterface binterface)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    BeamCutContext.Entry(binterface).State = EntityState.Modified;
                    BeamCutContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateBeamCutSeq(BeamCutSeq beamCutSeq)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    BeamCutContext.Entry(beamCutSeq).State = EntityState.Modified;
                    BeamCutContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateBeamSize(BeamCutSize beamCutSize)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    BeamCutContext.Entry(beamCutSize).State = EntityState.Modified;
                    BeamCutContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public ICollection<BDeviceOrder> GetBDeviceOrder(int machineId, int workerId, string date)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BDeviceOrders
                        .Where(i => (i.BeamCutDevice_Id == machineId || i.WorkerId == workerId) && i.Date == date)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public BDeviceOrder GetBDeviceOrder(OriginalPO po, int machineId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BDeviceOrders
                        .Where(i => i.BeamCutDevice_Id == machineId
                        && i.PoNumber == po.PoNumber
                        && i.ProductionLine_Id == po.ProductionLine_Id
                        && i.PoQty == po.Quantity)
                        .First();
                }
            }
            catch { return null; }
        }

        public BDeviceCutTimeRecord GetLastRecord(DateTime date)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    string Date = date.ToString("dd / MM/yyyy");
                    return BeamCutContext.BDeviceCutTimeRecords
                        .Where(i => i.Date.Contains(Date))
                        .OrderByDescending(i => i.id)
                        .First();
                }
            }
            catch { return null; }
        }

        public bool AddNewBCutTimeRecord(BDeviceCutTimeRecord record)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {

                    string datetime = DateTime.Now.ToString("dd/MM/yyyy");
                    var statistic = BeamCutContext.BMachineStatistices
                        .Where(i => i.BeamCutDevice_Id == record.BeamCutDevice_Id && i.Date == datetime)
                        .First();

                    if (statistic == null)
                        return false;

                    statistic.BDeviceCutTimeRecords = new List<BDeviceCutTimeRecord>(new BDeviceCutTimeRecord[]
                    {
                       new BDeviceCutTimeRecord
                       {
                           BeamCutDevice_Id = record.BeamCutDevice_Id,
                           BMachineStatistic_Id = statistic.id,
                           TotalCutTime = record.TotalCutTime,
                           ConfirmQuantity  = record.ConfirmQuantity,
                       }
                    });
                    statistic.TotalCuttime += record.TotalCutTime;
                    statistic.StopTime = DateTime.Now;
                    BeamCutContext.BMachineStatistices.Attach(statistic);
                    BeamCutContext.Entry(statistic).State = EntityState.Modified;
                    BeamCutContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            { return false; }
        }

        public bool UpdateBStatistic(BMachineStatistic statistic)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var current = BeamCutContext.BMachineStatistices.Find(statistic.id);
                    current.TotalCutQty = statistic.TotalCutQty;
                    current.TotalCuttime = statistic.TotalCuttime;
                    BeamCutContext.Entry(current).State = EntityState.Modified;
                    BeamCutContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public BMachineStatistic GetBMachineStatistic(int bmachineId, DateTime date)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    string datetime = date.ToString("dd/MM/yyyy");
                    return BeamCutContext.BMachineStatistices
                        .Include("BDeviceCutTimeRecords")
                        .Where(i => i.BeamCutDevice_Id == bmachineId && i.Date == datetime)
                        .First();
                }
            }
            catch { return null; }
        }

        public BMachineStatistic AddNewBcutStatistic(int bmachineId, DateTime date)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var result = BeamCutContext.BMachineStatistices.Add(new BMachineStatistic
                    {
                        BeamCutDevice_Id = bmachineId,
                        Date = date.ToString("dd/MM/yyyy"),
                        TotalCutQty = 0,
                        TotalCuttime = 0,
                    });
                    BeamCutContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public ICollection<BeamCutDevice> GetBeamCutDevices(int buildingId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutDevices
                        .Where(i => i.Building_Id == buildingId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<BeamCutInterface> GetBeamInterface(int BeamCutPo_Id)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutInterfaces
                        .Where(i => i.BeamCutPo_Id == BeamCutPo_Id)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<BDeviceOrder> AddNewOrder(BDeviceOrder BDeviceOrder)
        {
            try
            {
                var orders = GetBDeviceOrders(ShareFuncs.GetInt(BDeviceOrder.BeamCutDevice_Id));
                int place = 0;
                if (orders != null)
                {
                    var temp = orders.OrderByDescending(i => i.Place);
                    if (temp.Count() > 0)
                        place = ShareFuncs.GetInt(temp.First().Place) + 1;
                    else
                        place = 0;
                }
                else
                    place = 0;

                BDeviceOrder.Place = place;

                using (BeamCutContext = new BeamCutContext(database))
                {
                    BeamCutContext.BDeviceOrders.Add(BDeviceOrder);
                    BeamCutContext.SaveChanges();
                }

                return GetBDeviceOrders(ShareFuncs.GetInt(BDeviceOrder.BeamCutDevice_Id));
            }
            catch { return null; }
        }

        public ICollection<BDeviceOrder> GetBDeviceOrders(int deviceId)
        {
            return GetBDeviceOrders(deviceId, DateTime.Now.ToString("dd/MM/yyyy"));
        }

        public ICollection<BDeviceOrder> GetBDeviceOrders(int deviceId, string day)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BDeviceOrders
                        .Where(i => i.BeamCutDevice_Id == deviceId && i.Date == day)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public BeamCutDevice GetDeviceOrders(int deviceId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutDevices
                        .Include("BDeviceOrders")
                        .Where(i => i.id == deviceId)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<BDeviceOrder> DeleteOrder(BDeviceOrder _order)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    string date = _order.Date;
                    var order = BeamCutContext.BDeviceOrders.Find(_order.id);
                    BeamCutContext.BDeviceOrders.Remove(order);
                    BeamCutContext.SaveChanges();

                    return BeamCutContext.BDeviceOrders.Where(i => i.Date == date).ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<BDeviceOrder> MoveOrder(BDeviceOrder _order, int moveType = 1)// 2 : move down
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var order = BeamCutContext.BDeviceOrders.Find(_order.id);

                    int oldPlace = ShareFuncs.GetInt(order.Place);
                    BDeviceOrder changeOrder = new BDeviceOrder();
                    switch (moveType)
                    {
                        case 2: //move down
                            var porder1 = BeamCutContext.BDeviceOrders.Where(i => i.Place == oldPlace + 1);
                            if (porder1.Count() > 0)
                                changeOrder = porder1.First();
                            else
                                return null;
                            break;

                        default: // move up
                            int place = oldPlace >= 0 ? oldPlace - 1 : 0;
                            if (place < 0)
                                place = 0;

                            var porder2 = BeamCutContext.BDeviceOrders.Where(i => i.Place == place);
                            if (porder2.Count() > 0)
                                changeOrder = porder2.First();
                            else
                                return null;
                            break;
                    }

                    order.Place = changeOrder.Place;

                    BeamCutContext.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    BeamCutContext.SaveChanges();

                    changeOrder.Place = oldPlace;
                    BeamCutContext.Entry(changeOrder).State = System.Data.Entity.EntityState.Modified;
                    BeamCutContext.SaveChanges();

                    string date = DateTime.Now.ToString("dd/MM/yyyy");
                    return BeamCutContext.BDeviceOrders.Where(i => i.Date == date).OrderBy(i => i.Place).ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<BeamCutPo> GetBeamCutPos(int? originalPoId)
        {
            return GetBeamCutPos(ShareFuncs.GetInt(originalPoId));
        }
        public ICollection<BeamCutPo> GetBeamCutPos(int originalPoId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BeamCutPos
                        .Where(i => i.OriginalPo_Id == originalPoId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public bool UpdateBeamCutPo(BeamCutPo BCutPo)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    BeamCutContext.Entry(BCutPo).State = EntityState.Modified;
                    BeamCutContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public ICollection<BDeviceOrder> GetAllBDeviceOrders(int? deviceId)
        {
            return GetAllBDeviceOrders(ShareFuncs.GetInt(deviceId));
        }
        public ICollection<BDeviceOrder> GetAllBDeviceOrders(int deviceId)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BDeviceOrders
                        .Where(i => i.BeamCutDevice_Id == deviceId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public BMachineStatistic GetStatistic(string Date)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BMachineStatistices.Where(i => i.Date == Date).First();
                }
            }
            catch { return null; }
        }

        public List<BMachineStatistic> GetBDeviceStatistic(string StartDate, string StopDate)
        {
            try
            {
                var start = GetStatistic(StartDate);
                var stop = GetStatistic(StopDate);
                if (start == null || stop == null)
                    return null;

                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BMachineStatistices
                        .Where(i => i.id >= start.id && i.id <= stop.id)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public List<BMachineStatistic> GetBDeviceStatistic(DateTime StartDate, DateTime StopDate, BeamCutDevice device)
        {
            try
            {
                if (StartDate == null || StopDate == null)
                    return null;

                StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 0, 0, 0);
                StopDate = new DateTime(StopDate.Year, StopDate.Month, StopDate.Day, 23, 0, 0);

                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BMachineStatistices
                        .Where(i => (DateTime)i.StartTime >= StartDate
                        && StopDate >= (DateTime)i.StartTime
                        && i.BeamCutDevice_Id == device.id)
                        .ToList();
                }
            }
            catch { return null; }
        }
        public List<BeamCutInterface> GetBeamCutInterfaces(int bdeviceId, DateTime begin, DateTime end)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    var binfs = BeamCutContext.BeamCutInterfaces
                        .Where(i => i.BeamCutDevice_Id == bdeviceId)
                        .ToList();

                    begin = new DateTime(begin.Year, begin.Month, begin.Day, 0, 0, 0);
                    end = new DateTime(end.Year, end.Month, end.Day, 23, 0, 0);

                    List<BeamCutInterface> result = new List<BeamCutInterface>();
                    foreach (var item in binfs)
                    {
                        try
                        {
                            DateTime dateTime = (DateTime)item.StartCutTime;

                            if (dateTime >= begin && dateTime <= end)
                            {
                                result.Add(item);
                            }
                        }
                        catch { }
                    }
                    return result;
                }
            }
            catch { return null; }
        }

        public BDeviceOrder GetBDeviceOrder(Schedule schedule)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    return BeamCutContext.BDeviceOrders
                        .Where(i => i.PoNumber == schedule.PoNumber
                        && i.ProductionLine_Id == schedule.ProductionLine_Id
                        && i.PoQty == schedule.Quantity)
                        .First();
                }
            }
            catch { return null; }
        }

        public List<BDeviceCutTimeRecord> GetBeamCutDeviceTimeLineRecorde(int bdeviceId, DateTime date)
        {
            try
            {
                using (BeamCutContext = new BeamCutContext(database))
                {
                    string Date = date.ToString("dd / MM/yyyy"); 
                    return BeamCutContext.BDeviceCutTimeRecords
                        .Where(i => i.BeamCutDevice_Id == bdeviceId && i.Date.Contains(Date))
                        .ToList();
                }
            }
            catch { return null; }
        }
    }
}
