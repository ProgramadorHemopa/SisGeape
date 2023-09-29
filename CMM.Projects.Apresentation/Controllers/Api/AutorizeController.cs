using CCM.Projects.SisGeapeWeb2.Business.Interface;
using System;
using System.Web.Http;

namespace CMM.Projects.Apresentation.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/UserAutorize")]
    public class AutorizeController : ApiController
    {
        IFuncionarioBusiness _funcionarioBusiness;
        ICargoBusiness _cargoBusiness;
        IVinculoBusiness _vinculoBusiness;
        public AutorizeController(IFuncionarioBusiness funcionarioBusiness, ICargoBusiness cargoBusiness, IVinculoBusiness vinculoBusiness)
        {
            _funcionarioBusiness = funcionarioBusiness;
            _cargoBusiness = cargoBusiness;
            _vinculoBusiness = vinculoBusiness;
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AutorizarSistema(int? id_usuario, Sistemas? sistema)
        {
            try
            {
                if (!id_usuario.HasValue) { throw new Exception("A identificação do usuário não pode ser nula!"); }
                if (!sistema.HasValue) { throw new Exception("O tipo de sistema não pode ser nulo!"); }




                //TO DO 
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AutorizarSistema(string matricula, Sistemas? sistema)
        {
            try
            {
                if (string.IsNullOrEmpty(matricula)) { throw new Exception("A matricula do usuário não pode ser nula!"); }
                if (!sistema.HasValue) { throw new Exception("O tipo de sistema não pode ser nulo!"); }



                //TO DO 
                return Ok("");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

    public enum Sistemas
    {
        Sisind = 1,
        DocWeb = 2,
        SisRNC = 3
    }
}
