using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Order
  {
    public AStore Store { get; set; }
    public int? StoreID { get; set; }
    public int CustomerID { get; set; }
    public Customer Customer { get; set; }
    public string ItemSummary { get; set; }
    public System.DateTime TimeOfOrder { get; set; }
    // public APizza Pizza { get; set; }
    // public List<APizza> Items { get; set; }
    public decimal OrderTotal { get; set; }
    //private static FileRepository _fileRepository = new FileRepository();

    public Order()
    {
      TimeOfOrder = System.DateTime.Now;
    }
    /// <summary>
    /// 
    /// </summary>
    public void Save()
    {
      // if (_fileRepository.WriteToFile(Items, "orders.xml"))
      System.Console.WriteLine("Thanks, added to order!");
    }
  }
}
