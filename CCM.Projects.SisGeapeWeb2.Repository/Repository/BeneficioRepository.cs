using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class BeneficioRepository : BaseRepository<ap_beneficio>
    {
        public BeneficioRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}