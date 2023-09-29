namespace CMM.Projects.Apresentation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VinculoUnidadeModelView : IValidatableObject
    {

        [Key]
        [Display(Name = "LOTAÇÃO")]
        public int VNCU_ID { get; set; }

        [ScaffoldColumn(false)]
        public int FUN_ID { get; set; }


        [Display(Name = "VÍNCULO")]
        // [System.Web.Mvc.Remote("ValidaInsert", "VinculoxUnidade", HttpMethod = "POST", ErrorMessage = "O Vínculo já possui uma lotação ativa")]
        public int VNC_ID { get; set; }


        [ScaffoldColumn(false)]
        public string VNC_NOME { get; set; }


        [Display(Name = "UNIDADE")]
        public int UND_ID { get; set; }

        [ScaffoldColumn(false)]
        public string UND_NOME { get; set; }


        [Display(Name = "DATA INICIO")]
        [Required(ErrorMessage = "Informe a DATA INICIO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? VNCU_DATAINICIO { get; set; }

        [Display(Name = "DATA FIM")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? VNCU_DATAFIM { get; set; }


        [Display(Name = "ATRIBUIÇÃO")]
        [StringLength(600)]
        //[Required(ErrorMessage = "Informe o ATRIBUIÇÃO", AllowEmptyStrings = false)]
        public string VNCU_ATRIBUICAO { get; set; }

        [ScaffoldColumn(false)]
        public int? VNCU_REGUSER { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (VNCU_DATAFIM < VNCU_DATAINICIO)
            {
                yield return new ValidationResult("Data Fim não pode ser menor que Data Inicio", new[] { "VNCU_DATAINICIO" });

            }

            if (VNCU_DATAINICIO > DateTime.Today)
            {
                yield return new ValidationResult("Data Inicio não pode ser maior que Data Atual", new[] { "VNCU_DATAINICIO" });

            }


        }
    }
}