using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface ICidBusiness
    {
        int TotalRegistros();
        List<CidDomainModel> GetCid();
        CidDomainModel GetCidById(int id);
        bool AddUpdateCid(CidDomainModel domainModel);
        bool DeleteCid(CidDomainModel domainModel);
        bool Salvar();
    }
}
