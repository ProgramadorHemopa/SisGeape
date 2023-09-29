using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeape2.Domain
{

    public class FuncionarioCartaoPontoDomainModel
    {
        public int FUN_ID { get; set; }
        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public int TIPAGEM { get; set; }
        public byte[] FUN_FOTO { get; set; }
        public virtual ICollection<CartaoPontoDomainModel> CartaoPonto { get; set; }

    }
    public class CartaoPontoDomainModel
    {

        public int VNC_ID { get; set; }
        public int FUN_ID { get; set; }
        public int FRS_ID { get; set; }
        public int JUSTP_ID { get; set; }
        public DateTime? DATA { get; set; }
        public TimeSpan? ENTRADA1 { get; set; }
        public TimeSpan? SAIDA1 { get; set; }
        //[Display(Name = "ENTRADA 2")]
        //public string ENTRADA2 { get; set; }
        //[Display(Name = "SAÍDA 2")]
        //public string SAIDA2 { get; set; }
        public string JUSTIFICATIVA { get; set; }
        public string JUSTIFICATIVAPONTO_OBS { get; set; }
        public string FERIAS { get; set; }
        public string FERIAS_OBS { get; set; }

        public string FERIADO { get; set; }
        public string FERIADO_OBS { get; set; }
    }

    public class CartaoPontoUnidadeDomainModel
    {
        public int FUN_ID { get; set; }
        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public string TIPAGEM { get; set; }
        public byte[] FUN_FOTO { get; set; }
        public string UNIDADE { get; set; }
        public int VNC_ID { get; set; }
        public DateTime DATA { get; set; }
        public string DIA { get; set; }
        public string ENTRADA1 { get; set; }
        public string SAIDA1 { get; set; }
        public string FALTA { get; set; }
        public string DIFERENCA { get; set; }
        public string ATRASO { get; set; }
        public string JUSTIFICATIVA { get; set; }
    }

    public class PesquisaCartaoPontoDomainModel
    {
        public byte[] FUN_FOTO { get; set; }
        public int? FUN_ID { get; set; }
        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public int? IDCARGO { get; set; }
        public int? IDUNIDADE { get; set; }
        public string UNIDADE { get; set; }
        public string CARGO { get; set; }

    }

    public class UnidadeCartaoPontoDomainModel
    {
        public UnidadeCartaoPontoDomainModel()
        {
            Funcionarios = new HashSet<FuncionarioCartaoPontoDomainModel>();
        }

        public int UND_ID { get; set; }
        public string UNIDADE { get; set; }
        public DateTime? PERIODO { get; set; }
        public string CaminhoUnidade { get; set; }

        public virtual ICollection<FuncionarioCartaoPontoDomainModel> Funcionarios { get; set; }
    }

    //Adicionado por Angelo Matos em 27022020
    public class UnidadeCartaoPontoFaltasDomainModel
    {
        public UnidadeCartaoPontoFaltasDomainModel()
        {
            Funcionarios = new HashSet<FuncionarioCartaoPontoDomainModel>();
        }

        public int UND_ID { get; set; }
        public string UNIDADE { get; set; }
        public DateTime? PERIODO_INICIO { get; set; }
        public DateTime? PERIODO_FIM { get; set; }
        public string CaminhoUnidade { get; set; }

        public virtual ICollection<FuncionarioCartaoPontoDomainModel> Funcionarios { get; set; }
    }

}
