using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using CMM.Projects.Apresentation.Models.CustomValidation;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class FeriadoController : Controller
    {
        private infraMessage msg = new infraMessage();

        IFeriadoBusiness _feriadoBusiness;
        IVinculoBusiness _vinculoBusiness;



        public FeriadoController(IFeriadoBusiness feriadoBusiness, IVinculoBusiness vinculoBusiness)
        {
            _vinculoBusiness = vinculoBusiness;
            _feriadoBusiness = feriadoBusiness;
            _feriadoBusiness.Initialize(new ModelStateWrapper(this.ModelState));
        }

        // GET: Feriado
        public ActionResult Index(DateTime? dataInicio, DateTime? dataFim)
        {
            List<FeriadoDomainModel> listDomain = null;
            List<FeriadoModelView> listFeriado = new List<FeriadoModelView>();
            listDomain = _feriadoBusiness.GetFeriadoPorData(dataInicio, dataFim);

            Mapper.Map(listDomain, listFeriado);
            return View(listFeriado.ToList());
        }

        public ActionResult Cadastro(int id = 0)
        {
            FeriadoModelView _feriado = new FeriadoModelView();

            if (id == 0)
            {
                ViewBag.Title = "Novo Feriado";

            }
            else
            {
                ViewBag.Title = "Editar Feriado";
                FeriadoDomainModel feriadoDomainModel = _feriadoBusiness.GetFeriadoById(id);
                if (feriadoDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(feriadoDomainModel, _feriado);
            }

            return View(_feriado);
        }

        // POST: Unidade/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(FeriadoModelView _feriado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FeriadoDomainModel feriadoDomainModel = new FeriadoDomainModel();
                    _feriado.FER_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_feriado, feriadoDomainModel);




                    _feriadoBusiness.AddUpdateFeriado(feriadoDomainModel);


                    _feriadoBusiness.Salvar();

                    TempData["msgSuccess"] = msg.MensagemSucesso();
                    return RedirectToAction("Index");

                }
                else
                    throw new Exception("Exception Validar");
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
                TempData["msgInfo"] = mensg;
                return View(_feriado);

            }
        }

        // GET: Feriado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeriadoModelView _feriado = new FeriadoModelView();
            FeriadoDomainModel domainModel = new FeriadoDomainModel();
            domainModel = _feriadoBusiness.GetFeriadoById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, _feriado);
            return View(_feriado);
        }

        // POST: Ferias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                var _feriado = _feriadoBusiness.GetFeriadoById(id);
                if (_feriado != null)

                    _feriado.FER_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                _feriadoBusiness.DeleteFeriado(_feriado);

                _feriadoBusiness.Salvar();
                TempData["msgSuccess"] = msg.MensagemSucesso();
                return RedirectToAction("Index");




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
                TempData["msgSuccess"] = mensg;
                return View();
            }
        }
    }
}
