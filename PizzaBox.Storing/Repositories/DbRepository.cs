using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Mappers;

namespace PizzaBox.Storing.Repositories
{
  public class DbRepository
  {
    private readonly Entities.HeroesAppSeanContext context;
    private readonly OrderMapper oMapper = new OrderMapper();
    private readonly CustomerMapper cMapper = new CustomerMapper();
    public DbRepository(Entities.HeroesAppSeanContext context)
    {
      this.context = context;
    }
    public void AddCustomer(Customer c)
    {
      context.Add(cMapper.Map(c));
      context.SaveChanges();
    }
    public void AddOrder(Order o)
    {
      context.Add(oMapper.Map(o));
      context.SaveChanges();
    }

  }
}