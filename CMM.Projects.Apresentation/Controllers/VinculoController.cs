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
    public class VinculoController : Controller
    {
        private infraMessage msg = new infraMessage();

        IVinculoBusiness vinculoBusiness;
        ICargoBusiness cargoBusiness;
        IFuncionarioBusiness funcionarioBusiness;
        public VinculoController(IVinculoBusiness _vinculoBusiness, ICargoBusiness _cargoBusiness, IFuncionarioBusiness _funcionarioBusiness)
        {
            vinculoBusiness = _vinculoBusiness;
            cargoBusiness = _cargoBusiness;
            funcionarioBusiness = _funcionarioBusiness;
        }

        //metodo de validar o valor do numero do vinculo, para não haver duplicação de num do vinculo.
        public JsonResult ValidarNumVinculo(int Vnc_qtd, int Fun_id, int Vnc_id)
        {
            var vinculoFuncionario = vinculoBusiness.GetVinculoByFuncionario(Fun_id);
            return Json(!vinculoFuncionario.Any(x => x.VNC_QTD == Vnc_qtd && x.VNC_ID != Vnc_id), JsonRequestBehavior.AllowGet);

        }



        // GET: Vinculo
        public ActionResult ListVinculo(int? id)
        {
            ViewBag.FUN_ID = id;
            return PartialView();
        }

        // GET: Vinculo
        public ActionResult Situacao()
        {
            return View();
        }

        public ActionResult Tipo()
        {
            return View();
        }





        public JsonResult GetVinculo(ParametrosPaginacao paginacao, int? id)
        {
            var _list = vinculoBusiness.GetVinculoByFuncionario(id.Value);
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





        public JsonResult ListarVinculoSituacao(ParametrosPaginacao paginacao)
        {
            var _list = vinculoBusiness.GetAllVinculoSituacao();


            int tot = _list.Count();

            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                _list = _list.Where("VNCST_DESCRICAO.ToUpper().Contains(@0) ", paginacao.SearchValue.ToUpper()).ToList();
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

        public JsonResult ListarVinculoTipo(ParametrosPaginacao paginacao)
        {
            var _list = vinculoBusiness.GetAllTipoVinculo();


            int tot = _list.Count();


            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                _list = _list.Where("VNCTP_DESCRICAO.ToUpper().Contains(@0) ", paginacao.SearchValue.ToUpper()).ToList();
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



        // GET: Vinculo/Edit/5
        public ActionResult AddUpdateVinculo(int? id, int func = 0)
        {
            VinculoModelView vnc = new VinculoModelView();

            if (id == 0)
            {
                ViewBag.Title = "Novo Vínculo";
                vnc.FUN_ID = func;

                ViewBag.CRG_ID = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
                ViewBag.VNCST_ID = new SelectList(vinculoBusiness.GetAllVinculoSituacao(), "VNCST_ID", "VNCST_DESCRICAO");
                ViewBag.VNCTP_ID = new SelectList(vinculoBusiness.GetAllTipoVinculo(), "VNCTP_ID", "VNCTP_DESCRICAO");
                ViewBag.VNC_AREA = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "MEIO", Value="1" },
                new SelectListItem {Text = "FIM", Value="2" },

            }, "Value", "Text");
                return PartialView(vnc);
            }
            else
            {
                ViewBag.Title = "Editar Vínculo";
                VinculoDomainModel _vncDomainModel = vinculoBusiness.GetVinculobyId(id.Value);
                if (_vncDomainModel == null)
                {
                    return HttpNotFound();
                }

                Mapper.Map(_vncDomainModel, vnc);

                ViewBag.CRG_ID = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME", vnc.CRG_ID);
                ViewBag.VNCST_ID = new SelectList(vinculoBusiness.GetAllVinculoSituacao(), "VNCST_ID", "VNCST_DESCRICAO", vnc.VNCST_ID);
                ViewBag.VNCTP_ID = new SelectList(vinculoBusiness.GetAllTipoVinculo(), "VNCTP_ID", "VNCTP_DESCRICAO", vnc.VNCTP_ID);
                ViewBag.VNC_AREA = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "MEIO", Value="1" },
                new SelectListItem {Text = "FIM", Value="2" },

            }, "Value", "Text", vnc.VNC_AREA);
                return PartialView(vnc);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateVinculo(VinculoModelView _vinculo, bool cracha = false)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _vinculo.VNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    VinculoDomainModel _vinculoDomainModel = new VinculoDomainModel();
                    Mapper.Map(_vinculo, _vinculoDomainModel);
                    var result = vinculoBusiness.AddUpdateVinculo(_vinculoDomainModel);
                    if (cracha)
                    {
                        FuncionarioSolicitarCrachaDomainModel solicitacaoCracha = new FuncionarioSolicitarCrachaDomainModel();
                        solicitacaoCracha.FUNCRC_DATASOLICITACAO = DateTime.Now;
                        solicitacaoCracha.FUNCRC_TIPO = 1; //identificação
                        solicitacaoCracha.FUNCRC_SITUACAO = 1;//pendente;
                        solicitacaoCracha.VNC_ID = result.VNC_ID;
                        solicitacaoCracha.FUNCRC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                        funcionarioBusiness.AddUpdateSolicitacaoCracha(solicitacaoCracha);
                        funcionarioBusiness.Salvar();
                    };


                    return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);

                }
                else
                    throw new Exception();

            }
            catch (Exception ex)
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                if (erros.Count() > 0)
                {
                    foreach (var err in erros)
                    {
                        mensg += err.ErrorMessage + " <br/>";
                    }
                }
                else
                    mensg = ex.Message;

                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }
        }








        public ActionResult AddUpdateVNCST(int? id)
        {

            VinculoSituacaoModelView _vncst = new VinculoSituacaoModelView();
            if (id == null)
            {
                ViewBag.Title = "Nova Situação Vínculo";
                return PartialView(_vncst);
            }
            else
            {
                ViewBag.Title = "Editar Situação Vínculo";
                VinculoSituacaoDomainModel _vncstDomainModel = vinculoBusiness.GetVinculoSituacaobyId(id.Value);
                if (_vncstDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(_vncstDomainModel, _vncst);


                return PartialView(_vncst);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateVNCST(VinculoSituacaoModelView vinculosituacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vinculosituacao.VNCST_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    VinculoSituacaoDomainModel _vncstDomainModel = new VinculoSituacaoDomainModel();

                    Mapper.Map(vinculosituacao, _vncstDomainModel);
                    vinculoBusiness.AddUpdateVinculoSituacao(_vncstDomainModel);


                    if (vinculoBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = false, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);

                }
                else throw new Exception();

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
                    mensg += ex.Message;
                }
                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult AddUpdateVNCTP(int? id)
        {

            VinculoTipoModelView _vnctp = new VinculoTipoModelView();
            if (id == null)
            {
                ViewBag.Title = "Novo Tipo de Vínculo";
                return PartialView(_vnctp);
            }
            else
            {
                ViewBag.Title = "Editar Tipo de Vínculo";
                VinculoTipoDomainModel _vnctpDomainModel = vinculoBusiness.GetVinculoTipobyId(id.Value);
                if (_vnctpDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(_vnctpDomainModel, _vnctp);


                return PartialView(_vnctp);

            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateVNCTP(VinculoTipoModelView vinculotipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vinculotipo.VNCTP_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    VinculoTipoDomainModel _vnctpDomainModel = new VinculoTipoDomainModel();

                    Mapper.Map(vinculotipo, _vnctpDomainModel);
                    vinculoBusiness.AddUpdateVinculoTipo(_vnctpDomainModel);


                    if (vinculoBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = false, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);

                }
                else throw new Exception();

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



        // GET: Vinculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VinculoModelView _vinculo = new VinculoModelView();
            VinculoDomainModel _vinculoDomainModel = vinculoBusiness.GetVinculobyId(id.Value);
            if (_vinculoDomainModel == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(_vinculoDomainModel, _vinculo);
            return PartialView(_vinculo);
        }




        // POST: Vinculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (!vinculoBusiness.ValidarDelete(id))
                { throw new Exception("O Vinculo possuí uma lotação cadastrada"); }

                var _vinculo = vinculoBusiness.GetVinculobyId(id);
                if (_vinculo != null)
                {
                    _vinculo.VNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    vinculoBusiness.DeleteVinculo(_vinculo);

                    if (vinculoBusiness.Salvar())
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





        // GET: VinculoSituacao/Delete/5
        public ActionResult DeleteVNCST(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VinculoSituacaoModelView _vncst = new VinculoSituacaoModelView();
            VinculoSituacaoDomainModel _vncstDomainModel = vinculoBusiness.GetVinculoSituacaobyId(id.Value);
            if (_vncstDomainModel == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(_vncstDomainModel, _vncst);
            return PartialView(_vncst);
        }

        // POST: VinculoSituacao/Delete/5
        [HttpPost, ActionName("DeleteVNCST")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVNCSTConfirmed(int id)
        {
            try
            {
                var _vncst = vinculoBusiness.GetVinculoSituacaobyId(id);
                if (_vncst != null)
                {
                    _vncst.VNCST_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID; ;

                    vinculoBusiness.DeleteVinculoSituacao(_vncst);

                    if (vinculoBusiness.Salvar())
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


        // GET: VinculoSituacao/Delete/5
        public ActionResult DeleteVNCTP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VinculoTipoModelView _vnctp = new VinculoTipoModelView();
            VinculoTipoDomainModel _vnctpDomainModel = vinculoBusiness.GetVinculoTipobyId(id.Value);
            if (_vnctpDomainModel == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(_vnctpDomainModel, _vnctp);
            return PartialView(_vnctp);
        }

        // POST: VinculoSituacao/Delete/5
        [HttpPost, ActionName("DeleteVNCTP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVNCTPConfirmed(int id)
        {
            try
            {
                var _vnctp = vinculoBusiness.GetVinculoTipobyId(id);
                if (_vnctp != null)
                {
                    _vnctp.VNCTP_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID; ;

                    vinculoBusiness.DeleteVinculoTipo(_vnctp);

                    if (vinculoBusiness.Salvar())
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
