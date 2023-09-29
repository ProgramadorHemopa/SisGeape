using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository.SASS
{
    public class AvaliacaoClinicaRepository : BaseRepository<sass_avaliacaoclinica>
    {
        public AvaliacaoClinicaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
