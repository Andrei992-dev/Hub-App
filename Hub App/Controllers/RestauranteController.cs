using Microsoft.AspNetCore.Mvc;

namespace Hub_App.Controllers
{
    public class RestauranteController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
