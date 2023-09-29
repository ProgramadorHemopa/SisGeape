using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class JustificativaPontoDomainModel
    {
        public int JUSPT_ID { get; set; }
        public int MTVJUS_ID { get; set; }
        public string MTVJUS_NOME { get; set; }
        public int? CID_ID { get; set; }
        public string CID_NOME { get; set; }
        public DateTime? JUSPT_DATAINICIO { get; set; }
        public DateTime? JUSPT_DATAFIM { get; set; }
        public DateTime? JUSPT_DATARECEBIMENTO { get; set; }
        public int? VNC_ID { get; set; }
        public string FUN_MATRICULA { get; set; }
        public string FUN_NOME { get; set; }
        public string UND_NOME { get; set; }
        public string VNC_NOME { get; set; }
        public string JUSPT_OBSERVACAO { get; set; }
        public int? JUSPT_REGUSER { get; set; }
        public int FUN_ID { get; set; }
    }


    public class MotivoJustificativaDomainModel
    {
        public int MTVJUS_ID { get; set; }
        public string MTVJUS_NOME { get; set; }
        public int? MTVJUS_REGUSER { get; set; }
    }
}
