namespace CMM.Projects.Apresentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class FuncaoVinculoModelView : IValidatableObject
    {

        [Key]
        public int FNCVNC_ID { get; set; }

        [Display(Name = "VÍNCULO")]
        [Required(ErrorMessage = "Informe o VÍNCULO")]
        public int VNC_ID { get; set; }

        [Display(Name = "FUNÇÃO")]
        [Required(ErrorMessage = "Informe o FUNÇÃO")]
        public int FNC_ID { get; set; }

        [Display(Name = "DATA INICIO")]
        [Required(ErrorMessage = "Informe DATA INICIO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FNCVNC_DATAINICIO { get; set; }

        [Display(Name = "DATA FIM")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FNCVNC_DATAFIM { get; set; }

        [Display(Name = "DATA PORTARIA")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FNCVNC_DATAPORTARIA { get; set; }


        [Display(Name = "Nº PORTARIA")]
        [StringLength(45)]
        public string FNCVNC_NUMPORTARIA { get; set; }


        [ScaffoldColumn(false)]
        public string FNC_NOME { get; set; }


        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }

        [ScaffoldColumn(false)]
        public int FUN_ID { get; set; }

        [ScaffoldColumn(false)]
        public int? FNCVNC_REGUSER { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (FNCVNC_DATAFIM < FNCVNC_DATAINICIO)
            {
                yield return new ValidationResult("Data Fim não pode ser menor que Data Inicio", new[] { "FNCVNC_DATAINICIO" });

            }

            if (FNCVNC_DATAINICIO > DateTime.Today)
            {
                yield return new ValidationResult("Data Inicio não pode ser maior que Data Atual", new[] { "FNCVNC_DATAINICIO" });

            }

            if (FNCVNC_DATAPORTARIA < FNCVNC_DATAINICIO)
            {
                yield return new ValidationResult("Data da Portaria não pode ser menor que Data Inicio", new[] { "FNCVNC_DATAPORTARIA" });

            }
        }
    }
}