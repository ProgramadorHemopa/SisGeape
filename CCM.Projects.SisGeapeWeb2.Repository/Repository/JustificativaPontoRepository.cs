using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class JustificativaPontoRepository : BaseRepository<pon_justificativaponto>
    {
        public JustificativaPontoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
