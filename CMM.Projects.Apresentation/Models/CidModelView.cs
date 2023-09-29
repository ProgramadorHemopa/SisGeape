using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class CidModelView
    {

        [Key]
        public int CID_ID { get; set; }

        [Display(Name = "CODGIO")]
        [StringLength(10)]
        [Required(ErrorMessage = "Informe o CODIGO", AllowEmptyStrings = false)]
        public string CID_CODIGO { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [StringLength(500)]
        [Required(ErrorMessage = "Informe o DESCRIÇÃO", AllowEmptyStrings = false)]
        public string CID_DESCRICAO { get; set; }

        [ScaffoldColumn(false)]
        public int? CID_REGUSER { get; set; }
    }
}