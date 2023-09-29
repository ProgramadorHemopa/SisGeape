using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
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
    public class BancoController : Controller
    {
        private infraMessage msg = new infraMessage();


        IBancoBusiness bancoBusiness;

        public BancoController(IBancoBusiness _bancoBusiness)
        {
            bancoBusiness = _bancoBusiness;
        }


        // GET: Banco
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarBanco(ParametrosPaginacao paginacao)
        {
            List<BancoDomainModel> listDomain = new List<BancoDomainModel>();

            int tot = bancoBusiness.TotalRegistros();
            if (String.IsNullOrWhiteSpace(paginacao.SearchValue))
            {
                listDomain = bancoBusiness.GetBancoRecords(paginacao.Start, paginacao.Length).OrderBy(paginacao.CampoOrdenacao).ToList(); //  _banco.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToArrayAsync();

            }
            else
            {
                listDomain = bancoBusiness.GetBancoByName(paginacao.SearchValue);
            }

            int totFiltrado = listDomain.Count();


            return Json(new
            {
                data = listDomain,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }


        // GET: Banco/Create
        public ActionResult AddUpdateBanco(int id = 0)
        {
            bancoModelView _banco = new bancoModelView();
            if (id == 0)
            {
                ViewBag.Title = "Novo Banco";

                return PartialView(_banco);
            }
            else
            {
                ViewBag.Title = "Editar Banco";
                BancoDomainModel bancoDomainModel = bancoBusiness.GetBancoById(id);
                if (bancoDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(bancoDomainModel, _banco);


                return PartialView(_banco);

            }
        }

        // POST: Banco/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUpdateBanco([Bind(Include = "BNC_ID,BNC_DESCRICAO,BNC_REGDATE,BNC_REGUSER,BNC_STATUS")] bancoModelView _banco)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    BancoDomainModel bancoDomainModel = new BancoDomainModel();
                    _banco.BNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_banco, bancoDomainModel);
                    bancoBusiness.AddUpdateBanco(bancoDomainModel);


                    if (bancoBusiness.Salvar())
                        return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { resultado = false, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);





                }
                else
                    throw new Exception();

            }
            catch
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + " <br/>";
                }
                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);


            }

        }


        // GET: Banco/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bancoModelView _banco = new bancoModelView();
            BancoDomainModel bancoDomainModel = bancoBusiness.GetBancoById(id.Value);
            if (bancoDomainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(bancoDomainModel, _banco);

            return PartialView(_banco);
        }

        // POST: Banco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                BancoDomainModel bancoDomainModel = bancoBusiness.GetBancoById(id);
                if (bancoDomainModel != null)
                {
                    bancoDomainModel.BNC_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    bancoBusiness.DeleteBanco(bancoDomainModel);

                    if (bancoBusiness.Salvar())
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
            catch
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + " <br/>";
                }
                return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
