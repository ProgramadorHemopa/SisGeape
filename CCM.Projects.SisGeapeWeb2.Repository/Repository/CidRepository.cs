using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class CidRepository : BaseRepository<ap_cid10>
    {
        public CidRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
