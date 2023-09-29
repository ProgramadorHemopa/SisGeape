using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeape2.Domain
{
    public class ConselhoClasseDomainModel
    {
        public int CONCLA_ID { get; set; }
        public string CONCLA_DATAQUITACAO { get; set; }
        public string CONCLA_REFANO { get; set; }
        public int CONCLA_REGUSER { get; set; }
        public int VNC_ID { get; set; }
        public int CON_ID { get; set; }
        public string CONCLA_DATARECEBIMENTO { get; set; }
        public string CONSELHO { get; set; }


    }
    public class ConselhoDomainModel
    {
        public int CON_ID { get; set; }
        public Nullable<int> CRG_ID { get; set; }
        public string CON_NOME { get; set; }
        public string CRG_NOME { get; set; }

        public Nullable<int> CON_REGUSER { get; set; }
    }

    public class ConselhoClasseVinculoDomainModel
    {
        public byte[] FUN_FOTO { get; set; }
        public int FUN_ID { get; set; }
        public int VNC_ID { get; set; }
        public int CRG_ID { get; set; }
        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public string CARGO { get; set; }
        public string UNIDADE { get; set; }
        public string TIPO { get; set; }
        public string SITUACAO { get; set; }

        public virtual ICollection<ConselhoClasseDomainModel> ConselhoClasse { get; set; }
    }


    public class PesquisaConselhoClasseDomainModel
    {

        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public int? IDCARGO { get; set; }
        public int? IDUNIDADE { get; set; }
        public string UNIDADE { get; set; }
        public string CARGO { get; set; }
        public int? SITUACAO { get; set; }
        public string ANO { get; set; }

    }

    public class PesquisaConselhoClasseListDomainModel
    {

        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public string CARGO { get; set; }
        public string UNIDADE { get; set; }
        public int? VNC_ID { get; set; }
        public int? SITUACAO { get; set; }


    }
}
