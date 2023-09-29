using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class FuncionarioCrachaRepository : BaseRepository<ap_funcionariocracha>
    {
        public FuncionarioCrachaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
