using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using CMM.Projects.Apresentation.Models.CustomValidation;
using FluentDateTime;
using Microsoft.Reporting.WebForms;
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
    public class FeriasController : Controller
    {
        private infraMessage msg = new infraMessage();

        IFeriasBusiness _feriasBusiness;
        IVinculoBusiness _vinculoBusiness;



        public FeriasController(IFeriasBusiness feriasBusiness, IVinculoBusiness vinculoBusiness)
        {
            _vinculoBusiness = vinculoBusiness;
            _feriasBusiness = feriasBusiness;
            _feriasBusiness.Initialize(new ModelStateWrapper(this.ModelState));
        }

        // GET: Ferias
        public ActionResult Index()
        {
            return View();
        }
        // GET: Ferias
        public ActionResult Afastamento()
        {

            if (TempData["ListResult"] == null)
            {
                List<FeriasDomainModel> listDomain = _feriasBusiness.BuscaServidoresEmFerias(DateTime.Today.FirstDayOfMonth(), DateTime.Today.LastDayOfMonth());
                List<FeriasModelView> listFerias = new List<FeriasModelView>();
                Mapper.Map(listDomain, listFerias);

                TempData["ListResult"] = listFerias;
                TempData["Inicio"] = DateTime.Today.FirstDayOfMonth().ToShortDateString();
                TempData["Fim"] = DateTime.Today.LastDayOfMonth().ToShortDateString();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterfacePesquisa(FormCollection pesquisa)
        {
            if (Convert.ToDateTime(pesquisa["dataInicio"]) > Convert.ToDateTime(pesquisa["dataFim"]))
            {
                TempData["msgError"] = "Data inicio não pode ser maior que a data fim";
                return RedirectToAction("Afastamento");
            }
            List<FeriasDomainModel> listDomain = _feriasBusiness.BuscaServidoresEmFerias(Convert.ToDateTime(pesquisa["dataInicio"]), Convert.ToDateTime(pesquisa["dataFim"]));
            List<FeriasModelView> listFerias = new List<FeriasModelView>();
            Mapper.Map(listDomain, listFerias);

            TempData["ListResult"] = listFerias;
            TempData["Inicio"] = Convert.ToDateTime(pesquisa["dataInicio"]).ToShortDateString();
            TempData["Fim"] = Convert.ToDateTime(pesquisa["dataFim"]).ToShortDateString();
            return RedirectToAction("Afastamento");
        }

        public ActionResult ListFerias(int? id)
        {
            if (id != 0)
            {
                ViewBag.FUN_ID = id;
            }
            //int anocorrente = DateTime.Today.Year;
            //List<SelectListItem> lstAno = new List<SelectListItem>();

            //for (int i = anocorrente; i >= anocorrente - 30; i--)
            //{
            //    lstAno.Add(new SelectListItem { Text = Convert.ToString(i) });

            //}

            //ViewBag.ListAno = new SelectList(lstAno, "Text", "Text", DateTime.Today.Year.ToString());
            return PartialView();
        }



        public JsonResult ListarFerias_Funcionario(ParametrosPaginacao paginacao, int id = 0)
        {
            var _list = _feriasBusiness.GetFeriasByFuncionario(id);

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


        public ActionResult ComunicadoFerias(int? id)
        {
            try
            {

                var viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/InfraRelatorio/Reports/ComunicadoFerias.rdlc");



                List<ComunicadoFeriasDomainModel> dsComunicado = _feriasBusiness.GetDadosComunicado(id.Value);
                if (dsComunicado.Count > 0)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("dsFerias", dsComunicado));
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

        public ActionResult AddUpdateFerias(int id = 0, int func = 0)
        {
            FeriasModelView ferias = new FeriasModelView();

            if (id == 0)
            {
                ViewBag.Title = "Adicionar Férias";


                ViewBag.VNC_ID = new SelectList(_vinculoBusiness.ddlVinculoByFuncionario(func), "VNC_ID", "VNC_NOME");
            }
            else
            {
                ViewBag.Title = "Editar Férias";
                FeriasDomainModel domainModel = _feriasBusiness.GetFeriasById(id);
                if (domainModel == null)
                {
                    return HttpNotFound();
                }


                ViewBag.VNC_ID = new SelectList(_vinculoBusiness.ddlVinculoByFuncionario(domainModel.FUN_ID), "VNC_ID", "VNC_NOME", domainModel.VNC_ID);
                Mapper.Map(domainModel, ferias);
            }
            return PartialView(ferias);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateFerias(FeriasModelView ferias)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ferias.FRS_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    FeriasDomainModel _domainModel = new FeriasDomainModel();

                    Mapper.Map(ferias, _domainModel);
                    _feriasBusiness.AddUpdateFerias(_domainModel);


                    if (_feriasBusiness.Salvar())
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



        // GET: Ferias/Details/5
        // GET: Ferias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeriasModelView _ferias = new FeriasModelView();
            FeriasDomainModel domainModel = new FeriasDomainModel();
            domainModel = _feriasBusiness.GetFeriasById(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(domainModel, _ferias);
            return PartialView(_ferias);
        }

        // POST: Ferias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {

                var _ferias = _feriasBusiness.GetFeriasById(id);
                if (_ferias != null)
                {
                    _ferias.FRS_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    _feriasBusiness.DeleteFerias(_ferias);

                    if (_feriasBusiness.Salvar())
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
