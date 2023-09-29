using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class ConselhoClasseController : Controller
    {
        infraMessage msg = new infraMessage();

        private readonly IConselhoClasseBusiness conselhoClasseBusiness;
        private readonly ICargoBusiness cargoBusiness;
        private readonly IVinculoBusiness vinculoBusiness;
        private readonly IUnidadeBusiness unidadeBusiness;

        public ConselhoClasseController(IConselhoClasseBusiness _conselhoClasseBusiness, ICargoBusiness _cargoBusiness, IVinculoBusiness _vinculoBusiness, IUnidadeBusiness _unidadeBusiness)
        {
            conselhoClasseBusiness = _conselhoClasseBusiness;
            cargoBusiness = _cargoBusiness;
            vinculoBusiness = _vinculoBusiness;
            unidadeBusiness = _unidadeBusiness;
        }

        // GET: ConselhoClasse
        public ActionResult Index()
        {
            PesquisaConselhoClasseModelView pesquisa = new PesquisaConselhoClasseModelView();
            ViewBag.Cargo = new SelectList(cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");
            ViewBag.Situacao = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "PENDENTE", Value="0" },
                new SelectListItem {Text = "QUITE", Value="1" },

            }, "Value", "Text");

            int anocorrente = DateTime.Today.Year;
            List<SelectListItem> lstAno = new List<SelectListItem>();
            for (int i = anocorrente; i >= anocorrente - 10; i--)
            {
                lstAno.Add(new SelectListItem { Text = Convert.ToString(i) });
            }
            ViewBag.ListAno = new SelectList(lstAno.OrderBy(x => x.Text), "Text", "Text", DateTime.Today.Year.ToString());

            if (TempData["ParametrosPesquisa"] != null)
            {
                pesquisa = ((PesquisaConselhoClasseModelView)TempData["ParametrosPesquisa"]);
            }
            ViewBag.Situacao = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "PENDENTE", Value="0" },
                new SelectListItem {Text = "QUITE", Value="1" },

            }, "Value", "Text", pesquisa.SITUACAO);


            return View(pesquisa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterfacePesquisa(PesquisaConselhoClasseModelView pesquisa)
        {
            PesquisaConselhoClasseDomainModel pesquisaDomain = new PesquisaConselhoClasseDomainModel();
            List<PesquisaConselhoClasseListDomainModel> pesquisaListDomain = new List<PesquisaConselhoClasseListDomainModel>();
            List<PesquisaConselhoClasseListModelView> pesquisaListModelView = new List<PesquisaConselhoClasseListModelView>();
            Mapper.Map(pesquisa, pesquisaDomain);

            pesquisaListDomain = conselhoClasseBusiness.GetPesquisaConselho(pesquisaDomain);
            Mapper.Map(pesquisaListDomain, pesquisaListModelView);
            TempData["ListConselhoFuncionario"] = pesquisaListModelView;

            TempData["ParametrosPesquisa"] = pesquisa;
            return RedirectToAction("Index");
        }

        public ActionResult GerenciarConselho(int vinculo)
        {

            return View();
        }

        // GET: ConselhoClasse/Details/5
        public ActionResult Details(int? id)
        {
            TempData["Titulo"] = "Conselho de Classe";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConselhoClasseVinculoModelView conselhoClasse = new ConselhoClasseVinculoModelView();
            ConselhoClasseVinculoDomainModel domainModel = conselhoClasseBusiness.GetFuncionarioConselhoByVinculo(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, conselhoClasse);
            TempData["ListConselhoVinculo"] = conselhoClasse.ConselhoClasse.ToList();
            return View(conselhoClasse);
        }

        // GET: ConselhoClasse/Create
        public ActionResult CadastroConCla(int? id, int? vnc, int cargo)
        {
            ConselhoClasseModelView conselho = new ConselhoClasseModelView();
            if (id == null)
            {
                conselho.VNC_ID = vnc.Value;
                conselho.CONCLA_DATARECEBIMENTO = DateTime.Today.ToShortDateString();
                ViewBag.Title = "Registar Conselho";
                ViewBag.Conselho = new SelectList(conselhoClasseBusiness.ddlConselhoByCargo(cargo), "CON_ID", "CON_NOME");

            }
            else
            {
                ViewBag.Title = "Editar Conselho ";
                ConselhoClasseDomainModel _domainModel = conselhoClasseBusiness.GetConselhoClasseById(id.Value);
                ViewBag.Conselho = new SelectList(conselhoClasseBusiness.ddlConselhoByCargo(cargo), "CON_ID", "CON_NOME", _domainModel.CON_ID);
                Mapper.Map(_domainModel, conselho);
            }



            return PartialView(conselho);
        }

        // POST: ConselhoClasse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroConCla([Bind(Include = "CONCLA_ID,VNC_ID,CON_ID,CONCLA_DATAQUITACAO,CONCLA_DATARECEBIMENTO,CONCLA_REFANO")] ConselhoClasseModelView conselho)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ConselhoClasseDomainModel _domainModel = new ConselhoClasseDomainModel();
                    Mapper.Map(conselho, _domainModel);

                    if (conselhoClasseBusiness.AddUpdateConselhoClasse(_domainModel))
                    {
                        if (conselhoClasseBusiness.Salvar())
                        {
                            TempData["msgSuccess"] = msg.MensagemSucesso();
                            return RedirectToAction("Details", new { id = _domainModel.VNC_ID });
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

                return View("Index", conselho.VNC_ID);
            }
        }

        // GET: Conselho/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "Excluir Conselho de Classe";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConselhoClasseModelView conselho = new ConselhoClasseModelView();
            ConselhoClasseDomainModel domainModel = conselhoClasseBusiness.GetConselhoClasseById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, conselho);

            return PartialView(conselho);
        }


        // POST: Banco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConselhoClasseDomainModel domainModel = new ConselhoClasseDomainModel();
            try
            {
                domainModel = conselhoClasseBusiness.GetConselhoClasseById(id);
                if (domainModel != null)
                {
                    domainModel.CONCLA_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    conselhoClasseBusiness.DeleteConselhoClasse(domainModel);

                    if (conselhoClasseBusiness.Salvar())
                    {
                        if (conselhoClasseBusiness.Salvar())
                        {
                            TempData["msgSuccess"] = msg.MensagemSucesso().ToString();
                            return RedirectToAction("Details", new { id = domainModel.VNC_ID });
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
                TempData["msgError"] = "Ocorreu um erro na sua operação. </br> " + e.Message;


                return RedirectToAction("Details", new { id = domainModel.VNC_ID });
            }

        }
    }
}
