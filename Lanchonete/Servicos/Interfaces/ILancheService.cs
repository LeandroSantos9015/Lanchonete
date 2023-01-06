using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;

namespace Lanchonete.Servicos.Interfaces
{
    

    public interface ILancheService
    {
        IEnumerable<Lanche> Lanches();

        IEnumerable<Lanche> LanchesPreferidos { get; }

        Lanche GetLancheById(int lancheId);
    }
}
