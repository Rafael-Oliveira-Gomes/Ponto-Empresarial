using ForPonto.Model;
using ForPonto.Repository;

namespace ForPonto.Service
{
    public class PontoService
    {
        private readonly PontoRepository _pontoRepository;
        private readonly AuthService _authService;
        public PontoService(PontoRepository pontoRepository, AuthService authService)
        {
            _pontoRepository = pontoRepository;
            _authService = authService;
        }

        public async Task<bool> AdicionaPonto(PontoDto pontoDto)
        {
            var ponto = new Ponto(pontoDto);
            var currentUser = await _authService.GetCurrentUser();
            var result = await _pontoRepository.CreatePonto(ponto);
            
            return true;
        }
    }
}
