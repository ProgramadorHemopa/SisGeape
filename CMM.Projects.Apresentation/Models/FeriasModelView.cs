using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class FeriasModelView : IValidatableObject
    {

        [Key]
        public int FRS_ID { get; set; }

        [Display(Name = "VÍNCULO")]
        public int VNC_ID { get; set; }

        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }

        [ScaffoldColumn(false)]
        public string MATRICULA { get; set; }

        [ScaffoldColumn(false)]
        public string UNIDADE { get; set; }

        [ScaffoldColumn(false)]
        public string FUN_NOME { get; set; }

        [ScaffoldColumn(false)]
        public int FUN_ID { get; set; }


        [Display(Name = "PERÍODO AQUISITIVO")]
        [ScaffoldColumn(false)]
        public string FRS_PERIODOAQUISITIVO { get; set; }

        [Display(Name = "DATA INICIO AQUISITIVO")]
        [Required(ErrorMessage = "Informe a DATA INICIO AQUISITIVO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FRS_DATA_INICIOAQUISITIVO { get; set; }

        [Display(Name = "DATA FIM AQUISITIVO")]
        [Required(ErrorMessage = "Informe a DATA FIM AQUISITIVO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FRS_DATA_FIMAQUISITIVO { get; set; }

        [Display(Name = "DATA INICIO GOZO")]
        [Required(ErrorMessage = "Informe a DATA INICIO GOZO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FRS_DATA_INICIOGOZO { get; set; }

        [Display(Name = "DATA FIM GOZO")]
        [Required(ErrorMessage = "Informe a DATA FIM GOZO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FRS_DATA_FIMGOZO { get; set; }


        [Display(Name = "DATA RETORNO")]
        [Required(ErrorMessage = "Informe a DATA RETORNO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FRS_DATA_RETORNO { get; set; }

        [Display(Name = "DATA PORTARIA")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FRS_DATAPORTARIA { get; set; }

        [Display(Name = "Nº PORTARIA")]
        [StringLength(45)]
        public string FRS_NUMPORTARIA { get; set; }


        [Display(Name = "OBSERVAÇÃO")]
        [StringLength(600)]
        [DataType(DataType.MultilineText)]
        public string FRS_OBSERVACAO { get; set; }

        [ScaffoldColumn(false)]
        public int? FRS_REGUSER { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FRS_DATA_INICIOGOZO < FRS_DATA_FIMAQUISITIVO)
            {
                yield return new ValidationResult("Data Inicio do Gozo não pode ser menor que a Data Fim Aquisitivo", new[] { "FRS_DATA_INICIOGOZO" });
            }

            if (FRS_DATA_INICIOGOZO > FRS_DATA_FIMGOZO)
            {
                yield return new ValidationResult("Data Inicio do Gozo não pode ser maior que a Data Fim do Gozo", new[] { "FRS_DATA_INICIOGOZO" });
            }

            if (FRS_DATA_RETORNO < FRS_DATA_FIMGOZO)
            {
                yield return new ValidationResult("Data do Retorno não pode ser menor que a Data Fim do Gozo", new[] { "FRS_DATA_RETORNO" });
            }




        }
    }
}