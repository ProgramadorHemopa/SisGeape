using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using CMM.Projects.Apresentation.Models.CustomValidation;
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
    public class VinculoxUnidadeController : Controller
    {

        private infraMessage msg = new infraMessage();

        IVinculoUnidadeBusiness lotacaoBusiness;
        IVinculoBusiness vinculoBusiness;
        IUnidadeBusiness unidadeBusiness;


        public VinculoxUnidadeController(IVinculoUnidadeBusiness _lotacaoBusiness, IVinculoBusiness _vinculoBusiness, IUnidadeBusiness _unidadeBusiness)
        {
            lotacaoBusiness = _lotacaoBusiness;
            vinculoBusiness = _vinculoBusiness;
            unidadeBusiness = _unidadeBusiness;
            lotacaoBusiness.Initialize(new ModelStateWrapper(this.ModelState));
        }
        public ActionResult ListVinculoxUnidade(int? fun_id)
        {

            ViewBag.FUN_ID = fun_id.Value;


            return PartialView();
        }



        public JsonResult GetLotacao_Funcionario(ParametrosPaginacao paginacao, int? id)
        {
            var _list = lotacaoBusiness.GetLotacaoByFuncionario(id.Value);

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

        // GET: VinculoxUnidade
        public ActionResult Index()
        {
            return View();
        }

        // GET: VinculoxUnidade/Create
        public ActionResult AddUpdateVncUnd(int? id, int func = 0)
        {
            VinculoUnidadeModelView _lot = new VinculoUnidadeModelView();


            if (id == 0)
            {
                ViewBag.Title = "Nova Lotação";

                ViewBag.UND_ID = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
                ViewBag.VNC_ID = new SelectList(vinculoBusiness.ddlVinculoByFuncionario(func), "VNC_ID", "VNC_NOME");
                return PartialView(_lot);
            }
            else
            {

                ViewBag.Title = "Editar Lotação";

                VinculoUnidadeDomainModel _domainModel = lotacaoBusiness.GetLotacaoById(id.Value);

                if (_lot == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(_domainModel, _lot);
                ViewBag.UND_ID = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME", _lot.UND_ID);
                ViewBag.VNC_ID = new SelectList(vinculoBusiness.ddlVinculoByFuncionario(_lot.FUN_ID), "VNC_ID", "VNC_NOME", _lot.VNC_ID);

                return PartialView(_lot);

            }
        }

        // POST: VinculoxUnidade/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateVncUnd(VinculoUnidadeModelView _vinculounidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VinculoUnidadeDomainModel _domainModel = new VinculoUnidadeDomainModel();


                    _vinculounidade.VNCU_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(_vinculounidade, _domainModel);

                    if (lotacaoBusiness.AddUpdateLotacao(_domainModel))
                    {
                        if (lotacaoBusiness.Salvar())
                            return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                        else
                            throw new Exception();
                    }
                    else
                        throw new InvalidOperationException();

                }
                else
                    throw new InvalidOperationException();
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

        // GET: Unidade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VinculoUnidadeModelView _lotacao = new VinculoUnidadeModelView();
            VinculoUnidadeDomainModel _domainModel = lotacaoBusiness.GetLotacaoById(id.Value);

            if (_domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(_domainModel, _lotacao);
            return PartialView(_lotacao);
        }

        // POST: Unidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                var _lotacao = lotacaoBusiness.GetLotacaoById(id);
                if (_lotacao != null)
                {
                    _lotacao.VNCU_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    lotacaoBusiness.DeleteLotacao(_lotacao);

                    if (lotacaoBusiness.Salvar())
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
