using System.ComponentModel.DataAnnotations;

namespace Exercicio.Dominio.Entidades
{
    public class Usuario
    {
        public Usuario()
        {

        }

        public Usuario(UsuarioFront usuarioFront)
        {
            this.Codigo = usuarioFront.Codigo;
            this.Nome = usuarioFront.Nome;
            this.Senha = usuarioFront.Senha;
            this.Cargo = usuarioFront.Cargo;
            this.Cep = usuarioFront.Cep;
        }

        [Key]
        public int Codigo { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage ="O campo deve ter entre 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage ="O campo deve ter entre 3 a 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        public string Senha { get; set; }

        [MaxLength(20, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        public string Cargo { get; set; }

        [MaxLength(8, ErrorMessage = "O CEP é uma string com 8 números!")]
        [MinLength(8, ErrorMessage = "O CEP é uma string com 8 números!")]
        public string Cep { get; set; }

        [MaxLength(80, ErrorMessage = "O tamanho máximo é 80 caracteres")]
        public string Logradouro { get; set; }

        [MaxLength(20, ErrorMessage = "O tamanho máximo é 20 caracteres")]
        public string Bairro { get; set; }

        [MaxLength(20, ErrorMessage = "O tamanho máximo é 20 caracteres")]
        public string Cidade { get; set; }


    }
}
