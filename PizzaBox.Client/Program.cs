using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Models.Toppings;
using PizzaBox.Client.Singletons;
using System.Linq;
using System.Collections;
using Orderdb = PizzaBox.Storing.Entities.Order;
using Customerdb = PizzaBox.Storing.Entities.Customer;
using Storedb = PizzaBox.Storing.Entities.Store;
using PizzaBoxContext = PizzaBox.Storing.Entities.HeroesAppSeanContext;
using PizzaBox.Storing.Repositories;

namespace PizzaBox.Client
{
  /// <summary>
  /// 
  /// </summary>
  internal class Program
  {
    private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance;
    private static readonly PizzaSingleton _pizzaSingleton = PizzaSingleton.Instance;
    private static readonly ToppingSingleton _toppingSingleton = ToppingSingleton.Instance;
    private static readonly SizeSingleton _sizeSingleton = SizeSingleton.Instance;
    private static readonly CrustSingleton _crustSingleton = CrustSingleton.Instance;
    private static readonly PizzaBox.Storing.Entities.HeroesAppSeanContext _context = DbSingleton.Instance.Context;
    private static List<APizza> pizzas = new List<APizza>();
    private static readonly int _maxNumOfToppings = 5;
    private static readonly int _maxNumOfPizzas = 50;
    //private static readonly decimal _maxprice = 250M;

    public static List<APizza> Pizzas
    { get => pizzas; set => pizzas = value; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
      Run();
      // DbRepository repo = new DbRepository(_context);
      // Customer c = new Customer();
      // c.Name = "Larry";
      // repo.AddCustomer(c);
    }

    /// <summary>
    /// 
    /// </summary>
    private static void Run()
    {
      DbRepository repo = new DbRepository(_context);
      var order = new Order();
      var customer = new Customer();

      Console.WriteLine("Welcome to PizzaBox");

      customer.Name = GetName();

      DisplayMenu(_storeSingleton.Stores);

      order.Customer = new Customer();
      repo.AddCustomer(customer);
      order.Store = SelectStore();
      order.StoreID = order.Store.Id;
      Pizzas = GetOrder();
      order.OrderTotal = GetOrderPrice();
      order.ItemSummary = OrderSummary();
      customer.Id = order.CustomerID;

      repo.AddOrder(order);

      Console.WriteLine($"You've ordered {Pizzas.Count} pizzas at {order.Store}");

      order.Save();
    }

    private static List<APizza> GetOrder()
    {

      int addAnotherPizza;
      do
      {
        APizza p;
        DisplayMenu(_pizzaSingleton.Pizzas);
        p = SelectPizza();
        p.Size = SelectSize();
        p.Crust = SelectCrust();
        p.Toppings = SelectToppings();
        Pizzas.Add(p);
        if (Pizzas.Count < _maxNumOfPizzas)
        {
          Console.WriteLine("Would you like to add another pizza? \n1 - Yes \n2 - No");
          addAnotherPizza = int.Parse(Console.ReadLine());
        }
        else break;
      } while (addAnotherPizza == 1 && Pizzas.Count < _maxNumOfPizzas);
      ListOrder();
      return Pizzas;
    }
    private static string GetName()
    {
      string name = "";

      while (name.Length < 4)
      {
        Console.WriteLine("Please enter your name");
        name = Console.ReadLine();
      }
      return name;
    }
    private static void ModifyOrder()
    {
      Console.WriteLine("\n\n1 - Confirm Order\n2 - Add Item\n3 - Remove Item\n4 - Exit Application");
      var input = int.Parse(Console.ReadLine());
      if (input == 1)
      {
        return;
      }
      else if (input == 2)
      {
        GetOrder();
      }
      else if (input == 3)
      {
        if (Pizzas.Count > 0)
        {
          Console.WriteLine("Which item would you like to delete? (Enter Item #)");
          var itemToRemove = int.Parse(Console.ReadLine());
          Pizzas.RemoveAt(itemToRemove - 1);
          ListOrder();
        }
        else
        {
          Console.WriteLine("There's nothing to delete");
          ModifyOrder();
        }
      }
      else
      {
        Environment.Exit(0);
      }

    }
    private static void ListOrder()
    {
      Console.WriteLine("You've ordered:");
      int counter = 1;
      foreach (var p in Pizzas)
      {
        Console.WriteLine($"\n\n{counter++}\n{p.Pizza} \n{p.Size} \n{p.Crust} \nWith {p.Toppings.Count} toppings ({p.CalculateToppingPrice()}) \n PIZZA PRICE: {p.GetFinalPrice()}");
      }
      Console.WriteLine($"\n\nOrder total: {GetOrderPrice()}");
      ModifyOrder();
    }

