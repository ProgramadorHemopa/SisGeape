using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository.SASS
{
    public class ProntuarioRepository : BaseRepository<sass_prontuario>
    {
        public ProntuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
