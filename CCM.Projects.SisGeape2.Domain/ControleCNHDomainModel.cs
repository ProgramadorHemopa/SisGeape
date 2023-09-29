using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeape2.Domain
{
    public class ControleCNHDomainModel
    {
        public int CNH_ID { get; set; }
        public int VNC_ID { get; set; }

        public string CNH_NREG { get; set; }

        public int CNH_REGUSER { get; set; }
        public DateTime CNH_REGDATE { get; set; }
        public string CNH_STATUS { get; set; }

        public ICollection<ControleCNHAuxiliarDomainModel> auxiliar { get; set; }
    }

    public class ControleCNHAuxiliarDomainModel
    {
        public int? CNHA_ID { get; set; }

        public int? CNH_ID { get; set; }
        public ControleCNHDomainModel CNH { get; set; }

        public string CNHA_CATEGORIA { get; set; }

        public DateTime? CNHA_VALIDADE { get; set; }
        public DateTime? CNHA_EMISSAO { get; set; }
        public DateTime? CNHA_DATARECEBIMENTO { get; set; }

        public int? CNHA_REGUSER { get; set; }
        public DateTime CNHA_REGDATE { get; set; }
        public string CNHA_STATUS { get; set; }

    }

    public class ControleCNHVinculoDomainModel
    {
        public byte[] FUN_FOTO { get; set; }
        public int FUN_ID { get; set; }
        public int VNC_ID { get; set; }
        public int CRG_ID { get; set; }
        public string MATRICULA { get; set; }
        public string FUN_NOME { get; set; }
        public string CARGO { get; set; }
        public string UNIDADE { get; set; }
        public string TIPO { get; set; }
        public string SITUACAO { get; set; }

        public virtual ICollection<ControleCNHDomainModel> ControleCNH { get; set; }

    }

    public class ControleCNHPesquisaDomainModel
    {
        public string CNH_NREG { get; set; }

        public int? CNH_VNC_ID { get; set; }

        public DateTime? CNH_VNC_ADMISSAO { get; set; }
        public DateTime? CNH_VNC_DEMISSAO { get; set; }

        public byte[] CNH_FUN_FOTO { get; set; }

        public int? CNH_FUN_ID { get; set; }
        public string CNH_FUN_MATRICULA { get; set; }
        public string CNH_FUN_NOME { get; set; }

        public int? CNH_CRG_ID { get; set; }
        public string CNH_CRG_NOME { get; set; }

        public int? CNH_UND_ID { get; set; }
        public string CNH_UND_SIGLA { get; set; }

        public DateTime? CNH_VALIDADE { get; set; }
        public int? CNH_DIAS_PARA_VENCER { get; set; }

        public DateTime? CNH_EMISSAO { get; set; }
        public DateTime? CNH_DATARECEBIMENTO { get; set; }
        public string CNH_CATEGORIA { get; set; }

        public int? CNH_SITUACAO { get; set; }

        public string CNH_VNC_SITUACAO { get; set; }
        public string CNH_VNC_TIPO { get; set; }

        public int CNH_REGUSER { get; set; }
    }
}
