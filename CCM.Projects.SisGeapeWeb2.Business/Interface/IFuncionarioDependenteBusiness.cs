using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IFuncionarioDependenteBusiness
    {
        List<FuncionarioDependenteDomainModel> GetAll();
        List<FuncionarioDependenteDomainModel> GetPageRecords(int pageStart, int pageSize);
        List<FuncionarioDependenteDomainModel> GetDependenteByFuncionario(int func);
        FuncionarioDependenteDomainModel GetDependentebyId(int Id);
        bool AddUpdateDependente(FuncionarioDependenteDomainModel _domainModel);
        bool DeleteDependente(FuncionarioDependenteDomainModel _domainModel);
        bool Salvar();
        int TotalCount();
        int TotalCountByFuncionario(int id);

    }
}
