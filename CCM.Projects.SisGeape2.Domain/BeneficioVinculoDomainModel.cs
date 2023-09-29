using System;

namespace CCM.Projects.SisGeape2.Domain
{
    public class BeneficioVinculoDomainModel
    {
        public int BNFVNC_ID { get; set; }

        public int VNC_ID { get; set; }

        public int BNF_ID { get; set; }


        public DateTime? BNFVNC_DATAINICIO { get; set; }

        public DateTime? BNFVNC_DATAFIM { get; set; }


        public DateTime? BNFVNC_DATAPORTARIA { get; set; }


        public string BNFVNC_NUMPORTARIA { get; set; }

        public int? BNFVNC_REGUSER { get; set; }


        public string BNF_NOME { get; set; }

        public string VNC_NOME { get; set; }

        public int FUN_ID { get; set; }

    }

    public class CountBeneficioVinculoDomainModel
    {
        public int BNF_ID { get; set; }
        public string BENEFICIO { get; set; }
        public int QUANTIDADE { get; set; }
    }
}
