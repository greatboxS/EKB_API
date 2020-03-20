using SCHEDULE;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB.InterfaceQuery
{
    public interface IScheduleQuery
    {
        ScheduleContext ScheduleContext { get; }
        Schedule GetSchedule(int _scheduleId);
        ICollection<Schedule> GetSchedules(string _poNumber);
        ICollection<Schedule> GetSchedules(ProductionLine _productionLine);
        ICollection<Schedule> GetSchedules(ScheduleClass _scheduleClass);
        ICollection<Schedule> GetSchedules(DateTime _scheduleTime);
        Schedule GetSchedule(OriginalPO _originalPo);
        FilePath GetFilePath(string _type);

        ScheduleClass GetScheduleClass(int _scheduleClassId);
        ScheduleClass GetScheduleClass(string _scheduleName);
        ScheduleClass GetScheduleClass(DateTime _scheduleTime);
        UpdateSchProperty GetUpdateScheduleProperties(int _scheduleClassId);

        bool UpdateScheule(Schedule _schedule);
        bool UpdateScheuleClass(ScheduleClass _scheduleClass);
        bool UpdateScheuleProperties(UpdateSchProperty _updateSchProperty);


        bool AddNewScheule(Schedule _schedule);
        bool AddNewScheuleClass(ScheduleClass _scheduleClass);
        bool AddNewScheduleProperties(UpdateSchProperty _updateScheuleProperties);
        bool AddNewFilePath(FilePath _filePath);

        bool DeleteAllSchedule(ICollection<Schedule> _schedules);
    }
}
