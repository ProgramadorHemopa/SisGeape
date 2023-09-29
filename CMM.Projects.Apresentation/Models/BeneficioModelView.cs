
namespace CMM.Projects.Apresentation.Models
{

    using System.ComponentModel.DataAnnotations;

    public class BeneficioModelView
    {
        [Key]
        [Display(Name = "BENEFÍCIO")]
        public int BNF_ID { get; set; }


        [Display(Name = "NOME")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o NOME do Benefício", AllowEmptyStrings = false)]
        public string BNF_DESCRICAO { get; set; }

        [Display(Name = "VALOR")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal BNF_VALOR { get; set; }

        [ScaffoldColumn(false)]
        public int? BNF_REGUSER { get; set; }


    }
}