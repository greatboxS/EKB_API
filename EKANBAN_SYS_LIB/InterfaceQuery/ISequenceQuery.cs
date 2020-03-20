using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEQUENCE;
namespace EKANBAN_SYS_LIB.InterfaceQuery
{
    public interface ISequenceQuery
    {
        SequenceContext SequenceContext { get; }
        OriginalPO GetOriginalPo(EKanbanLoading _ekanbanLoading);
        //OriginalPO GetOriginalPo(BeamCutComponent _beamcutComponent);
        OriginalPO GetOriginalPo(OriginalPOsequence _originalPOsequence);
        OriginalPO GetOriginalPo(int _originalPoId);

        ICollection<OriginalPO> GetOriginalPo(ProductionLine _productionLine);
        bool AddNewOriginalPo(OriginalPO _originalPo);
        bool UpdateOriginalPo(OriginalPO _originalPo);


        OriginalPOsequence GetOriginalSequence(int _originalSequenceId);
        bool AddNewOriginalSequence(OriginalPOsequence _originalSequence);
        bool UpdateOriginalSequence(OriginalPOsequence _originalSequence);

        OriginalSize GetOriginalSize(int _originalSizeId);
        ICollection<OriginalSize> GetOriginalSizes(int _originalSequence_Id);
        bool UpdateOriginalSequence(OriginalSize _originalSize);

        ICollection<OriginalPOsequence> GetOriginalSequence(Schedule _schedule);
        OriginalPO GetOriginalPo(Schedule _schedule);

        OriginalPO GetOriginalPo(OriginalPO _originalPo);

        /// <returns> 
        /// 0 not found => add new 
        /// 1 diff article >> update 
        /// 2 diff productionline  >> update line Id
        /// 3 same po same article same modelname same qty >> skip
        /// </returns>
        int CheckOriginalPo(OriginalPO _originalPo); 
    }
}
