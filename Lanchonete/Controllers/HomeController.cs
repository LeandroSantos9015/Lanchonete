using Lanchonete.Models;
using Lanchonete.Servicos.Interfaces;
using Lanchonete.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lanchonete.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheService _lancheService;

        public HomeController(ILancheService lancheService)
        {
            _lancheService = lancheService;     
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheService.LanchesPreferidos
            };


            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}