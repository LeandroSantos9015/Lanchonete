using Lanchonete.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaMenu(ICategoriaService categoriaService) { _categoriaService = categoriaService; }

        public IViewComponentResult Invoke()
        {
            var categoria = _categoriaService.Categorias.OrderBy(c => c.CategoriaNome);

            return View(categoria);     
        }



    }
}
