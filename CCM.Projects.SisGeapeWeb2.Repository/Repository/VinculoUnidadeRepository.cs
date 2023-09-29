using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class VinculoUnidadeRepository : BaseRepository<ap_vinculoxunidade>
    {
        public VinculoUnidadeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