    private static string OrderSummary()
    {
      string desc = "";
      int count = 1;
      foreach (var p in Pizzas)
      {
        desc += $"{count} {p.Pizza} with {p.Toppings.Count}, ";
        count++;
      }
      return desc;
    }

    private static decimal GetOrderPrice()
    {
      decimal totalPrice = 0;
      foreach (var p in Pizzas)
      {
        totalPrice += p.GetFinalPrice();
      }
      return totalPrice;
    }

    /// <summary>
    /// 
    /// </summary>
    private static void DisplayOrder(APizza pizza)
    {
      Console.WriteLine($"Your order is: {pizza}");
    }
    private static void DisplayMenu(dynamic type)
    {
      var index = 0;

      foreach (var item in type)
      {
        Console.WriteLine($"{++index} - {item}");
      }
    }


    private static AStore SelectStore()
    {
      var input = int.Parse(Console.ReadLine()); // be careful (think execpetion/error handling)

      // DisplayMenu(_pizzaSingleton.Pizzas);

      return _storeSingleton.Stores[input - 1];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static APizza SelectPizza()
    {
      var input = int.Parse(Console.ReadLine());
      //var pizza = _pizzaSingleton.Pizzas[input - 1];

      //DisplayOrder(pizza);
      DisplayMenu(_sizeSingleton.Sizes.OfType<Size>());

      return _pizzaSingleton.Pizzas[input - 1];
    }
    private static Size SelectSize()
    {
      var input = int.Parse(Console.ReadLine()); // be careful (think execpetion/error handling)

      DisplayMenu(_crustSingleton.Crusts);

      return _sizeSingleton.Sizes[input - 1];
    }
    private static Crust SelectCrust()
    {
      var input = int.Parse(Console.ReadLine()); // be careful (think execpetion/error handling)

      DisplayMenu(_toppingSingleton.Toppings);

      return _crustSingleton.Crusts[input - 1];
    }
    private static List<Topping> SelectToppings()
    {
      List<Topping> toppings = new List<Topping>();
      Console.WriteLine("Would you like a topping? \n1 - Yes \n2 - No");
      var addToppings = int.Parse(Console.ReadLine());

      while (addToppings == 1 && toppings.Count < _maxNumOfToppings)
      {
        Console.WriteLine($"Great! Select one from the list above {toppings.Count + 1}/5");
        var input = int.Parse(Console.ReadLine());
        var topping = _toppingSingleton.Toppings[input - 1];
        toppings.Add(topping);

        if (toppings.Count < _maxNumOfToppings)
        {
          Console.WriteLine("Add another topping? \n1 - Yes \n2 - No");
          addToppings = int.Parse(Console.ReadLine());
        }
        else break;
      }
      return toppings;
    }

    private void GetPreviousOrders(int id)
    {
      PizzaBoxContext context = new PizzaBoxContext();
      var orders = context.Orders.ToList();
      foreach (var order in orders)
      {
        Console.WriteLine($"{order.ItemSummary}\nPrice: {order.OrderTotal}");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

  }
}
