using Lanchonete.Context;
using Microsoft.EntityFrameworkCore;

namespace Lanchonete.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context) { _context = context; }

        public string CarrinhoCompraId { get; set; }

        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            var carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context) { CarrinhoCompraId = carrinhoId };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem is null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
                carrinhoCompraItem.Quantidade++;

            _context.SaveChanges();
        }

        public void RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _context.CarrinhoCompraItens.SingleOrDefault(
                    x => x.Lanche.LancheId == lanche.LancheId &&
                    x.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem.Quantidade > 1)
                carrinhoCompraItem.Quantidade--;
            else
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);

            _context.SaveChanges();
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ??
                (CarrinhoCompraItens = _context.CarrinhoCompraItens
                    .Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(s => s.Lanche)
                    .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == CarrinhoCompraId);

            _context.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                .Where(x => x.CarrinhoCompraId == CarrinhoCompraId).Select(x => x.Lanche.Preco * x.Quantidade).Sum();

            return total;
        }
    }

}