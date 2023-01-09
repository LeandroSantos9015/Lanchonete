using Lanchonete.Context;
using Lanchonete.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;

namespace Lanchonete.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var res = from obj in _context.Pedidos select obj;

            if (minDate.HasValue)
                res = res.Where(x => x.PedidoEnviado >= minDate.Value.Date);

            if (maxDate.HasValue)
                res = res.Where(x => x.PedidoEnviado <= maxDate.Value.AddDays(1).AddSeconds(-1));

            return await res.Include(l => l.PedidoItens)
                .ThenInclude(l => l.Lanche)
                .OrderByDescending(x => x.PedidoEnviado).ToListAsync();


        }
    }
}
