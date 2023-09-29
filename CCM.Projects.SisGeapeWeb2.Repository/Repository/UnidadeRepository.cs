using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class UnidadeRepository : BaseRepository<ap_unidade>
    {
        public UnidadeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
