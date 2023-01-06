using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using Lanchonete.Servicos.Interfaces;

namespace Lanchonete.Servicos
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository) { _pedidoRepository = pedidoRepository; }

        public void CriarPedido(Pedido pedido) => _pedidoRepository.CriarPedido(pedido);
    }
}
