
namespace CMM.Projects.Apresentation.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CargoModelView
    {
        [Key]
        public int CRG_ID { get; set; }

        [Display(Name = "CÓDIGO")]
        [StringLength(45)]
        [Required(ErrorMessage = "Informe o CODIGO", AllowEmptyStrings = false)]
        public string CRG_CODIGO { get; set; }


        [Display(Name = "NOME")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o NOME", AllowEmptyStrings = false)]
        public string CRG_NOME { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [StringLength(1000, ErrorMessage = "O limite máximo de {0} caracteres foi excedido.")]
        [DataType(DataType.MultilineText)]
        public string CRG_DESCRICAO { get; set; }

        [Display(Name = "REF SALARIAL")]
        [StringLength(100)]
        //[Required(ErrorMessage = "Informe o REF SALARIAL", AllowEmptyStrings = false)]
        public string CRG_REFSALARIAL { get; set; }

        [Display(Name = "REF CARGO")]
        [StringLength(20)]
        public string CRG_REFCARGO { get; set; }

        [Display(Name = "RISCO")]
        [StringLength(100)]
        public string CRG_RISCOS { get; set; }

        [Display(Name = "ESCOLARIDADE")]
        [Required(ErrorMessage = "Informe a Escolaridade")]
        public int CRG_ESCOLARIDADE { get; set; }

        [Display(Name = "BASE SALARIAL")]
        //[NumeroBrasil(ErrorMessage = "Número inválido", DecimalRequerido = true, PontoMilharPermitido = false)]
        public decimal CRG_BASESALARIAL { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "ESCOLARIDADE")]
        public string CRG_ESCOLARIDADENOME { get; set; }

        [ScaffoldColumn(false)]
        public int? CRG_REGUSER { get; set; }

        public string _Escolaridade
        {
            get
            {
                switch (CRG_ESCOLARIDADE)
                {
                    case 1: return "Fundamental";
                    case 2: return "Médio";
                    case 3: return "Superior";

                    default:
                        return "";
                }

            }
        }
    }
}