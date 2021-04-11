using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Order
  {
    public AStore Store { get; set; }
    public Customer Customer { get; set; }
    public APizza Pizza { get; set; }
    public List<APizza> Items { get; set; }
    private static FileRepository _fileRepository = new FileRepository();

    /// <summary>
    /// 
    /// </summary>
    public void Save()
    {
      if (_fileRepository.WriteToFile(Items, "orders.xml"))
        System.Console.WriteLine("Thanks, added to order!");
    }
  }
}
