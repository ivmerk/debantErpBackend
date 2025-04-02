using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IProductionRateDAL
    {
        List<ProductionRateModel> GetProductionRatesByProductOperationId(int productOperationId);
        ProductionRateModel GetProductionRate(int id);
        ProductionRateModel CreateProductionRate(ProductionRateModel productionRate);
        ProductionRateModel UpdateProductionRate(ProductionRateModel productionRate);
        int DeleteProductionRate(int id);
    }
}
