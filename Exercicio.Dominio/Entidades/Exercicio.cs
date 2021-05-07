using System.ComponentModel.DataAnnotations;

namespace Exercicio.Dominio.Entidades
{
    public class Exercicio
    {
        [Key]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        public string Titulo { get; set; }
    }
}