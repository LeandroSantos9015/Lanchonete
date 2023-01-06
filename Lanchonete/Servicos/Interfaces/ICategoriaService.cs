using Lanchonete.Models;

namespace Lanchonete.Servicos.Interfaces
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
