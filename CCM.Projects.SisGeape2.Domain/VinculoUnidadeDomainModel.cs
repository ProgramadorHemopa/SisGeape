using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class VinculoUnidadeDomainModel
    {
        public int VNCU_ID { get; set; }
        public string VNC_NOME { get; set; }
        public int VNC_ID { get; set; }
        public int FUN_ID { get; set; }
        public string UND_NOME { get; set; }
        public int UND_ID { get; set; }
        public DateTime? VNCU_DATAINICIO { get; set; }
        public DateTime? VNCU_DATAFIM { get; set; }
        public string VNCU_ATRIBUICAO { get; set; }
        public int? VNCU_REGUSER { get; set; }
    }
}