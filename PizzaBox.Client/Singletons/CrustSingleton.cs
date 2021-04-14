using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Client.Singletons
{
  /// <summary>
  /// 
  /// </summary>
  public class CrustSingleton
  {
    private static CrustSingleton _instance;

    public List<Crust> Crusts { get; set; }
    public static CrustSingleton Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new CrustSingleton();
        }

        return _instance;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private CrustSingleton()
    {
      Crusts = new List<Crust>()
      {
        new NewYorkStyleCrust(),
        new DeepDishCrust(),
        new CheeseStuffCrust(),
        new ThinCrust()
      };
    }
  }
}
