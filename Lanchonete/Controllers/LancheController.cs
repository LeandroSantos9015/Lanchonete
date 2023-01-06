using Lanchonete.Models;
using Lanchonete.Servicos;
using Lanchonete.Servicos.Interfaces;
using Lanchonete.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheService _lancheService;
        public LancheController(ILancheService lancheService) { _lancheService = lancheService; }

        public IActionResult List(string categoria)
        {
            //ViewData["Titulo"] = "Todos os Lanches";
            //ViewData["Data"] = DateTime.Now;
            //var lanches = _lancheService.List();
            //return View(lanches);

            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheService.Lanches().OrderBy(l => l.LancheId).ToList();
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheService.Lanches().Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                        .OrderBy(l => l.Nome);

                categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);

        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheService.Lanches().FirstOrDefault(l => l.LancheId == lancheId);

            return View(lanche);

        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheService.Lanches().OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheService.Lanches().Where(p => p.Nome.ToLower() == searchString.ToLower());

                categoriaAtual = lanches.Any() ? "Lanches" : "Nenhum Lanche foi encontrado";
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }

    }
}
