using ForPonto.Model;
using ForPonto.Repository;

namespace ForPonto.Service
{
    public class FuncionarioService
    {
        private readonly FuncionarioRepository _funcionarioRepository;
        private readonly AuthService _authService;
        public FuncionarioService(FuncionarioRepository funcionarioRepository, AuthService authService)
        {
            _funcionarioRepository = funcionarioRepository;
            _authService = authService;
        }

        public async Task<bool> AdicionaFuncionario(FuncionarioDto funcionarioDto)
        {
            var funcionario = new Funcionario(funcionarioDto);
            var currentUser = await _authService.GetCurrentUser();
            var result = await _funcionarioRepository.CreateFuncionario(funcionario);

            return true;
        }

        public async Task<bool> RemoverFuncionario(int funcionarioId)
        {
            await _funcionarioRepository.DeleteFuncionarioAsync(funcionarioId);

            return true;
        }

        public async Task<Funcionario> GetFuncionario(int funcionarioId)
        {
            var funcionario = await _funcionarioRepository.GetFuncionarioById(funcionarioId);
            if (funcionario == null) throw new Exception("Funcionário não encontrado!");

            return funcionario;
        }

        public async Task<Funcionario> GetFuncionarioByNome(string nomeFuncionario)
        {
            var Funcionario = await _funcionarioRepository.GetFuncionarioByNome(nomeFuncionario);
            if (Funcionario == null) throw new Exception("Não encontrou");

            return Funcionario;
        }

        public async Task<List<Funcionario>> ListarFuncionario()
        {
            var funcionarios = await _funcionarioRepository.ListFuncionarios();

            return funcionarios;
        }

        public async Task<int> UpdateFuncionario(FuncionarioDto funcionarioDto)
        {
            var funcionario = new Funcionario(funcionarioDto);

            var currentUser = await _authService.GetCurrentUser();
           // funcionario.UserUltimoUpdate = currentUser.UserName;

            return await _funcionarioRepository.UpdateFuncionario(funcionario);
        }

        public async Task<Funcionario> GetHorarios(int funcionarioId)
        {
            var funcionario = await _funcionarioRepository.GetFuncionarioById(funcionarioId);
            if (funcionario == null) throw new Exception("Funcionário não encontrado!");


            return funcionario;
        }
    }
}
