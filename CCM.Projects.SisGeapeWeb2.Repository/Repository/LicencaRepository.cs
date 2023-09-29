using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class LicencaRepository : BaseRepository<ap_licenca>
    {
        public LicencaRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
