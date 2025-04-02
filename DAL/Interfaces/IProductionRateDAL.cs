using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IProductionRateDAL : IBaseDAL<ProductionRateModel>
    {
        public Task<List<ProductionRateModel>> GetByProductionOperationId(
            int productionOperationId
        );
    }
}
