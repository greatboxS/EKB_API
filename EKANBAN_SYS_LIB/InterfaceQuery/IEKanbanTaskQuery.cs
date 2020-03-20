using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS_MODELS;
using EKANBAN_TASK;

namespace EKANBAN_SYS_LIB.InterfaceQuery
{
    public interface IEKanbanTaskQuery
    {
        EKanbanTaskContext EKanbanTaskContext { get; }
        bool AddNewEKanbanInterface(EKanbanInterface _ekanbanInterface);
        bool AddNewEKanbanLoading(EKanbanLoading _ekanbanLoading);
        bool AddNewEKanbanComponent(EKanbanComponent _ekanbanComponent);
        bool AddNewEKanbanDevice(EKanbanDevice _ekanbanDevice);

        bool UpdateEKanbanInterface(EKanbanInterface _ekanbanInterface);
        bool UpdateEKanbanLoading(EKanbanLoading _ekanbanLoading);
        bool UpdateEKanbanComponent(EKanbanComponent _ekanbanComponent);
        bool UpdateEKanbanDevice(EKanbanDevice _ekanbanDevice);

        EKanbanDevice GetEKanbanDevice(int _ekanbanDeviceId);
        ICollection<EKanbanDevice> GetEKanbanDevicesByProductionLine(int _productionLineId);
        ICollection<EKanbanDevice> GetEKanbanDevicesByBuilding(int _buildingId);
        EKanbanInterface GetEKanbanInterface(int _ekanbanInterfaceId);
        EKanbanLoading GetEKanbanLoading(int _ekanbanLoadingId);
        EKanbanLoading GetEKanbanLoadingBySequence(int _originalSequenceId);
        ICollection<EKanbanLoading> GetEKanbanLoadingByPo(int _originalPoId);

        ICollection<EKanbanComponent> GetEKanbanComponent(int _ekanbanInterfaceId);
        ICollection<EKanbanLoading> GetEKanbanLoadings(int _ekanbanInterfaceId);
        ICollection<EKanbanInterface> GetEKanbanInterfaces(int _ekanbanDeviceId);
        EKanbanInterface GetLastEKanbanInterface(int _ekanbanDeviceId);

        bool DeleteEKanbanLoading(EKanbanLoading _ekanbanLoading);
        bool DeleteEKanbanInterface(EKanbanInterface _ekanbanInterface);

    }
}
