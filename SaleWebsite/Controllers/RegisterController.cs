using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SaleWebsite.Interfaces;
using SaleWebsite.Models;
using SaleWebsite.Session_Extensions;
using System.Diagnostics;

namespace SaleWebsite.Controllers;
public class RegisterController : Controller
{
    #region Class Properties

    private readonly DataContext _dataContext;
    private readonly IPasswordHasher _passwordHasher;
   

    #endregion

    #region Controller Constructor
    public RegisterController(DataContext dataContext, IPasswordHasher passwordHasher)
    {
        _dataContext = dataContext;
        _passwordHasher = passwordHasher;
    }

    #endregion

    #region Login Page Get
    [HttpGet]
    [AllowAnonymous]
    [PreventLoggedInAccess]
    public IActionResult Login()
    {
        return View();
    }

    #endregion

    #region Login User Posting
    [HttpPost]
    [AllowAnonymous]
    [PreventLoggedInAccess]
    public IActionResult Login(string Email, string Password)
    {
        var user = _dataContext.Users.FirstOrDefault(x => x.Email == Email);
        if (user == null)
        {
            ModelState.AddModelError("Login", "Email or Password is Incorrect!");
            return View();
        }
        if (_passwordHasher.Verify(Password, user.Password))
        {
            HttpContext.Session.SetObjectAsJson("user", user);
            HttpContext.Session.SetObjectAsJson("isLoggedIn", true);

            // Redirect to a secure area of your application
            var prodId = HttpContext.Session.GetString("product_to_show");
            Debug.WriteLine("this is prodid" + prodId);
            if (string.IsNullOrEmpty(prodId))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Product", "Home", new { id = prodId });
            }
        }
        else
        {
            ModelState.AddModelError("Login", "Email or Password is Incorrect!");
            return View();
        }

    }
    #endregion
    
    #region Logout User 
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
    #endregion

    #region Signup Page Get
    [HttpGet]
    [AllowAnonymous]
    [PreventLoggedInAccess]
    public IActionResult Signup()
    {
        User user = new();
        return View(user);
    }
    #endregion

    #region Signup User Posting
    [HttpPost]
    [AllowAnonymous]
    [PreventLoggedInAccess]
    public async Task<IActionResult> Signup(User newUser)
    {
        if (HttpContext.Session.GetObjectFromJson<bool>("isLoggedIn"))
        {
            return RedirectToAction("Index", "Home");
        }
        if (_dataContext.Users.FirstOrDefault(user => user.Email == newUser.Email) != null)
        {
            ModelState.AddModelError("Register", "This email is used by someone!");
            return View(new User());
        }

        newUser.Password = _passwordHasher.Hash(newUser.Password);
        await _dataContext.Users.AddAsync(newUser);
        await _dataContext.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
    #endregion

    #region Reset Password Page

    public IActionResult ResetPassword()
    {
        return View();
    }

    #endregion

    #region Reset Password Posting

    public async Task<IActionResult> ResetUserPassword()
    {
        string password = Request.Form["Password"].ToString();
        string confirmPassword = Request.Form["ConfirmPassword"].ToString();
        string email = Request.Form["Email"].ToString();
        var user = HttpContext.Session.GetObjectFromJson<User>("user");
        user ??= await _dataContext
                .Users
                .SingleOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            return NotFound();
        }
        if (password == confirmPassword)
        {
            user.Password = _passwordHasher.Hash(password);

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Profile", "Account");
    }

    #endregion

}


#region Prevention Class For Access Block


public class PreventLoggedInAccessAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Session.GetObjectFromJson<bool>("isLoggedIn") == true)
        {
            context.Result = new RedirectResult("/Home/Index");
        }
    }
}


#endregion