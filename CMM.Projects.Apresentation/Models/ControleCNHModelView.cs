using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class ControleCNHModelView : IValidatableObject
    {
        [Key]
        public int? CNH_ID { get; set; }

        [Display(Name = "VÍNCULO")]
        [Required(ErrorMessage = "Informe o VÍNCULO")]
        public int? VNC_ID { get; set; }

        [Display(Name = "NUMERO DE REGISTRO")]
        [Required(ErrorMessage = "Informe o NUMERO DE REGISTRO", AllowEmptyStrings = false)]
        [CustomValidation.ValidaCNH(ErrorMessage = "CNH inválido")]
        public string CNH_NREG { get; set; }

        [ScaffoldColumn(false)]
        public int? CNH_REGUSER { get; set; }

        [Display(Name = "CATEGORIA")]
        [Required(ErrorMessage = "Informe a CATEGORIA")]
        public string CNHA_CATEGORIA { get; set; }

        [Display(Name = "DATA DE VALIDADE")]
        [Required(ErrorMessage = "Informe a DATA DE VALIDADE")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [CustomValidation.ValidaDataValidadeCNH(ErrorMessage = "A Data de Validade não pode ser menor que a Data Atual")]
        public DateTime? CNHA_VALIDADE { get; set; }

        [Display(Name = "DATA DE EMISSAO")]
        [Required(ErrorMessage = "Informe a DATA DE EMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [CustomValidation.ValidaDataEmissaoCNH(ErrorMessage = "A Data de Emissão não pode ser maior que a Data Atual")]
        public DateTime? CNHA_EMISSAO { get; set; }

        [Display(Name = "DATA DE RECEBIMENTO")]
        [Required(ErrorMessage = "Informe a DATA DE RECEBIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? CNHA_DATARECEBIMENTO { get; set; }

        [ScaffoldColumn(false)]
        public int CNHA_ID { get; set; }

        [ScaffoldColumn(false)]
        public string CNHA_STATUS { get; set; }

        [ScaffoldColumn(false)]
        public string CNH_STATUS { get; set; }

        [ScaffoldColumn(false)]
        public int? CNHA_REGUSER { get; set; }

        public ICollection<ControleCNHAuxiliarModelView> auxiliar { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (CNHA_VALIDADE < CNHA_EMISSAO)
            {
                yield return new ValidationResult("A Data de Validade não pode ser Menor que a de Emissao: ", new[] { "CNHA_VALIDADE" });
            }
        }
    }

    public class ControleCNHAuxiliarModelView
    {
        [Key]
        public int CNHA_ID { get; set; }

        [Display(Name = "CATEGORIA")]
        [Required(ErrorMessage = "Informe a CATEGORIA")]
        public string CNHA_CATEGORIA { get; set; }

        public int? CNH_ID { get; set; }

        public ControleCNHModelView CNH { get; set; }

        [Display(Name = "DATA DE VALIDADE")]
        [Required(ErrorMessage = "Informe a DATA DE VALIDADE")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? CNHA_VALIDADE { get; set; }

        [Display(Name = "DATA DE EMISSAO")]
        [Required(ErrorMessage = "Informe a DATA DE EMISSÃO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? CNHA_EMISSAO { get; set; }

        [Display(Name = "DATA DE RECEBIMENTO")]
        [Required(ErrorMessage = "Informe a DATA DE RECEBIMENTO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? CNHA_DATARECEBIMENTO { get; set; }

        [ScaffoldColumn(false)]
        public int? CNHA_REGUSER { get; set; }

        [ScaffoldColumn(false)]
        public string CNHA_STATUS { get; set; }

    }

    public class ControleCNHVinculoModelView
    {
        public byte[] FUN_FOTO { get; set; }

        public int FUN_ID { get; set; }
        public int CRG_ID { get; set; }

        public int VNC_ID { get; set; }

        [Display(Name = "MATRICULA")]
        public string MATRICULA { get; set; }

        [Display(Name = "NOME")]
        public string FUN_NOME { get; set; }

        [Display(Name = "CARGO")]
        public string CARGO { get; set; }

        [Display(Name = "UNIDADE")]
        public string UNIDADE { get; set; }

        [Display(Name = "TIPO DE VÍNCULO")]
        public string TIPO { get; set; }

        [Display(Name = "SITUAÇÃO")]
        public string SITUACAO { get; set; }

        public virtual ICollection<ControleCNHModelView> ControleCNH { get; set; }
    }

    public class ControleCNHPesquisaModelView
    {
        [Display(Name = "NUMERO DE REGISTRO")]
        public string CNH_NREG { get; set; }

        public int? CNH_VNC_ID { get; set; }

        [Display(Name = "DATA DE ADMISSAO")]
        public DateTime? CNH_VNC_ADMISSAO { get; set; }

        [Display(Name = "DATA DE EMISSAO")]
        public DateTime? CNH_VNC_DEMISSAO { get; set; }

        public byte[] CNH_FUN_FOTO { get; set; }

        public int? CNH_FUN_ID { get; set; }

        [Display(Name = "MATRICULA")]
        public string CNH_FUN_MATRICULA { get; set; }

        [Display(Name = "NOME")]
        public string CNH_FUN_NOME { get; set; }

        public int? CNH_CRG_ID { get; set; }

        [Display(Name = "CARGO")]
        public string CNH_CRG_NOME { get; set; }

        public int? CNH_UND_ID { get; set; }

        [Display(Name = "UNIDADE")]
        public string CNH_UND_SIGLA { get; set; }

        [Display(Name = "DATA DE VALIDADE")]
        public DateTime? CNH_VALIDADE { get; set; }

        [Display(Name = "DIAS PARA VENCER")]
        public int? CNH_DIAS_PARA_VENCER { get; set; }

        public int? CNH_SITUACAO { get; set; }

        [Display(Name = "DATA DE EMISSAO")]
        public DateTime? CNH_EMISSAO { get; set; }

        [Display(Name = "DATA DE RECEBIMENTO")]
        public DateTime? CNH_DATARECEBIMENTO { get; set; }

        [Display(Name = "CATEGORIA")]
        public string CNH_CATEGORIA { get; set; }

        [Display(Name = "SITUAÇÃO")]
        public string CNH_VNC_SITUACAO { get; set; }

        [Display(Name = "TIPO")]
        public string CNH_VNC_TIPO { get; set; }

        [ScaffoldColumn(false)]
        public int CNH_REGUSER { get; set; }

    }
}