using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using Rotativa.MVC;
using SisGeape2.Apresentation.InfraPaginacao;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class JustificativaController : Controller
    {

        private infraMessage msg = new infraMessage();

        IJustificativaPontoBusiness justificativaBusiness;
        IVinculoBusiness vinculoBusiness;
        ICargoBusiness cargoBusiness;
        IUnidadeBusiness unidadeBusiness;
        ICidBusiness cidBusiness;


        public JustificativaController(IJustificativaPontoBusiness _justificativaBusiness, IVinculoBusiness _vinculoBusiness, ICidBusiness _cidBusiness, ICargoBusiness _cargoBusiness, IUnidadeBusiness _unidadeBusiness)
        {
            justificativaBusiness = _justificativaBusiness;
            vinculoBusiness = _vinculoBusiness;
            cidBusiness = _cidBusiness;
            cargoBusiness = _cargoBusiness;
            unidadeBusiness = _unidadeBusiness;

        }

        public ActionResult Afastamento()
        {
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
            ViewBag.Motivo = new SelectList(justificativaBusiness.GetMotivoJustificativa(), "MTVJUS_ID", "MTVJUS_NOME");
            return View(new List<JustificativaPontoModelView>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Afastamento(string matricula, string nome, int? cargo, int? motivo, int? unidade, DateTime? inicio, DateTime? fim)
        {
            List<JustificativaPontoDomainModel> justificativaPontoDomain;
            List<JustificativaPontoModelView> justificativas = new List<JustificativaPontoModelView>();


            CamposJustificativaBuscaRelatorioModelView CJust = new CamposJustificativaBuscaRelatorioModelView
            {
                MATRICULA = matricula,
                NOME = nome,
                CARGO = cargo,
                MOTIVO = motivo,
                UNIDADE = unidade,
                INICIO = inicio,
                FIM = fim,
                TEXTO_CARGO = cargo.HasValue && cargoBusiness.DdlCargo().Where(x => x.CRG_ID == cargo.Value).Count() > 0 ? cargoBusiness.DdlCargo().Where(x => x.CRG_ID == cargo.Value).FirstOrDefault().CRG_NOME : "",
                TEXTO_UNIDADE = unidade.HasValue && unidadeBusiness.ddlUnidade().Where(X => X.UND_ID == unidade.Value).Count() > 0 ? unidadeBusiness.ddlUnidade().Where(X => X.UND_ID == unidade.Value).FirstOrDefault().UND_NOME : "",
                TEXTO_MOTIVO = motivo.HasValue && justificativaBusiness.GetMotivoJustificativa().Where(x => x.MTVJUS_ID == motivo.Value).Count() > 0 ? justificativaBusiness.GetMotivoJustificativa().Where(x => x.MTVJUS_ID == motivo.Value).FirstOrDefault().MTVJUS_NOME : "TODOS"
            };
            TempData["CJust"] = CJust;

            justificativaPontoDomain = justificativaBusiness.BuscarJustificativaPorParametros(null, cargo, unidade, motivo, matricula, nome, inicio, fim);
            Mapper.Map(justificativaPontoDomain, justificativas);

            TempData["ListResult"] = justificativas;

            TempData["Inicio"] = inicio.HasValue ? inicio.Value.ToShortDateString() : null;
            TempData["Fim"] = fim.HasValue ? fim.Value.ToShortDateString() : null;
            TempData["Nome"] = nome;
            TempData["matricula"] = matricula;
            TempData["cargo"] = cargo;
            TempData["motivo"] = motivo;
            TempData["unidade"] = unidade;
            //Mapper.Map(pesquisa, pesquisaDomain);

            //pesquisaListDomain = cartaoPontoBusiness.GetPesquisa(pesquisaDomain);
            //Mapper.Map(pesquisaListDomain, pesquisaListModelView);
            //TempData["ListFuncionario"] = pesquisaListModelView;

            //TempData["ParametrosPesquisa"] = pesquisa;

            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME", cargo);
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME", unidade);
            ViewBag.Motivo = new SelectList(justificativaBusiness.GetMotivoJustificativa(), "MTVJUS_ID", "MTVJUS_NOME", motivo);
            return View("Afastamento");
        }

        public async Task<ActionResult> GerarRelatorioServidorJustificativa()
        {
            //CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            //List<JustificativaPontoModelView> justificativas = TempData["ListResult"] as List<JustificativaPontoModelView>;
            CamposJustificativaBuscaRelatorioModelView CJust = TempData["CJust"] as CamposJustificativaBuscaRelatorioModelView;


            string UnidadeText = "";
            try
            {
                ViewAsPdf pdfResult = null;
                int cont = 0;
                List<JustificativaPontoDomainModel> JustificativasDomainModel = justificativaBusiness.BuscarJustificativaPorParametros(null, CJust.CARGO, CJust.UNIDADE, CJust.MOTIVO, CJust.MATRICULA, CJust.NOME, CJust.INICIO, CJust.FIM);
                //List<FuncionarioRelatorioFuncaoDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioFuncao(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.FUNCAO, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO, Cfunc.FUNCAO_DATA_INICIO, Cfunc.FUNCAO_DATA_FIM); ;
                List<JustificativaPontoModelView> justificativaRelatorioModelView = new List<JustificativaPontoModelView>();
                //foreach (FuncionarioRelatorioFuncaoDomainModel funcionarioRelatorioDM in domainModel)
                foreach (JustificativaPontoDomainModel justificativa in JustificativasDomainModel)
                {
                    JustificativaPontoModelView justificativaRelatorioMV = new JustificativaPontoModelView();
                    Mapper.Map(justificativa, justificativaRelatorioMV);
                    UnidadeText += string.Format("{0} - ", justificativa.UND_NOME);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    justificativaRelatorioModelView.Add(justificativaRelatorioMV);

                    pdfResult = new ViewAsPdf("RelatorioJustificativaPdf" + "")
                    {
                        RotativaOptions = new Rotativa.Core.DriverOptions
                        {
                            PageOrientation = Rotativa.Core.Options.Orientation.Portrait,
                            IsGrayScale = false,
                            CustomSwitches = customSwitches,
                            PageSize = Rotativa.Core.Options.Size.A4,
                        }
                    };

                    cont++;
                }
                TempData["RelatorioJustificativa"] = justificativaRelatorioModelView;
                TempData["Unidades"] = UnidadeText;
                return pdfResult;
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    resultado = false,
                    msg = string.Format("Não foi possivel gerar o relatório. {0}", ex.Message),
                    detail = string.Format("Detalhes. {0}", ex)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Justificativa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ListJusFun(int? func)
        {
            ViewBag.FUN_ID = func;
            return PartialView();
        }

        public ActionResult Motivo()
        {
            return View();
        }

        public JsonResult ListarMotivo(ParametrosPaginacao paginacao)
        {
            var _list = justificativaBusiness.GetMotivoJustificativa();

            int tot = _list.Count();

            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                _list = _list.Where("MTVJUS_NOME.ToUpper().Contains(@0) ", paginacao.SearchValue.ToUpper()).ToList();
            }
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

        public JsonResult ListarJustificativa(ParametrosPaginacao paginacao, int? id)
        {
            var _list = justificativaBusiness.GetJustificativaByFuncionario(id.Value);

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

        public ActionResult AddUpdateJusFun(int? id, int func = 0)
        {
            JustificativaPontoModelView justificativa = new JustificativaPontoModelView();
            ViewBag.FUN_ID = func;

            justificativa.JUSPT_DATARECEBIMENTO = DateTime.Today;
            if (id == null)
            {
                ViewBag.Title = "Nova Justificativa de Ponto";
                ViewBag.VNC_ID = new SelectList(vinculoBusiness.ddlVinculoByFuncionario(func), "VNC_ID", "VNC_NOME");
                ViewBag.MTVJUS_ID = new SelectList(justificativaBusiness.GetMotivoJustificativa(), "MTVJUS_ID", "MTVJUS_NOME");
                ViewBag.CIDJUS_ID = new SelectList(cidBusiness.GetCid(), "CID_ID", "CID_CODIGO");
            }
            else
            {
                ViewBag.Title = "Editar Justificativa de Ponto";

                JustificativaPontoDomainModel domainModel = justificativaBusiness.GetJustificativaById(id.Value);
                Mapper.Map(domainModel, justificativa);

                if (domainModel == null)
                {
                    return HttpNotFound();
                }

                ViewBag.VNC_ID = new SelectList(vinculoBusiness.ddlVinculoByFuncionario(justificativa.FUN_ID), "VNC_ID", "VNC_NOME", domainModel.VNC_ID);
                ViewBag.MTVJUS_ID = new SelectList(justificativaBusiness.GetMotivoJustificativa(), "MTVJUS_ID", "MTVJUS_NOME", domainModel.MTVJUS_ID);
                ViewBag.CIDJUS_ID = new SelectList(cidBusiness.GetCid(), "CID_ID", "CID_CODIGO", domainModel.CID_ID);

            }
            return PartialView(justificativa);

        }

        // GET: JustificativaPonto/AddUpdateCartao/5
        public ActionResult AddUpdateJusCartao(int? id, DateTime? inicio, int func = 0)
        {
            JustificativaPontoModelView justificativa = new JustificativaPontoModelView();
            ViewBag.FUN_ID = func;

            if (inicio.HasValue)
            {
                justificativa.JUSPT_DATAINICIO = inicio.Value.Date;
            }

            justificativa.JUSPT_DATARECEBIMENTO = DateTime.Today;
            if (id == null)
            {
                ViewBag.Title = "Nova Justificativa de Ponto";
                ViewBag.VNC_ID = new SelectList(vinculoBusiness.ddlVinculoByFuncionario(func), "VNC_ID", "VNC_NOME");
                ViewBag.MTVJUS_ID = new SelectList(justificativaBusiness.GetMotivoJustificativa(), "MTVJUS_ID", "MTVJUS_NOME");
                ViewBag.CIDJUS_ID = new SelectList(cidBusiness.GetCid(), "CIDJUS_ID", "CIDJUS_CODIGO");
            }

            else
            {
                ViewBag.Title = "Editar Justificativa de Ponto";
                JustificativaPontoDomainModel domainModel = justificativaBusiness.GetJustificativaById(id.Value);
                ViewBag.VNC_ID = new SelectList(vinculoBusiness.ddlVinculoByFuncionario(domainModel.FUN_ID), "VNC_ID", "VNC_NOME", domainModel.VNC_ID);
                ViewBag.MTVJUS_ID = new SelectList(justificativaBusiness.GetMotivoJustificativa(), "MTVJUS_ID", "MTVJUS_NOME", domainModel.MTVJUS_ID);
                ViewBag.CIDJUS_ID = new SelectList(cidBusiness.GetCid(), "CID_ID", "CID_CODIGO", domainModel.CID_ID);

                if (domainModel == null)
                {
                    return HttpNotFound();
                }

                Mapper.Map(domainModel, justificativa);
            }
            return PartialView(justificativa);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateJusFun(JustificativaPontoModelView justificativa)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    JustificativaPontoDomainModel _domainModel = new JustificativaPontoDomainModel();


                    justificativa.JUSPT_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(justificativa, _domainModel);
                    justificativaBusiness.AddUpdateJustificativa(_domainModel);

                    if (justificativaBusiness.Salvar())
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


        public ActionResult AddUpdateMotivo(int id = 0)
        {
            MotivoJustificativaModelView _motivo = new MotivoJustificativaModelView();
            if (id == 0)
            {
                ViewBag.Title = "Novo Motivo de Justificativa";


            }
            else
            {
                ViewBag.Title = "Editar Motivo de Justificativa";

                MotivoJustificativaDomainModel domainModel = justificativaBusiness.GetMotivoJustificativaById(id);

                if (domainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(domainModel, _motivo);
            }
            return PartialView(_motivo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateMotivo(MotivoJustificativaModelView _motivo)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    MotivoJustificativaDomainModel _domainModel = new MotivoJustificativaDomainModel();

                    _motivo.MTVJUS_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_motivo, _domainModel);
                    justificativaBusiness.AddUpdateMotivoJustificativa(_domainModel);

                    if (justificativaBusiness.Salvar())
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
            JustificativaPontoModelView justificativa = new JustificativaPontoModelView();
            JustificativaPontoDomainModel domainModel = justificativaBusiness.GetJustificativaById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.FUN_ID = domainModel.FUN_ID;
            Mapper.Map(domainModel, justificativa);
            return PartialView(justificativa);
        }

        // POST: JustificativaPonto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var justificativaponto = justificativaBusiness.GetJustificativaById(id);

                if (justificativaponto != null)
                {
                    justificativaponto.JUSPT_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    justificativaBusiness.DeleteJustificativa(justificativaponto);

                    if (justificativaBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        resultado = false,
                        tipomsg = "",
                        msg = msg.Error444()
                    });

                }

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

        // GET: Motivo/Delete/5
        public ActionResult DeleteMotivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoJustificativaModelView _motivo = new MotivoJustificativaModelView();
            MotivoJustificativaDomainModel domainModel = justificativaBusiness.GetMotivoJustificativaById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, _motivo);
            return PartialView(_motivo);
        }

        // POST: MotivoJustificativa/Delete/5
        [HttpPost, ActionName("DeleteMotivo")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMotivoConfirmed(int id)
        {
            try
            {
                MotivoJustificativaDomainModel domainModel = justificativaBusiness.GetMotivoJustificativaById(id);
                if (domainModel != null)
                {
                    domainModel.MTVJUS_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    justificativaBusiness.DeleteMotivoJustificativa(domainModel);


                    if (justificativaBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new
                    {
                        resultado = false,
                        tipomsg = "",
                        msg = msg.Error444()
                    });

                }

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
