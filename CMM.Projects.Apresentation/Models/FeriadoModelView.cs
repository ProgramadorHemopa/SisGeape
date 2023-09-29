using CCM.Projects.SisGeape2.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMM.Projects.Apresentation.Models
{
    public class FeriadoModelView
    {
        [Key]
        public int FER_ID { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        public string FER_DESCRICAO { get; set; }

        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "Informe o Tipo de Feriado")]
        public TipoFeriado? FER_TIPO { get; set; }


        [Display(Name = "DATA DO FERIADO")]
        [Required(ErrorMessage = "Informe a DATA DO FERIADO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? FER_DATA { get; set; }


        [ScaffoldColumn(false)]
        public int FER_REGUSER { get; set; }
    }


}