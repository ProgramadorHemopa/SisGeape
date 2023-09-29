using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class ConselhoController : Controller
    {
        infraMessage msg = new infraMessage();
        IConselhoClasseBusiness _conselhoBusiness;
        ICargoBusiness _cargoBusiness;
        public ConselhoController(IConselhoClasseBusiness conselhoBusiness, ICargoBusiness cargoBusiness)
        {
            _conselhoBusiness = conselhoBusiness;
            _cargoBusiness = cargoBusiness;
        }

        // GET: Conselho
        public ActionResult Index()
        {
            List<ConselhoModelView> conselho = new List<ConselhoModelView>();

            var listDomain = _conselhoBusiness.GetConselho();
            Mapper.Map(listDomain, conselho);
            TempData["ListConselho"] = conselho;

            return View();
        }


        // GET: Conselho/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConselhoModelView conselho = new ConselhoModelView();
            ConselhoDomainModel domainModel = _conselhoBusiness.GetConselhoById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, conselho);

            return View(conselho);
        }

        // GET: Conselho/Create
        public ActionResult Cadastro(int? id)
        {
            ConselhoModelView conselho = new ConselhoModelView();
            if (id == null)
            {
                ViewBag.Title = "Cadastrar Conselho";

                ViewBag.Cargo = new SelectList(_cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME");

            }
            else
            {
                ConselhoDomainModel _domainModel = _conselhoBusiness.GetConselhoById(id.Value);
                ViewBag.Title = "Editar Conselho - " + _domainModel.CON_NOME;

                ViewBag.Cargo = new SelectList(_cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME", _domainModel.CRG_ID);

                Mapper.Map(_domainModel, conselho);
            }
            return View(conselho);
        }

        // POST: Conselho/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Include = "CON_ID,CRG_ID,CON_NOME")] ConselhoModelView conselho)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ConselhoDomainModel _domainModel = new ConselhoDomainModel();


                    conselho.CON_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(conselho, _domainModel);

                    if (_conselhoBusiness.AddUpdateConselho(_domainModel))
                    {
                        if (_conselhoBusiness.Salvar())
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
                ViewBag.Cargo = new SelectList(_cargoBusiness.DdlCargo(), "CRG_ID", "CRG_NOME", conselho.CRG_ID);

                return View(conselho);
            }
        }




        // GET: Conselho/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "Excluir Conselho";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConselhoModelView conselho = new ConselhoModelView();
            ConselhoDomainModel domainModel = _conselhoBusiness.GetConselhoById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, conselho);

            return View(conselho);
        }
        // POST: Banco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ConselhoDomainModel domainModel = _conselhoBusiness.GetConselhoById(id);
                if (domainModel != null)
                {
                    domainModel.CON_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    _conselhoBusiness.DeleteConselho(domainModel);


                    if (_conselhoBusiness.Salvar())
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso().ToString();
                        return RedirectToAction("Index");
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new InvalidOperationException();


            }
            catch (Exception e)
            {
                TempData["msgError"] = "Ocorreu um erro na sua operação. </br> " + e.Message;


                return RedirectToAction("Index");
            }

        }
    }
}
