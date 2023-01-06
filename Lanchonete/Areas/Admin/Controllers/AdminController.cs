using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // verificar em que perfil está (retirando o Authorize)
            var aut = User.Identity;

            aut.ToString();

            return View();
        }
    }
}
