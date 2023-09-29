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
    public class ReferenciaFuncaoController : Controller
    {
        private infraMessage msg = new infraMessage();

        IFuncaoBusiness funcaoBusiness;
        public ReferenciaFuncaoController(IFuncaoBusiness _funcaoBusiness)
        {
            funcaoBusiness = _funcaoBusiness;
        }

        // GET: ReferenciaFuncao
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult ListarRefFuncao(ParametrosPaginacao paginacao)
        {
            var _list = funcaoBusiness.GetAllReferenciaFuncao();

            int tot = _list.Count();

            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                _list = _list.Where(" REFFNC_NOME.ToUpper().Contains(@0) ", paginacao.SearchValue.ToUpper()).ToList();
            }
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

        public ActionResult AddUpdateRef(int id = 0)
        {
            ReferenciaFuncaoModelView _referencia = new ReferenciaFuncaoModelView();
            if (id == 0)
            {
                ViewBag.Title = "Nova Referência Função";
                return PartialView(_referencia);
            }
            else
            {
                ViewBag.Title = "Editar Referência Função";
                ReferenciaFuncaoDomainModel referenciaFuncaoDomainModel = funcaoBusiness.GetReferenciaFuncaoById(id);
                if (referenciaFuncaoDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(referenciaFuncaoDomainModel, _referencia);
                return PartialView(_referencia);

            }
        }

        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateRef(ReferenciaFuncaoModelView _referenciafuncao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ReferenciaFuncaoDomainModel referenciaDomainModel = new ReferenciaFuncaoDomainModel();
                    _referenciafuncao.REFFNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_referenciafuncao, referenciaDomainModel);
                    funcaoBusiness.AddUpdateReferenciaFuncao(referenciaDomainModel);


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

        // GET: ReferenciaFuncao/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferenciaFuncaoModelView _referencia = new ReferenciaFuncaoModelView();
            ReferenciaFuncaoDomainModel referenciaDomainModel = funcaoBusiness.GetReferenciaFuncaoById(id.Value);

            if (referenciaDomainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(referenciaDomainModel, _referencia);
            return PartialView(_referencia);
        }

        // POST: ReferenciaFuncao/Delete/5
        // POST: Unidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                var referencia = funcaoBusiness.GetReferenciaFuncaoById(id);
                if (referencia != null)
                {
                    referencia.REFFNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    funcaoBusiness.DeleteReferenciaFuncao(referencia);

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
