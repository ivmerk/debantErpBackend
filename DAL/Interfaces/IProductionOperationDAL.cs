using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IProductionOperationDAL
    {
        ProductionOperationModel GetProductionOperation(int id);
        List<ProductionOperationModel> GetProductionOperations();
        int CreateProductionOperation(ProductionOperationModel model);
        int DeleteProductionOperation(int id);
    }
}
