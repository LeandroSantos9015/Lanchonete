using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using Lanchonete.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverItemNoCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);

            return RedirectToAction("Index");
        }
    }
}
