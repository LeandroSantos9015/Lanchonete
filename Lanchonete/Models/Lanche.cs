using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("Lanches")]
    public class Lanche
    {

        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage ="O nome do lanche deve ser informado")]
        [Display(Name ="Nome do lanche")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Descrição do lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres"), MaxLength(200)]
        public string DescricaoCurta { get; set; }
        
        public string DescricaoDetalhada { get; set; }

        [Required]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99,ErrorMessage ="O preço deve estar entre {0} e {1}")]
        public decimal Preco { get; set; }

        public string ImagemUrl { get; set; }

        public string ImagemThumbnailUrl { get; set; }

        public bool IsLanchePreferido { get; set; }

        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

        [NotMapped]
        public int Teste { get; set; }
    }
}
