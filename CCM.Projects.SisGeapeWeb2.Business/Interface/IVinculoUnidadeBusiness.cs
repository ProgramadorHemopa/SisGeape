using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IVinculoUnidadeBusiness : IBusiness
    {
        int TotalRegistro();
        int TotalRegistroByFuncionario(int id);

        List<VinculoUnidadeDomainModel> GetLotacaoByFuncionario(int func);
        List<VinculoUnidadeDomainModel> GetLotacaoByUnidade(int und);

        VinculoUnidadeDomainModel GetLotacaoById(int id);
        bool AddUpdateLotacao(VinculoUnidadeDomainModel _domainModel);
        bool DeleteLotacao(VinculoUnidadeDomainModel _domainModel);
        bool Salvar();

    }
}
