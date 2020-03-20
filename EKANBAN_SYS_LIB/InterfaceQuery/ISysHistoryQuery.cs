using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EKANBAN_HIS;
namespace EKANBAN_SYS_LIB.InterfaceQuery
{
    public interface ISysHistoryQuery
    {
        HistoryContext HistoryContext { get; }
        bool AddNewEKanbanHisotry(EKanbanHis _ekanbanHis);
        bool UpdateEKanbanHis(EKanbanHis _ekanbanHis);
        bool AddNewAddHistory(EKanbanAddHis _ekanbanAddHis);
        bool AddNewConfirmHistory(EKanbanConfirmHis _ekanbanConfirmHis);
        bool AddNewClearHistory(EKanbanClearHis _ekanbanClearHis);

        EKanbanHis GetEKanbanHistory(int _ekanbanHisId);
        EKanbanHis GetEKanbanHistory(EKanbanInterface _ekanbanInterface);

        EKanbanHis GetLastEKanbanHistory(int _ekanbanDevice_Id);
        EKanbanHis GetLastEKanbanHistory(EKanbanDevice _ekanbanDevice);
        ICollection<EKanbanHis> GetLastEKanbanHistory(ProductionLine _productionLine);

        ICollection<EKanbanHis> GetEKanbanHistories(int _ekanbanDevice_Id);

        bool AddNewBeamCutHis(BeamCutHis _beamCutHis);
        bool AddNewBeamCuttingDetailHis(BeamCut_CuttingHis _beamCuttingHis);
        bool AddNewConfirmSizeHis(BeamCut_ConfirmSizeHis _beamCutConfirmSizeHis);

        bool UpdateNewBeamCutHis(BeamCutHis _beamCutHis);
        bool UpdateBeamCuttingDetailHis(BeamCut_CuttingHis _beamCuttingHis);
        bool UpdateConfirmSizeHis(BeamCut_ConfirmSizeHis _beamCutConfirmSizeHis);

        BeamCutHis GetBeamCutHisById(int _beamcutHisId);
        BeamCutHis GetBeamCutHisByInterface(int _bInterfaceId);
        BeamCutHis GetBeamCutHisByMachine(int _machineId);
        BeamCutHis GetBeamCutHisByEmployee(int _employeeId);

        ICollection<BeamCut_CuttingHis> GetBeamCutHisDetail(int _beamcutHisId);
        ICollection<BeamCut_ConfirmSizeHis> GetBeamCutConfirmSizeDetail(int _beamcutDetailId);

        bool AddNewAppHistory(AppHistory _appHis);
        bool UpdateAppHistory(AppHistory _appHis);
        AppHistory GetAppHisory(int _appHisId);
        ICollection<AppHistory> GetAppHisories(int _sysActionCode);
        ICollection<AppHistory> GetAppHisories(DateTime _updateTime);
    }
}
