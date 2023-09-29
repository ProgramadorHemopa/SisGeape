

namespace CMM.Projects.Apresentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class BeneficioVinculoModelView : IValidatableObject
    {
        [Key]
        public int BNFVNC_ID { get; set; }

        [Display(Name = "VÍNCULO")]
        public int VNC_ID { get; set; }

        [Display(Name = "BENEFÍCIO")]
        public int BNF_ID { get; set; }

        [ScaffoldColumn(false)]
        public string BNF_NOME { get; set; }

        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }

        [ScaffoldColumn(false)]
        public int FUN_ID { get; set; }


        [Display(Name = "DATA INICIO")]
        [Required(ErrorMessage = "Informe DATA INICIO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BNFVNC_DATAINICIO { get; set; }

        [Display(Name = "DATA FIM")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BNFVNC_DATAFIM { get; set; }


        [Display(Name = "DATA PORTARIA")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BNFVNC_DATAPORTARIA { get; set; }


        [Display(Name = "Nº PORTARIA")]
        [StringLength(45)]
        public string BNFVNC_NUMPORTARIA { get; set; }

        [ScaffoldColumn(false)]
        public int? BNFVNC_REGUSER { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BNFVNC_DATAFIM < BNFVNC_DATAINICIO)
            {
                yield return new ValidationResult("Data Fim não pode ser menor que Data Inicio", new[] { "VNCU_DATAINICIO" });

            }

            if (BNFVNC_DATAINICIO > DateTime.Today)
            {
                yield return new ValidationResult("Data Inicio não pode ser maior que Data Atual", new[] { "VNCU_DATAINICIO" });

            }


            if (BNFVNC_DATAPORTARIA < BNFVNC_DATAINICIO)
            {
                yield return new ValidationResult("Data da Portaria não pode ser menor que Data Inicio", new[] { "VNCU_DATAPORTARIA" });

            }
        }
    }
}