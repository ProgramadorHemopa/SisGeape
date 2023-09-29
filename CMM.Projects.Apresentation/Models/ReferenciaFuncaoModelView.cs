namespace CMM.Projects.Apresentation.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ReferenciaFuncaoModelView
    {

        [Key]
        [Display(Name = "REF.")]
        public int REFFNC_ID { get; set; }


        [Display(Name = "NOME")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o NOME", AllowEmptyStrings = false)]
        public string REFFNC_NOME { get; set; }

        [Display(Name = "VALOR")]
        // [NumeroBrasil(ErrorMessage = "Número inválido", DecimalRequerido = true, PontoMilharPermitido = false)]
        public decimal REFFNC_VALOR { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        [StringLength(200)]
        public string REFFNC_OBSERVACAO { get; set; }

        [ScaffoldColumn(false)]
        public int? REFFNC_REGUSER { get; set; }


    }
}