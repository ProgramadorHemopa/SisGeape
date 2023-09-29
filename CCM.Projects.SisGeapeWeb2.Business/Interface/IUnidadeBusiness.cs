using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IUnidadeBusiness
    {
        Task<List<UnidadeDomainModel>> GetAllAsync();
        List<UnidadeDomainModel> GetUnidadePageRecords(int pageStart, int pageSize);
        List<UnidadeDomainModel> GetUnidadeByName(string name);
        Task<UnidadeDomainModel> GetUnidadeyId(int Id);
        List<UnidadeDomainModel> ddlUnidade();

        bool ExistsUnidade(string name, int id);
        bool AddUpdateUnidade(UnidadeDomainModel _domainModel);
        bool DeleteUnidade(UnidadeDomainModel _domainModel);
        bool Salvar();
        int TotalUnidadeCount();

    }
}
