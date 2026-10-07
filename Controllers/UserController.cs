using Microsoft.AspNetCore.Mvc;

namespace PennyEcommerce.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public bool IsUserLoggedIn()
    {
        return true;
    }

    public IActionResult Details(int id)
    {
        return View();
    }
}