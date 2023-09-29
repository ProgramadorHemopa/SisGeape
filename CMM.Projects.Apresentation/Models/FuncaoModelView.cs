namespace CMM.Projects.Apresentation.Models
{

    using System.ComponentModel.DataAnnotations;

    public class FuncaoModelView
    {
        [Key]
        [Display(Name = "FUNÇÃO")]
        public int FNC_ID { get; set; }

        [Display(Name = "NOME")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o Nome", AllowEmptyStrings = false)]
        public string FNC_NOME { get; set; }

        [Display(Name = "Nº VAGAS")]
        [Range(1, int.MaxValue, ErrorMessage = "Insira um Número válido.")]
        [Required(ErrorMessage = "Informe o Nº DE VAGAS", AllowEmptyStrings = false)]
        public int? FNC_QUANTIDADE { get; set; }


        [Display(Name = "REF.")]
        public int REFFNC_ID { get; set; }

        [ScaffoldColumn(false)]
        public string REF_NOME { get; set; }

        [ScaffoldColumn(false)]
        public int? FNC_REGUSER { get; set; }
    }


}