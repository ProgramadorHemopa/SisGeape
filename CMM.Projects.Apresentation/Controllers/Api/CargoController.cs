using CCM.Projects.SisGeapeWeb2.Business.Interface;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMM.Projects.Apresentation.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/cargo")]
    public class CargoController : ApiController
    {
        ICargoBusiness cargoBusiness;

        public CargoController(ICargoBusiness _cargoBusiness)
        {
            cargoBusiness = _cargoBusiness;
        }

        [HttpGet]
        public HttpResponseMessage getCargo()
        {
            var cargo = cargoBusiness.GetCargo();
            if (cargo != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, cargo);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
