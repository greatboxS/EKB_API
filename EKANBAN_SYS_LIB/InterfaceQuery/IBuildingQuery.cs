using BUILDING;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB.InterfaceQuery
{
    public interface IBuildingQuery
    {
        BuildingContext BuildingContext { get; }
        Building GetBuilding(int _buildingId);
        ICollection<Building> GetBuildings();
        ProductionLine GetProductionLine(int _productionLineId);
        ICollection<ProductionLine> GetProductionLines(int _buildingId);
        ProductionLine GetProductionLineByCode(string _sysLineCode);
        ProductionLine GetProductionLineByName(string _lineName);
        List<ProductionLine> GetProductionLines(string _lineName);
    }
}
