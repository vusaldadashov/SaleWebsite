using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleWebsite.Models;
using SaleWebsite.Session_Extensions;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;

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

    #region Home Page - Index
    public IActionResult Index()
    {
        var Products = _dataContext.Products
            .Include(p => p.Images)
            .Include(p => p.Premiums)
            .Include(p => p.User)
                .ThenInclude(u => u.Vips)
            .AsSplitQuery()
            .AsQueryable();

        FilterViewModel filters = new();
        if (Request.Method == "POST")
        {
            filters.SearchKey = Request.Form["SearchKey"].ToString() ?? "";
            filters.Type = Request.Form["Type"].ToString();
            filters.Condition = Request.Form["Condition"].ToString();
            filters.City = Request.Form["City"].ToString();
            filters.Category = Request.Form["Category"].ToString();
            Products = Products
        .Where(x => string.IsNullOrEmpty(filters.SearchKey) || x.Title.Contains(filters.SearchKey))
        .Where(x => filters.Type == "All" || string.IsNullOrEmpty(filters.Type) || x.Categories == filters.Type)
        .Where(x => filters.Condition == "All" || string.IsNullOrEmpty(filters.Condition) || x.Condition == filters.Condition)
        .Where(x => filters.City == "All" || string.IsNullOrEmpty(filters.City) || x.City == filters.City);
        }
        DateTime currentDate = DateTime.Now;
        var vipProducts = Products
            .Where(p=> p.User.Vips.Any())
            .OrderByDescending(x => x.Id)
            .Take(20)
            .ToList();

        var latestProducts = Products
            .Where(p => !p.Premiums.Any())
            .OrderByDescending(p => p.CreatedDate)
            .Take(20)
            .ToList();

        var premiumProducts = Products
            .Where(p => p.Premiums.Any())
            .OrderByDescending(x => x.Id)
            .Take(50)
            .ToList();

        var model = (Filter: filters, VipProducts: vipProducts, LatestProducts: latestProducts, PremiumProducts: premiumProducts);

        return View(model);
    }

    #endregion

    #region Product Page
    public IActionResult Product(string Id)
    {
        var product = _dataContext.Products
            .Include(x => x.Images)
            .Include(x => x.User)
            .FirstOrDefault(x => x.Id == int.Parse(Id));

        if (product == null || product.User == null)
        {
            return RedirectToAction("Index");
        }
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
