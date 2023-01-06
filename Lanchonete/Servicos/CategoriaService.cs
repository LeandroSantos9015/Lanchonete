using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using Lanchonete.Servicos.Interfaces;

namespace Lanchonete.Servicos
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository) { _categoriaRepository = categoriaRepository; }

        public IEnumerable<Categoria> Categorias => _categoriaRepository.Categorias;
    }
}
