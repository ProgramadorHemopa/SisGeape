using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class ConselhoRepository : BaseRepository<ap_conselho>
    {
        public ConselhoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
