using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class FuncaoVinculoDomainModel
    {
        public int FNCVNC_ID { get; set; }
        public int VNC_ID { get; set; }
        public int FUN_ID { get; set; }

        public string FNC_NOME { get; set; }
        public string VNC_NOME { get; set; }
        public int FNC_ID { get; set; }
        public DateTime? FNCVNC_DATAINICIO { get; set; }
        public DateTime? FNCVNC_DATAFIM { get; set; }
        public DateTime? FNCVNC_DATAPORTARIA { get; set; }
        public string FNCVNC_NUMPORTARIA { get; set; }
        public int? FNCVNC_REGUSER { get; set; }
    }
}
