using ForPonto.Config.Context;
using ForPonto.Model;
using Microsoft.EntityFrameworkCore;

namespace ForPonto.Repository
{
    public class FuncionarioRepository
    {
        public readonly MySqlContext _context;

        public FuncionarioRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<List<Funcionario>> ListFuncionarios()
        {
            List<Funcionario> list = await _context.Funcionarios.OrderBy(p => p.Nome).ToListAsync();
            return list;
        }

        public async Task<Funcionario> GetFuncionarioById(int funcionarioId)
        {
            Funcionario Funcionario = await _context.Funcionarios.FirstOrDefaultAsync(p => p.Id == funcionarioId);
            return Funcionario;
        }

        public async Task<Funcionario> GetFuncionarioByNome(string nomeFuncionario)
        {
            Funcionario Funcionario = await _context.Funcionarios.Where(x => x.Nome == nomeFuncionario).FirstOrDefaultAsync();
            return Funcionario;
        }
        public async Task<Funcionario> CreateFuncionario(Funcionario Funcionario)
        {
            var ret = await _context.Funcionarios.AddAsync(Funcionario);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdateFuncionario(Funcionario Funcionario)
        {
            _context.Entry(Funcionario).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteFuncionarioAsync(int FuncionarioId)
        {
            var item = await _context.Funcionarios.FindAsync(FuncionarioId);
            _context.Funcionarios.Remove(item);

            await _context.SaveChangesAsync();
            return true;

        }
    }
}
