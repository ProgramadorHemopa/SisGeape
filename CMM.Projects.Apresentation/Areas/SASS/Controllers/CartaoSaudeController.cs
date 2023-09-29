using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeape2.Domain.SASS;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Business.Interface.SASS;
using CMM.Projects.Apresentation.Areas.SASS.Models;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using Newtonsoft.Json;
using Rotativa.MVC;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CMM.Projects.Apresentation.Areas.SASS.Controllers
{
    [Filtros.AutorizarOperadorSASS]
    [RouteArea("SASS")]
    public class CartaoSaudeController : Controller
    {
        infraMessage msg = new infraMessage();

        private readonly ICargoBusiness cargoBusiness;
        private readonly ICartaoSaudeBusiness cartaoSaudeBusiness;
        private readonly IUnidadeBusiness unidadeBusiness;
        private readonly IFuncionarioBusiness funcionarioBusiness;


        public CartaoSaudeController(ICargoBusiness _cargoBusiness, IUnidadeBusiness _unidadeBusiness, IFuncionarioBusiness _funcionarioBusiness, ICartaoSaudeBusiness _cartaoSaudeBusiness)
        {
            cargoBusiness = _cargoBusiness;
            unidadeBusiness = _unidadeBusiness;
            cartaoSaudeBusiness = _cartaoSaudeBusiness;
            funcionarioBusiness = _funcionarioBusiness;
        }


        // GET: SASS/CartaoSaude/Details/5
        public async Task<ActionResult> Details(int id)
        {
            FuncionarioCartaoSaudeModelView funcionario = new FuncionarioCartaoSaudeModelView();
            ViewBag.Title = "Editar Funcionario";
            FuncionarioCartaoSaudeDomainModel domainModel = await cartaoSaudeBusiness.buscarConsultaPorFuncionario(id);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, funcionario);
            TempData["ListAtendimento"] = funcionario.Consulta;
            return View(funcionario);
        }



        //ação responsavel por receber o id do funcionario e renderizar o html que será transformado em PDF
        public async Task<ActionResult> RelatorioCartaoSaude(int? id)
        {
            List<ConsultaModelView> list = new List<ConsultaModelView>();
            List<Object> IMC = new List<Object>();
            List<Object> Glicemia = new List<Object>();
            List<Object> Trigli = new List<Object>();
            List<Object> Colesterol = new List<Object>();
            List<Object> HDL = new List<Object>();
            List<Object> LDL = new List<Object>();
            FuncionarioCartaoSaudeModelView funcionario = new FuncionarioCartaoSaudeModelView();
            FuncionarioCartaoSaudeDomainModel domainModel = await cartaoSaudeBusiness.buscarConsultaPorFuncionario(id.Value);
            Mapper.Map(domainModel, funcionario);
            //select no primeiro atendimento dos ultimos 3 anos.
            var ultimosAnos = funcionario.Consulta.OrderByDescending(x => x.CON_DATA).GroupBy(x => x.CON_DATA.Value.Year).Take(3).Select(z => new { c = z.AsParallel().FirstOrDefault() });
            //for para adicionar os dados em uma lista de cada item
            foreach (var item in ultimosAnos.OrderBy(x => x.c.CON_DATA).ToList())
            {
                list.Add(item.c);
                if (item.c.CON_TRIGLICERIDEOS.HasValue)
                {
                    Trigli.Add(new
                    {
                        label = item.c.CON_DATA.Value.Year,
                        data = item.c.CON_TRIGLICERIDEOS.Value
                    });
                }
                if (item.c.CON_IMC.HasValue)
                {
                    IMC.Add(new
                    {
                        label = item.c.CON_DATA.Value.Year,
                        data = item.c.CON_IMC.Value
                    });
                }
                if (item.c.CON_GLICEMIA.HasValue)
                {
                    Glicemia.Add(new
                    {
                        label = item.c.CON_DATA.Value.Year,
                        data = item.c.CON_GLICEMIA.Value
                    });
                }
                if (item.c.CON_COLESTEROLTOTAL.HasValue)
                {
                    Colesterol.Add(new
                    {
                        label = item.c.CON_DATA.Value.Year,
                        data = item.c.CON_COLESTEROLTOTAL.Value
                    });
                }
                if (item.c.CON_HDL.HasValue)
                {
                    HDL.Add(new
                    {
                        label = item.c.CON_DATA.Value.Year,
                        data = item.c.CON_HDL.Value
                    });
                }
                if (item.c.CON_LDL.HasValue)
                {
                    LDL.Add(new
                    {
                        label = item.c.CON_DATA.Value.Year,
                        data = item.c.CON_LDL.Value
                    });
                }
            }

            //serializando e atribuindo a um tempdata para renderizar no foreach de atendimento e nos graficos da pagina
            TempData["List"] = list;
            TempData["Trigli"] = JsonConvert.SerializeObject(Trigli, Formatting.Indented);
            TempData["IMC"] = JsonConvert.SerializeObject(IMC, Formatting.Indented);
            TempData["Glicemia"] = JsonConvert.SerializeObject(Glicemia, Formatting.Indented);
            TempData["Colesterol"] = JsonConvert.SerializeObject(Colesterol, Formatting.Indented);
            TempData["HDL"] = JsonConvert.SerializeObject(HDL, Formatting.Indented);
            TempData["LDL"] = JsonConvert.SerializeObject(LDL, Formatting.Indented);

            return View(funcionario);
        }
        public ActionResult GerarCartaoSaude(int? id)
        {
            try
            {
                //--javascript - delay 100
                string customSwitches =
                " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                    DateTime.Now + "  Pág. [page]/[toPage]\"" +
                    " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";
                return new ActionAsPdf("RelatorioCartaoSaude", new { id = id.Value })
                {
                    RotativaOptions = new Rotativa.Core.DriverOptions
                    {
                        PageOrientation = Rotativa.Core.Options.Orientation.Portrait,
                        IsGrayScale = false,
                        CustomSwitches = customSwitches,
                        PageSize = Rotativa.Core.Options.Size.A4,
                    }
                };
                //return RedirectToAction("RelatorioCartaoSaude", new { id = id.Value });
            }
            catch (Exception e)
            {
                TempData["msgInfo"] = e.Message;
                return View(Request.UrlReferrer.ToString());
            }
        }

        public ActionResult Gerencial()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Gerencial(int? ano)
        {

            if (ano != null)
            {
                List<ConsultaModelView> listConsulta = new List<ConsultaModelView>();
                Mapper.Map(cartaoSaudeBusiness.ListarTodos().Where(x => x.CON_DATA.Value.Year == ano), listConsulta);
                TempData["Details"] = listConsulta;
                TempData["Ano"] = ano.Value.ToString();

            }


            return View();

        }

        [NonAction]
        private string CalculaPercentual(int valorCalculo, int valorTotal)
        {
            decimal percentual;

            if (valorCalculo == 0 || valorTotal == 0)
            {
                percentual = 0;
            }
            else
            {
                percentual = (Convert.ToDecimal(valorCalculo) / Convert.ToDecimal(valorTotal)) * 100;
            }

            return percentual.ToString("N2");
        }


        public ActionResult RelatorioGerencial(int? ano, bool detalhado = false)
        {

            try
            {

                if (ano == null)
                {
                    ano = DateTime.Today.Year;
                }

                List<ConsultaModelView> listConsulta = new List<ConsultaModelView>();
                Mapper.Map(cartaoSaudeBusiness.ListarTodos().Where(x => x.CON_DATA.Value.Year == ano), listConsulta);
                TempData["Details"] = listConsulta;
                TempData["Ano"] = ano.Value.ToString();


                TempData["%Fuma"] = CalculaPercentual(listConsulta.Count(x => x.CON_FUMA == ConsultaModelView.SimNao.Sim), listConsulta.Count());
                TempData["%Etilismo"] = CalculaPercentual(listConsulta.Count(x => x.CON_ETILISMO == ConsultaModelView.SimNao.Sim), listConsulta.Count());
                TempData["%Diabetico"] = CalculaPercentual(listConsulta.Count(x => x.CON_DIABETICO == ConsultaModelView.SimNao.Sim), listConsulta.Count());
                TempData["%Hipertenso"] = CalculaPercentual(listConsulta.Count(x => x.CON_HIPERTENSO == ConsultaModelView.SimNao.Sim), listConsulta.Count());
                TempData["%Sedentarismo"] = CalculaPercentual(listConsulta.Count(x => x.CON_SEDENTARISMO == ConsultaModelView.SimNao.Sim), listConsulta.Count());
                TempData["%Glicemia"] = CalculaPercentual(listConsulta.Count(x => x.CON_GLICEMIA > 99), listConsulta.Count());
                TempData["%Pressao"] = CalculaPercentual(listConsulta.Count(x => x.CON_PRESSAOARTERIALMAX >= 130 || x.CON_PRESSAOARTERIALMIN <= 85), listConsulta.Count());
                TempData["%Colesterol"] = CalculaPercentual(listConsulta.Count(x => x.CON_COLESTEROLTOTAL >= 200), listConsulta.Count());
                TempData["%HDL"] = CalculaPercentual(listConsulta.Count(x => (x.Funcionario.FUN_SEXO == "M" && x.CON_HDL < 40) || (x.Funcionario.FUN_SEXO == "F" && x.CON_HDL < 50)), listConsulta.Count());
                TempData["%Triglicerideos"] = CalculaPercentual(listConsulta.Count(x => x.CON_TRIGLICERIDEOS >= 150), listConsulta.Count());





                string customSwitches =
                " --footer-right \"Usuario: " + ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_LOGIN + "  Emissão: " +
                    DateTime.Now + "  Pág. [page]/[toPage]\"" +
                    " --footer-font-size \"8\" --footer-spacing 6 --footer-font-name \"Roboto\"";


                var pdfResult = new ViewAsPdf()
                {
                    RotativaOptions = new Rotativa.Core.DriverOptions
                    {
                        PageOrientation = Rotativa.Core.Options.Orientation.Portrait,
                        IsGrayScale = false,

                        CustomSwitches = customSwitches,
                        PageSize = Rotativa.Core.Options.Size.A4,


                    }
                };
                if (detalhado)
                {
                    pdfResult.ViewName = "RelatorioGerencial_detalhado";
                }
                else
                {
                    pdfResult.ViewName = "RelatorioGerencial_consolidado";

                }

                return pdfResult;
                //return View(pdfResult.ViewName);
            }
            catch (Exception e)
            {
                TempData["msgInfo"] = e.Message;
                return View(Request.UrlReferrer.ToString());
            }
        }


        // GET: SASS/CartaoSaude
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
            List<PesquisaCartaoPontoModelView> pesquisaListModelView = new List<PesquisaCartaoPontoModelView>();
            Mapper.Map(pesquisa, pesquisaDomain);

            List<PesquisaCartaoPontoDomainModel> pesquisaListDomain = cartaoSaudeBusiness.getPesquisa(pesquisaDomain);
            Mapper.Map(pesquisaListDomain, pesquisaListModelView);
            TempData["ListFuncionario"] = pesquisaListModelView;

            TempData["ParametrosPesquisa"] = pesquisa;
            return RedirectToAction("Index");
        }

        [Route("Relatorio/Consulta")]
        public ActionResult Consulta()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Consulta(FormCollection form)
        {
            try
            {
                //ConsultaModelView consulta = new ConsultaModelView();
                //if (f == null && id == null)
                //    throw new Exception("selecione o servidor para registrar o atendimento.");
                //if (id == null)
                //{ consulta.FUN_ID = f.Value; }
                //else
                //{
                //    ConsultaDomainModel domainModel = cartaoSaudeBusiness.buscarPorId(id.Value); Mapper.Map(domainModel, consulta);
                //}

                //var listResponsavel = funcionarioBusiness.ddlFuncionarioAtendimentoSASS();
                //ViewBag.RespAtendimento = new SelectList(listResponsavel, "FUN_ID", "FUN_NOME", consulta.CON_RESPATENDIMENTO, listResponsavel.Where(x => !x.Ativo).Select(x => x.FUN_ID));

                return View();
            }
            catch (Exception e)
            {
                TempData["msgInfo"] = e.Message;
                return RedirectToAction("Index");
            }
        }



        // GET: SASS/CartaoSaude/Create
        public ActionResult Atendimento(int? id, int? f)
        {
            try
            {
                ConsultaModelView consulta = new ConsultaModelView();
                if (f == null && id == null)
                    throw new Exception("selecione o servidor para registrar o atendimento.");
                if (id == null)
                { consulta.FUN_ID = f.Value; }
                else
                {
                    ConsultaDomainModel domainModel = cartaoSaudeBusiness.buscarPorId(id.Value); Mapper.Map(domainModel, consulta);
                }

                var listResponsavel = funcionarioBusiness.ddlFuncionarioAtendimentoSASS();
                ViewBag.RespAtendimento = new SelectList(listResponsavel, "FUN_ID", "FUN_NOME", consulta.CON_RESPATENDIMENTO, listResponsavel.Where(x => !x.Ativo).Select(x => x.FUN_ID));

                return View(consulta);
            }
            catch (Exception e)
            {
                TempData["msgInfo"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atendimento(ConsultaModelView consulta)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ConsultaDomainModel domainModel = new ConsultaDomainModel();
                    consulta.CON_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(consulta, domainModel);
                    if (cartaoSaudeBusiness.AddUpdateConsulta(domainModel) && cartaoSaudeBusiness.Salvar().Result)
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso();
                        return RedirectToAction("Details", new { id = consulta.FUN_ID });
                    }
                    else
                        throw new Exception();
                }
                else
                {
                    ViewBag.RespAtendimento = new SelectList(funcionarioBusiness.ddlFuncionarioAtendimentoSASS(), "FUN_ID", "FUN_NOME");
                    return View(consulta);
                }

            }
            catch (Exception e)
            {
                TempData["msgInfo"] = string.Format(" {0}. {1}", msg.MensagemErro(), e.Message);
                return View();
            }
        }


        [Route("Cartaosaude/Atendimento/Details/{id?}")]
        public ActionResult AtendimentoDetails(int? id)
        {
            ConsultaModelView consulta = new ConsultaModelView();
            ConsultaDomainModel domainModel = cartaoSaudeBusiness.buscarPorId(id.Value);
            if (domainModel == null)
            { return HttpNotFound(); }
            Mapper.Map(domainModel, consulta);
            return View(consulta);
        }


        // GET: SASS/CartaoSaude/Delete/5
        [HttpGet]
        [Route("Cartaosaude/Atendimento/Delete/{id?}")]
        public ActionResult AtendimentoDelete(int id)
        {
            ConsultaModelView consulta = new ConsultaModelView();
            ConsultaDomainModel domainModel = cartaoSaudeBusiness.buscarPorId(id);
            if (domainModel == null)
            { return HttpNotFound(); }
            Mapper.Map(domainModel, consulta);
            return View(consulta);
        }

        // POST: SASS/CartaoSaude/Delete/5
        [HttpPost, Route("Cartaosaude/Atendimento/Delete/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                ConsultaDomainModel domainModel = cartaoSaudeBusiness.buscarPorId(id);
                if (domainModel != null)
                {
                    domainModel.CON_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    cartaoSaudeBusiness.DeleteConsulta(domainModel);
                    if (await cartaoSaudeBusiness.Salvar())
                        TempData["msgSuccess"] = msg.MensagemSucesso();
                    else
                        throw new Exception("Erro ao salvar no banco de dados!");

                    return RedirectToAction("Details", new { id = domainModel.FUN_ID });

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

        [Route("Cartaosaude/Atendimento/Pendente")]
        public async Task<ActionResult> AtendimentoPendente()
        {
            if (TempData["ListResult"] == null)
            {
                List<VinculoModelView> vinculo = new List<VinculoModelView>();
                List<VinculoDomainModel> domainModel = await cartaoSaudeBusiness.buscarVinculoPendenteDeAtendimentoPorData(DateTime.Today.AddMonths(-6), null);
                Mapper.Map(domainModel, vinculo);
                TempData["ListResult"] = vinculo;

            }
            return View();
        }

        //[HttpPost, Route("Cartaosaude/Atendimento/Pendente")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AtendimentoPendente(DateTime? inicio, DateTime? fim)
        //{
        //    List<VinculoModelView> vinculo = new List<VinculoModelView>();
        //    List<VinculoDomainModel> domainModel = await cartaoSaudeBusiness.buscarVinculoPendenteDeAtendimentoPorData(inicio, fim);
        //    Mapper.Map(domainModel, vinculo);
        //    TempData["ListResult"] = vinculo;
        //    return View();
        //}

    }
}
