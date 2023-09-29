using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class FuncionarioRepository : BaseRepository<ap_funcionario>
    {
        public FuncionarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
