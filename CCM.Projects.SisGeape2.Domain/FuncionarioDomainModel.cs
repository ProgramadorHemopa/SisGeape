using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeape2.Domain
{
    public class FuncionarioDomainModel
    {
        public int FUN_ID { get; set; }
        public string FUN_MATRICULA { get; set; }
        public string FUN_NOME { get; set; }
        public string FUN_NOMECRACHA { get; set; }
        public string FUN_SEXO { get; set; }
        public int? BNC_ID { get; set; }
        public string FUN_AGENCIA { get; set; }
        public string FUN_CONTACORRENTE { get; set; }
        public int? FUN_ESTADOCIVIL { get; set; }
        public string FUN_CONJUGE { get; set; }
        public string FUN_PAI { get; set; }
        public string FUN_MAE { get; set; }
        public int? FUN_TIPOSANGUINEO { get; set; }
        public string FUN_LOGRADOURO { get; set; }
        public string FUN_ENDERECO { get; set; }
        public string FUN_NUMEROEND { get; set; }
        public string FUN_BAIRRO { get; set; }
        public string FUN_COMPLEMENTO { get; set; }
        public string FUN_CEP { get; set; }
        public string FUN_CIDADE { get; set; }
        public string FUN_CIDADENATAL { get; set; }
        public DateTime? FUN_DATANASCIMENTO { get; set; }
        public string FUN_NACIONALIDADE { get; set; }
        public string FUN_RG { get; set; }
        public string FUN_RGEMISSOR { get; set; }
        public string FUN_RGESTADO { get; set; }
        public DateTime? FUN_RGDATAEMISSAO { get; set; }
        public string FUN_CPF { get; set; }
        public string FUN_PIS { get; set; }
        public string FUN_CNS { get; set; }
        public string FUN_TITULOELEITOR { get; set; }
        public string FUN_TITULOZONA { get; set; }
        public string FUN_TITULOSESSAO { get; set; }
        public DateTime? FUN_TITULODATAEMISSAO { get; set; }
        public string FUN_CERTIFICADOMILITAR { get; set; }
        public string FUN_CMSERIE { get; set; }
        public string FUN_CMCORPORACAO { get; set; }
        public string FUN_CTPS { get; set; }
        public DateTime? FUN_CTPSDATAEMISSAO { get; set; }
        public DateTime? FUN_CNHVALIDADE { get; set; }
        public string FUN_CNH { get; set; }
        public string FUN_CNHCATEGORIA { get; set; }
        public string FUN_EMAIL { get; set; }
        public string FUN_TELEFONE { get; set; }
        public string FUN_CELULAR { get; set; }
        public int? FUN_ESCOLARIDADE { get; set; }
        public string FUN_IDIOMA { get; set; }
        public int? FUN_NECESSIDADE_ESPECIAL { get; set; }
        public string FUN_ASSISTENCIAMEDICA { get; set; }
        public int? FUN_IMOVEL { get; set; }
        public string FUN_OBSERVACAO { get; set; }
        public int? FUN_REGUSER { get; set; }
        public byte[] FUN_FOTO { get; set; }
        public virtual ICollection<VinculoDomainModel> Vinculos { get; set; }
    }
    public class FuncionarioEndereco
    {
        public string FUN_CEP { get; set; }
        public string FUN_BAIRRO { get; set; }
        public string FUN_MUNICIPIO { get; set; }
        public string FUN_ENDERECO { get; set; }
    }
    public class FuncionarioSolicitarCrachaDomainModel
    {
        public int FUNCRC_ID { get; set; }
        public Nullable<int> VIA { get; set; }
        public Nullable<int> VNC_ID { get; set; }
        public string FUNCRC_NUMERO { get; set; }
        public Nullable<System.DateTime> FUNCRC_DATAEMISSAO { get; set; }
        public Nullable<int> FUNCRC_TIPO { get; set; }
        public Nullable<System.DateTime> FUNCRC_DATASOLICITACAO { get; set; }
        public Nullable<int> FUNCRC_SITUACAO { get; set; }
        public string FUNCRC_OBSERVACAO { get; set; }
        public Nullable<int> FUNCRC_REGUSER { get; set; }
        public string VNC_NOME { get; set; }
        public Nullable<int> FUN_ID { get; set; }
        public string FUN_NOME { get; set; }
        public string MATRICULA { get; set; }
        public byte[] FUN_FOTO { get; set; }
    }
    public class FuncionarioNomeCrachaDomainModel
    {
        public int FUN_ID { get; set; }
        public string FUN_NOMECRACHA { get; set; }
        public Nullable<int> FUN_REGUSER { get; set; }
    }
    public class CountFuncionarioSituacaoDomainModel
    {
        public int FUN_ID { get; set; }
        public string SITUACAO { get; set; }
        public int QUANTIDADE { get; set; }
    }
    public class FuncionarioEmitirCrachaDomainModel
    {
        public string CRG_NOME { get; set; }
        public int FUN_TIPAGEM { get; set; }
        public string FUN_TIPOSANGUINEO { get; set; }
        public string FUN_MATRICULA { get; set; }
        public string FUN_NOME { get; set; }
        public byte[] FUN_CODIGOBARRA { get; set; }
        public byte[] FUNFT_ARQUIVO { get; set; }
        public string GetTipagem
        {
            get
            {
                switch (FUN_TIPAGEM)
                {
                    case 1:
                        return "A-";
                    case 2:
                        return "A+";
                    case 3:
                        return "B-";
                    case 4:
                        return "B+";
                    case 5:
                        return "AB-";
                    case 6:
                        return "AB+";
                    case 7:
                        return "O-";
                    case 8:
                        return "O+";
                    default:
                        return "";
                }
            }
        }
    }
    public class FuncionarioPesquisaDomainModel
    {
        public int FUN_ID { get; set; }
        public string MATRICULA { get; set; }
        public string NOME { get; set; }
        public int? CARGO { get; set; }
        public int? UNIDADE { get; set; }
        public int? TIPO { get; set; }
        public int? SITUACAO { get; set; }
    }

    public class FuncionarioIndexDomainModel
    {
        public int FUN_ID { get; set; }
        public string FUN_MATRICULA { get; set; }
        public string FUN_NOME { get; set; }
        public int SITUACAO { get; set; }
        public bool Ativo { get; set; }
        public byte[] FUN_FOTO { get; set; }
    }

    public class FuncionarioRelatorioDomainModel
    {
        public int? FUN_ID { get; set; }
        public string FUN_MATRICULA { get; set; }
        public string FUN_NOME { get; set; }
        public string CARGO { get; set; }
        public int SITUACAO { get; set; }
        public bool Ativo { get; set; }
        public string FUN_UNIDADE { get; set; }
        public DateTime? FUN_DATA_ADMISSAO { get; set; }
        public DateTime? FUN_DATA_DEMISSAO { get; set; }
    }

    //Adicionado por Angelo Matos em 25092019
    public class FuncionarioRelatorioFuncaoDomainModel : FuncionarioRelatorioDomainModel
    {
        public string FUNCAO { get; set; }
        public DateTime? FUNCAO_DATA_INICIO { get; set; }
        public DateTime? FUNCAO_DATA_FIM { get; set; }
    }

    //Adicionado por Angelo Matos em 25092019
    public class FuncionarioRelatorioAposentadoriaDomainModel : FuncionarioRelatorioDomainModel
    {
        public string SITUACAO_NOME { get; set; }
        public string TIPO_APOSENTADORIA { get; set; }
        public DateTime? ENTRADA_APOSENTADORIA { get; set; }
    }

    //Adicionado por Angelo Matos em 25092019
    public class FuncionarioRelatorioBeneficioDomainModel : FuncionarioRelatorioDomainModel
    {
        public string BENEFICIO_NOME { get; set; }
        public DateTime? BENEFICIO_DATA_INICIO { get; set; }
        public DateTime? BENEFICIO_DATA_FIM { get; set; }
    }

    //Adicionado por Angelo Matos em 25092019
    public class FuncionarioRelatorioCessaoDomainModel : FuncionarioRelatorioDomainModel
    {
        public string ORGAO_DESTINO { get; set; }
        public string ONUS { get; set; }
        public DateTime? DATA_CESSAO { get; set; }
    }
}
