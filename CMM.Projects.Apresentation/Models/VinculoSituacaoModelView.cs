namespace CMM.Projects.Apresentation.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VinculoSituacaoModelView
    {
        [Key]
        public int VNCST_ID { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "Informe a Descrição")]
        [StringLength(300)]
        public string VNCST_DESCRICAO { get; set; }

        [ScaffoldColumn(false)]
        public int? VNCST_REGUSER { get; set; }

    }
}