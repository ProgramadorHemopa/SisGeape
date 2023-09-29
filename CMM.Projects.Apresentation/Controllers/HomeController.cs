using CCM.Projects.SisGeapeWeb2.Business.Interface;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class HomeController : Controller
    {
        IFuncionarioBusiness funcionarioBusiness;
        IBeneficioVinculoBusiness beneficioVinculoBusiness;
        IFeriasBusiness feriasBusiness;
        public HomeController(IFuncionarioBusiness _funcionarioBusiness, IBeneficioVinculoBusiness _beneficioVinculoBusiness, IFeriasBusiness _feriasBusiness)
        {
            funcionarioBusiness = _funcionarioBusiness;
            beneficioVinculoBusiness = _beneficioVinculoBusiness;
            feriasBusiness = _feriasBusiness;
        }

        public ActionResult Index()
        {
            LoadSaudacao();
            ViewBag.TotalSolicitacaoCracha = funcionarioBusiness.TotalSolicitacaoCrachaPendente();
            ViewBag.TotalServidorFerias = feriasBusiness.TotalServidoresFerias();
            ViewBag.DataBeneficio = JsonConvert.SerializeObject(beneficioVinculoBusiness.GetcountByBeneficio(), Formatting.Indented);    //ViewBag.ListBeneficio = beneficioVinculoBusiness.
            return View();
        }


        public ActionResult InformativosTop()
        {
            ViewBag.Count = 55;
            return PartialView("InformativosTop");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [NonAction]
        public void LoadSaudacao()
        {
            string _saud;
            DateTime _time = DateTime.Now.ToLocalTime();
            if (_time.Hour >= 0 && _time.Hour <= 12)
            {
                _saud = "Bom dia";
            }
            else if (_time.Hour >= 13 && _time.Hour <= 18)
            {
                _saud = "Boa tarde";

            }
            else
            {
                _saud = "Boa noite";

            }
            _saud += ", " + HttpContext.User.Identity.Name.ToString().ToUpper() + ". Seja Bem-vindo(a) - " + _time.ToShortDateString() + ", " + _time.ToString("ddddd", new System.Globalization.CultureInfo("pt-br"));

            ViewBag.Saudacao = _saud;




        }
    }
}