using Lanchonete.Models;
using Lanchonete.Servicos.Interfaces;

namespace Lanchonete.Repositories.Interfaces
{
    public interface IPedidoRepository
    {

        void CriarPedido(Pedido pedido);
    }
}
