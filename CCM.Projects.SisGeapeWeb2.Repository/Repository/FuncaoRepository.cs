using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class FuncaoRepository : BaseRepository<ap_funcao>
    {
        public FuncaoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
