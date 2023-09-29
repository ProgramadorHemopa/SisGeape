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
    public class FuncionarioDependenteController : Controller
    {
        private infraMessage msg = new infraMessage();


        IFuncionarioDependenteBusiness dependenteBusiness;

        public FuncionarioDependenteController(IFuncionarioDependenteBusiness _dependenteBusiness)
        {
            dependenteBusiness = _dependenteBusiness;
        }

        public ActionResult ListDependente(int id)
        {
            if (id != 0)
            {
                ViewBag.FUN_ID = id;
            }

            return PartialView();
        }

        public JsonResult GetDependente(ParametrosPaginacao paginacao, int? id)
        {
            var _list = dependenteBusiness.GetDependenteByFuncionario(id.Value);

            int tot = _list.Count();
            ViewBag.FUN_ID = id;

            int totFiltrado = _list.Count();

            var dadosPaginados = _list.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();

            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUpdateDependente(int id = 0, int func = 0)
        {
            FuncionarioDependenteModelView _dependente = new FuncionarioDependenteModelView();

            if (id == 0)
            {
                ViewBag.Title = "Novo Dependente";
                _dependente.FUN_ID = func;


                ViewBag.FUN_TIPO = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "Pai", Value="1" },
                new SelectListItem {Text = "Mãe", Value="2" },
                new SelectListItem {Text = "Filho(a)", Value="3" },
                new SelectListItem {Text = "Avô(ó)", Value="4" },
                new SelectListItem {Text = "Enteado(a)", Value="5" }
            }, "Value", "Text");
                return PartialView(_dependente);

            }
            else
            {
                ViewBag.Title = "Editar Dependente";
                FuncionarioDependenteDomainModel _domainModel = dependenteBusiness.GetDependentebyId(id);
                if (_domainModel == null)
                {
                    return HttpNotFound();
                }

                Mapper.Map(_domainModel, _dependente);

                ViewBag.FUN_TIPO = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "Pai", Value="1" },
                new SelectListItem {Text = "Mãe", Value="2" },
                new SelectListItem {Text = "Filho(a)", Value="3" },
                new SelectListItem {Text = "Avô(ó)", Value="4" },
                new SelectListItem {Text = "Enteado(a)", Value="5" }
            }, "Value", "Text", _dependente.FUNDEP_TIPO);

                return PartialView(_dependente);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateDependente(FuncionarioDependenteModelView _dependente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dependente.FUNDEP_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    FuncionarioDependenteDomainModel _domainModel = new FuncionarioDependenteDomainModel();

                    Mapper.Map(_dependente, _domainModel);
                    dependenteBusiness.AddUpdateDependente(_domainModel);


                    if (dependenteBusiness.Salvar())
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
        // GET: VinculoSituacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionarioDependenteModelView _dependente = new FuncionarioDependenteModelView();
            FuncionarioDependenteDomainModel _domainModel = dependenteBusiness.GetDependentebyId(id.Value);
            if (_domainModel == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(_domainModel, _dependente);
            return PartialView(_dependente);
        }

        // POST: VinculoSituacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var _dependente = dependenteBusiness.GetDependentebyId(id);
                if (_dependente != null)
                {
                    _dependente.FUNDEP_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID; ;

                    dependenteBusiness.DeleteDependente(_dependente);

                    if (dependenteBusiness.Salvar())
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
