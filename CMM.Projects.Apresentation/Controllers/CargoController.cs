using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using SisGeape2.Apresentation.InfraPaginacao;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class CargoController : Controller
    {
        private infraMessage msg = new infraMessage();


        ICargoBusiness cargoBusiness;

        public CargoController(ICargoBusiness _cargoBusiness)
        {
            cargoBusiness = _cargoBusiness;
        }

        // GET: Cargo
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarCargo(ParametrosPaginacao paginacao)
        {

            List<CargoDomainModel> listDomain = new List<CargoDomainModel>();

            var cargo = cargoBusiness.GetCargo();

            int tot = cargo.Count();


            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                cargo = cargo.Where(x => x.CRG_CODIGO.ToUpper().Contains(paginacao.SearchValue.ToUpper()) || x.CRG_NOME.ToUpper().Contains(paginacao.SearchValue.ToUpper())).ToList();
            }

            int totFiltrado = cargo.Count();


            var dadosPaginados = cargo.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();


            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUpdateCargo(int id = 0)
        {
            ViewBag.ESCOLARIDADE = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "Fundamental", Value="1" },
                new SelectListItem {Text = "Médio", Value="2" },
                new SelectListItem {Text = "Superior", Value="3" },
            }, "Value", "Text");
            CargoModelView _cargo = new CargoModelView();

            if (id == 0)
            {
                ViewBag.Title = "Novo Cargo";

                return PartialView(new CargoModelView());
            }
            else
            {
                ViewBag.Title = "Editar Cargo";
                CargoDomainModel cargoDomainModel = cargoBusiness.GetCargoById(id);
                ((SelectList)ViewBag.ESCOLARIDADE).Select(x => x.Value == _cargo.CRG_ESCOLARIDADE.ToString());
                if (cargoDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(cargoDomainModel, _cargo);


                return PartialView(_cargo);

            }
        }

        // POST: Cargo/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateCargo(CargoModelView _cargo)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    CargoDomainModel cargoDomainModel = new CargoDomainModel();
                    _cargo.CRG_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_cargo, cargoDomainModel);
                    cargoBusiness.AddUpdateCargo(cargoDomainModel);

                    if (cargoBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = false, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);


                }
                else
                    throw new Exception();
            }
            catch
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + " <br/>";
                }
                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }
        }
        // GET: Cargo/Details/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoModelView _cargo = new CargoModelView();
            CargoDomainModel cargoDomainModel = cargoBusiness.GetCargoById(id.Value);
            if (cargoDomainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(cargoDomainModel, _cargo);

            return PartialView(_cargo);
        }

        // GET: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                CargoDomainModel cargoDomainModel = cargoBusiness.GetCargoById(id);
                if (cargoDomainModel != null)
                {
                    cargoDomainModel.CRG_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    cargoBusiness.DeleteBanco(cargoDomainModel);

                    if (cargoBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(new
                    {
                        resultado = false,
                        tipomsg = "",
                        msg = msg.Error444()
                    });



            }
            catch
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + " <br/>";
                }
                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }
        }

    }
}
