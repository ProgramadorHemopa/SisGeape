using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using CMM.Projects.Apresentation.RelatorioExcel;
using CMM.Projects.Apresentation.Utils;
using Microsoft.Reporting.WebForms;
using Rotativa.MVC;
using SisGeape2.Apresentation.InfraPaginacao;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class FuncionarioController : Controller
    {
        private infraMessage msg = new infraMessage();

        IVinculoBusiness vinculoBusiness;
        ICargoBusiness cargoBusiness;
        IBeneficioBusiness beneficioBusiness;
        IFuncaoBusiness funcaoBusiness;
        IFuncaoVinculoBusiness funcaoVinculoBusiness;
        IUnidadeBusiness unidadeBusiness;
        IFuncionarioBusiness funcionarioBusiness;
        IBancoBusiness bancoBusiness;

        public FuncionarioController(IFuncionarioBusiness _funcionarioBusiness, IBancoBusiness _bancoBusiness, IVinculoBusiness _vinculoBusiness, ICargoBusiness _cargoBusiness, IBeneficioBusiness _beneficioBusiness, IFuncaoBusiness _funcaoBusiness, IFuncaoVinculoBusiness _funcaoVinculoBusiness, IUnidadeBusiness _unidadeBusiness)
        {
            funcionarioBusiness = _funcionarioBusiness;
            vinculoBusiness = _vinculoBusiness;
            cargoBusiness = _cargoBusiness;
            beneficioBusiness = _beneficioBusiness;
            funcaoBusiness = _funcaoBusiness;
            funcaoVinculoBusiness = _funcaoVinculoBusiness;
            unidadeBusiness = _unidadeBusiness;
            bancoBusiness = _bancoBusiness;
        }


        // GET: Funcionario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admissao(FuncionarioPesquisaModelView pesquisa)
        {


            TempData["ListResult"] = pesquisa;
            List<FuncionarioPesquisaModelView> listFuncionarioAdmissao = new List<FuncionarioPesquisaModelView>();
            TempData["AdmissaoFuncionario"] = listFuncionarioAdmissao;

            return View(pesquisa);
        }


        public ActionResult ConsultaCartaoPonto()
        {
            FuncionarioPesquisaModelView pesquisa = new FuncionarioPesquisaModelView();
            if (TempData["PesqCartaoPonto"] != null)
            {
                pesquisa = ((FuncionarioPesquisaModelView)TempData["PesqCartaoPonto"]);
            }
            return View(pesquisa);
        }

        [HttpPost]
        public ActionResult InterfacePesquisaCartaoPonto(FuncionarioPesquisaModelView pesquisa)
        {
            FuncionarioPesquisaDomainModel pesquisaDomain = new FuncionarioPesquisaDomainModel();
            List<FuncionarioPesquisaModelView> listFuncionarioModel = new List<FuncionarioPesquisaModelView>();
            List<FuncionarioIndexDomainModel> listFuncionarioDomain = new List<FuncionarioIndexDomainModel>();
            Mapper.Map(pesquisa, pesquisaDomain);
            listFuncionarioDomain = funcionarioBusiness.GetFuncionarioByNameOrMatricula(pesquisaDomain);
            TempData["ListIndexDomain"] = listFuncionarioDomain;
            TempData["PesqCartaoPonto"] = pesquisa;
            return RedirectToAction("ConsultaCartaoPonto");
        }


        #region Cracha

        [HttpPost]
        public JsonResult InterfacePesquisaCracha(ParametrosPaginacao paginacao, int? fun_id, string matricula, string nome, string numero, int? cargo, int? unidade, int? tipo, int? situacao, DateTime? dataInicio, DateTime? dataFim)
        {
            var _list = funcionarioBusiness.LoadCrachaByParameters(fun_id, matricula, nome, numero, cargo, unidade, tipo, situacao, dataInicio, dataFim, null, null).Select(x => new
            {
                FUNCRC_DATAEMISSAO = x.FUNCRC_DATAEMISSAO,
                FUNCRC_DATASOLICITACAO = x.FUNCRC_DATASOLICITACAO,
                FUNCRC_ID = x.FUNCRC_ID,
                FUNCRC_NUMERO = x.FUNCRC_NUMERO,
                FUNCRC_OBSERVACAO = x.FUNCRC_OBSERVACAO,
                FUNCRC_REGUSER = x.FUNCRC_REGUSER,
                FUNCRC_SITUACAO = x.FUNCRC_SITUACAO,
                FUNCRC_TIPO = x.FUNCRC_TIPO,
                FUN_ID = x.FUN_ID,
                VNC_ID = x.VNC_ID,
                VNC_NOME = x.VNC_NOME,
                FUN_NOME = x.FUN_NOME,
                VIA = x.VIA,
                FUN_FOTO = Convert.ToBase64String(x.FUN_FOTO)
            });

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

            //return View("CrachaPendente");
        }

        [HttpGet]
        [Route("funcionario/cracha/relatorio")]
        public ActionResult RelatorioSolicitacao()
        {
            return View();
        }

        [HttpPost, Route("funcionario/cracha/relatorio")]
        [ValidateAntiForgeryToken]
        public ActionResult RelatorioSolicitacao(DateTime? dataInicio, DateTime? dataFim)
        {

            TempData["inicio"] = dataInicio.Value.ToShortDateString();
            TempData["fim"] = dataFim.Value.ToShortDateString();
            TempData.Keep("inicio");
            TempData.Keep("fim");
            List<FuncionarioSolicitarCrachaModelView> solicitacao = new List<FuncionarioSolicitarCrachaModelView>();
            Mapper.Map(funcionarioBusiness.LoadCrachaByParameters(null, "", "", "", null, null, null, (int)FuncionarioSolicitarCrachaModelView.Situacao.Impresso, null, null, dataInicio, dataFim), solicitacao);

            string customSwitches =
            " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                DateTime.Now + "  Pág. [page]/[toPage]\"" +
                " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";


            var pdfResult = new ViewAsPdf("GerarMemorando", solicitacao)
            {
                RotativaOptions = new Rotativa.Core.DriverOptions
                {
                    PageOrientation = Rotativa.Core.Options.Orientation.Portrait,
                    IsGrayScale = false,
                    CustomSwitches = customSwitches,
                    PageSize = Rotativa.Core.Options.Size.A4,

                }
            };

            return pdfResult;
        }


        [HttpGet]
        [Route("funcionario/cracha/pendente")]
        public ActionResult CrachaPendente()
        {
            List<FuncionarioSolicitarCrachaModelView> solicitacao = new List<FuncionarioSolicitarCrachaModelView>();
            Mapper.Map(funcionarioBusiness.LoadCrachaByParameters(null, "", "", "", null, null, null, (int)FuncionarioSolicitarCrachaModelView.Situacao.Pendente, null, null, null, null), solicitacao);

            ViewBag.SITUACAO = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "PENDENTE", Value="1" },
                new SelectListItem {Text = "IMPRESSO", Value="2" },
                new SelectListItem {Text = "PERDA", Value="3" },
                new SelectListItem {Text = "DANIFICADO", Value="4" },

            }, "Value", "Text");


            ViewBag.Tipo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "IDENTIFICAÇÃO", Value="1" },
                new SelectListItem {Text = "PROXIMIDADE", Value="2" },

            }, "Value", "Text");
            return View(solicitacao);
        }

        [HttpGet]
        [Route("funcionario/cracha")]
        public ActionResult SolicitacaoIndex()
        {
            List<FuncionarioSolicitarCrachaModelView> solicitacao = new List<FuncionarioSolicitarCrachaModelView>();
            Mapper.Map(funcionarioBusiness.LoadCrachaByParameters(0, "", "", null, (int)FuncionarioSolicitarCrachaModelView.Situacao.Pendente, null, null, null, null, null, null, null), solicitacao);
            ViewBag.CARGO = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.DATASOLICITACAO = new SelectList(funcionarioBusiness.ddlFuncionarioAtendimentoSASS(), "FUN_ID", "FUN_NOME");

            ViewBag.UNIDADE = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");

            ViewBag.SITUACAO = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "PENDENTE", Value="1" },
                new SelectListItem {Text = "IMPRESSO", Value="2" },
                new SelectListItem {Text = "PERDA", Value="3" },
                new SelectListItem {Text = "DANIFICADO", Value="4" },

            }, "Value", "Text");


            ViewBag.Tipo = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "IDENTIFICAÇÃO", Value="1" },
                new SelectListItem {Text = "PROXIMIDADE", Value="2" },

            }, "Value", "Text");
            return View(solicitacao);
        }


        [Route("funcionario/cracha/registrar-impressao/{id?}")]
        public ActionResult RegistrarCracha(int? id)
        {
            ViewBag.Title = "Registrar Impressão do Crachá";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionarioSolicitarCrachaModelView solicitacao = new FuncionarioSolicitarCrachaModelView();
            FuncionarioSolicitarCrachaDomainModel domainModel = funcionarioBusiness.GetSolicitacaoCrachaByID(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, solicitacao);
            return View(solicitacao);

        }

        [HttpPost, Route("funcionario/cracha/registrar-impressao/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarCracha(FuncionarioSolicitarCrachaModelView solicitacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FuncionarioSolicitarCrachaDomainModel _domainModel = funcionarioBusiness.GetSolicitacaoCrachaByID(solicitacao.FUNCRC_ID);
                    _domainModel.FUNCRC_DATAEMISSAO = DateTime.Now;
                    _domainModel.FUNCRC_SITUACAO = (int)FuncionarioSolicitarCrachaModelView.Situacao.Impresso;
                    _domainModel.FUNCRC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    if (_domainModel.FUNCRC_TIPO == (int)FuncionarioSolicitarCrachaModelView.Tipo.PROXIMIDADE)
                        _domainModel.FUNCRC_NUMERO = solicitacao.FUNCRC_NUMERO;


                    if (funcionarioBusiness.AddUpdateSolicitacaoCracha(_domainModel) && funcionarioBusiness.Salvar())
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso();
                        return RedirectToAction("CrachaPendente");
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new InvalidOperationException();
            }
            catch (Exception e)
            {
                TempData["msgInfo"] = e.Message;
                solicitacao.FUNCRC_DATASOLICITACAO = DateTime.Now;
                return View(solicitacao);

            }

        }

        // GET: Conselho/Delete/5
        [Route("funcionario/cracha/cancelar/{id?}")]
        public ActionResult CancelarSolicitacao(int? id)
        {
            ViewBag.Title = "Cancelar Solicitação de Crachá";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionarioSolicitarCrachaModelView solicitacao = new FuncionarioSolicitarCrachaModelView();
            FuncionarioSolicitarCrachaDomainModel domainModel = funcionarioBusiness.GetSolicitacaoCrachaByID(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, solicitacao);

            return View(solicitacao);
        }


        [HttpPost, Route("funcionario/cracha/cancelar/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelarSolicitacaoConfirmed(int id)
        {
            FuncionarioSolicitarCrachaDomainModel domainModel = new FuncionarioSolicitarCrachaDomainModel();
            try
            {
                domainModel = funcionarioBusiness.GetSolicitacaoCrachaByID(id);
                if (domainModel != null)
                {
                    domainModel.FUNCRC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    funcionarioBusiness.DeleteSolicitacaoCracha(domainModel);

                    if (funcionarioBusiness.Salvar())
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso().ToString();
                        return RedirectToAction("SolicitacaoIndex");
                    }
                    else
                        throw new InvalidOperationException();
                }
                else
                    throw new InvalidOperationException();


            }
            catch (Exception e)
            {
                TempData["msgError"] = "Ocorreu um erro na sua operação. </br> " + e.Message;
                return View();
            }

        }


        [Route("funcionario/cracha/editar-nome/{id?}")]
        public ActionResult NomeCracha(int id)
        {
            ViewBag.Title = "Editar Nome do Crachá";
            FuncionarioNomeCrachaModelView funcionario = new FuncionarioNomeCrachaModelView();
            Mapper.Map(funcionarioBusiness.GetNomeCrachaByFuncionario(id), funcionario);
            return View(funcionario);
        }

        [HttpPost, Route("funcionario/cracha/editar-nome/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult NomeCracha(FuncionarioNomeCrachaModelView nomeCracha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FuncionarioNomeCrachaDomainModel _domainModel = new FuncionarioNomeCrachaDomainModel();
                    Mapper.Map(nomeCracha, _domainModel);
                    if (funcionarioBusiness.EditarNomeCracha(_domainModel))
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso();
                        return RedirectToAction("SolicitacaoIndex");
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new InvalidOperationException();



            }
            catch (Exception e)
            {
                TempData["msgInfo"] = "Ocorreu um erro na sua operação. </br> " + e.Message;
                return View();
            }


        }


        // GET: Funcionario
        public ActionResult ListCracha(int FUN_ID)
        {
            ViewBag.Title = "Histórico de Solicitação";
            List<FuncionarioSolicitarCrachaModelView> solicitacao = new List<FuncionarioSolicitarCrachaModelView>();
            Mapper.Map(funcionarioBusiness.LoadCrachaByParameters(FUN_ID, null, null, null, null, null, null, null, null, null, null, null), solicitacao);
            return PartialView(solicitacao);
        }

        public ActionResult SolicitarCracha(int FUN_ID)
        {
            ViewBag.Title = "Nova Solicitação";
            FuncionarioSolicitarCrachaModelView domainModel = new FuncionarioSolicitarCrachaModelView();

            domainModel.FUNCRC_DATASOLICITACAO = DateTime.Today;
            domainModel.FUNCRC_SITUACAO = (int)FuncionarioSolicitarCrachaModelView.Situacao.Pendente;
            ViewBag.VNC_ID = new SelectList(vinculoBusiness.ddlVinculoByFuncionario(FUN_ID), "VNC_ID", "VNC_NOME");
            ViewBag.FUNCRC_TIPO = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "IDENTIFICAÇÃO", Value="1" },
                new SelectListItem {Text = "PROXIMIDADE", Value="2" },

            }, "Value", "Text"); return PartialView(domainModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SolicitarCracha(FuncionarioSolicitarCrachaModelView solicitacao)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    solicitacao.FUNCRC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    FuncionarioSolicitarCrachaDomainModel _domainModel = new FuncionarioSolicitarCrachaDomainModel();
                    Mapper.Map(solicitacao, _domainModel);
                    var result = funcionarioBusiness.AddUpdateSolicitacaoCracha(_domainModel);
                    if (funcionarioBusiness.Salvar())
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



        public ActionResult GerarCracha(int id)
        {
            try
            {
                var viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/InfraRelatorio/Reports/CrachaServidor.rdlc");

                List<FuncionarioEmitirCrachaDomainModel> FunCracha = funcionarioBusiness.GetDadosCracha(id);

                if (FunCracha.Count > 0)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("dsCracha", FunCracha));
                    viewer.SizeToReportContent = true;
                    viewer.ZoomMode = ZoomMode.Percent;
                    viewer.ZoomPercent = 150;
                    string reportType = "PDF";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    string deviceInfo =
                        "<DeviceInfo>" +
                        "<OutputFormat>" + "PDF" + "</OutputFormat>" +
                        "<PageWidth>5,7cm</PageWidth>" +
                        "<PageHeight>8,5cm</PageHeight>" +
                        "<MarginTop>0.0in</MarginTop>" +
                        "<MarginLeft>0.0in</MarginLeft>" +
                        "<MarginRight>0.0in</MarginRight>" +
                        "<MarginBottom>0.0in</MarginBottom>" +
                        "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;

                    var renderedBytes = viewer.LocalReport.Render(
                       reportType,
                       deviceInfo,
                       out mimeType,
                       out encoding,
                       out fileNameExtension,
                       out streams,
                       out warnings
                   );

                    return File(renderedBytes, mimeType);
                }
                throw new System.Exception();
            }
            catch (System.Exception e)
            {
                throw new System.Exception(e.Message);
            }
        }


        #endregion


        #region [RELATORIOS]



        [Route("funcionario/relatorio")]
        public ActionResult RelatorioServidor()
        {
            FuncionarioPesquisaModelView pesquisa = new FuncionarioPesquisaModelView();
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
            ViewBag.Tipo = new SelectList(vinculoBusiness.GetAllTipoVinculo(), "VNCTP_ID", "VNCTP_DESCRICAO");
            ViewBag.Situacao = new SelectList(vinculoBusiness.GetAllVinculoSituacao(), "VNCST_ID", "VNCST_DESCRICAO");
            ViewBag.Beneficio = new SelectList(beneficioBusiness.GetAllBeneficio(), "BNF_ID", "BNF_DESCRICAO");
            ViewBag.Funcao = new SelectList(funcaoBusiness.GetFuncao(), "FNC_ID", "FNC_NOME");
            pesquisa.SITUACAO = (int)VinculoModelView.Situacao.Ativo;
            return View(pesquisa);
        }

        //Modificado por Angelo Matos em 16012020
        [HttpPost]
        public JsonResult InterfacePesquisaRelatorio(
            ParametrosPaginacao paginacao, string pMat, string pNome, int?[] pCrg, int?[] pUnd, int?[] pVNCTP, int?[] pVNCST,
            string pSexo, int? pIdade, int?[] pFuncao, DateTime? pInicio_Periodo_Admissao, DateTime? pFim_Periodo_Admissao, int?[] pBNF,
            string[] pTextoUnidade, string[] pTextoCargo, string[] pTextoTipo, string[] pTextoSituacao, string[] pTextoBeneficio, string[] pTextoFuncao,
            DateTime? pInicio_Periodo_Demissao, DateTime? pFim_Periodo_Demissao
            )
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = new CamposFuncionarioBuscaRelatorioModelView
            {
                MATRICULA = pMat,
                NOME = pNome,
                CARGO = pCrg,
                TEXTO_CARGO = pTextoCargo,
                UNIDADE = pUnd,
                TEXTO_UNIDADE = pTextoUnidade,
                TIPO_VINCULO = pVNCTP,
                TEXTO_TIPO_VINCULO = pTextoTipo,
                SITUACAO_VINCULO = pVNCST,
                TEXTO_SITUACAO_VINCULO = pTextoSituacao,
                BENEFICIO = pBNF,
                TEXTO_BENEFICIO = pTextoBeneficio,
                SEXO = pSexo,
                IDADE = pIdade,
                INICIO_PERIODO_ADMISSAO = pInicio_Periodo_Admissao,
                FIM_PERIODO_ADMISSAO = pFim_Periodo_Admissao,
                FUNCAO = pFuncao,
                TEXTO_FUNCAO = pTextoFuncao,
                INICIO_PERIODO_DEMISSAO = pInicio_Periodo_Demissao,
                FIM_PERIODO_DEMISSAO = pFim_Periodo_Demissao
            };

            TempData["Cfunc"] = Cfunc;

            var _list = funcionarioBusiness.GetFuncionarioPageByParametersRelatorio(pNome, pMat, pUnd, pVNCTP, pVNCST, pCrg, pBNF, 0, 0, pSexo, pIdade, pFuncao, pInicio_Periodo_Admissao, pFim_Periodo_Admissao, pInicio_Periodo_Demissao, pFim_Periodo_Demissao);

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

        //Adicionado por Angelo Matos em 13012020
        public ActionResult GerarRelatorioServidorXLSX()
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            try
            {
                List<FuncionarioRelatorioDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorio(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.TIPO_VINCULO, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, Cfunc.BENEFICIO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.FUNCAO, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO);
                List<FuncionarioRelatorioModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioModelView>();
                Mapper.Map(domainModel, funcionarioRelatorioModelView);
                string[] headers = new string[] { "MATRICULA", "NOME", "UNIDADE", "CARGO", "ADMISSÃO", "DEMISSÃO" };


                RelatorioServidorEmExcel relatorio = new RelatorioServidorEmExcel(headers.ToList(), funcionarioRelatorioModelView, Cfunc);
                return File(relatorio.CriarRelatorio(), "application/vnd.ms-excel", string.Format("Relatório_Servidor_{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy")));

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

        //Modificado por Angelo Matos em 20012020
        public async Task<ActionResult> GerarRelatorioServidor()
        {
            string UnidadeText = "";
            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            try
            {
                ViewAsPdf pdfResult = null;
                int cont = 0;
                List<FuncionarioRelatorioDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorio(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.TIPO_VINCULO, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, Cfunc.BENEFICIO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.FUNCAO, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO);
                List<FuncionarioRelatorioModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioModelView>();
                foreach (FuncionarioRelatorioDomainModel funcionarioRelatorioDM in domainModel)
                {
                    FuncionarioRelatorioModelView funcionarioRelatorioMV = new FuncionarioRelatorioModelView();
                    Mapper.Map(funcionarioRelatorioDM, funcionarioRelatorioMV);
                    UnidadeText += string.Format("{0} - ", funcionarioRelatorioDM.FUN_UNIDADE);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    funcionarioRelatorioModelView.Add(funcionarioRelatorioMV);

                    pdfResult = new ViewAsPdf("RelatorioServidorPdf" + "")
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
                TempData["RelatorioServidor"] = funcionarioRelatorioModelView;
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

        [Route("funcionario/relatorioFuncao")]
        public ActionResult RelatorioServidorFuncao()
        {
            FuncionarioPesquisaModelView pesquisa = new FuncionarioPesquisaModelView();
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
            ViewBag.Tipo = new SelectList(vinculoBusiness.GetAllTipoVinculo(), "VNCTP_ID", "VNCTP_DESCRICAO");
            ViewBag.Situacao = new SelectList(vinculoBusiness.GetAllVinculoSituacao().Where(x => x.VNCST_ID > 3), "VNCST_ID", "VNCST_DESCRICAO");
            ViewBag.Funcao = new SelectList(funcaoBusiness.GetFuncao(), "FNC_ID", "FNC_NOME");
            pesquisa.SITUACAO = (int)VinculoModelView.Situacao.Ativo;
            return View(pesquisa);
        }

        [HttpPost]
        public JsonResult InterfacePesquisaRelatorioFuncao(
            ParametrosPaginacao paginacao, string pMat, string pNome, int?[] pCrg, int?[] pUnd, int?[] pVNCST,
            string pSexo, int? pIdade, int?[] pFuncao, DateTime? pInicio_Periodo_Admissao, DateTime? pFim_Periodo_Admissao, string[] pTextoUnidade,
            string[] pTextoCargo, string[] pTextoSituacao, string[] pTextoFuncao, DateTime? pInicioFuncao, DateTime? pFimFuncao,
            DateTime? pInicio_Periodo_Demissao, DateTime? pFim_Periodo_Demissao
        )
        {

            CamposFuncionarioBuscaRelatorioModelView Cfunc = new CamposFuncionarioBuscaRelatorioModelView
            {
                MATRICULA = pMat,
                NOME = pNome,
                CARGO = pCrg,
                TEXTO_CARGO = pTextoCargo,
                UNIDADE = pUnd,
                TEXTO_UNIDADE = pTextoUnidade,
                SITUACAO_VINCULO = pVNCST,
                TEXTO_SITUACAO_VINCULO = pTextoSituacao,
                SEXO = pSexo,
                IDADE = pIdade,
                INICIO_PERIODO_ADMISSAO = pInicio_Periodo_Admissao,
                FIM_PERIODO_ADMISSAO = pFim_Periodo_Admissao,
                INICIO_PERIODO_DEMISSAO = pInicio_Periodo_Demissao,
                FIM_PERIODO_DEMISSAO = pFim_Periodo_Demissao,
                FUNCAO = pFuncao,
                TEXTO_FUNCAO = pTextoFuncao,
                FUNCAO_DATA_INICIO = pInicioFuncao,
                FUNCAO_DATA_FIM = pFimFuncao
            };

            TempData["CFunc"] = Cfunc;

            var _list = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioFuncao(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.FUNCAO, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO, Cfunc.FUNCAO_DATA_INICIO, Cfunc.FUNCAO_DATA_FIM);

            int tot = _list.Count;

            int totFiltrado = _list.Count;

            var dadosPaginados = _list.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();

            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }

        //Adicionado por Angelo Matos em 21012020
        public ActionResult GerarRelatorioServidorFuncaoXLSX()
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            try
            {
                List<FuncionarioRelatorioFuncaoDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioFuncao(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.FUNCAO, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO, Cfunc.FUNCAO_DATA_INICIO, Cfunc.FUNCAO_DATA_FIM);
                List<FuncionarioRelatorioFuncaoModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioFuncaoModelView>();
                Mapper.Map(domainModel, funcionarioRelatorioModelView);
                string[] headers = new string[] { "MATRICULA", "NOME", "UNIDADE", "CARGO", "FUNÇÃO", "INÍCIO", "FIM" };


                RelatorioServidorFuncaoEmExcel relatorio = new RelatorioServidorFuncaoEmExcel(headers.ToList(), funcionarioRelatorioModelView, Cfunc);
                return File(relatorio.CriarRelatorio(), "application/vnd.ms-excel", string.Format("Relatório_Servidor_Funcao_{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy")));

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

        public async Task<ActionResult> GerarRelatorioServidorFuncao()
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            string UnidadeText = "";
            try
            {
                ViewAsPdf pdfResult = null;
                int cont = 0;
                List<FuncionarioRelatorioFuncaoDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioFuncao(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.FUNCAO, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO, Cfunc.FUNCAO_DATA_INICIO, Cfunc.FUNCAO_DATA_FIM); ;
                List<FuncionarioRelatorioFuncaoModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioFuncaoModelView>();
                foreach (FuncionarioRelatorioFuncaoDomainModel funcionarioRelatorioDM in domainModel)
                {
                    FuncionarioRelatorioFuncaoModelView funcionarioRelatorioMV = new FuncionarioRelatorioFuncaoModelView();
                    Mapper.Map(funcionarioRelatorioDM, funcionarioRelatorioMV);
                    UnidadeText += string.Format("{0} - ", funcionarioRelatorioDM.FUN_UNIDADE);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    funcionarioRelatorioModelView.Add(funcionarioRelatorioMV);

                    pdfResult = new ViewAsPdf("RelatorioServidorFuncaoPdf" + "")
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
                TempData["RelatorioServidor"] = funcionarioRelatorioModelView;
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

        [Route("funcionario/relatorioBeneficio")]
        public ActionResult RelatorioServidorBeneficio()
        {
            FuncionarioPesquisaModelView pesquisa = new FuncionarioPesquisaModelView();
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
            ViewBag.Tipo = new SelectList(vinculoBusiness.GetAllTipoVinculo(), "VNCTP_ID", "VNCTP_DESCRICAO");
            ViewBag.Situacao = new SelectList(vinculoBusiness.GetAllVinculoSituacao().Where(x => x.VNCST_ID > 3), "VNCST_ID", "VNCST_DESCRICAO");
            ViewBag.Beneficio = new SelectList(beneficioBusiness.GetAllBeneficio(), "BNF_ID", "BNF_DESCRICAO");
            pesquisa.SITUACAO = (int)VinculoModelView.Situacao.Ativo;
            return View(pesquisa);
        }

        [HttpPost]
        public JsonResult InterfacePesquisaRelatorioBeneficio(
            ParametrosPaginacao paginacao, string pMat, string pNome, int?[] pCrg, int?[] pUnd, int?[] pVNCST,
            int?[] pBNF, string pSexo, int? pIdade, DateTime? pInicio_Periodo_Admissao, DateTime? pFim_Periodo_Admissao, string[] pTextoUnidade,
            string[] pTextoCargo, string[] pTextoSituacao, string[] pTextoBeneficio, DateTime? pBeneficioInicio, DateTime? pBeneficioFim, DateTime? pInicio_Periodo_Demissao,
            DateTime? pFim_Periodo_Demissao
        )
        {

            CamposFuncionarioBuscaRelatorioModelView Cfunc = new CamposFuncionarioBuscaRelatorioModelView
            {
                MATRICULA = pMat,
                NOME = pNome,
                CARGO = pCrg,
                TEXTO_CARGO = pTextoCargo,
                UNIDADE = pUnd,
                TEXTO_UNIDADE = pTextoUnidade,
                SITUACAO_VINCULO = pVNCST,
                TEXTO_SITUACAO_VINCULO = pTextoSituacao,
                SEXO = pSexo,
                IDADE = pIdade,
                INICIO_PERIODO_ADMISSAO = pInicio_Periodo_Admissao,
                FIM_PERIODO_ADMISSAO = pFim_Periodo_Admissao,
                INICIO_PERIODO_DEMISSAO = pInicio_Periodo_Demissao,
                FIM_PERIODO_DEMISSAO = pFim_Periodo_Demissao,
                BENEFICIO = pBNF,
                TEXTO_BENEFICIO = pTextoBeneficio,
                BENEFICIO_DATA_INICIO = pBeneficioInicio,
                BENEFICIO_DATA_FIM = pBeneficioFim
            };

            TempData["CFunc"] = Cfunc;

            var _list = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioBeneficio(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, Cfunc.BENEFICIO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO, Cfunc.BENEFICIO_DATA_INICIO, Cfunc.BENEFICIO_DATA_FIM);

            int tot = _list.Count;

            int totFiltrado = _list.Count;

            var dadosPaginados = _list.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();

            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }

        //Adicionado por Angelo Matos em 21012020
        public ActionResult GerarRelatorioServidorBeneficioXLSX()
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            try
            {
                List<FuncionarioRelatorioBeneficioDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioBeneficio(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, Cfunc.BENEFICIO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO, Cfunc.BENEFICIO_DATA_INICIO, Cfunc.BENEFICIO_DATA_FIM);
                List<FuncionarioRelatorioBeneficioModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioBeneficioModelView>();
                Mapper.Map(domainModel, funcionarioRelatorioModelView);
                string[] headers = new string[] { "MATRICULA", "NOME", "UNIDADE", "CARGO", "FUNÇÃO", "INÍCIO", "FIM" };


                RelatorioServidorBeneficioEmExcel relatorio = new RelatorioServidorBeneficioEmExcel(headers.ToList(), funcionarioRelatorioModelView, Cfunc);
                return File(relatorio.CriarRelatorio(), "application/vnd.ms-excel", string.Format("Relatório_Servidor_Beneficio_{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy")));

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

        public async Task<ActionResult> GerarRelatorioServidorBeneficio()
        {

            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;


            string UnidadeText = "";
            try
            {
                ViewAsPdf pdfResult = null;
                int cont = 0;
                List<FuncionarioRelatorioBeneficioDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioBeneficio(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, Cfunc.BENEFICIO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.INICIO_PERIODO_DEMISSAO, Cfunc.FIM_PERIODO_DEMISSAO, Cfunc.BENEFICIO_DATA_INICIO, Cfunc.BENEFICIO_DATA_FIM);
                List<FuncionarioRelatorioBeneficioModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioBeneficioModelView>();
                foreach (FuncionarioRelatorioBeneficioDomainModel funcionarioRelatorioDM in domainModel)
                {
                    FuncionarioRelatorioBeneficioModelView funcionarioRelatorioMV = new FuncionarioRelatorioBeneficioModelView();
                    Mapper.Map(funcionarioRelatorioDM, funcionarioRelatorioMV);
                    UnidadeText += string.Format("{0} - ", funcionarioRelatorioDM.FUN_UNIDADE);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    funcionarioRelatorioModelView.Add(funcionarioRelatorioMV);

                    pdfResult = new ViewAsPdf("RelatorioServidorBeneficioPdf" + "")
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
                TempData["RelatorioServidor"] = funcionarioRelatorioModelView;
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

        //Adicionado por Angelo Matos em 22012020
        [Route("funcionario/relatorioAposentadoria")]
        public ActionResult RelatorioServidorAposentadoria()
        {
            FuncionarioPesquisaModelView pesquisa = new FuncionarioPesquisaModelView();
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
            ViewBag.Tipo = new SelectList(vinculoBusiness.GetAllTipoVinculo(), "VNCTP_ID", "VNCTP_DESCRICAO");
            ViewBag.Situacao = new SelectList(vinculoBusiness.GetAllVinculoSituacao(), "VNCST_ID", "VNCST_DESCRICAO");
            ViewBag.Beneficio = new SelectList(beneficioBusiness.GetAllBeneficio(), "BNF_ID", "BNF_DESCRICAO");
            ViewBag.TipoAposentadoria = new SelectList(Enum.GetValues(typeof(TipoAposentadoria)).Cast<TipoAposentadoria>().Select(x => new SelectListItem { Text = x.Descricao(), Value = ((int)x).ToString() }), "Value", "Text");

            pesquisa.SITUACAO = (int)VinculoModelView.Situacao.AguardandoAposentadoria;
            return View(pesquisa);
        }

        //Adicionado por Angelo Matos em 22012020
        [HttpPost]
        public JsonResult InterfacePesquisaRelatorioAposentadoria(
            ParametrosPaginacao paginacao, string pMat, string pNome, int?[] pCrg, int?[] pUnd, int?[] pVNCST, string pSexo, int? pIdade,
            string[] pTextoUnidade, DateTime? pInicio_Periodo_Admissao, DateTime? pFim_Periodo_Admissao, string[] pTextoCargo, string[] pTextoSituacao, DateTime? pEntradaAposentadoriaInicio, DateTime? pEntradaAposentadoriaFim, int?[] pTipoAposentadoria, string[] pTextoTipoAposentadoria
        )
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = new CamposFuncionarioBuscaRelatorioModelView
            {
                MATRICULA = pMat,
                NOME = pNome,
                CARGO = pCrg,
                TEXTO_CARGO = pTextoCargo,
                UNIDADE = pUnd,
                TEXTO_UNIDADE = pTextoUnidade,
                SITUACAO_VINCULO = pVNCST,
                TEXTO_SITUACAO_VINCULO = pTextoSituacao,
                SEXO = pSexo,
                IDADE = pIdade,
                INICIO_PERIODO_ADMISSAO = pInicio_Periodo_Admissao,
                FIM_PERIODO_ADMISSAO = pFim_Periodo_Admissao,
                ENTRADA_APOSENTADORIA_DATA_INICIO = pEntradaAposentadoriaInicio,
                ENTRADA_APOSENTADORIA_DATA_FIM = pEntradaAposentadoriaFim,
                TIPO_APOSENTADORIA = pTipoAposentadoria,
                TEXTO_TIPO_APOSENTADORIA = pTextoTipoAposentadoria
            };

            TempData["CFunc"] = Cfunc;

            var _list = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioAposentadoria(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.ENTRADA_APOSENTADORIA_DATA_INICIO, Cfunc.ENTRADA_APOSENTADORIA_DATA_FIM, Cfunc.TIPO_APOSENTADORIA);
            int tot = _list.Count;

            int totFiltrado = _list.Count;

            var dadosPaginados = _list.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();

            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }

        //Adicionado por Angelo Matos em 22012020
        public ActionResult GerarRelatorioServidorAposentadoriaXLSX()
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            try
            {
                List<FuncionarioRelatorioAposentadoriaDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioAposentadoria(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.ENTRADA_APOSENTADORIA_DATA_INICIO, Cfunc.ENTRADA_APOSENTADORIA_DATA_FIM, Cfunc.TIPO_APOSENTADORIA);
                List<FuncionarioRelatorioAposentadoriaModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioAposentadoriaModelView>();
                Mapper.Map(domainModel, funcionarioRelatorioModelView);
                string[] headers = new string[] { "MATRICULA", "NOME", "UNIDADE", "CARGO", "SITUAÇÃO", "TIPO APOSENTADORIA", "DATA ENTRADA" };

                RelatorioServidorAposentadoriaEmExcel relatorio = new RelatorioServidorAposentadoriaEmExcel(headers.ToList(), funcionarioRelatorioModelView, Cfunc);
                return File(relatorio.CriarRelatorio(), "application/vnd.ms-excel", string.Format("Relatório_Servidor_Aposentadoria_{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy")));

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

        public async Task<ActionResult> GerarRelatorioServidorAposentadoria()
        {
            CamposFuncionarioBuscaRelatorioModelView Cfunc = TempData["Cfunc"] as CamposFuncionarioBuscaRelatorioModelView;

            string UnidadeText = "";
            try
            {
                ViewAsPdf pdfResult = null;
                int cont = 0;
                List<FuncionarioRelatorioAposentadoriaDomainModel> domainModel = funcionarioBusiness.GetFuncionarioPageByParametersRelatorioAposentadoria(Cfunc.NOME, Cfunc.MATRICULA, Cfunc.UNIDADE, Cfunc.SITUACAO_VINCULO, Cfunc.CARGO, 0, 0, Cfunc.SEXO, Cfunc.IDADE, Cfunc.INICIO_PERIODO_ADMISSAO, Cfunc.FIM_PERIODO_ADMISSAO, Cfunc.ENTRADA_APOSENTADORIA_DATA_INICIO, Cfunc.ENTRADA_APOSENTADORIA_DATA_FIM, Cfunc.TIPO_APOSENTADORIA);
                List<FuncionarioRelatorioAposentadoriaModelView> funcionarioRelatorioModelView = new List<FuncionarioRelatorioAposentadoriaModelView>();
                foreach (FuncionarioRelatorioAposentadoriaDomainModel funcionarioRelatorioDM in domainModel)
                {
                    FuncionarioRelatorioAposentadoriaModelView funcionarioRelatorioMV = new FuncionarioRelatorioAposentadoriaModelView();
                    Mapper.Map(funcionarioRelatorioDM, funcionarioRelatorioMV);
                    UnidadeText += string.Format("{0} - ", funcionarioRelatorioDM.FUN_UNIDADE);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    funcionarioRelatorioModelView.Add(funcionarioRelatorioMV);

                    pdfResult = new ViewAsPdf("RelatorioServidorAposentadoriaPdf" + "")
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
                TempData["RelatorioServidor"] = funcionarioRelatorioModelView;
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

        #endregion



        public ActionResult Consulta()
        {

            FuncionarioPesquisaModelView pesquisa = new FuncionarioPesquisaModelView();
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
            ViewBag.Tipo = new SelectList(vinculoBusiness.GetAllTipoVinculo(), "VNCTP_ID", "VNCTP_DESCRICAO");
            ViewBag.Situacao = new SelectList(vinculoBusiness.GetAllVinculoSituacao(), "VNCST_ID", "VNCST_DESCRICAO");
            ViewBag.Beneficio = new SelectList(beneficioBusiness.GetAllBeneficio(), "BNF_ID", "BNF_DESCRICAO");
            pesquisa.SITUACAO = (int)VinculoModelView.Situacao.Ativo;
            return View(pesquisa);
        }

        [HttpPost]
        public JsonResult InterfacePesquisa(ParametrosPaginacao paginacao, string pMat, string pNome, int? pCrg, int? pUnd, int? pVNCTP, int? pVNCST, int? pBNF)
        {
            var _list = funcionarioBusiness.GetFuncionarioPageByParameters(pNome, pMat, pUnd, pVNCTP, pVNCST, pCrg, pBNF, null, 0, 0);

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

            //int tot;
            //int totFiltrado;
            //List<FuncionarioIndexDomainModel> _list = funcionarioBusiness.GetFuncionarioPageByParameters(pNome, pMat, pUnd, pVNCTP, pVNCST, pCrg, pBNF, null,paginacao.Start,paginacao.Length, out tot, out totFiltrado);

            ////var dadosPaginados = _list.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();

            //var dadosPaginados = _list.ToList();
            //return Json(new
            //{
            //    data = _list,
            //    draw = paginacao.RowCount,
            //    recordsTotal = tot,
            //    recordsFiltered = totFiltrado
            //}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExistFuncionario(string fun_matricula, int fun_id)
        {
            return Json(!funcionarioBusiness.ExistFuncionario(fun_matricula, fun_id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cadastro(int id = 0)
        {
            FuncionarioModelView _funcionario = new FuncionarioModelView();
            if (id == 0)
            {
                ViewBag.Title = "Novo Funcionario";
                ViewBag.BNC_ID = new SelectList(bancoBusiness.GetBanco(), "BNC_ID", "BNC_DESCRICAO");
                string path = Server.MapPath("~/Imagem/DefaultServidor.png");
                byte[] imagemByteDados = System.IO.File.ReadAllBytes(path);
                _funcionario.FUN_FOTO = imagemByteDados;

            }
            else
            {
                ViewBag.Title = "Editar Funcionario";
                FuncionarioDomainModel funcionarioDomainModel = funcionarioBusiness.GetFuncionarioById(id);
                if (funcionarioDomainModel == null)
                {
                    return HttpNotFound();

                }

                Mapper.Map(funcionarioDomainModel, _funcionario);
                ViewBag.BNC_ID = new SelectList(bancoBusiness.GetBanco(), "BNC_ID", "BNC_DESCRICAO", _funcionario.BNC_ID);



            }
            ViewBag.FUN_ID = id;
            return View(_funcionario);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(FuncionarioModelView funcionario)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    funcionario.FUN_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    FuncionarioDomainModel _domainModel = new FuncionarioDomainModel();


                    if (funcionario.FOTO != null)
                    {
                        WebImage image = new WebImage(funcionario.FOTO.InputStream);
                        funcionario.FUN_FOTO = image.GetBytes();
                    }
                    Mapper.Map(funcionario, _domainModel);

                    var result = funcionarioBusiness.AddUpdateFuncionario(_domainModel);
                    if (funcionario.FUN_ID == 0)
                        return Json(new { resultado = true, tipomsg = "", msg = "Cadastrado com sucesso", FUN_ID = result.FUN_ID }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = true, edit = true, tipomsg = "", msg = "Registro atualizado com sucesso!", FUN_ID = funcionario.FUN_ID }, JsonRequestBehavior.AllowGet);




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


        public JsonResult BuscarCep(string cep)
        {
            var resposta = funcionarioBusiness.buscarCep(cep);
            return Json(new { resultado = true, endereco = resposta.FUN_ENDERECO, bairro = resposta.FUN_BAIRRO, cidade = resposta.FUN_MUNICIPIO }, JsonRequestBehavior.AllowGet);

        }
    }
}
