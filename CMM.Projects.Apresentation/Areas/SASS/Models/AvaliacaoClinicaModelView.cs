using System;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Areas.SASS.Models
{
    public class AvaliacaoClinicaModelView
    {
        [Key]
        public int AVC_ID { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "Informe a DESCRIÇÃO")]
        public string AVC_DESCRICAO { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<int> AVC_REGUSER { get; set; }
    }
}