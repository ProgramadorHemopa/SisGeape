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
    public class FuncaoController : Controller
    {
        private infraMessage msg = new infraMessage();


        IFuncaoBusiness funcaoBusiness;


        public FuncaoController(IFuncaoBusiness _funcaoBusiness)
        {
            funcaoBusiness = _funcaoBusiness;
        }

        // GET: Funcao
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult ListarFuncao(ParametrosPaginacao paginacao)
        {
            var _list = funcaoBusiness.GetFuncao();

            int tot = _list.Count();
            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                _list = _list.Where("FNC_NOME.ToUpper().Contains(@0) OR REF_NOME.ToUpper().Contains(@0) ", paginacao.SearchValue.ToUpper()).ToList();
            }
            int totFiltrado = _list.Count();

            var dadosPaginados = _list.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();
            //var data = new
            //{
            //    FNC_ID = "",
            //    FNC_NOME = "",
            //    FNC_QUANTIDADE = "",
            //    REF_NOME = "",
            //};
            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddUpdateFuncao(int id = 0)
        {

            FuncaoModelView _funcao = new FuncaoModelView();
            if (id == 0)
            {

                ViewBag.Title = "Nova Função";
                ViewBag.Ref = new SelectList(funcaoBusiness.GetAllReferenciaFuncao(), "REFFNC_ID", "REFFNC_NOME");
                return PartialView(_funcao);
            }
            else
            {
                ViewBag.Title = "Editar Função";
                FuncaoDomainModel funcaoDomainModel = funcaoBusiness.GetFuncaoById(id);
                if (funcaoDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(funcaoDomainModel, _funcao);
                ViewBag.Ref = new SelectList(funcaoBusiness.GetAllReferenciaFuncao(), "REFFNC_ID", "REFFNC_NOME", _funcao.REFFNC_ID);
                return PartialView(_funcao);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateFuncao(FuncaoModelView _funcao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FuncaoDomainModel funcaoDomainModel = new FuncaoDomainModel();
                    _funcao.FNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_funcao, funcaoDomainModel);
                    funcaoBusiness.AddUpdateFuncao(funcaoDomainModel);


                    if (funcaoBusiness.Salvar())
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
        // GET: Objeto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncaoModelView _funcao = new FuncaoModelView();
            FuncaoDomainModel funcaoDomainModel = funcaoBusiness.GetFuncaoById(id.Value);
            if (_funcao == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(funcaoDomainModel, _funcao);
            return PartialView(_funcao);
        }

        // POST: Objeto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                var funcao = funcaoBusiness.GetFuncaoById(id);
                if (funcao != null)
                {
                    funcao.FNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    funcaoBusiness.DeleteFuncao(funcao);

                    if (funcaoBusiness.Salvar())
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
