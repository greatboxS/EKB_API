using EKANBAN_HIS;
using EKANBAN_SYS_LIB.InterfaceQuery;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB
{
    public class SysHistoryQuery : ISysHistoryQuery
    {
        private IDbName database;
        public HistoryContext HistoryContext { get; set; }
        public SysHistoryQuery(IDbName _database)
        {
            database = _database;
        }

        public bool AddNewAddHistory(EKanbanAddHis _ekanbanAddHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {

                    HistoryContext.EKanbanAddHis.Add(_ekanbanAddHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewBeamCutHis(BeamCutHis _beamCutHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {

                    HistoryContext.BeamCutHis.Add(_beamCutHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewBeamCuttingDetailHis(BeamCut_CuttingHis _beamCuttingHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {

                    HistoryContext.BeamCut_CuttingHis.Add(_beamCuttingHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewClearHistory(EKanbanClearHis _ekanbanClearHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.EKanbanClearHis.Add(_ekanbanClearHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewConfirmHistory(EKanbanConfirmHis _ekanbanConfirmHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.EKanbanConfirmHis.Add(_ekanbanConfirmHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewConfirmSizeHis(BeamCut_ConfirmSizeHis _beamCutConfirmSizeHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.BeamCut_ConfirmSizeHis.Add(_beamCutConfirmSizeHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewEKanbanHisotry(EKanbanHis _ekanbanHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.EKanbanHis.Add(_ekanbanHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public ICollection<BeamCut_ConfirmSizeHis> GetBeamCutConfirmSizeDetail(int _beamcutDetailId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    var beamHisDetail = HistoryContext.BeamCut_CuttingHis
                        .Include("BeamCut_ConfirmSizeHis")
                        .Where(i => i.id == _beamcutDetailId)
                        .First();
                    return beamHisDetail.BeamCut_ConfirmSizeHis;
                }
            }
            catch { return null; }
        }

        public BeamCutHis GetBeamCutHisByEmployee(int _employeeId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.BeamCutHis
                        .Include("BeamCut_CuttingHis")
                        .Where(i => i.Employee_Id == _employeeId)
                        .First();
                }
            }
            catch { return null; }
        }

        public BeamCutHis GetBeamCutHisById(int _beamcutHisId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.BeamCutHis
                        .Include("BeamCut_CuttingHis")
                        .Where(i => i.id == _beamcutHisId)
                        .First();
                }
            }
            catch { return null; }
        }

        public BeamCutHis GetBeamCutHisByInterface(int _bInterfaceId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.BeamCutHis
                        .Include("BeamCut_CuttingHis")
                        .Where(i => i.BeamCutInterface_Id == _bInterfaceId)
                        .First();
                }
            }
            catch { return null; }
        }

        public BeamCutHis GetBeamCutHisByMachine(int _machineId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.BeamCutHis
                        .Include("BeamCut_CuttingHis")
                        .Where(i => i.BeamMachine_Id == _machineId)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<BeamCut_CuttingHis> GetBeamCutHisDetail(int _beamcutHisId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    var beamHis = HistoryContext.BeamCutHis
                        .Where(i => i.BeamMachine_Id == _beamcutHisId)
                        .First();

                    return beamHis.BeamCut_CuttingHis;
                }
            }
            catch { return null; }
        }

        public ICollection<EKanbanHis> GetEKanbanHistories(int _ekanbanDevice_Id)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.EKanbanHis
                        .Include("EKanbanAddHis")
                        .Include("EKanbanConfirmHis")
                        .Include("EKanbanClearHis")
                        .Where(i => i.EKanbanDevice_Id == _ekanbanDevice_Id)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public EKanbanHis GetEKanbanHistory(int _ekanbanHisId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.EKanbanHis
                        .Include("EKanbanAddHis")
                        .Include("EKanbanConfirmHis")
                        .Include("EKanbanClearHis")
                        .Where(i => i.id == _ekanbanHisId)
                        .First();
                }
            }
            catch { return null; }
        }

        public EKanbanHis GetEKanbanHistory(EKanbanInterface _ekanbanInterface)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.EKanbanHis
                        .Include("EKanbanAddHis")
                        .Include("EKanbanConfirmHis")
                        .Include("EKanbanClearHis")
                        .Where(i => i.EKanbanInterface_Id == _ekanbanInterface.id)
                        .First();
                }
            }
            catch { return null; }
        }

        public EKanbanHis GetEKanbanHistoryByInterface(int _ekanbanInterfaceId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.EKanbanHis
                        .Where(i => i.EKanbanInterface_Id == _ekanbanInterfaceId)
                        .First();
                }
            }
            catch { return null; }
        }

        public EKanbanHis GetLastEKanbanHistory(int _ekanbanDevice_Id)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.EKanbanHis
                        .Include("EKanbanAddHis")
                        .Include("EKanbanConfirmHis")
                        .Include("EKanbanClearHis")
                        .Where(i => i.EKanbanDevice_Id == _ekanbanDevice_Id)
                        .First();
                }
            }
            catch { return null; }
        }

        public EKanbanHis GetLastEKanbanHistory(EKanbanDevice _ekanbanDevice)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.EKanbanHis
                        .Include("EKanbanAddHis")
                        .Include("EKanbanConfirmHis")
                        .Include("EKanbanClearHis")
                        .Where(i => i.EKanbanDevice_Id == _ekanbanDevice.id)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<EKanbanHis> GetLastEKanbanHistory(ProductionLine _productionLine)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.EKanbanHis
                        .Include("EKanbanAddHis")
                        .Include("EKanbanConfirmHis")
                        .Include("EKanbanClearHis")
                        .Where(i => i.ProductionLine_Id == _productionLine.id)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public bool UpdateBeamCuttingDetailHis(BeamCut_CuttingHis _beamCuttingHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.Entry(_beamCuttingHis).State = EntityState.Modified;
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateConfirmSizeHis(BeamCut_ConfirmSizeHis _beamCutConfirmSizeHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.Entry(_beamCutConfirmSizeHis).State = EntityState.Modified;
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateEKanbanHis(EKanbanHis _ekanbanHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.Entry(_ekanbanHis).State = EntityState.Modified;
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateNewBeamCutHis(BeamCutHis _beamCutHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.Entry(_beamCutHis).State = EntityState.Modified;
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewAppHistory(AppHistory _appHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.AppHistories.Add(_appHis);
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateAppHistory(AppHistory _appHis)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    HistoryContext.Entry(_appHis).State = EntityState.Modified;
                    HistoryContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public AppHistory GetAppHisory(int _appHisId)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.AppHistories.Find(_appHisId);
                }
            }
            catch { return null; }
        }

        public ICollection<AppHistory> GetAppHisories(int _sysActionCode)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.AppHistories
                        .Where(i => i.SysActionCode == _sysActionCode)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<AppHistory> GetAppHisories(DateTime _updateTime)
        {
            try
            {
                using (HistoryContext = new HistoryContext(database))
                {
                    return HistoryContext.AppHistories
                        .Where(i => i.DateTime.ToString("dd/MM/yyyy") == _updateTime.ToString("dd/MMM/yyyy"))
                        .ToList();
                }
            }
            catch { return null; }
        }
    }
}
