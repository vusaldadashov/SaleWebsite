using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleWebsite.Models;
using SaleWebsite.Session_Extensions;
using System.Diagnostics;
using SaleWebsite.Helper_Functions;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace SaleWebsite.Controllers;
public class AccountController : Controller
{
    #region Class Properties
    private readonly DataContext _dataContext;
    private readonly IWebHostEnvironment _environment;

    #endregion

    #region Class Constructor

    public AccountController(DataContext dataContext, IWebHostEnvironment environment)
    {
        _dataContext = dataContext;
        _environment = environment;
    }

    #endregion

    #region Profile Page
    public IActionResult Profile()
    {
        var user = HttpContext.Session.GetObjectFromJson<User>("user");
        return View(user);
    }

    public async Task<IActionResult> UpdateProfile(User user)
    {
        var userProfile = HttpContext.Session.GetObjectFromJson<User>("user");
        if (userProfile == null)
        {
            return RedirectToAction("Login", "Register");
        }
        userProfile.Name = user.Name;
        userProfile.Surname = user.Surname;
        userProfile.UserName = user.UserName;
        userProfile.Email = user.Email;

        var file = Request.Form.Files[0];

        var fileName = Path.GetRandomFileName() + file.FileName.ToLower(); // Generate a unique filename
        var uploadPath = Path.Combine(_environment.WebRootPath, "imgs/user", fileName);
        using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        var imgUrl = "/imgs/user/" + fileName;
        userProfile.Img = imgUrl;
        HttpContext.Session.SetObjectAsJson("user", userProfile);
        _dataContext.Users.Update(userProfile);
        await _dataContext.SaveChangesAsync();

        return RedirectToAction("Profile");
    }

    #endregion

    #region Products Page
    public async Task<IActionResult> Products()
    {
        var user = HttpContext.Session.GetObjectFromJson<User>("user");
        if (user == null)
        {
            return RedirectToAction("Login", "Register");
        }
        var products = await _dataContext.Products
            .Include(x => x.Images)
            .Where(x => x.UserId == user.UserId)
            .ToListAsync();
        return View(products);
    }

    #endregion

