using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS_MODELS._ENUM
{
    public enum EKB_SYS_REQUEST
    {
        EKANBAN_GET_INTERFACE,
       
        BEAM_START_CUTTING,
        BEAM_STOP_CUTTING,
        BEAM_CONFIRM_SIZE,
        BEAM_GET_INTERFACE,
        BEAM_GET_SCHEDULE,
        BEAM_GET_SEQUENCE,
        BEAM_GET_SIZE,
        BEAM_GET_COMPONENT,
        GET_TIME,
        GET_USER_INFO,
        BEAM_GET_PO_INFO,
        GET_DEVICE_INFO,
        BEAM_GET_LAST_CUT,
        FINISH_ALL,
        CUT_TIME_RECORD,
    }
}
