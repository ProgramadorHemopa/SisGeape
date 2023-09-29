using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class FeriadoRepository : BaseRepository<ap_feriado>
    {
        public FeriadoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
