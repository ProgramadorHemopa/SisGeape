using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class VinculoTipoRepository : BaseRepository<ap_vinculotipo>
    {
        public VinculoTipoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
