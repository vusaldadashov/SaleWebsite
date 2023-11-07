using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleWebsite.Models;
using SaleWebsite.Session_Extensions;
using System.Diagnostics;

namespace SaleWebsite.Controllers;
public class HomeController : Controller
{
    #region Class Properties
    private readonly DataContext _dataContext;
    #endregion

    #region Class Constructor
    public HomeController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    #endregion

    [HttpGet]

    #region Home Page - Index
    public async Task<IActionResult> Index()
    {
        string SearchKey = Request.Query["SearchKey"].ToString();
        string Type = Request.Query["Type"].ToString();
        string Condition = Request.Query["Condition"].ToString();
        string City = Request.Query["City"].ToString();
        var products = _dataContext.Products.AsQueryable();
        var productsList = new List<Product>();
        if (!string.IsNullOrEmpty(SearchKey))
        {
            products = products.Where(x => x.Title.Contains(SearchKey));
        }
        if (!string.IsNullOrEmpty(Type))
        {
            products = products.Where(x => x.Categories == Type);
        }
        if (!string.IsNullOrEmpty(Condition))
        {
            products = products.Where(x => x.Condition == Condition);
        }
        if (!string.IsNullOrEmpty(City))
        {
            products = products.Where(x => x.City == City);
        }

        productsList = await products
            .Include(x => x.Images)
            .ToListAsync();

        TempData["SearchKey"] = SearchKey;
        TempData["Type"] = Type;
        TempData["Condition"] = Condition;
        TempData["City"] = City;
        return View(productsList);
    }

    #endregion

    #region Product Page
    public IActionResult Product(string Id)
    {
        var product = _dataContext.Products
            .Include(x => x.Images)
            .Include(x=> x.User)
            .FirstOrDefault(x => x.Id == Int32.Parse(Id));
        return View(product);
    }

    #endregion

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    #region Error Page
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #endregion
}
