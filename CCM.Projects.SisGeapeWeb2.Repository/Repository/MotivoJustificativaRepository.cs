using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class MotivoJustificativaRepository : BaseRepository<pon_motivojustificativa>
    {
        public MotivoJustificativaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
