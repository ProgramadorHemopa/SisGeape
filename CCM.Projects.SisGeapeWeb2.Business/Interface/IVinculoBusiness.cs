using CCM.Projects.SisGeape2.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IVinculoBusiness
    {

        List<VinculoDomainModel> BuscarTodosAtivos();
        List<VinculoDomainModel> BuscarTodos();

        Task<List<VinculoDomainModel>> GetAllAsyncVinculoByUnidade(int id);

        List<VinculoDomainModel> ddlVinculoByFuncionario(int id);
        List<VinculoDomainModel> GetPageRecords(int pageStart, int pageSize);
        List<VinculoDomainModel> GetVinculoByFuncionario(int func);
        VinculoDomainModel GetVinculobyId(int Id);
        Task<List<VinculoDomainModel>> GetVinculobyDate(DateTime? inicio, DateTime? fim);
        VinculoDomainModel AddUpdateVinculo(VinculoDomainModel _domainModel);
        bool DeleteVinculo(VinculoDomainModel _domainModel);
        bool Salvar();
        int TotalCount();
        bool ValidarDelete(int vinculo);



        // Situacao Vinculo

        List<VinculoSituacaoDomainModel> GetAllVinculoSituacao();
        VinculoSituacaoDomainModel GetVinculoSituacaobyId(int Id);
        bool AddUpdateVinculoSituacao(VinculoSituacaoDomainModel _domainModel);
        bool DeleteVinculoSituacao(VinculoSituacaoDomainModel _domainModel);
        int TotalCountVinculoSituacao();


        //Tipo Vinculo

        List<VinculoTipoDomainModel> GetAllTipoVinculo();
        VinculoTipoDomainModel GetVinculoTipobyId(int Id);
        bool AddUpdateVinculoTipo(VinculoTipoDomainModel _domainModel);
        bool DeleteVinculoTipo(VinculoTipoDomainModel _domainModel);
        int TotalCountVinculoTipo();
    }
}
