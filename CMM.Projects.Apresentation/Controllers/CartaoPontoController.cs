using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Utils;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using Microsoft.Reporting.WebForms;
using Rotativa.MVC;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class CartaoPontoController : Controller
    {
        readonly infraMessage msg = new infraMessage();
        private readonly ICargoBusiness cargoBusiness;
        private readonly IVinculoBusiness vinculoBusiness;
        private readonly IUnidadeBusiness unidadeBusiness;
        private readonly ICartaoPontoBusiness cartaoPontoBusiness;


        public CartaoPontoController(ICargoBusiness _cargoBusiness, IVinculoBusiness _vinculoBusiness, IUnidadeBusiness _unidadeBusiness, ICartaoPontoBusiness _cartaoPontoBusiness)
        {
            cargoBusiness = _cargoBusiness;
            vinculoBusiness = _vinculoBusiness;
            unidadeBusiness = _unidadeBusiness;
            cartaoPontoBusiness = _cartaoPontoBusiness;
        }

        // GET: CartaoPonto
        public ActionResult Unidade()
        {

            // var card = cartaoPontoBusiness.BuscarCartaoPontoPorUnidadeFeriadoFimDeSemana(76, DateTime.Parse("01/05/2019"));

            UnidadeCartaoPontoModelView modelView = new UnidadeCartaoPontoModelView();
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");

            return View(modelView);
        }

        //Adicionado por Angelo Matos em 27022020
        public ActionResult UnidadeFalta()
        {
            UnidadeCartaoPontoFaltasModelView modelView = new UnidadeCartaoPontoFaltasModelView();
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");

            return View(modelView);
        }

        [Route("CartaoPonto/Unidade/Plantao")]
        public ActionResult UnidadePlantao()
        {

            // var card = cartaoPontoBusiness.BuscarCartaoPontoPorUnidadeFeriadoFimDeSemana(76, DateTime.Parse("01/05/2019"));

            UnidadeCartaoPontoModelView modelView = new UnidadeCartaoPontoModelView();
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");

            return View(modelView);
        }



        // GET: CartaoPonto 
        // Servidores
        public ActionResult Index()
        {
            PesquisaCartaoPontoModelView pesquisa = new PesquisaCartaoPontoModelView();
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");


            if (TempData["ParametrosPesquisa"] != null)
            {
                pesquisa = ((PesquisaCartaoPontoModelView)TempData["ParametrosPesquisa"]);
            }
            return View(pesquisa);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterfacePesquisa(PesquisaCartaoPontoModelView pesquisa)
        {
            PesquisaCartaoPontoDomainModel pesquisaDomain = new PesquisaCartaoPontoDomainModel();
            List<PesquisaCartaoPontoDomainModel> pesquisaListDomain = new List<PesquisaCartaoPontoDomainModel>();
            List<PesquisaCartaoPontoModelView> pesquisaListModelView = new List<PesquisaCartaoPontoModelView>();
            Mapper.Map(pesquisa, pesquisaDomain);

            pesquisaListDomain = cartaoPontoBusiness.GetPesquisa(pesquisaDomain);
            Mapper.Map(pesquisaListDomain, pesquisaListModelView);
            TempData["ListFuncionario"] = pesquisaListModelView;

            TempData["ParametrosPesquisa"] = pesquisa;
            return RedirectToAction("Index");
        }


        // GET: CartaoPonto/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Title = "Cartão Ponto Servidor";
            FuncionarioCartaoPontoModelView lFuncionario = new FuncionarioCartaoPontoModelView();
            if (id == null && TempData["FuncionarioCartaoPonto"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (TempData["FuncionarioCartaoPonto"] == null)
            {
                FuncionarioCartaoPontoDomainModel domainModel = await cartaoPontoBusiness.BuscarCartaoPontoPorFuncionario(id.Value, null, null);
                Mapper.Map(domainModel, lFuncionario);
            }
            else
            {
                lFuncionario = ((FuncionarioCartaoPontoModelView)TempData["FuncionarioCartaoPonto"]);
                TempData["ImprimirCartao"] = lFuncionario;
            }
            return View(lFuncionario);
        }



        public async Task<ActionResult> GerarCartaoPonto(DateTime? inicio, DateTime? fim, int? fun_id)
        {
            FuncionarioCartaoPontoModelView lFuncionario = new FuncionarioCartaoPontoModelView();

            FuncionarioCartaoPontoDomainModel domainModel = await cartaoPontoBusiness.BuscarCartaoPontoPorFuncionario(fun_id.Value, inicio, fim);
            Mapper.Map(domainModel, lFuncionario);
            TempData["FuncionarioCartaoPonto"] = lFuncionario;
            TempData["Inicio"] = inicio.Value.ToShortDateString();
            TempData["Fim"] = fim.Value.ToShortDateString();

            string customSwitches =
            " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                DateTime.Now + "  Pág. [page]/[toPage]\"" +
                " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";


            var pdfResult = new ViewAsPdf("CartaoPontoServidor")
            {
                RotativaOptions = new Rotativa.Core.DriverOptions
                {
                    PageOrientation = Rotativa.Core.Options.Orientation.Portrait,
                    IsGrayScale = false,
                    CustomSwitches = customSwitches,
                    PageSize = Rotativa.Core.Options.Size.A4,


                }
            };

            //return View(pdfResult.ViewName);

            return pdfResult;

            //return RedirectToAction("Details", new { id = fun_id });
        }

        public ActionResult CartaoPontoServidor()
        {
            return View();
        }

        public ActionResult ImprimirCartao()
        {
            try
            {
                var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/InfraRelatorio/Reports/CartaoPontoServidor.rdlc");


                if (TempData["ImprimirCartao"] != null)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ((FuncionarioCartaoPontoModelView)TempData["ImprimirCartao"])));
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ((FuncionarioCartaoPontoModelView)TempData["ImprimirCartao"]).CartaoPonto));
                    viewer.SizeToReportContent = true;


                    string reportType = "PDF";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;



                    string deviceInfo =
                        "<DeviceInfo>" +
                        "<OutputFormat>" + "PDF" + "</OutputFormat>" +
                        "<PageWidth>21cm</PageWidth>" +
                        "<PageHeight>29,7cm</PageHeight>" +
                        "<MarginTop>0.1in</MarginTop>" +
                        "<MarginLeft>0.1in</MarginLeft>" +
                        "<MarginRight>0.1in</MarginRight>" +
                        "<MarginBottom>0.1in</MarginBottom>" +
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
                throw new Exception();
            }
            catch (Exception e)
            {



                /*  TempData["Resultado"] = "false";
                  TempData["Mensagem"] = "Ocorreu um erro no processamento do relátorio. Tente novamente!";
                  return RedirectToAction("Index");*/



                throw new Exception(e.Message);

            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CartaoUnidade(UnidadeCartaoPontoModelView cartaoPonto)
        {
            UnidadeCartaoPontoDomainModel domainModel = new UnidadeCartaoPontoDomainModel();
            List<CartaoPontoUnidadeDomainModel> CartaoUnidadeListDomain = new List<CartaoPontoUnidadeDomainModel>();
            Mapper.Map(cartaoPonto, domainModel);

            CartaoUnidadeListDomain = cartaoPontoBusiness.GetPointCardByUnidade(domainModel);


            try
            {
                var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/InfraRelatorio/Reports/CartaoPonto.rdlc");


                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CartaoUnidadeListDomain));


                viewer.SizeToReportContent = true;


                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =
                    "<DeviceInfo>" +
                    "<OutputFormat>" + "PDF" + "</OutputFormat>" +
                    "<PageWidth>21cm</PageWidth>" +
                    "<PageHeight>29,7cm</PageHeight>" +
                    "<MarginTop>0.1in</MarginTop>" +
                    "<MarginLeft>0.1in</MarginLeft>" +
                    "<MarginRight>0.1in</MarginRight>" +
                    "<MarginBottom>0.1in</MarginBottom>" +
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
            catch (Exception e)
            {



                /*  TempData["Resultado"] = "false";
                  TempData["Mensagem"] = "Ocorreu um erro no processamento do relátorio. Tente novamente!";
                  return RedirectToAction("Index");*/



                throw new Exception(e.Message);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GerarCartaoPontoUnidadeFaltas(int? und_id, DateTime? Periodo_Inicio, DateTime? Periodo_Fim, string radioArquivo)
        {
            try
            {
                int cont = 0;
                string result = "";
                List<UnidadeCartaoPontoFaltasDomainModel> domainModel = await cartaoPontoBusiness.BuscarCartaoPontoPorUnidadeFaltas(und_id.Value, Periodo_Inicio, Periodo_Fim);
                TempData["PeriodoInicio"] = Periodo_Inicio.Value;
                TempData["PeriodoFim"] = Periodo_Fim.Value;
                foreach (UnidadeCartaoPontoFaltasDomainModel unidadeDM in domainModel)
                {
                    UnidadeCartaoPontoFaltasModelView UnidadeMV = new UnidadeCartaoPontoFaltasModelView();
                    Mapper.Map(unidadeDM, UnidadeMV);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    TempData["Unidade"] = UnidadeMV;
                    var pdfResult = new ViewAsPdf("CartaoPontoUnidadeFaltas" + "")
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

                    //return View(pdfResult.ViewName);
                    if (radioArquivo == "A")
                    {
                        try
                        {

                            string caminho = string.Format("{0}{1}\\", UnidadeMV.CaminhoUnidade, Periodo_Inicio.Value.Year);
                            string nomeArquivo = string.Format("Relatorio de Ponto Faltas -{0}-{1}", UnidadeMV.UNIDADE, Periodo_Inicio.Value.ToString("MMMMyyyy").ToUpper() + " - " + Periodo_Fim.Value.ToString("MMMMyyyy").ToUpper());
                            result += SalvaArquivo.SalvaArquivoDiretorio(caminho, pdfResult.BuildPdf(this.ControllerContext), nomeArquivo, ".pdf");
                            result += " </n> ";

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    else
                        return pdfResult;

                }
                return Json(new
                {
                    resultado = true,
                    msg = string.Format("Relatorio gerado com sucesso. Caminho do arquivo: {0}", result),
                }, JsonRequestBehavior.AllowGet);
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GerarCartaoPontoUnidade(int? und_id, DateTime? Periodo, string radioArquivo)
        {
            try
            {
                int cont = 0;
                string result = "";
                List<UnidadeCartaoPontoDomainModel> domainModel = await cartaoPontoBusiness.BuscarCartaoPontoPorUnidade(und_id.Value, Periodo);
                TempData["Periodo"] = Periodo.Value;
                foreach (UnidadeCartaoPontoDomainModel unidadeDM in domainModel)
                {
                    UnidadeCartaoPontoModelView UnidadeMV = new UnidadeCartaoPontoModelView();
                    Mapper.Map(unidadeDM, UnidadeMV);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    TempData["Unidade"] = UnidadeMV;
                    var pdfResult = new ViewAsPdf("CartaoPontoUnidade" + "")
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

                    //return View(pdfResult.ViewName);
                    if (radioArquivo == "A")
                    {
                        try
                        {

                            string caminho = string.Format("{0}{1}\\", UnidadeMV.CaminhoUnidade, Periodo.Value.Year);
                            string nomeArquivo = string.Format("Relatorio de Ponto -{0}-{1}", UnidadeMV.UNIDADE, Periodo.Value.ToString("MMMMyyyy").ToUpper());
                            result += SalvaArquivo.SalvaArquivoDiretorio(caminho, pdfResult.BuildPdf(this.ControllerContext), nomeArquivo, ".pdf");
                            result += " </n> ";

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                        return pdfResult;

                }
                return Json(new
                {
                    resultado = true,
                    msg = string.Format("Relatorio gerado com sucesso. Caminho do arquivo: {0}", result),
                }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GerarCartaoPontoUnidadePlantao(int? und_id, DateTime? Periodo, string radioArquivo)
        {
            try
            {
                int cont = 0;
                string result = "";
                List<UnidadeCartaoPontoDomainModel> domainModel = await cartaoPontoBusiness.BuscarCartaoPontoPorUnidadeFeriadoFimDeSemana(und_id.Value, Periodo);
                TempData["Periodo"] = Periodo.Value;
                foreach (UnidadeCartaoPontoDomainModel unidadeDM in domainModel)
                {
                    UnidadeCartaoPontoModelView UnidadeMV = new UnidadeCartaoPontoModelView();
                    Mapper.Map(unidadeDM, UnidadeMV);
                    string customSwitches =
                    " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                        DateTime.Now + "  Pág. [page]/[toPage]\"" +
                        " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";

                    TempData["Unidade"] = UnidadeMV;

                    //INICIO -> Area de criação da lista de CarataoPontoPlantaoView utilizado na construção da view

                    List<CartaoPontoPlantaoView> CPonto = new List<CartaoPontoPlantaoView>();
                    UnidadeCartaoPontoModelView unidade = (UnidadeCartaoPontoModelView)TempData["Unidade"];
                    foreach (var Funcionarios in unidade.Funcionarios)
                    {
                        foreach (var cartaoPonto in Funcionarios.CartaoPonto.OrderBy(x => x.DATA.Month).ToList())
                        {
                            CartaoPontoPlantaoView cp = new CartaoPontoPlantaoView()
                            {
                                NOME_FUNCIONARIO = Funcionarios.NOME,
                                DATA = cartaoPonto.DATA,
                                ENTRADA = cartaoPonto.ENTRADA1,
                                SAIDA = cartaoPonto.SAIDA1,
                                DIFERENCA = cartaoPonto.Diferenca(),
                                OBSERVACOES = cartaoPonto.Justificativa(),
                                FERIADO = cartaoPonto.FERIADO
                            };
                            CPonto.Add(cp);
                        }
                    }

                    TempData["CartaoPonto"] = CPonto;

                    //FIM -> Area de criação da lista de CarataoPontoPlantaoView utilizado na construção da view

                    var pdfResult = new ViewAsPdf("CartaoPontoUnidadePlantao" + "")
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

                    //return View(pdfResult.ViewName);
                    if (radioArquivo == "A")
                    {
                        try
                        {

                            string caminho = string.Format("{0}{1}\\", UnidadeMV.CaminhoUnidade, Periodo.Value.Year);
                            string nomeArquivo = string.Format("Relatorio de Ponto -{0}-{1}", UnidadeMV.UNIDADE, Periodo.Value.ToString("MMMMyyyy").ToUpper());
                            result += SalvaArquivo.SalvaArquivoDiretorio(caminho, pdfResult.BuildPdf(this.ControllerContext), nomeArquivo, ".pdf");
                            result += " </n> ";

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                        return pdfResult;

                }

                return Json(new
                {
                    resultado = true,
                    msg = string.Format("Relatorio gerado com sucesso. Caminho do arquivo: {0}", result),
                }, JsonRequestBehavior.AllowGet);
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








    }
}
