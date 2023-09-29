
namespace CMM.Projects.Apresentation.Models
{
    using System.ComponentModel.DataAnnotations;


    public class bancoModelView
    {


        [Key]
        public int BNC_ID { get; set; }

        [Display(Name = "NOME")]
        [StringLength(300)]
        [Required(ErrorMessage = "Informe o NOME do Banco", AllowEmptyStrings = false)]
        public string BNC_DESCRICAO { get; set; }

        [ScaffoldColumn(false)]
        public int? BNC_REGUSER { get; set; }






    }
}