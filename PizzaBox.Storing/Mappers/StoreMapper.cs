using ms = PizzaBox.Domain.Models.Store;
using es = PizzaBox.Storing.Entities.Store;

namespace PizzaBox.Storing.Mappers
{
  public class StoreMapper : IMapper<ms, es>
  {
    public ms Map(es store)
    {
      return new ms
      {
        Id = store.Id,
        Name = store.Name
      };
    }

    public es Map(ms store)
    {
      return new es
      {
        Id = store.Id,
        Name = store.Name
      };

    }
  }
}