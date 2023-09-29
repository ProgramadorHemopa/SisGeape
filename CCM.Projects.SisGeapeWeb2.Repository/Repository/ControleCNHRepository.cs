using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class ControleCNHRepository : BaseRepository<ap_controlecnh>
    {
        public ControleCNHRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public class ControleCNHAuxiliarRepository : BaseRepository<ap_controlecnh_aux>
    {
        public ControleCNHAuxiliarRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
