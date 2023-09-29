using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;

namespace CCM.Projects.SisGeapeWeb2.Repository.Repository.SASS
{
    public class ConsultaRepository : BaseRepository<sass_consulta>
    {
        public ConsultaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
