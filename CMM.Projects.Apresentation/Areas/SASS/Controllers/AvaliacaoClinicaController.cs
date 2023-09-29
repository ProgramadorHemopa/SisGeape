using AutoMapper;
using CCM.Projects.SisGeape2.Domain.SASS;
using CCM.Projects.SisGeapeWeb2.Business.Interface.SASS;
using CMM.Projects.Apresentation.Areas.SASS.Models;
using CMM.Projects.Apresentation.InfraAuthentication;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Areas.SASS.Controllers
{
    [Filtros.AutorizarOperadorSASS]
    [RouteArea("SASS")]
    public class AvaliacaoClinicaController : Controller
    {

        private infraMessage msg = new infraMessage();


        IAvaliacaoClinicaBusiness avaliacaoBusiness;

        public AvaliacaoClinicaController(IAvaliacaoClinicaBusiness _avaliacaoClinicaBusiness)
        {
            avaliacaoBusiness = _avaliacaoClinicaBusiness;
        }
        // GET: AvaliacaoClinica
        public ActionResult Index()
        {
            List<AvaliacaoClinicaModelView> listAvaliacao = new List<AvaliacaoClinicaModelView>();
            List<AvaliacaoClinicaDomainModel> listDomain = avaliacaoBusiness.ListarAvaliacaoClinica();
            Mapper.Map(listDomain, listAvaliacao);
            return View(listAvaliacao);
        }


        // GET: AvaliacaoClinica/Cadastro
        public ActionResult Cadastro(int? id)
        {
            AvaliacaoClinicaModelView avaliacao = new AvaliacaoClinicaModelView();

            if (id != null)
            {

                AvaliacaoClinicaDomainModel modelDomain = avaliacaoBusiness.AvaliacaoClinicaById(id.Value);
                Mapper.Map(modelDomain, avaliacao);
            }

            return View(avaliacao);
        }

        // POST: AvaliacaoClinica/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(AvaliacaoClinicaModelView avaliacaoClinica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AvaliacaoClinicaDomainModel domainModel = new AvaliacaoClinicaDomainModel();
                    avaliacaoClinica.AVC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    Mapper.Map(avaliacaoClinica, domainModel);
                    avaliacaoBusiness.AddUpdateAvalicaoClinica(domainModel);
                    if (!avaliacaoBusiness.Salvar())
                        throw new Exception("Erro ao salvar no banco de dados!");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["msgInfo"] = "Ocorreu um erro na sua operação. </br> " + e.Message;
                return View(avaliacaoClinica);
            }
        }


        // GET: AvaliacaoClinica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvaliacaoClinicaModelView avaliacao = new AvaliacaoClinicaModelView();
            AvaliacaoClinicaDomainModel domainModel = avaliacaoBusiness.AvaliacaoClinicaById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, avaliacao);

            return View(avaliacao);
        }

        // POST: AvaliacaoClinica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                AvaliacaoClinicaDomainModel domainModel = avaliacaoBusiness.AvaliacaoClinicaById(id);
                if (domainModel != null)
                {
                    domainModel.AVC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    avaliacaoBusiness.DeleteAvaliacaoClinica(domainModel);
                    if (!avaliacaoBusiness.Salvar())
                        throw new Exception("Erro ao salvar no banco de dados!");

                    return RedirectToAction("Index");

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
