using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    //[Authorize]
    //[Authorize(Roles ="Admin")]
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return RedirectToAction("Login", "Account");
        }
    }
}
