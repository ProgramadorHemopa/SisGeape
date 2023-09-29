using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMM.Projects.Apresentation.Models
{
    [Table("systemuserxroles")]
    public class SystemUserRolesModelView
    {
        [Key]
        public int SUROL_ID { get; set; }

        [Required]
        public int? SIS_ID { get; set; }

        [Required]
        public int SUSR_ID { get; set; }

        [Required]
        public int ROL_ID { get; set; }

        [ScaffoldColumn(false)]
        public int? SUROL_REGUSER { get; set; }





    }

}