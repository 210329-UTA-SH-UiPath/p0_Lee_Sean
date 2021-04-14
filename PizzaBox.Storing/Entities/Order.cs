using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaBox.Storing.Entities
{
  public partial class Order
  {
    public int Id { get; set; }
    public string ItemSummary { get; set; }
    public decimal OrderTotal { get; set; }
    public DateTime OrderPlaced { get; set; }
    public int CustomerId { get; set; }
    public int? StoreId { get; set; }

    public virtual Store Store { get; set; }
  }
}
