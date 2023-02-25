namespace ForPonto.Model
{
    public class Ponto
    {
        public Ponto() { }

        public Ponto(PontoDto dto)
        {
            Id = dto.Id;
            Segunda = dto.Segunda;
            Terca = dto.Terca;
            Quarta = dto.Quarta;
        }

        public int Id { get; set; }
        public int Segunda { get; set; }
        public int Terca { get; set; }
        public int Quarta { get; set;}

        public ICollection<Funcionario> Funcionarios { get; set; }
    }

    public class PontoDto
    {
        public int Id { get; set; }
        public int Segunda { get; set; }
        public  int Terca { get; set; }
        public int Quarta { get; set;}
    }
}
