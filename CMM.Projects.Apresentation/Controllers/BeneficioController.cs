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
    public class BeneficioController : Controller
    {

        private infraMessage msg = new infraMessage();


        IBeneficioBusiness beneficioBusiness;

        public BeneficioController(IBeneficioBusiness _beneficioBusiness)
        {
            beneficioBusiness = _beneficioBusiness;
        }
        // GET: Beneficio
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarBeneficio(ParametrosPaginacao paginacao)
        {


            var listDomain = beneficioBusiness.GetAllBeneficio();

            int tot = listDomain.Count();


            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                listDomain = listDomain.Where("BNF_DESCRICAO.ToUpper().Contains(@0)", paginacao.SearchValue.ToUpper()).ToList();
            }

            int totFiltrado = listDomain.Count();


            var dadosPaginados = listDomain.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();


            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }


        // GET: Beneficio/Create
        public ActionResult AddUpdateBeneficio(int id = 0)
        {
            BeneficioModelView _beneficio = new BeneficioModelView();

            if (id == 0)
            {
                ViewBag.Title = "Novo Benefício";

                return PartialView(_beneficio);
            }
            else
            {
                ViewBag.Title = "Editar Benefício";
                BeneficioDomainModel beneficioDomainModel = beneficioBusiness.GetBeneficioById(id);
                if (beneficioDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(beneficioDomainModel, _beneficio);


                return PartialView(_beneficio);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateBeneficio(BeneficioModelView _beneficio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BeneficioDomainModel beneficioDomainModel = new BeneficioDomainModel();
                    _beneficio.BNF_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_beneficio, beneficioDomainModel);
                    beneficioBusiness.AddUpdateBeneficio(beneficioDomainModel);


                    if (beneficioBusiness.Salvar())
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


        // GET: Beneficio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficioModelView _beneficio = new BeneficioModelView();
            BeneficioDomainModel beneficioDomainModel = beneficioBusiness.GetBeneficioById(id.Value);
            if (beneficioDomainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(beneficioDomainModel, _beneficio);

            return PartialView(_beneficio);
        }
        // POST: Banco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                BeneficioDomainModel beneficioDomainModel = beneficioBusiness.GetBeneficioById(id);
                if (beneficioDomainModel != null)
                {
                    beneficioDomainModel.BNF_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    beneficioBusiness.DeleteBeneficio(beneficioDomainModel);

                    if (beneficioBusiness.Salvar())
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
