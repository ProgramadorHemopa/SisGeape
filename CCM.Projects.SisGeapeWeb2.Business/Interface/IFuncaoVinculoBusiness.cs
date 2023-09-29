using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IFuncaoVinculoBusiness
    {
        int TotalRegistro();
        int TotalRegistroByFuncionario(int id);

        List<FuncaoVinculoDomainModel> GetFuncaoVinculoByFuncionario(int func);
        List<FuncaoVinculoDomainModel> GetFuncaoVinculoByVinculo(int vnc);

        FuncaoVinculoDomainModel GetFuncaoVinculoById(int id);
        bool AddUpdateFuncaoVinculo(FuncaoVinculoDomainModel _domainModel);
        bool DeleteFuncaoVinculo(FuncaoVinculoDomainModel _domainModel);
        bool Salvar();

    }
}
