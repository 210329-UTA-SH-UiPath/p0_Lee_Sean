using mo = PizzaBox.Domain.Models.Order;
using eo = PizzaBox.Storing.Entities.Order;

namespace PizzaBox.Storing.Mappers
{
  public class OrderMapper : IMapper<mo, eo>
  {
    public mo Map(eo order)
    {
      return new mo
      {

        ItemSummary = order.ItemSummary,
        OrderTotal = order.OrderTotal,
        TimeOfOrder = order.OrderPlaced,
        CustomerID = order.CustomerId,
        StoreID = order.StoreId
      };
    }

    public eo Map(mo order)
    {
      return new eo
      {
        ItemSummary = order.ItemSummary,
        OrderTotal = order.OrderTotal,
        OrderPlaced = order.TimeOfOrder,
        CustomerId = order.CustomerID,
        StoreId = order.StoreID
      };
    }

  }
}