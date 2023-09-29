using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IBeneficioVinculoBusiness
    {
        int TotalRegistro();
        int TotalRegistroByFuncionario(int id);

        List<BeneficioVinculoDomainModel> GetFuncaoVinculoByFuncionario(int func);
        List<BeneficioVinculoDomainModel> GetFuncaoVinculoByVinculo(int vnc);
        BeneficioVinculoDomainModel GetBeneficioVinculoById(int id);
        List<CountBeneficioVinculoDomainModel> GetcountByBeneficio();
        bool AddUpdateBeneficioVinculo(BeneficioVinculoDomainModel _domainModel);
        bool DeleteBeneficioVinculo(BeneficioVinculoDomainModel _domainModel);
        bool Salvar();
    }
}
