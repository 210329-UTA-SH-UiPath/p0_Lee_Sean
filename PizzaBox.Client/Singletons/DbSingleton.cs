using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Entities;

namespace PizzaBox.Client.Singletons
{
  /// <summary>
  /// 
  /// </summary>
  public class DbSingleton
  {
    private static DbSingleton _instance;
    private readonly HeroesAppSeanContext context;
    public static DbSingleton Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new DbSingleton();
        }

        return _instance;
      }
    }
    public HeroesAppSeanContext Context
    {
      get
      {
        return context;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private DbSingleton()
    {
      context = new HeroesAppSeanContext();
    }
  }
}
