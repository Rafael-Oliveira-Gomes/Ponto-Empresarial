using ForPonto.Config.Context;
using ForPonto.Model;
using Microsoft.EntityFrameworkCore;

namespace ForPonto.Repository
{
    public class PontoRepository
    {
        private readonly MySqlContext _context;
        public PontoRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<Ponto> CreatePonto(Ponto Ponto)
        {
            var ret = await _context.Pontos.AddAsync(Ponto);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;

            return ret.Entity;
        }
    }
}
