using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;


namespace CCM.Projects.SisGeapeWeb2.Repository.Repository
{
    public class BancoRepository : BaseRepository<ap_banco>
    {
        public BancoRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}
