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
    public class ControleCNHController : Controller
    {
        infraMessage msg = new infraMessage();

        private readonly IControleCNHBusiness controleCNHBusiness;
        private readonly IVinculoBusiness vinculoBusiness;
        private readonly IUnidadeBusiness unidadeBusiness;

        public ControleCNHController(IControleCNHBusiness _controleCNHBusiness, IVinculoBusiness _vinculoBusiness, IUnidadeBusiness _unidadeBusiness)
        {
            controleCNHBusiness = _controleCNHBusiness;
            vinculoBusiness = _vinculoBusiness;
            unidadeBusiness = _unidadeBusiness;
        }

        // GET: ControleCNH
        public ActionResult Index()
        {
            ControleCNHPesquisaModelView pesquisa = new ControleCNHPesquisaModelView();
            ViewBag.Unidade = new SelectList(unidadeBusiness.ddlUnidade().Where(x => x.UND_ID == 52 || x.UND_NIVEL == 6 || x.UND_NIVEL == 7), "UND_ID", "UND_NOME");

            if (TempData["ParametrosPesquisa"] != null)
            {
                pesquisa = ((ControleCNHPesquisaModelView)TempData["ParametrosPesquisa"]);
            }

            ViewBag.Situacao = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "Há 1 Ano do Vencimento", Value="0" },
                new SelectListItem {Text = "Há 6 Meses do Vencimento", Value="1" },
                new SelectListItem {Text = "Há 3 Meses do Vencimento", Value="2" },
                new SelectListItem {Text = "Há 1 Mês do Vencimento", Value="3" },
                new SelectListItem {Text = "Vencidos", Value="4" },

            }, "Value", "Text", pesquisa.CNH_SITUACAO);

            return View(pesquisa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterfacePesquisa(ControleCNHPesquisaModelView pesquisa)
        {
            ControleCNHPesquisaDomainModel pesquisaDomain = new ControleCNHPesquisaDomainModel();
            List<ControleCNHPesquisaDomainModel> pesquisaListDomain = new List<ControleCNHPesquisaDomainModel>();
            List<ControleCNHPesquisaModelView> pesquisaListModelView = new List<ControleCNHPesquisaModelView>();
            Mapper.Map(pesquisa, pesquisaDomain);

            pesquisaListDomain = controleCNHBusiness.GetControleCNHPesquisa(pesquisa.CNH_FUN_MATRICULA, pesquisa.CNH_FUN_NOME, pesquisa.CNH_SITUACAO, pesquisa.CNH_UND_ID);
            Mapper.Map(pesquisaListDomain, pesquisaListModelView);
            TempData["ListControleCNH"] = pesquisaListModelView;

            TempData["ParametrosPesquisa"] = pesquisa;
            return RedirectToAction("Index");
        }


        // GET: ConselhoClasse/Create
        public ActionResult CadastroControleCNH(int? id, int? vnc)
        {
            if (vnc != null)
            {
                ControleCNHVinculoDomainModel domainModel = controleCNHBusiness.GetControleCNHByVinculo(vnc.Value);
                if (domainModel != null)
                {
                    if (domainModel.ControleCNH.Count > 0)
                    {
                        ViewBag.Title = "Adicionar Dados de CNH";
                        return RedirectToAction("CadastroControleCNHA", new { CNH_ID = domainModel.ControleCNH.SingleOrDefault().CNH_ID });
                    }
                }
            }

            ControleCNHModelView cnh = new ControleCNHModelView();

            cnh.CNHA_DATARECEBIMENTO = DateTime.Now;

            if (id == null)
            {
                cnh.VNC_ID = vnc.Value;
                ViewBag.Title = "Registar CNH";

            }
            else
            {
                ViewBag.Title = "Editar CNH ";
                ControleCNHDomainModel _domainModel = controleCNHBusiness.GetControleCNHById(id.Value);
                Mapper.Map(_domainModel, cnh);
            }

            ViewBag.Categoria = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "A", Value="A" },
                new SelectListItem {Text = "B", Value="B" },
                new SelectListItem {Text = "C", Value="C" },
                new SelectListItem {Text = "D", Value="D" },
                new SelectListItem {Text = "E", Value="E" },
                new SelectListItem {Text = "AB", Value="AB" },
                new SelectListItem {Text = "AC", Value="AC" },
                new SelectListItem {Text = "AD", Value="AD" },
                new SelectListItem {Text = "AE", Value="AE" },

            }, "Value", "Text", cnh.CNHA_CATEGORIA);

            return PartialView(cnh);
        }

        // POST: ControleCNH/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroControleCNH([Bind(Include = "CNH_ID,VNC_ID, CNH_NREG, CNHA_VALIDADE, CNHA_EMISSAO, CNHA_DATARECEBIMENTO, CNHA_CATEGORIA")] ControleCNHModelView cnh)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ControleCNHDomainModel domainModel = new ControleCNHDomainModel();
                    ControleCNHAuxiliarDomainModel domainModelFilho = new ControleCNHAuxiliarDomainModel();

                    Mapper.Map(cnh, domainModel);
                    Mapper.Map(cnh, domainModelFilho);
                    domainModel.auxiliar.Add(domainModelFilho);

                    domainModel.CNH_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    domainModelFilho.CNHA_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    controleCNHBusiness.AddUpdateControleCNH(domainModel);
                    if (controleCNHBusiness.Salvar())
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso();
                        return RedirectToAction("Details", new { id = cnh.VNC_ID });
                    }
                    else
                    {
                        throw new Exception("Não foi possivel salvar dados de CNH");
                    }

                }
                else
                    throw new InvalidOperationException();

            }
            catch (Exception e)
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + ".";
                }
                mensg = mensg == "" ? e.Message : mensg;
                TempData["msgInfo"] = "Ocorreu um erro na sua operação. " + mensg;
                return RedirectToAction("Details", new { id = cnh.VNC_ID });
            }
        }

        // GET: ConselhoClasse/Create
        public ActionResult CadastroControleCNHA(int? id, int? CNH_ID)
        {
            ControleCNHModelView cnh = new ControleCNHModelView();

            if (id == null)
            {
                ViewBag.Title = "Registar Dados CNH";
                if (CNH_ID != null)
                {
                    ControleCNHDomainModel _domainModel = controleCNHBusiness.GetControleCNHById(CNH_ID.Value);
                    cnh.VNC_ID = _domainModel.VNC_ID;
                    cnh.CNH_NREG = _domainModel.CNH_NREG;
                    cnh.CNHA_DATARECEBIMENTO = DateTime.Now;
                }
            }
            else
            {
                ViewBag.Title = "Atualizar Dados CNH ";
                ControleCNHAuxiliarDomainModel _domainModel = controleCNHBusiness.GetControleCNHAById(id.Value);

                cnh = new ControleCNHModelView()
                {
                    CNH_NREG = _domainModel.CNH.CNH_NREG,
                    CNH_STATUS = _domainModel.CNH.CNH_STATUS,
                    CNH_ID = _domainModel.CNH_ID,
                    CNHA_CATEGORIA = _domainModel.CNHA_CATEGORIA,
                    CNHA_DATARECEBIMENTO = _domainModel.CNHA_DATARECEBIMENTO,
                    CNHA_EMISSAO = _domainModel.CNHA_EMISSAO,
                    CNHA_VALIDADE = _domainModel.CNHA_VALIDADE,
                    CNHA_ID = _domainModel.CNHA_ID.Value,
                    CNHA_STATUS = _domainModel.CNHA_STATUS,
                    VNC_ID = _domainModel.CNH.VNC_ID
                };
            }

            ViewBag.Categoria = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text = "A", Value="A" },
                new SelectListItem {Text = "B", Value="B" },
                new SelectListItem {Text = "C", Value="C" },
                new SelectListItem {Text = "D", Value="D" },
                new SelectListItem {Text = "E", Value="E" },
                new SelectListItem {Text = "AB", Value="AB" },
                new SelectListItem {Text = "AC", Value="AC" },
                new SelectListItem {Text = "AD", Value="AD" },
                new SelectListItem {Text = "AE", Value="AE" },

            }, "Value", "Text", cnh.CNHA_CATEGORIA);

            return PartialView(cnh);
        }

        // POST: ControleCNH/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroControleCNHA(ControleCNHModelView cnh)
        {
            try
            {
                ControleCNHAuxiliarDomainModel domainModel = new ControleCNHAuxiliarDomainModel();
                domainModel.CNH = new ControleCNHDomainModel() { CNH_NREG = cnh.CNH_NREG, VNC_ID = cnh.VNC_ID.Value };
                Mapper.Map(cnh, domainModel);



                if (ModelState.IsValid)
                {
                    domainModel.CNHA_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    var controle = controleCNHBusiness.GetControleCNHById(domainModel.CNH_ID.Value).auxiliar.LastOrDefault();
                    if (controle.CNHA_VALIDADE < domainModel.CNHA_VALIDADE)
                    {
                        if (controleCNHBusiness.AddUpdateControleCNHFilho(domainModel))
                        {
                            if (controleCNHBusiness.Salvar())
                            {
                                TempData["msgSuccess"] = msg.MensagemSucesso();
                                return RedirectToAction("Details", new { id = cnh.VNC_ID });
                            }
                            else
                                throw new Exception();
                        }
                        else
                            throw new InvalidOperationException();
                    }
                    else
                    {
                        string mensg = "Data de validade é menor que o último registro de CNH informado!";
                        TempData["msgInfo"] = "Ocorreu um erro na sua operação. " + mensg;
                        return RedirectToAction("Details", new { id = cnh.VNC_ID });
                    }



                }
                else
                    throw new InvalidOperationException();

            }
            catch (Exception e)
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + ".";
                }
                mensg = mensg == "" ? e.Message : mensg;
                TempData["msgInfo"] = "Ocorreu um erro na sua operação.  " + mensg;
                return RedirectToAction("Details", new { id = cnh.VNC_ID });
            }
        }

        // GET: ControleCNH/Details/5
        public ActionResult Details(int? id)
        {

            TempData["Titulo"] = "Detalhes Controle CNH";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControleCNHVinculoDomainModel domainModel = controleCNHBusiness.GetControleCNHByVinculo(id.Value);
            if (domainModel == null)
            {
                return HttpNotFound();
            }
            ControleCNHVinculoModelView controleCNH = new ControleCNHVinculoModelView()
            {
                CARGO = domainModel.CARGO,
                CRG_ID = domainModel.CRG_ID,
                FUN_FOTO = domainModel.FUN_FOTO,
                FUN_ID = domainModel.FUN_ID,
                FUN_NOME = domainModel.FUN_NOME,
                MATRICULA = domainModel.MATRICULA,
                SITUACAO = domainModel.SITUACAO,
                TIPO = domainModel.TIPO,
                UNIDADE = domainModel.UNIDADE,
                VNC_ID = domainModel.VNC_ID
            };
            List<ControleCNHModelView> dModel = new List<ControleCNHModelView>();
            Mapper.Map(domainModel.ControleCNH.Select(x => x), dModel);
            controleCNH.ControleCNH = dModel;

            var lista = controleCNH.ControleCNH.Count > 0 ? controleCNH.ControleCNH.Where(x => x.auxiliar != null).SingleOrDefault().auxiliar.Select(x => new ControleCNHModelView
            {
                CNHA_CATEGORIA = x.CNHA_CATEGORIA,
                CNHA_DATARECEBIMENTO = x.CNHA_DATARECEBIMENTO,
                CNHA_EMISSAO = x.CNHA_EMISSAO,
                CNHA_VALIDADE = x.CNHA_VALIDADE,
                CNH_NREG = x.CNH.CNH_NREG,
                CNH_ID = x.CNH.CNH_ID,
                VNC_ID = x.CNH.VNC_ID,
                CNHA_ID = x.CNHA_ID
            }).ToList() : null;

            TempData["ListControleCNH"] = lista;

            return View(controleCNH);
        }

        // GET: ControleCNH/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ControleCNH/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ControleCNH/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ControleCNH/Edit/5
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

        // GET: ControleCNH/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "Excluir Dados de CNH";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControleCNHModelView controle = new ControleCNHModelView();
            ControleCNHAuxiliarDomainModel _domainModel = controleCNHBusiness.GetControleCNHAById(id.Value);
            if (_domainModel == null)
            {
                return HttpNotFound();
            }
            controle = new ControleCNHModelView()
            {
                CNH_NREG = _domainModel.CNH.CNH_NREG,
                CNH_STATUS = _domainModel.CNH.CNH_STATUS,
                CNH_ID = _domainModel.CNH_ID,
                CNHA_CATEGORIA = _domainModel.CNHA_CATEGORIA,
                CNHA_DATARECEBIMENTO = _domainModel.CNHA_DATARECEBIMENTO,
                CNHA_EMISSAO = _domainModel.CNHA_EMISSAO,
                CNHA_VALIDADE = _domainModel.CNHA_VALIDADE,
                CNHA_ID = _domainModel.CNHA_ID.Value,
                CNHA_STATUS = _domainModel.CNHA_STATUS,
                VNC_ID = _domainModel.CNH.VNC_ID
            };

            return PartialView(controle);
        }

        // POST: ControleCNH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ControleCNHAuxiliarDomainModel domainModel = new ControleCNHAuxiliarDomainModel();
            try
            {
                domainModel = controleCNHBusiness.GetControleCNHAById(id);
                if (domainModel != null)
                {
                    domainModel.CNHA_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    controleCNHBusiness.DeleteControleCNHFilho(domainModel);

                    if (controleCNHBusiness.Salvar())
                    {
                        if (controleCNHBusiness.Salvar())
                        {
                            TempData["msgSuccess"] = msg.MensagemSucesso().ToString();
                            return RedirectToAction("Details", new { id = domainModel.CNH.VNC_ID });
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


                return RedirectToAction("Details", new { id = domainModel.CNH.VNC_ID });
            }

        }
    }
}
