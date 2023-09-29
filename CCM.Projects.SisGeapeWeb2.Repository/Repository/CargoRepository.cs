using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class CargoRepository : BaseRepository<ap_cargo>
    {
        public CargoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
