﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaBox.Storing.Entities
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
