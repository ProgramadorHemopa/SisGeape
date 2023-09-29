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
    public class FuncaoVinculoController : Controller
    {
        private infraMessage msg = new infraMessage();

        IFuncaoVinculoBusiness _funcaoVinculoBusiness;
        IFuncaoBusiness _funcaoBusiness;

        IVinculoBusiness _vinculoBusiness;


        public FuncaoVinculoController(IFuncaoVinculoBusiness funcaoVinculoBusiness, IVinculoBusiness vinculoBusiness, IFuncaoBusiness funcaoBusiness)
        {
            _funcaoVinculoBusiness = funcaoVinculoBusiness;
            _vinculoBusiness = vinculoBusiness;
            _funcaoBusiness = funcaoBusiness;

        }
        // GET: FuncaoVinculo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ListFuncao(int? id)
        {
            ViewBag.FUN_ID = id.Value;
            //List<FuncaoVinculoDomainModel> listDomain = _funcaoVinculoBusiness.GetFuncaoVinculoByFuncionario(id.Value);
            //List<FuncaoVinculoModelView> modelView = new List<FuncaoVinculoModelView>();
            //Mapper.Map(listDomain, modelView);
            return PartialView();
        }


        public JsonResult _ListFuncaoFuncionario(ParametrosPaginacao paginacao, int? id)
        {

            var _list = _funcaoVinculoBusiness.GetFuncaoVinculoByFuncionario(id.Value);

            int tot = _list.Count();

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





        // GET: FuncaoxVinculo/Edit/5
        public ActionResult AddUpdateFNCVNC(int? id, int func = 0)
        {
            FuncaoVinculoModelView funcaoVinculo = new FuncaoVinculoModelView();
            ViewBag.FUN_ID = func;
            if (id == 0)
            {
                ViewBag.Title = "Nova Função";
                ViewBag.FNC_ID = new SelectList(_funcaoBusiness.GetFuncao(), "FNC_ID", "FNC_NOME");
                ViewBag.VNC_ID = new SelectList(_vinculoBusiness.ddlVinculoByFuncionario(func), "VNC_ID", "VNC_NOME");


            }
            else
            {
                ViewBag.Title = "Editar Função";
                FuncaoVinculoDomainModel _domainModel = _funcaoVinculoBusiness.GetFuncaoVinculoById(id.Value);
                if (_domainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(_domainModel, funcaoVinculo);
                ViewBag.FNC_ID = new SelectList(_funcaoBusiness.GetFuncao(), "FNC_ID", "FNC_NOME", funcaoVinculo.FNC_ID);
                ViewBag.VNC_ID = new SelectList(_vinculoBusiness.ddlVinculoByFuncionario(funcaoVinculo.FUN_ID), "VNC_ID", "VNC_NOME");

            }
            return PartialView(funcaoVinculo);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateFNCVNC(FuncaoVinculoModelView funcaoVinculo)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    FuncaoVinculoDomainModel _domainModel = new FuncaoVinculoDomainModel();


                    funcaoVinculo.FNCVNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(funcaoVinculo, _domainModel);
                    _funcaoVinculoBusiness.AddUpdateFuncaoVinculo(_domainModel);


                    if (_funcaoVinculoBusiness.Salvar())
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
            FuncaoVinculoDomainModel _domainModel = _funcaoVinculoBusiness.GetFuncaoVinculoById(id.Value);
            FuncaoVinculoModelView funcaoVinculo = new FuncaoVinculoModelView();

            if (_domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(_domainModel, funcaoVinculo);
            return PartialView(funcaoVinculo);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var _funcaoxvinculo = _funcaoVinculoBusiness.GetFuncaoVinculoById(id);
                if (_funcaoxvinculo != null)
                {
                    _funcaoxvinculo.FNCVNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;


                    _funcaoVinculoBusiness.DeleteFuncaoVinculo(_funcaoxvinculo);

                    if (_funcaoVinculoBusiness.Salvar())
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
