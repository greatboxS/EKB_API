using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYS_MODELS;
using COMPONENT;

namespace EKANBAN_SYS_LIB
{
    public class ComponentQuery
    {
        public ShoeContext ShoeContext { get; set; }
        private IDbName database;
        public ComponentQuery(IDbName _database)
        {
            database = _database;
        }

        public ShoeModel GetShoeModel(int _shoeModelId)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ShoeModels
                        .Include("ModelComponents")
                        .Where(i => i.Id == _shoeModelId)
                        .First();
                }
            }
            catch { return null; }
        }

        public ShoeModel GetShoeModel(string _shoeModel)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ShoeModels
                        .Include("ModelComponents")
                        .Where(i => i.Model_Number == _shoeModel)
                        .First();
                }
            }
            catch { return null; }
        }

        public ShoeModel GetShoeModel(string _shoeModelstr, string _shoeModelNamestr)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ShoeModels
                        .Include("ModelComponents")
                        .Where(i => i.Model_Number == _shoeModelstr && i.Model_Name == _shoeModelNamestr)
                        .First();
                }
            }
            catch { return null; }
        }

        public ShoeSize GetShoeSize(int _shoeSizeId)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ShoeSizes.Find(_shoeSizeId);
                }
            }
            catch { return null; }
        }

        public ShoeComponent GetShoeComponent(int _shoeComponent)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ShoeComponents.Find(_shoeComponent);
                }
            }
            catch { return null; }
        }

        public ShoeComponent GetShoeComponent(string _componentRef)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ShoeComponents
                        .Where(i => i.Reference == _componentRef)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<ModelComponent> GetModelComponents(string _shoeModel, string _shoeModelName)
        {
            try
            {
                return GetShoeModel(_shoeModel, _shoeModelName).ModelComponents;
            }
            catch { return null; }
        }

        public ICollection<ModelComponent> GetModelComponents(int _shoeModelId)
        {
            try
            {
                return GetShoeModel(_shoeModelId).ModelComponents;
            }
            catch { return null; }
        }

        public ICollection<ShoeComponent> GetModelComponents(string _shoeModel)
        {
            try
            {
                List<ShoeComponent> results = new List<ShoeComponent>();
                var modelComponents = GetShoeModel(_shoeModel).ModelComponents;
                int i = 0;
                foreach (var item in modelComponents)
                {
                    i++;
                    ShoeComponent component = GetShoeComponent(ShareFuncs.GetInt(item.ShoeComponent_Id));
                    CuttingType cuttype = GetCuttingType(ShareFuncs.GetInt(item.CuttingType_Id));
                    component.CuttingType = cuttype;
                    component.Index = i;
                    component.AddToEkanban = true;
                    results.Add(component);
                }
                return results;
            }
            catch { return null; }
        }

        public ICollection<ShoeComponent> GetShoeComponents(string _shoeModel, string _shoeModelName)
        {
            try
            {
                List<ShoeComponent> ShoeComponents = new List<ShoeComponent>();
                var modelComponent = GetModelComponents(_shoeModel, _shoeModelName);
                foreach (var item in modelComponent)
                {
                    ShoeComponents.Add(GetShoeComponent(ShareFuncs.GetInt(item.ShoeComponent_Id)));
                }
                return ShoeComponents;
            }
            catch { return null; }
        }

        public ShoeComponent AddNewShoeComponent(ShoeComponent _shoeComponent)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    _shoeComponent = ShoeContext.ShoeComponents.Add(_shoeComponent);
                    ShoeContext.SaveChanges();
                    return _shoeComponent;
                }
            }
            catch { return null; }
        }

        public ShoeComponent AddNewShoeComponent(string _shoeComponentRef)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    ShoeComponent shoeComponent = new ShoeComponent
                    {
                        Reference = _shoeComponentRef,
                    };
                    var result = ShoeContext.ShoeComponents.Add(shoeComponent);
                    ShoeContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public ShoeComponent GetAndAddShoeComponent(ShoeComponent _shoeComponent)
        {
            try
            {
                ShoeComponent shoeComponent = GetShoeComponent(_shoeComponent.Reference);

                if (shoeComponent == null)
                {
                    shoeComponent = AddNewShoeComponent(_shoeComponent);
                }

                return shoeComponent;
            }
            catch { return null; }
        }

        public ShoeComponent GetAndAddShoeComponent(string _shoeComponentRef)
        {
            try
            {
                ShoeComponent shoeComponent = GetShoeComponent(_shoeComponentRef);

                if (shoeComponent == null)
                {
                    shoeComponent = AddNewShoeComponent(new ShoeComponent { Reference = _shoeComponentRef });
                }

                return shoeComponent;
            }
            catch { return null; }
        }

        public ShoeModel AddNewShoeModel(ShoeModel _shoeModel)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    _shoeModel = ShoeContext.ShoeModels.Add(_shoeModel);
                    ShoeContext.SaveChanges();
                    return _shoeModel;
                }
            }
            catch { return null; }
        }

        public ShoeModel GetAndAddShoeModel(ShoeModel _shoeModel)
        {
            try
            {
                var shoeModel = GetShoeModel(_shoeModel.Model_Number, _shoeModel.Model_Name);
                if (shoeModel != null)
                    return shoeModel;

                return AddNewShoeModel(_shoeModel);
            }
            catch { return null; }
        }

        public CuttingType GetCuttingType(string _cuttingType)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    var cutTypes = ShoeContext.CuttingTypes
                        .Where(i => i.TypeName.Contains(_cuttingType));
                    if (cutTypes.Count() > 0)
                        return cutTypes.First();

                    var result = ShoeContext.CuttingTypes.Add(new CuttingType { TypeName = _cuttingType });
                    ShoeContext.SaveChanges();
                    return result;
                }
            }
            catch { return null; }
        }

        public CuttingType GetCuttingType(int _cuttingTypeId)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.CuttingTypes.Find(_cuttingTypeId);
                }
            }
            catch { return null; }
        }

        public ModelComponent GetModelComponent(int _shoeModelId, string _cuttingType)
        {
            try
            {
                var cuttingType = GetCuttingType(_cuttingType);

                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ModelComponents
                        .Where(i => i.ShoeModel_Id == _shoeModelId && i.CuttingType_Id == cuttingType.id)
                        .First();
                }
            }
            catch { return null; }
        }

        public ModelComponent AddNewModelComponent(int _shoeModelId, int _shoeComponentId, int _cuttingTypeId)
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    var modelComponent = ShoeContext.ModelComponents.Add(new ModelComponent
                    {
                        ShoeModel_Id = _shoeModelId,
                        ShoeComponent_Id = _shoeComponentId,
                        CuttingType_Id = _cuttingTypeId,
                    });
                    ShoeContext.SaveChanges();
                    return modelComponent;
                }
            }
            catch { return null; }
        }

        public bool ClearAllModelComponents()
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    var modelComponent = ShoeContext.ModelComponents.ToList();
                    ShoeContext.ModelComponents.RemoveRange(modelComponent);
                    ShoeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public ICollection<ShoeSize> GetShoeSizes()
        {
            try
            {
                using (ShoeContext = new ShoeContext(database))
                {
                    return ShoeContext.ShoeSizes.ToList();
                }
            }
            catch { return null; }
        }
    }
}
