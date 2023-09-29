using System;
using System.ComponentModel.DataAnnotations;

namespace CCM.Projects.SisGeape2.Domain
{
    public class FeriadoDomainModel
    {
        public int FER_ID { get; set; }
        public string FER_DESCRICAO { get; set; }
        public TipoFeriado? FER_TIPO { get; set; }
        public DateTime? FER_DATA { get; set; }
        public int FER_REGUSER { get; set; }
        public DateTime FER_REGDATE { get; set; }
        public string FER_STATUS { get; set; }
    }

    public enum TipoFeriado
    {
        [Display(Name = "Feriado Nacional")]
        feriadoNacional = 1,
        [Display(Name = "Feriado Estadual")]
        feriadoEstadual = 2,
        [Display(Name = "Ponto Facultativo")]
        pontoFacultativo = 3
    }
}
