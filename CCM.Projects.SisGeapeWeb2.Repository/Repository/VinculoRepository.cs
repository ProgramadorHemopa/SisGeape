using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class VinculoRepository : BaseRepository<ap_vinculo>
    {
        public VinculoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
