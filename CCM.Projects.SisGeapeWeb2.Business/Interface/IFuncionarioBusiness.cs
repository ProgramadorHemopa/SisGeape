using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeape2.Domain.SASS;
using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IFuncionarioBusiness
    {

        int TotalFuncionario();
        int TotalFuncionarioPorParametros(DateTime? vinculoInicio, DateTime? vinculoFim, int? situacao, int? cargo);

        List<CountFuncionarioSituacaoDomainModel> TotalRegistroPorSituacao();


        List<FuncionarioDomainModel> GetAllFuncionario();
        List<FuncionarioIndexDomainModel> GetFuncionarioByNameOrMatricula(FuncionarioPesquisaDomainModel domainModel);
        List<FuncionarioDomainModel> GetAllFuncionarioPage(int pageStart, int pageSize);
        List<FuncionarioDomainModel> GetFuncionarioPage(int pageStart, int pageSize);
        List<FuncionarioIndexDomainModel> GetFuncionarioPageByParameters(string nome, string matricula, int? unidade, int? tipoVinculo, int? situacaoVinculo, int? cargo, int? beneficio, int? funcao, int pageStart, int pageSize);

        //Modificado por Angelo Matos em 20012020
        List<FuncionarioRelatorioDomainModel> GetFuncionarioPageByParametersRelatorio(string nome, string matricula, int?[] unidade, int?[] tipoVinculo, int?[] situacaoVinculo, int?[] cargo, int?[] beneficio, int pageStart, int pageSize, string sexo, int? idade, int?[] funcao, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? inicioDemissao, DateTime? fimDemissao);

        FuncionarioEndereco buscarCep(string cep);
        List<FuncionarioDomainModel> GetFuncionarioByPIS(string _pis);
        FuncionarioDomainModel GetFuncionarioById(int Id);
        FuncionarioDomainModel AddUpdateFuncionario(FuncionarioDomainModel modelDomain);
        bool EditarNomeCracha(FuncionarioNomeCrachaDomainModel modelDomain);
        bool AddUpdateSolicitacaoCracha(FuncionarioSolicitarCrachaDomainModel modelDomain);
        List<FuncionarioSolicitarCrachaDomainModel> LoadCrachaByParameters(int? fun_id, string matricula, string nome, string numero, int? cargo, int? unidade, int? tipo, int? situacao, DateTime? dataInicio, DateTime? dataFim, DateTime? inicioEmissao, DateTime? fimEmissao);
        List<FuncionarioCartaoPontoDomainModel> GetCartaoPontoByVinculo(int vinculo, DateTime dataInicio, DateTime dataFim);
        FuncionarioCartaoPontoDomainModel GetDadosCartaoPonto(int id);
        bool DeleteFuncionario(FuncionarioDomainModel modelDomain);



        //Cracha
        int TotalSolicitacaoCrachaPendente();
        FuncionarioSolicitarCrachaDomainModel GetSolicitacaoCrachaByID(int id);
        FuncionarioNomeCrachaDomainModel GetNomeCrachaByFuncionario(int id);
        List<FuncionarioEmitirCrachaDomainModel> GetDadosCracha(int id);



        //CartaoSaude - Area: SASS
        FuncionarioCartaoSaudeDomainModel buscarFuncionarioCartaoSaudePorId(int id);
        // List<FuncionarioIndexDomainModel> ddlFuncionarioAtendimentoSASS();
        List<FuncionarioIndexDomainModel> ddlFuncionarioAtendimentoSASS();
        //IEnumerable<FuncionarioSolicitarCrachaDomainModel> LoadCrachaByParameters(int? fun_id, string matricula, string nome, DateTime datafim, DateTime datainicio);

        bool DeleteSolicitacaoCracha(FuncionarioSolicitarCrachaDomainModel modelDomain);

        //Adicionado por Angelo Matos em 19082019:1206
        bool ExistFuncionario(string matricula, int id);

        //Adicionado por Angelo Matos em 25092019
        //Modificado por Angelo Matos em 20012020
        List<FuncionarioRelatorioFuncaoDomainModel> GetFuncionarioPageByParametersRelatorioFuncao(string nome, string matricula, int?[] unidade, int?[] situacaoVinculo, int?[] cargo, int pageStart, int pageSize, string sexo, int? idade, int?[] funcao, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? inicioDemissao, DateTime? fimDemissao, DateTime? inicioFuncao, DateTime? fimFuncao);
        List<FuncionarioRelatorioAposentadoriaDomainModel> GetFuncionarioPageByParametersRelatorioAposentadoria(string nome, string matricula, int?[] unidade, int?[] situacaoVinculo, int?[] cargo, int pageStart, int pageSize, string sexo, int? idade, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? entradaAposentadoriaInicio, DateTime? entradaAposentadoriaFim, int?[] tipoAposentadoria);
        //Modificado por Angelo Matos em 21012020
        List<FuncionarioRelatorioBeneficioDomainModel> GetFuncionarioPageByParametersRelatorioBeneficio(string nome, string matricula, int?[] unidade, int?[] situacaoVinculo, int?[] cargo, int?[] beneficio, int pageStart, int pageSize, string sexo, int? idade, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? inicioDemissao, DateTime? fimDemissao, DateTime? beneficioInicio, DateTime? beneficioFim);
        List<FuncionarioRelatorioCessaoDomainModel> GetFuncionarioPageByParametersRelatorioCessao(string nome, string matricula, int?[] unidade, int?[] cargo, int?[] beneficio, int pageStart, int pageSize, string sexo, int? idade, string onus, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? cessaoInicio, DateTime? cessaoFim);

        bool ValidaCracha(int Id);
        void GetCracha(int Id);
        bool Salvar();

    }
}
