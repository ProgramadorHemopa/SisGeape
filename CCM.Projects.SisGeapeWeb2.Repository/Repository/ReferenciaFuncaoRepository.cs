using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class ReferenciaFuncaoRepository : BaseRepository<ap_referenciafuncao>
    {
        public ReferenciaFuncaoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
