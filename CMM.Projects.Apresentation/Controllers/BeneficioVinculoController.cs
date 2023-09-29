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
    public class BeneficioVinculoController : Controller
    {
        private infraMessage msg = new infraMessage();

        IBeneficioVinculoBusiness _beneficioVinculoBusiness;
        IBeneficioBusiness _beneficioBusiness;

        IVinculoBusiness _vinculoBusiness;


        public BeneficioVinculoController(IBeneficioVinculoBusiness beneficioVinculoBusiness, IVinculoBusiness vinculoBusiness, IBeneficioBusiness beneficioBusiness)
        {
            _beneficioVinculoBusiness = beneficioVinculoBusiness;
            _vinculoBusiness = vinculoBusiness;
            _beneficioBusiness = beneficioBusiness;

        }
        // GET: BeneficioVinculo
        public ActionResult Index()
        {
            return View();
        }

        // GET: BeneficioxVinculo
        public ActionResult _ListBeneficio(int? id)
        {
            return PartialView();
        }

        public JsonResult _ListFuncaoFuncionario(ParametrosPaginacao paginacao, int? id)
        {

            var _list = _beneficioVinculoBusiness.GetFuncaoVinculoByFuncionario(id.Value);

            int tot = _list.Count();

            int totFiltrado = _list.Count();

            var dadosPaginados = _list.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length);

            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: BeneficioxVinculo/Edit/5
        public ActionResult AddUpdateBNFVNC(int? id, int func = 0)
        {
            BeneficioVinculoModelView beneficioVinculo = new BeneficioVinculoModelView();
            ViewBag.FUN_ID = func;
            if (id == 0)
            {
                ViewBag.Title = "Novo Benefício";
                ViewBag.BNF_ID = new SelectList(_beneficioBusiness.GetAllBeneficio(), "BNF_ID", "BNF_DESCRICAO");
                ViewBag.VNC_ID = new SelectList(_vinculoBusiness.ddlVinculoByFuncionario(func), "VNC_ID", "VNC_NOME");


            }
            else
            {
                ViewBag.Title = "Editar Função";
                BeneficioVinculoDomainModel domainModel = _beneficioVinculoBusiness.GetBeneficioVinculoById(id.Value);
                if (domainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(domainModel, beneficioVinculo);
                ViewBag.BNF_ID = new SelectList(_beneficioBusiness.GetAllBeneficio(), "BNF_ID", "BNF_DESCRICAO", beneficioVinculo.BNF_ID);
                ViewBag.VNC_ID = new SelectList(_vinculoBusiness.ddlVinculoByFuncionario(beneficioVinculo.FUN_ID), "VNC_ID", "VNC_NOME", beneficioVinculo.VNC_ID);
            }

            return PartialView(beneficioVinculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateBNFVNC(BeneficioVinculoModelView beneficioVinculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BeneficioVinculoDomainModel _domainModel = new BeneficioVinculoDomainModel();

                    beneficioVinculo.BNFVNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(beneficioVinculo, _domainModel);
                    _beneficioVinculoBusiness.AddUpdateBeneficioVinculo(_domainModel);

                    if (_beneficioVinculoBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = false, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);

                }
                else
                    throw new Exception();


            }
            catch (Exception ex)
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + " <br/>";
                }

                if (mensg.Length == 0)
                {
                    mensg = ex.Message;
                }
                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficioVinculoDomainModel _domainModel = _beneficioVinculoBusiness.GetBeneficioVinculoById(id.Value);
            BeneficioVinculoModelView beneficioVinculo = new BeneficioVinculoModelView();

            if (_domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(_domainModel, beneficioVinculo);
            return PartialView(beneficioVinculo);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var beneficioVinculo = _beneficioVinculoBusiness.GetBeneficioVinculoById(id);
                if (beneficioVinculo != null)
                {
                    beneficioVinculo.BNFVNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;


                    _beneficioVinculoBusiness.DeleteBeneficioVinculo(beneficioVinculo);

                    if (_beneficioVinculoBusiness.Salvar())
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
            catch (Exception ex)
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + " <br/>";
                }

                if (mensg.Length == 0)
                {
                    mensg = ex.Message;
                }
                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }
        }

    }
}
