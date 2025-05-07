using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Order
{
  public interface IOrder
  {
    public Task<int> CreateOrder(CreateOrderDto dto);
    public Task<OrderRdo> GetOrder(int id);
    public Task<List<OrderRdo>> GetOrders();
    public Task<int> UpdateOrder(UpdateOrderDto dto);
    public Task<int> DeleteOrder(int id);
  }
}
