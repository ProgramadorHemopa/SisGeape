using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class JustificativaPontoModelView : IValidatableObject
    {


        [Key]
        [Display(Name = "JUSTIFICATIVA")]
        public int JUSPT_ID { get; set; }

        [Display(Name = "MOTIVO")]
        public int MTVJUS_ID { get; set; }

        [ScaffoldColumn(false)]
        public string MTVJUS_NOME { get; set; }


        [Display(Name = "CID")]
        public int? CID_ID { get; set; }

        [ScaffoldColumn(false)]
        public string CID_NOME { get; set; }

        [Display(Name = "DATA INICIO")]
        [Required(ErrorMessage = "Informe a DATA INICIO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? JUSPT_DATAINICIO { get; set; }

        [Display(Name = "DATA FIM")]
        [Required(ErrorMessage = "Informe a DATA FIM")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? JUSPT_DATAFIM { get; set; }

        [Display(Name = "DATA RECEBIMENTO")]
        [Required(ErrorMessage = "Informe a DATA RECEBIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? JUSPT_DATARECEBIMENTO { get; set; }


        [Display(Name = "VÍNCULO")]
        [Required(ErrorMessage = " Informe o VÍNCULO")]
        public int? VNC_ID { get; set; }

        [ScaffoldColumn(false)]
        public int FUN_ID { get; set; }

        [ScaffoldColumn(false)]
        public string FUN_MATRICULA { get; set; }
        [ScaffoldColumn(false)]
        public string FUN_NOME { get; set; }
        [ScaffoldColumn(false)]
        public string UND_NOME { get; set; }

        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }

        [Display(Name = "OBSERVAÇÃO")]
        [DataType(DataType.MultilineText)]
        public string JUSPT_OBSERVACAO { get; set; }

        [ScaffoldColumn(false)]
        public int? JUSPT_REGUSER { get; set; }




        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (MTVJUS_ID == 6 && CID_ID == null)
            //{
            //    yield return new ValidationResult("Para Justificativas por motivo de atestado médico, deverá ser informado o CID.", new[] { "CIDJUS_ID" });
            //}

            if (JUSPT_DATAINICIO > JUSPT_DATAFIM)
            {
                yield return new ValidationResult(" Data Fim não pode ser menor que a Data Inicio", new[] { "JUSPT_DATAFIM" });
            }

            if (JUSPT_DATAINICIO > JUSPT_DATARECEBIMENTO)
            {
                yield return new ValidationResult(" Data Inicio não pode ser maior que a Data atual", new[] { "JUSPT_DATAINICIO" });
            }

        }
    }


    public class MotivoJustificativaModelView
    {

        [Key]
        [Display(Name = "MOTIVO JUST.")]
        public int MTVJUS_ID { get; set; }

        [Display(Name = "MOTIVO")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o Motivo da Justificativa", AllowEmptyStrings = false)]
        public string MTVJUS_NOME { get; set; }

        [ScaffoldColumn(false)]
        public int? MTVJUS_REGUSER { get; set; }
    }

    public class CamposJustificativaBuscaRelatorioModelView
    {
        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public int? CARGO { get; set; }
        public string TEXTO_CARGO { get; set; }
        public int? MOTIVO { get; set; }
        public string TEXTO_MOTIVO { get; set; }
        public int? UNIDADE { get; set; }
        public string TEXTO_UNIDADE { get; set; }
        public DateTime? INICIO { get; set; }
        public DateTime? FIM { get; set; }
    }

}