using System;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class PontoImportacaoModelView
    {
        [Key]
        [Display(Name = "IMPORTACAO")]
        public int PONIMP_ID { get; set; }


        [Display(Name = "OBSERVAÇÃO")]
        [StringLength(300)]
        [DataType(DataType.MultilineText)]
        public string PONIMP_OBSERVACAO { get; set; }


        [Display(Name = "INICIO")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PONIMP_DATAINICIO { get; set; }

        [Required(ErrorMessage = "Informe a DATA FIM")]
        [Display(Name = "FIM")]
        [DataType(DataType.Date), DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PONIMP_DATAFIM { get; set; }

        [Display(Name = "TOTAL")]
        [ScaffoldColumn(false)]
        public Nullable<int> PONIMP_TOTAL { get; set; }

        [Display(Name = "EQUIPAMENTO")]
        [ScaffoldColumn(false)]
        public Nullable<int> PONEQP_ID { get; set; }


        public Nullable<int> PONIMP_REGUSER { get; set; }
    }
}