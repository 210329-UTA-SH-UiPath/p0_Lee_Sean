using mc = PizzaBox.Domain.Models.Customer;
using ec = PizzaBox.Storing.Entities.Customer;

namespace PizzaBox.Storing.Mappers
{
  public class CustomerMapper : IMapper<mc, ec>
  {
    public mc Map(ec customer)
    {
      return new mc
      {
        Id = customer.Id,
        Name = customer.Name
      };
    }

    public ec Map(mc customer)
    {
      return new ec
      {
        Id = customer.Id,
        Name = customer.Name
      };

    }
  }
}