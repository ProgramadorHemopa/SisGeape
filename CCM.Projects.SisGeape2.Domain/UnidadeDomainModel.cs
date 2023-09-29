using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class UnidadeDomainModel
    {

        public int UND_ID { get; set; }
        public string UND_CODIGO { get; set; }
        public string UND_SIGLA { get; set; }
        public string UND_RAMAL { get; set; }
        public string UND_COMPETENCIA { get; set; }
        public DateTime? UND_DATAINICIO { get; set; }
        public DateTime? UND_DATAFIM { get; set; }
        public string UND_NOME { get; set; }
        public int? UND_NIVEL { get; set; }
        public string UND_NIVEL_NOME { get; set; }
        public int? UND_PAI { get; set; }
        public string UND_PAI_NOME { get; set; }
        public string UND_DIRETORIO { get; set; }
        public int UND_REGUSER { get; set; }
    }
}
