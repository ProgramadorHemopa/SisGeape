using CCM.Projects.SisGeape2.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface ICartaoPontoBusiness
    {
        int TotalRegistros();
        List<PesquisaCartaoPontoDomainModel> GetPesquisa(PesquisaCartaoPontoDomainModel pesquisa);
        FuncionarioCartaoPontoDomainModel GetPointCardByVnc(int funcionario, int vinculo, DateTime? dataInicio, DateTime? dataFim);
        List<CartaoPontoUnidadeDomainModel> GetPointCardByUnidade(UnidadeCartaoPontoDomainModel domainModel);
        Task<FuncionarioCartaoPontoDomainModel> BuscarCartaoPontoPorFuncionario(int id, DateTime? inicio, DateTime? fim);
        Task<List<UnidadeCartaoPontoDomainModel>> BuscarCartaoPontoPorUnidade(int und, DateTime? Periodo);
        Task<List<UnidadeCartaoPontoDomainModel>> BuscarCartaoPontoPorUnidadeFeriadoFimDeSemana(int und, DateTime? Periodo);

        //Adicionado por Angelo Matos em 27022020
        Task<List<UnidadeCartaoPontoFaltasDomainModel>> BuscarCartaoPontoPorUnidadeFaltas(int und, DateTime? PeriodoInicio, DateTime? PeriodoFim);

        List<string> ddlMes();
        List<string> ddlAno();



    }
}
