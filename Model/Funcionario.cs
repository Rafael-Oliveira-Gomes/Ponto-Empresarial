namespace ForPonto.Model
{
    public class Funcionario
    {
        public Funcionario()
        {
        }
        public Funcionario(FuncionarioDto dto)
        {
            Id = dto.Id;
            Nome = dto.Nome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }


        public ICollection<Ponto> Pontos { get; set; }
    }
    public class FuncionarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

}
