using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IBeneficioBusiness
    {
        int TotalRegistros();
        List<BeneficioDomainModel> GetAllBeneficio();
        List<BeneficioDomainModel> GetBeneficioRecords(int pageStart, int pageSize);
        List<BeneficioDomainModel> GetBeneficioByName(string name);
        BeneficioDomainModel GetBeneficioById(int Id);
        void AddUpdateBeneficio(BeneficioDomainModel _domainModel);
        bool DeleteBeneficio(BeneficioDomainModel _domainModel);
        bool Salvar();

    }
}
