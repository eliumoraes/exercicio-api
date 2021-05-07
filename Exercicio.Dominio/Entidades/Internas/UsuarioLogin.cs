using System.ComponentModel.DataAnnotations;

namespace Exercicio.Dominio.Entidades
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "O campo deve ter entre 3 a 20 caracteres")]
        public string Senha { get; set; }
    }
}
