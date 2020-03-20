using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using EKANBAN_SYS_LIB;
using SYS_MODELS._ENUM;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespComponent
    {
        public RespComponent(IDbName _database, int _scheduleId, int originalPoId)
        {
            try
            {
                ComponentQuery componentQuery = new ComponentQuery(_database);
                ScheduleQuery scheduleQuery = new ScheduleQuery(_database);
                SequenceQuery SequenceQuery = new SequenceQuery(_database);

                var schedule = scheduleQuery.GetSchedule(_scheduleId);

                var originalPo = SequenceQuery.GetOriginalPo(originalPoId);

                if (schedule == null)
                {
                    if (originalPo == null)
                    {
                        Exception = new RespException(true, "Can not find original po", EKB_SYS_REQUEST.BEAM_GET_COMPONENT);
                        return;
                    }

                    schedule = scheduleQuery.GetSchedule(originalPo);
                    if (schedule == null)
                    {
                        Exception = new RespException(true, "Can not find schedule", EKB_SYS_REQUEST.BEAM_GET_COMPONENT);
                        return;
                    }
                }

                var components = componentQuery.GetModelComponents(schedule.Model);

                if (components == null)
                {
                    Exception = new RespException(true, "Can not find component list", EKB_SYS_REQUEST.BEAM_GET_COMPONENT);
                    return;
                }

                components = components.OrderByDescending(i => i.CuttingType.id).ToList();

                ComponentList = new List<Component_t>();

                foreach (var item in components)
                {
                    ComponentList.Add(new Component_t(item));
                }

                Exception = new RespException(false, "Get component: Ok", EKB_SYS_REQUEST.BEAM_GET_COMPONENT);
            }
            catch (Exception e)
            {
                Exception = new RespException(true, e.Message, EKB_SYS_REQUEST.BEAM_GET_COMPONENT);
            }
        }

        public List<Component_t> ComponentList { get; set; }
        public RespException Exception { get; set; }
    }
    public class Component_t
    {
        public Component_t(ShoeComponent _component)
        {
            id = _component.Id;
            Name = ShareFuncs.ConvertToUnSign(_component.Reference);
        }
        public int id { get; set; }
        public string Name { get; set; }
    }
}