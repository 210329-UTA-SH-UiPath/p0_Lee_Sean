using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Storing.Repositories;
using PizzaBox.Domain.Models.Toppings;
using System.IO;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client.Singletons
{
  class ToppingSingleton
  {
    private static ToppingSingleton _instance;
    private static readonly FileRepository _fileRepository = new FileRepository();
    private const string _path = @"Topping.xml";
    public List<Topping> Toppings { get; set; }
    public static ToppingSingleton Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new ToppingSingleton();
        }
        return _instance;
      }
    }

    public void CreateToppings()
    {
      if (!File.Exists(_path))
      {
        Toppings = new List<Topping>()
        {
          new VeggieTopping(),
          new CheeseTopping(),
          new MeatTopping()
        };

        if (_fileRepository.WriteToFile(Toppings, _path))
          System.Console.WriteLine("Successfully created list");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private ToppingSingleton()
    {
      CreateToppings();
      Toppings = _fileRepository.ReadFromFile<Topping>(_path);
    }
  }
}