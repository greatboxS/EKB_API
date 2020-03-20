namespace EKANBAN_SYS_LIB
{
    using EKANBAN_SYS_LIB.InterfaceQuery;
    using SEQUENCE;
    using SYS_MODELS;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class SequenceQuery : ISequenceQuery
    {
        public SequenceContext SequenceContext { get; set; }
        private IDbName database;
        public SequenceQuery(IDbName _database)
        {
            database = _database;
        }

        public bool AddNewOriginalPo(OriginalPO _originalPo)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    SequenceContext.OriginalPOes.Add(_originalPo);
                    SequenceContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewOriginalSequence(OriginalPOsequence _originalSequence)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    SequenceContext.OriginalPOsequences.Add(_originalSequence);
                    SequenceContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public OriginalPO GetOriginalPo(EKanbanLoading _ekanbanLoading)
        {
            return GetOriginalPo(ShareFuncs.GetInt(_ekanbanLoading.OriginalPo_Id));
        }

        //public OriginalPO GetOriginalPo(BeamCutComponent _beamcutComponent)
        //{
        //    return GetOriginalPo(ShareFuncs.GetInt(_beamcutComponent.OriginalPo_Id));
        //}

        public OriginalPO GetOriginalPo(OriginalPOsequence _originalPOsequence)
        {
            return GetOriginalPo(ShareFuncs.GetInt(_originalPOsequence.OriginalPO_Id));
        }

        public OriginalPO GetOriginalPo(int _originalPoId)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOes
                          .Include("OriginalPOsequences")
                          .Where(i => i.id == _originalPoId)
                          .First();
                }
            }
            catch { return null; }
        }

        public ICollection<OriginalPO> GetOriginalPo(ProductionLine _productionLine)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOes
                          .Include("OriginalPOsequences")
                          .Where(i => i.ProductionLine_Id == _productionLine.id)
                          .ToList();
                }
            }
            catch { return null; }
        }

        public OriginalPOsequence GetOriginalSequence(int _originalSequenceId)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOsequences
                            .Include("OriginalSizes")
                            .Where(i => i.id == _originalSequenceId)
                            .First();
                }
            }
            catch { return null; }
        }

        public OriginalSize GetOriginalSize(int _originalSizeId)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalSizes.Find(_originalSizeId);
                }
            }
            catch { return null; }
        }

        public ICollection<OriginalSize> GetOriginalSizes(int _originalSequence_Id)
        {
            var sequence = GetOriginalSequence(_originalSequence_Id);
            if (sequence == null)
                return null;
            else
                return sequence.OriginalSizes;
        }

        public bool UpdateOriginalPo(OriginalPO _originalPo)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    var po = SequenceContext.OriginalPOes.Find(_originalPo.id);
                    po.ProductionLine_Id = _originalPo.ProductionLine_Id;
                    po.Article = _originalPo.Article;
                    SequenceContext.Entry(po).State = EntityState.Modified;
                    SequenceContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateOriginalSequence(OriginalPOsequence _originalSequence)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    var poSequence = SequenceContext.OriginalPOsequences.Find(_originalSequence.id);
                    SequenceContext.Entry(poSequence).State = EntityState.Modified;
                    SequenceContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateOriginalSequence(OriginalSize _originalSize)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    var size = SequenceContext.OriginalSizes.Find(_originalSize.id);
                    SequenceContext.Entry(size).State = EntityState.Modified;
                    SequenceContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public ICollection<OriginalPOsequence> GetOriginalSequence(Schedule _schedule)
        {
            try
            {
                var originalPo = GetOriginalPo(_schedule);
                return originalPo.OriginalPOsequences;
            }
            catch { return null; }
        }

        public OriginalPO GetOriginalPo(Schedule _schedule)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOes
                        .Include("OriginalPOsequences")
                        .Where(i => i.PoNumber == _schedule.PoNumber
                        //&& i.ModelName == _schedule.ModelName
                        //&& i.Article == _schedule.Article
                        && i.Quantity == _schedule.Quantity
                        && i.ProductionLine_Id == _schedule.ProductionLine_Id
                        ).First();
                }
            }
            catch { return null; }
        }

        public ICollection<OriginalPO> GetOriginalPos(Schedule _schedule)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOes
                        .Include("OriginalPOsequences")
                        .Where(i => i.PoNumber == _schedule.PoNumber
                        && i.Quantity == _schedule.Quantity
                        ).ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<OriginalPO> GetOriginalPos(string PoNumber)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOes
                        .Where(i => i.PoNumber.Contains(PoNumber)
                        ).ToList();
                }
            }
            catch { return null; }
        }

        public OriginalPO GetOriginalPo(OriginalPO _originalPo)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOes
                        .Where(i => i.PoNumber == _originalPo.PoNumber
                        && i.ModelName == _originalPo.ModelName
                        && i.Article == _originalPo.Article
                        && i.Quantity == _originalPo.Quantity
                        ).First();
                }
            }
            catch { return null; }
        }


        /// <returns> 
        /// 0 not found => add new 
        /// 1 diff article >> update 
        /// 2 diff productionline  >> update line Id
        /// 3 same po same article same modelname same qty >> skip
        /// </returns>
        public int CheckOriginalPo(OriginalPO _originalPo)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    var po1 = SequenceContext.OriginalPOes
                        .Where(i => i.PoNumber == _originalPo.PoNumber
                        && i.ModelName == _originalPo.ModelName
                        && i.Article == _originalPo.Article
                        && i.Quantity == _originalPo.Quantity
                        && i.ProductionLine_Id == _originalPo.ProductionLine_Id);

                    if (po1.Count() > 0)
                        return 3;

                    var po2 = SequenceContext.OriginalPOes
                        .Where(i => i.PoNumber == _originalPo.PoNumber
                        && i.ModelName == _originalPo.ModelName
                        && i.Article == _originalPo.Article
                        && i.Quantity == _originalPo.Quantity
                        && i.ProductionLine_Id != _originalPo.ProductionLine_Id);

                    if (po2.Count() > 0)
                    {
                        var po22 = po2.First();
                        po22.ProductionLine_Id = _originalPo.ProductionLine_Id;
                        if (UpdateOriginalPo(po22))
                            return 2;
                        else
                            return 4;
                    }

                    var po3 = SequenceContext.OriginalPOes
                        .Where(i => i.PoNumber == _originalPo.PoNumber
                        && i.ModelName == _originalPo.ModelName
                        && i.Article != _originalPo.Article
                        && i.Quantity == _originalPo.Quantity
                        && i.ProductionLine_Id == _originalPo.ProductionLine_Id);

                    if (po3.Count() > 0)
                    {
                        var po33 = po3.First();
                        po33.Article = _originalPo.Article;
                        if (UpdateOriginalPo(po33))
                            return 1;
                        else 
                            return 5;
                    }

                    return 0;
                }
            }
            catch { return 0; }
        }
    
        public OriginalPO GetOriginalPo(BDeviceOrder bDeviceOrder)
        {
            try
            {
                using (SequenceContext = new SequenceContext(database))
                {
                    return SequenceContext.OriginalPOes
                        .Where(i => i.PoNumber == bDeviceOrder.PoNumber
                        && i.ProductionLine_Id == bDeviceOrder.ProductionLine_Id
                        && i.Quantity == bDeviceOrder.PoQty
                        ).First();
                }
            }
            catch { return null; }
        }
    }
}
