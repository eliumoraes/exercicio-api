using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercicio.Dominio.Entidades
{
    public class Produto
    {
        [Key]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 a 60 caracteres")]
        public string Titulo { get; set; }

        [MaxLength(1024, ErrorMessage ="Máximo de 1024 caracteres nesse campo")]
        public string Descricao { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatorio")]
        [Range(1, int.MaxValue, ErrorMessage ="O preço deve ser maior que zero")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage ="Categoria inválida")]
        public int CategoriaCodigo { get; set; }
        public Categoria Categoria { get; set; }
    }
}
