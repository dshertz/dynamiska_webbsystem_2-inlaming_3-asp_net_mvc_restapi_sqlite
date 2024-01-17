using Microsoft.AspNetCore.Mvc;

namespace westcoast_education.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
