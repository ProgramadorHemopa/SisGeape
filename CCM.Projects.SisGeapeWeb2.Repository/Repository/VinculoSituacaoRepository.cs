using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class VinculoSituacaoRepository : BaseRepository<ap_vinculosituacao>
    {
        public VinculoSituacaoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
