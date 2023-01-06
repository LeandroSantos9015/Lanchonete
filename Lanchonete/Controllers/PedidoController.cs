using Lanchonete.Models;
using Lanchonete.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoService pedidoService, CarrinhoCompra carrinhoCompra)
        {
            _pedidoService = pedidoService;
            _carrinhoCompra = carrinhoCompra;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalDoPedido = 0m;

            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = items;

            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
                ModelState.AddModelError("", "Seu carrinho está vazio");


            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalDoPedido += item.Lanche.Preco * item.Quantidade;

            }


            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalDoPedido;

            if (ModelState.IsValid)
            {
                _pedidoService.CriarPedido(pedido);

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                _carrinhoCompra.LimparCarrinho();

                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            return View(pedido);

        }
    }
}