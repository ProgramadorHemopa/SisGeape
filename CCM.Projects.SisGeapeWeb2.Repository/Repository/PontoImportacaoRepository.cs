using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class PontoImportacaoRepository : BaseRepository<pon_pontoimportacao>
    {
        public PontoImportacaoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