    #region Edit Product Page
    public async Task<IActionResult> EditProduct(string Id)
    {
        if (!HttpContext.Session.GetObjectFromJson<bool>("isLoggedIn"))
        {
            return NotFound();
        }
        var product = await _dataContext.Products
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == Int32.Parse(Id));
        return View(product);
    }

    #endregion

    #region Edit Product Posting

    public async Task<IActionResult> UpdateProduct(Product product)
    {
        var user = HttpContext.Session.GetObjectFromJson<User>("user");
        int productId = product.Id;

        if (user == null)
        {
            return RedirectToAction("Login", "Register");
        }

        // Validation for productId and user authentication can be added here.

        using var transaction = _dataContext.Database.BeginTransaction();

        try
        {
            var UpdatedProduct = _dataContext.Products.FirstOrDefault(x => x.Id == productId);
            if (UpdatedProduct == null)
            {
                return RedirectToAction("Login", "Register");
            }

            product.UserId = user.UserId;


            // Remove selected images
            var ImagesToRemove = Request.Form["ImagesToRemove"].ToString();
            if (!string.IsNullOrEmpty(ImagesToRemove))
            {
                ImagesToRemove = ImagesToRemove.Trim(',');
                var ImagesToRemoveList = ImagesToRemove.Split(',');
                foreach (var id in ImagesToRemoveList)
                {
                    var imageToRemove = _dataContext.Images.FirstOrDefault(img => img.Id == int.Parse(id));
                    if (imageToRemove != null)
                    {
                        // Remove the image file from the server
                        if (FileSystemForImages.RemoveImageFromLocal(_environment.WebRootPath, imageToRemove))
                        {
                            _dataContext.Images.Remove(imageToRemove);
                        }
                    }
                }
            }

            // Update product properties.
            UpdatedProduct.Title = product.Title;
            UpdatedProduct.Description = product.Description;
            UpdatedProduct.Price = product.Price;
            UpdatedProduct.City = product.City;
            UpdatedProduct.Country = product.Country;
            UpdatedProduct.Condition = product.Condition;
            UpdatedProduct.Categories = product.Categories;

            _dataContext.Products.Update(UpdatedProduct);
            await _dataContext.SaveChangesAsync();

            //Store new images 
            var files = Request.Form.Files;
            var result = await StoreProductImages(files, UpdatedProduct.Id);
            if (result)
            {
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                NotFound();
            }
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            // Handle and log the exception, and possibly roll back the transaction.
            transaction.Rollback();
            // You can return an error view or a JSON response here.
        }

        return RedirectToAction("EditProduct", "Account", new { id = product.Id });
    }



    #endregion

    #region Create-Product Page
    public IActionResult CreateProduct()
    {
        if (!HttpContext.Session.GetObjectFromJson<bool>("isLoggedIn"))
        {
            return RedirectToAction("Index", "Home");
        }
        Product product = new();
        return View(product);
    }

    #endregion

    #region Upload Created Product

    public async Task<IActionResult> UploadProduct(Product product)
    {
        var user = HttpContext.Session.GetObjectFromJson<User>("user");
        if (user == null)
        {
            return RedirectToAction("Login", "Register");
        }

        product.UserId = user.UserId;
        using var transaction = _dataContext.Database.BeginTransaction();

        try
        {
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();

            // Handle file uploads
            var Files = Request.Form.Files;
            var result = await StoreProductImages(Files, product.Id);

            if (!result)
            {
                NotFound(product.Id);
            }

            transaction.Commit();
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            NotFound(ex);
            // Handle any exceptions and roll back the transaction if necessary.
            transaction.Rollback();
            // Log the exception or return an error view.
            return View("Error");
        }

    }
    #endregion

    #region Delete Product

    public IActionResult DeleteProduct(string Id)
    {
        var product = _dataContext.Products
            .Include(x => x.Images)
            .FirstOrDefault(x => x.Id == int.Parse(Id));
        if (product == null)
        {
            return NotFound();
        }
        foreach (var image in product.Images)
        {
            FileSystemForImages.RemoveImageFromLocal(_environment.WebRootPath, image);
        }
        _dataContext.Products.Remove(product);
        _dataContext.SaveChanges();
        return RedirectToAction("Products", "Account");
    }

    #endregion

    #region Chat Page 

    [HttpGet]
    public IActionResult ChatPage()
    {
        return View();
    }

    #endregion

    #region Add Chat User to chat
    [HttpPost]

    public async Task<IActionResult> AddChatUser(int Id, int UserId)
    {
        var sessionUser = HttpContext.Session.GetObjectFromJson<User>("user");
        if(sessionUser == null)
        {
            HttpContext.Session.SetString("product_to_show", Id.ToString());
            return RedirectToAction("Login", "Register");
        }
        var user1 = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserId == sessionUser.UserId);
        var user2 = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserId.Equals(UserId));



        if (user2 == null || user1 == null )
        {
            return RedirectToAction("Product", "Home", new { id = Id });
        }
        
        var existChat = await  _dataContext.Chats
            .FirstOrDefaultAsync(c => c.Participants.Any(p=> p.UserId == user1.UserId) &&
                                       c.Participants.Any(p=> p.UserId == user2.UserId));
        
        if (existChat == null)
            try
            {

                Chat chat = new();
                chat.Participants.Add(user1);
                chat.Participants.Add(user2);

                _dataContext.Chats.Add(chat);

                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("-------------------->>>>>>>>>>>" + ex.Message);
                Debug.WriteLine("|Error occured - AccountControl->ChatPage Post -> Try catch block|" + ex.InnerException?.Message);
                Debug.Write("sessionuser: " + user1.Name);
                Debug.Write("newuser: " + user2.Name);
            }

        return RedirectToAction("ChatPage", "Account");
    }

    #endregion


    #region Store Product Images Method
    private async Task<bool> StoreProductImages(IFormFileCollection Files, int ProdId)
    {
        try
        {
            if (Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file != null)
                    {
                        // File upload code here.
                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        var fileName = Path.GetRandomFileName() + file.FileName.ToLower(); // Generate a unique filename
                        var uploadPath = Path.Combine(_environment.WebRootPath, "imgs", fileName);

                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Create and save Image entity.
                        var image = new Image
                        {
                            ProductId = ProdId,
                            Url = "/imgs/" + fileName
                        };

                        await _dataContext.Images.AddAsync(image);
                        await _dataContext.SaveChangesAsync();
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            NotFound(ex);
            return false;
        }

    }

    #endregion
}

