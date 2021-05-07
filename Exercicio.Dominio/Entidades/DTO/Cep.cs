using Exercicio.Dominio.Entidades.Externas;

namespace Exercicio.Dominio.Entidades.DTO
{
    public class Cep
    {
        public Cep()
        {

        }

        public Cep(ViaCepResponse cep)
        {
            this.NumeroDoCep = cep.cep;
            this.Logradouro = cep.logradouro;
            this.Bairro = cep.bairro;
            this.Cidade = cep.localidade;
        }
        public string NumeroDoCep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
    }
}
