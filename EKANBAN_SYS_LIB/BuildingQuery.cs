using BUILDING;
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
    public class BuildingQuery : IBuildingQuery
    {
        private IDbName database;
        public BuildingContext BuildingContext { get; set; }

        public BuildingQuery(IDbName _database)
        {
            database = _database;
        }

        public Building GetBuilding(int? _buildingId)
        {
            return GetBuilding(ShareFuncs.GetInt(_buildingId));
        }

        public Building GetBuilding(int _buildingId)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    return BuildingContext.Buildings
                        .Include("ProductionLines")
                      .Where(i => i.id == _buildingId)
                      .First();
                }
            }
            catch { return null; }

        }

        public ProductionLine GetProductionLineByName(string _lineName)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    var line = BuildingContext.ProductionLines
                        .Where(i => _lineName.Contains(i.LineName))
                        .OrderByDescending(i => i.id)
                        .First();
                    return line;
                }
            }
            catch (Exception e)
            { return null; }
        }

        public List<ProductionLine> FindProductionLine(string _lineName)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    var line = BuildingContext.ProductionLines
                        .Where(i => i.LineName.Contains(_lineName))
                        .ToList();
                    return line;
                }
            }
            catch (Exception e)
            { return null; }
        }

        public ProductionLine GetProductionLine(int? _productionLineId)
        {
            return GetProductionLine(ShareFuncs.GetInt(_productionLineId));
        }
        public ProductionLine GetProductionLine(int _productionLineId)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    return BuildingContext.ProductionLines.Find(_productionLineId);
                }
            }
            catch { return null; }
        }

        public ProductionLine GetProductionLineByCode(string _sysLineCode)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    var productionLineName = BuildingContext.ProductionLineNames
                        .Where(i => i.SystemCode == _sysLineCode || i.LineCode == _sysLineCode)
                        .First();

                    return BuildingContext.ProductionLines
                        .Where(i => i.LineName == productionLineName.DisplayCode)
                        .First();
                }
            }
            catch { return null; }
        }

        public List<ProductionLine> GetProductionLines(string _lineName)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    return BuildingContext.ProductionLines
                        .Where(i => i.LineName.Contains(_lineName))
                        .ToList();
                }
            }
            catch { return null; }
        }

        public ICollection<ProductionLine> GetProductionLines(int? _buildingId)
        {
            return GetProductionLines(ShareFuncs.GetInt(_buildingId));
        }
        public ICollection<ProductionLine> GetProductionLines(int _buildingId)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    var buiding = BuildingContext.Buildings.Include("ProductionLines")
                        .Where(i => i.id == _buildingId)
                        .First();
                    return buiding.ProductionLines;
                }
            }
            catch { return null; }
        }

        public ICollection<ProductionLine> GetProductionLines()
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    var lines = BuildingContext.ProductionLines
                        .ToList();
                    return lines;
                }
            }
            catch { return null; }
        }

        public ICollection<Building> GetBuildings()
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    return BuildingContext.Buildings
                        .Include("ProductionLines")
                        .ToList();
                }
            }
            catch { return null; }
        }

        public Building GetBuilding(string buildingName)
        {
            try
            {
                using (BuildingContext = new BuildingContext(database))
                {
                    return BuildingContext.Buildings
                        .Include("ProductionLines")
                        .Where(i => i.Name == buildingName)
                        .First();
                }
            }
            catch { return null; }
        }
    }
}
