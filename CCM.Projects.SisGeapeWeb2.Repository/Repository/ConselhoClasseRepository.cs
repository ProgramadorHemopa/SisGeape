using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class ConselhoClasseRepository : BaseRepository<ap_conselhoclasse>
    {
        public ConselhoClasseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
