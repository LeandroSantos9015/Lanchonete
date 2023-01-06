using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using Lanchonete.Servicos.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lanchonete.Servicos
{
    public class LancheService : ILancheService
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheService(ILancheRepository lancheRepository) { _lancheRepository = lancheRepository; }

        public IEnumerable<Lanche> Lanches() { return _lancheRepository.Lanches; }

        public IEnumerable<Lanche> LanchesPreferidos => _lancheRepository.LanchesPreferidos.ToList();

        public Lanche GetLancheById(int lancheId) => _lancheRepository.GetLancheById(lancheId);
    }
}
