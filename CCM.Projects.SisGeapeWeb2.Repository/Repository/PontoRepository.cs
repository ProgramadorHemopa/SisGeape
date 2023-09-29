using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class PontoRepository : BaseRepository<pon_ponto>
    {
        public PontoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
