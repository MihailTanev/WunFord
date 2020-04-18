namespace WunFord.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}