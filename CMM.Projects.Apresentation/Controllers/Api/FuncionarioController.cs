using CCM.Projects.SisGeapeWeb2.Business.Interface;
using System.Web.Http;

namespace CMM.Projects.Apresentation.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/funcionario")]
    public class FuncionarioController : ApiController
    {
        IFuncionarioBusiness _funcionarioBusiness;
        public FuncionarioController(IFuncionarioBusiness funcionarioBusiness)
        {
            _funcionarioBusiness = funcionarioBusiness;
        }
        [HttpGet]
        [ActionName("buscar")]
        [Route("todos")]
        public IHttpActionResult GetFuncionario()
        {
            var cargo = _funcionarioBusiness.GetAllFuncionario();
            if (cargo != null)
            {
                return Ok(cargo);
            }
            return NotFound();
        }

    }
}
