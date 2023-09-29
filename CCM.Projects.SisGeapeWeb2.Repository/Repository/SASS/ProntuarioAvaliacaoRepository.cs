using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository.SASS
{
    public class ProntuarioAvaliacaoRepository : BaseRepository<sass_prontuarioxavaliacao>
    {
        public ProntuarioAvaliacaoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
