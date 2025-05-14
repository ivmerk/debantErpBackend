using DebantErp.DAL;
using DebantErp.DAL.Models;
using DebantErp.Dtos;
using DebantErp.Rdos;
namespace DebantErp.BL.OrderLaborCost
{
  public class OrderLostCost : IOrderLaborCost
  {
    private readonly IOrderLaborCostDAL _orderLaborCostDAL;

    public OrderLostCost(IOrderLaborCostDAL orderLaborCostDAL) => _orderLaborCostDAL = orderLaborCostDAL;

    
    public async Task<int> Create(CreateOrderLaborCostDto dto)
    {
      var orderLaborCost = new OrderLaborCostModel
      {
        EmployeeId = dto.EmployeeId,
        ProductionRateId = dto.ProductionRateId,
        Quantity = dto.Quantity,
        OrderId = dto.OrderId,
        
      };
      return await _orderLaborCostDAL.Create(orderLaborCost);
    }

    public async Task<List<OrderLaborCostRdo>> Get() 
    {
      
      var costs = await _orderLaborCostDAL.Get();
      if (costs == null || costs.Count == 0) throw new Exception("Costs not found");
      var costsRdo = costs
        .Select(c => new OrderLaborCostRdo
        {
          Id = c.Id,
          EmployeeId = c.EmployeeId,
          ProductionRateId = c.ProductionRateId,
          Quantity = c.Quantity,
          OrderId = c.OrderId,
          CreatedAt = c.CreatedAt,
        })
        .ToList();
      return costsRdo;
    }

    public async Task<OrderLaborCostRdo> Get(int id) 
    {
      var cost = await _orderLaborCostDAL.Get(id);
      if (cost == null) throw new Exception("Cost not found");
      var costRdo = new OrderLaborCostRdo
      {
        Id = cost.Id,
        EmployeeId = cost.EmployeeId,
        ProductionRateId = cost.ProductionRateId,
        Quantity = cost.Quantity,
        OrderId = cost.OrderId,
        CreatedAt = cost.CreatedAt,
      };
      return costRdo;
    }

    public async Task<int> Update(int id, UpdateOrderLaborCostDto dto) 
    {
      var orderLaborCost = await _orderLaborCostDAL.Get(id);
      if (orderLaborCost == null) throw new Exception("Cost not found");
      if (dto.Quantity != null) orderLaborCost.Quantity = dto.Quantity.Value;
      return await _orderLaborCostDAL.Update(orderLaborCost);
    }

    public Task<int> Delete(int id) 
    {
      var orderLaborCost = _orderLaborCostDAL.Get(id);
      if (orderLaborCost == null) throw new Exception("Cost not found");
      return _orderLaborCostDAL.Delete(id);
    }
  }
}
