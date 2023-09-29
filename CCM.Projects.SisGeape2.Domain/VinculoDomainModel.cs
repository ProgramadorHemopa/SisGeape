using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CCM.Projects.SisGeape2.Domain
{
    public class VinculoDomainModel
    {
        public VinculoDomainModel()
        {
            Lotacao = new HashSet<VinculoUnidadeDomainModel>();
        }

        public int VNC_ID { get; set; }
        public string VNC_NOME { get; set; }

        public int FUN_ID { get; set; }
        public string FUN_NOME { get; set; }

        public int VNCTP_ID { get; set; }
        public string VNC_TIPO { get; set; }

        public string VNC_SITUACAO { get; set; }
        public int VNCST_ID { get; set; }

        public string CRG_NOME { get; set; }

        public int CRG_ID { get; set; }

        public DateTime? VNC_ADMISSAO { get; set; }
        public DateTime? VNC_DEMISSAO { get; set; }
        public DateTime? VNC_DATACESSAO { get; set; }
        public DateTime? VNC_ENTRADA_APOSENT { get; set; }
        public int? VNC_TIPO_APOSENT { get; set; }
        public int? VNC_CARGAHORARIA { get; set; }
        public int? VNC_QTD { get; set; }
        public string VNC_ORGAO_ORIGEM { get; set; }
        public string VNC_ORGAO_DESTINO { get; set; }
        public string VNC_ONUS { get; set; }
        public int VNC_AREA { get; set; }
        public string VNC_CONCURSO { get; set; }
        public string VNC_HISTORICO { get; set; }
        public int? VNC_REGUSER { get; set; }

        public virtual ICollection<VinculoUnidadeDomainModel> Lotacao { get; set; }
        public virtual FuncionarioDomainModel Funcionario { get; set; }
        //Adicionado por Angelo Matos em 26102020
        public virtual ICollection<FuncaoVinculoDomainModel> FuncaoVinculo { get; set; }
        //Fim Adição

    }

    //Adicionado por Angelo Matos em 25092019
    public enum TipoAposentadoria
    {
        [Display(Name = "Antecipada")]
        Antecipada = 1,
        [Display(Name = "Compusória")]
        Compusoria = 2,
        [Display(Name = "Especial")]
        Especial = 3,
        [Display(Name = "Invalidez")]
        Invalidez = 4,
        [Display(Name = "Voluntária")]
        Voluntaria = 5
    }

    //Adicionado por Angelo Matos em 25092019
    public enum TipoOnus
    {
        [Display(Name = "Origem")]
        O = 1,
        [Display(Name = "Destino")]
        D = 2
    }

}
