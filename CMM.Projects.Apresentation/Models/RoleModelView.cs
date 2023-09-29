using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMM.Projects.Apresentation.Models
{
    [Table("roles")]
    public class RoleModelView
    {
        [Key]
        [Display(Name = "PERFIL")]
        public int ROL_ID { get; set; }

        [Display(Name = "NOME")]
        [StringLength(200)]
        [Required(ErrorMessage = "Informe o NOME do Perfil", AllowEmptyStrings = false)]
        public string ROL_NOME { get; set; }

        [ScaffoldColumn(false)]
        public int? ROL_REGUSER { get; set; }

    }
}