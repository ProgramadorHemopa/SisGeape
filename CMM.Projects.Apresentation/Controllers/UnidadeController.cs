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
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class UnidadeController : Controller
    {

        private infraMessage msg = new infraMessage();


        IUnidadeBusiness unidadeBusiness;
        IVinculoBusiness vinculoBusiness;


        public UnidadeController(IUnidadeBusiness _unidadeBusiness, IVinculoBusiness _vinculoBusiness)
        {
            unidadeBusiness = _unidadeBusiness;
            vinculoBusiness = _vinculoBusiness;
        }

        // GET: Unidade
        public async Task<ActionResult> Index()
        {
            List<UnidadeModelView> listUnidade = new List<UnidadeModelView>();
            List<UnidadeDomainModel> domainModel = await unidadeBusiness.GetAllAsync();
            Mapper.Map(domainModel, listUnidade);
            TempData["listUnidade"] = listUnidade;
            return View();
        }

        public JsonResult ListarUnidade(ParametrosPaginacao paginacao)
        {
            var unidade = unidadeBusiness.GetAllAsync().Result;

            int tot = unidade.Count();


            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                unidade = unidade.Where("UND_SIGLA.ToUpper().Contains(@0) OR UND_NOME.ToUpper().Contains(@0) ", paginacao.SearchValue.ToUpper()).ToList();
            }
            int totFiltrado = unidade.Count();

            var dadosPaginados = unidade.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();

            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExistUnidade(string und_sigla, int und_id)
        {

            return Json(!unidadeBusiness.ExistsUnidade(und_sigla, und_id), JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> Cadastro(int id = 0)
        {
            UnidadeModelView _unidade = new UnidadeModelView();

            if (id == 0)
            {
                ViewBag.Title = "Nova Unidade";
                ViewBag.Pai = new SelectList(unidadeBusiness.ddlUnidade(), "UND_ID", "UND_NOME");

            }
            else
            {
                ViewBag.Title = "Editar Unidade";
                UnidadeDomainModel unidadeDomainModel = await unidadeBusiness.GetUnidadeyId(id);
                if (unidadeDomainModel == null)
                {
                    return HttpNotFound();
                }
                Mapper.Map(unidadeDomainModel, _unidade);
                ViewBag.Pai = new SelectList(unidadeBusiness.ddlUnidade().Where(x => x.UND_ID != id), "UND_ID", "UND_NOME");
            }

            return View(_unidade);
        }

        // POST: Unidade/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(UnidadeModelView _unidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UnidadeDomainModel unidadeDomainModel = new UnidadeDomainModel();
                    _unidade.UND_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    Mapper.Map(_unidade, unidadeDomainModel);
                    unidadeBusiness.AddUpdateUnidade(unidadeDomainModel);


                    if (unidadeBusiness.Salvar())
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso();
                        return RedirectToAction("Index");
                    }
                    else
                        throw new Exception();





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
                TempData["msgInfo"] = mensg;
                return View(_unidade);

            }
        }

        // GET: Unidade/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadeModelView _unidade = new UnidadeModelView();
            UnidadeDomainModel unidadeDomainModel = unidadeBusiness.GetUnidadeyId(id.Value).Result;

            if (unidadeDomainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(unidadeDomainModel, _unidade);
            return PartialView(_unidade);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnidadeModelView _unidade = new UnidadeModelView();
            List<UnidadeModelView> _listUnidadeFilho = new List<UnidadeModelView>();
            List<VinculoModelView> _listVinculoUnidade = new List<VinculoModelView>();

            UnidadeDomainModel unidadeDomainModel = await unidadeBusiness.GetUnidadeyId(id.Value);
            List<UnidadeDomainModel> listUnidadeFilhosDomainModel = await unidadeBusiness.GetAllAsync();
            List<VinculoDomainModel> listVinculoUnidadeDomainModel = await vinculoBusiness.GetAllAsyncVinculoByUnidade(id.Value);


            if (unidadeDomainModel == null)
            {
                return HttpNotFound();
            }
            Mapper.Map(unidadeDomainModel, _unidade);
            if (listUnidadeFilhosDomainModel != null)
                Mapper.Map(listUnidadeFilhosDomainModel.Where(x => x.UND_PAI == id).ToList(), _listUnidadeFilho);
            if (listVinculoUnidadeDomainModel != null)
                Mapper.Map(listVinculoUnidadeDomainModel.Where(x => x.VNC_DEMISSAO == null && x.Lotacao.Any(z => z.UND_ID == id.Value && z.VNCU_DATAFIM == null)).ToList(), _listVinculoUnidade);

            TempData["vinculoUnidade"] = _listVinculoUnidade;
            TempData["unidadeFilho"] = _listUnidadeFilho;
            return View(_unidade);
        }

        // POST: Unidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                var unidade = unidadeBusiness.GetUnidadeyId(id).Result;
                if (unidade != null)
                {
                    unidade.UND_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    unidadeBusiness.DeleteUnidade(unidade);

                    if (unidadeBusiness.Salvar())
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
