using System;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class ConselhoModelView
    {
        [Key]
        public int CON_ID { get; set; }

        [Display(Name = "CARGO")]
        [Required(ErrorMessage = "Informe o CARGO", AllowEmptyStrings = false)]
        public Nullable<int> CRG_ID { get; set; }

        [Display(Name = "CONSELHO")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o CONSELHO", AllowEmptyStrings = false)]
        public string CON_NOME { get; set; }

        [ScaffoldColumn(false)]
        public string CRG_NOME { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<int> CON_REGUSER { get; set; }

    }
}