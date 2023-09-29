using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class ImportacaoController : Controller
    {

        private infraMessage msg = new infraMessage();
        private PontoSecullum4Entities db = new PontoSecullum4Entities();

        IPontoImportacaoBusiness importacaoBusiness;

        public ImportacaoController(IPontoImportacaoBusiness _importacaoBusiness)
        {
            importacaoBusiness = _importacaoBusiness;
        }
        // GET: Importacao
        public ActionResult Index()
        {
            List<PontoImportacaoDomainModel> ListDomain = importacaoBusiness.GetAll();
            List<PontoImportacaoModelView> ListImportacao = new List<PontoImportacaoModelView>();
            Mapper.Map(ListDomain, ListImportacao);
            TempData["ListImportacao"] = ListImportacao;
            return View();
        }

        // GET: Importacao/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Importacao/Create
        public ActionResult Create()
        {

            return PartialView();
        }

        // POST: Importacao/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "PONIMP_OBSERVACAO,PONIMP_DATAFIM,PONIMP_DATAINICIO")] PontoImportacaoModelView importacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PontoImportacaoDomainModel _domainModel = new PontoImportacaoDomainModel();
                    _domainModel.PONEQP_ID = 2;

                    importacao.PONIMP_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(importacao, _domainModel);

                    var listBatidas = db.batidas.Where(x => x.data >= importacao.PONIMP_DATAINICIO && x.data <= importacao.PONIMP_DATAFIM).ToList();

                    if (listBatidas.Count() > 0)
                    {
                        foreach (var item in listBatidas)
                        {

                            PontoDomainModel entrada1 = new PontoDomainModel();
                            PontoDomainModel entrada2 = new PontoDomainModel();
                            PontoDomainModel entrada3 = new PontoDomainModel();
                            PontoDomainModel saida1 = new PontoDomainModel();
                            PontoDomainModel saida2 = new PontoDomainModel();
                            PontoDomainModel saida3 = new PontoDomainModel();

                            if (item.entrada1 != null)
                            {
                                entrada1.PON_DATA = item.data.Add(TimeSpan.Parse(item.entrada1));
                                entrada1.IDENTIFICADOR_FUNCIONARIO = item.funcionarios.n_pis;
                                entrada1.MATRICULA_FUNCIONARIO = item.funcionarios.n_identificador;
                                _domainModel.pontos.Add(entrada1);
                            }
                            if (item.entrada2 != null)
                            {
                                entrada2.PON_DATA = item.data.Add(TimeSpan.Parse(item.entrada2));
                                entrada2.IDENTIFICADOR_FUNCIONARIO = item.funcionarios.n_pis;
                                entrada2.MATRICULA_FUNCIONARIO = item.funcionarios.n_identificador;
                                _domainModel.pontos.Add(entrada2);
                            }
                            if (item.entrada3 != null)
                            {
                                entrada3.PON_DATA = item.data.Add(TimeSpan.Parse(item.entrada3));
                                entrada3.IDENTIFICADOR_FUNCIONARIO = item.funcionarios.n_pis;
                                entrada3.MATRICULA_FUNCIONARIO = item.funcionarios.n_identificador;
                                _domainModel.pontos.Add(entrada3);
                            }
                            if (item.saida1 != null)
                            {
                                saida1.PON_DATA = item.data.Add(TimeSpan.Parse(item.saida1));
                                saida1.IDENTIFICADOR_FUNCIONARIO = item.funcionarios.n_pis;
                                saida1.MATRICULA_FUNCIONARIO = item.funcionarios.n_identificador;
                                _domainModel.pontos.Add(saida1);
                            }
                            if (item.saida2 != null)
                            {
                                saida2.PON_DATA = item.data.Add(TimeSpan.Parse(item.saida2));
                                saida2.IDENTIFICADOR_FUNCIONARIO = item.funcionarios.n_pis;
                                saida2.MATRICULA_FUNCIONARIO = item.funcionarios.n_identificador;
                                _domainModel.pontos.Add(saida2);
                            }
                            if (item.saida3 != null)
                            {
                                saida3.PON_DATA = item.data.Add(TimeSpan.Parse(item.saida3));
                                saida3.IDENTIFICADOR_FUNCIONARIO = item.funcionarios.n_pis;
                                saida3.MATRICULA_FUNCIONARIO = item.funcionarios.n_identificador;
                                _domainModel.pontos.Add(saida3);
                            }
                        }
                    }


                    if (importacaoBusiness.AddImportacao(_domainModel))
                    {
                        if (importacaoBusiness.Salvar())
                        {
                            TempData["msgSuccess"] = msg.MensagemSucesso();
                            return RedirectToAction("Index");
                        }
                        else
                            throw new Exception();
                    }
                    else
                        throw new InvalidOperationException();

                }
                else
                    throw new InvalidOperationException();


            }
            catch (Exception e)
            {
                TempData["msgInfo"] = "Ocorreu um erro na sua operação. </br> " + e.Message;

                return View(importacao);
            }
        }

        // GET: Importacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Importacao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ImportarArquivo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportarArquivo(HttpPostedFileBase arquivo, DateTime? inicio, DateTime? fim, string observacao)
        {
            try
            {
                if (inicio > fim)
                {
                    TempData["msgInfo"] = "Data Inicio não pode ser maior que a Data Fim.";
                    return View();
                }
                importacaoBusiness.ImportarBatidasPorArquivoTxt(arquivo.InputStream, inicio, fim, observacao);
                if (importacaoBusiness.Salvar())
                {
                    TempData["msgSuccess"] = msg.MensagemSucesso();
                    return RedirectToAction("Index");
                }
                else
                    throw new Exception();

            }
            catch (Exception e)
            {
                TempData["msgInfo"] = "Ocorreu um erro na sua operação. </br> " + e.Message;

                return View();
            }
        }
    }
}
