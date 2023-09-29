using CCM.Projects.SisGeapeWeb2.Business.Interface;
using System.Threading.Tasks;
using System.Web.Http;

namespace CMM.Projects.Apresentation.Controllers.Api
{

    [RoutePrefix("api/unidade")]
    public class UnidadeController : ApiController
    {
        IUnidadeBusiness unidadeBusiness;
        public UnidadeController(IUnidadeBusiness _unidadeBusiness)
        {
            unidadeBusiness = _unidadeBusiness;
        }


        [HttpGet]
        [Route("todos")]
        public async Task<IHttpActionResult> BuscarUnidades()
        {
            var und = await unidadeBusiness.GetAllAsync();
            if (und != null)
            {
                return Ok(und);
            }
            return NotFound();
        }

        [HttpGet, Route("{id:int}")]
        public async Task<IHttpActionResult> BuscarUnidades(int id)
        {
            var und = await unidadeBusiness.GetUnidadeyId(id);
            if (und != null)
            {
                return Ok(und);
            }
            return NotFound();
        }


    }
}
