namespace CMM.Projects.Apresentation.Models
{
    using System.ComponentModel.DataAnnotations;
    public class VinculoTipoModelView
    {
        [Key]
        public int VNCTP_ID { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "Informe a Descrição")]
        [StringLength(300)]
        public string VNCTP_DESCRICAO { get; set; }

        [ScaffoldColumn(false)]
        public int? VNCTP_REGUSER { get; set; }


    }
}