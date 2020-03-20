using EKANBAN_SYS_LIB.InterfaceQuery;
using EKANBAN_TASK;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB
{
    public class EKanbanTaskQuery : IEKanbanTaskQuery
    {
        private IDbName database;
        public EKanbanTaskContext EKanbanTaskContext { get; set; }
        public EKanbanTaskQuery(IDbName _database)
        {
            database = _database;
        }
        public bool AddNewEKanbanComponent(EKanbanComponent _ekanbanComponent)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.EkanbanComponents.Add(_ekanbanComponent);
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public bool AddNewEKanbanDevice(EKanbanDevice _ekanbanDevice)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.EKanbanDevices.Add(_ekanbanDevice);
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public bool AddNewEKanbanInterface(EKanbanInterface _ekanbanInterface)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.EKanbanInterfaces.Add(_ekanbanInterface);
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public bool AddNewEKanbanLoading(EKanbanLoading _ekanbanLoading)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.EKanbanLoadings.Add(_ekanbanLoading);
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public ICollection<EKanbanComponent> GetEKanbanComponent(int _ekanbanInterfaceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    var ekanbanInterface = EKanbanTaskContext.EKanbanInterfaces.
                        Include("")
                        .Where(i => i.id == _ekanbanInterfaceId)
                        .First();

                    return ekanbanInterface.EkanbanComponents;
                }
            }
            catch { return null; }
        }

        public EKanbanDevice GetEKanbanDevice(int _ekanbanDeviceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanDevices.Find(_ekanbanDeviceId);
                }
            }
            catch { return null; }
        }

        public EKanbanDevice GetEKanbanDevice(string _ekanbanDeviceName)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanDevices.Where(i => i.Name == _ekanbanDeviceName).First();
                }
            }
            catch { return null; }
        }

        public EKanbanInterface GetEKanbanInterface(int _ekanbanInterfaceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanInterfaces
                        .Include("EKanbanLoadings")
                        .Include("EkanbanComponents")
                        .Where(i => i.id == _ekanbanInterfaceId)
                        .First();
                }
            }
            catch { return null; }
        }

        public EKanbanInterface FindEKanbanInterface(int _ekanbanInterfaceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanInterfaces.Find(_ekanbanInterfaceId);
                }
            }
            catch { return null; }
        }

        public EKanbanLoading GetEKanbanLoading(int _ekanbanLoadingId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanLoadings
                        .Where(i => i.id == _ekanbanLoadingId)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<EKanbanLoading> GetEKanbanLoadings(int _ekanbanInterfaceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanLoadings
                        .Where(i => i.EKanbanInterface_Id == _ekanbanInterfaceId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<EKanbanInterface> GetEKanbanInterfaces(int _ekanbanDeviceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanInterfaces
                        .Where(i => i.EKanbanDevice_Id == _ekanbanDeviceId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public bool UpdateEKanbanComponent(EKanbanComponent _ekanbanComponent)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.Entry(_ekanbanComponent).State = EntityState.Modified;
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public bool UpdateEKanbanDevice(EKanbanDevice _ekanbanDevice)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.Entry(_ekanbanDevice).State = EntityState.Modified;
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public bool UpdateEKanbanInterface(EKanbanInterface _ekanbanInterface)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.Entry(_ekanbanInterface).State = EntityState.Modified;
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public bool UpdateEKanbanLoading(EKanbanLoading _ekanbanLoading)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.Entry(_ekanbanLoading).State = EntityState.Modified;
                    EKanbanTaskContext.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }

        public ICollection<EKanbanDevice> GetEKanbanDevicesByProductionLine(int _productionLineId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanDevices
                        .Where(i => i.PropductionLine_Id == _productionLineId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<EKanbanDevice> GetEKanbanDevicesByBuilding(int _buildingId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanDevices
                        .Where(i => i.Building_Id == _buildingId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public EKanbanLoading GetEKanbanLoadingBySequence(int _originalSequenceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanLoadings
                        .Where(i => i.OriginalSequence_Id == _originalSequenceId)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<EKanbanLoading> GetEKanbanLoadingByPo(int _originalPoId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanLoadings
                        .Where(i => i.OriginalPo_Id == _originalPoId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public EKanbanInterface GetLastEKanbanInterface(int _ekanbanDeviceId)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    return EKanbanTaskContext.EKanbanInterfaces
                        .Include("EkanbanComponents")
                        .Include("EKanbanLoadings")
                        .Where(i => i.EKanbanDevice_Id == _ekanbanDeviceId)
                        .OrderByDescending(i => i.id)
                        .First();
                }
            }
            catch { return null; }
        }

        public bool DeleteEKanbanLoading(EKanbanLoading _ekanbanLoading)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    EKanbanTaskContext.EKanbanLoadings.Attach(_ekanbanLoading);
                    EKanbanTaskContext.Entry(_ekanbanLoading).State = EntityState.Deleted;
                    EKanbanTaskContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteEKanbanInterface(EKanbanInterface _ekanbanInterface)
        {
            try
            {
                using (EKanbanTaskContext = new EKanbanTaskContext(database))
                {
                    var EKanban = EKanbanTaskContext.EKanbanInterfaces.Find(_ekanbanInterface.id);
                    EKanbanTaskContext.Entry(EKanban).State = EntityState.Deleted;
                    EKanbanTaskContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }
    }
}
