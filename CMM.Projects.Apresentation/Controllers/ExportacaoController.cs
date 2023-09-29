using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class ExportacaoController : Controller
    {
        private PontoSecullum4Entities db = new PontoSecullum4Entities();


        IVinculoBusiness vinculoBusiness;
        ICargoBusiness cargoBusiness;
        IFuncionarioBusiness funcionarioBusiness;
        public ExportacaoController(IVinculoBusiness _vinculoBusiness, ICargoBusiness _cargoBusiness, IFuncionarioBusiness _funcionarioBusiness)
        {
            vinculoBusiness = _vinculoBusiness;
            cargoBusiness = _cargoBusiness;
            funcionarioBusiness = _funcionarioBusiness;
        }

        // GET: Exportacao
        public ActionResult Index()
        {
            return View(db.funcionarios.ToList());
        }

        // GET: Exportacao/Details/5
        public ActionResult Details()
        {
            var listaVinculos = funcionarioBusiness.GetAllFuncionario();


            //}).ToList();
            List<funcionarios> listaExportacao = new List<funcionarios>();
            List<funcionarios> listaAtualizacao = new List<funcionarios>();


            var num = db.funcionarios.OrderByDescending(x => x.id).Select(x => x.n_folha).FirstOrDefault();

            int cont = Convert.ToInt32(num);
            foreach (var item in listaVinculos.Where(x => x.Vinculos != null && (x.FUN_MATRICULA != "" && x.FUN_MATRICULA != "0")))
            {
                cont++;

                //todos os funcionarios ativos.
                if (item.Vinculos.Any(z => z.VNCST_ID == (int)VinculoModelView.Situacao.AguardandoExercicio || z.VNCST_ID == (int)VinculoModelView.Situacao.Ativo))
                {

                    //verifica se existe registro no banco DBSecullum, se não existir, cria novo.
                    if (!db.funcionarios.Any(x => x.n_identificador == item.FUN_MATRICULA || x.n_pis == item.FUN_PIS))
                    {
                        funcionarios novoUsuario = new funcionarios()
                        {
                            n_identificador = item.FUN_MATRICULA,
                            n_pis = item.FUN_PIS != null ? String.Join("", System.Text.RegularExpressions.Regex.Split(item.FUN_PIS, @"[^\d]")) : null,
                            nome = item.FUN_NOME,
                            horario_num = 1,
                            admissao = item.Vinculos.Where(z => z.VNCST_ID == (int)VinculoModelView.Situacao.AguardandoExercicio || z.VNCST_ID == (int)VinculoModelView.Situacao.Ativo).FirstOrDefault().VNC_ADMISSAO,
                            empresa_id = 2,
                            departamento_id = 1,
                            funcao_id = 1,
                            invisivel = false,
                            escala_mensal = false,
                            sexo_masculino = false,
                            web_nao_altera = false,
                            web_bloqueado = false,
                            alt_usuario_id = 1,
                            estado = 0,
                            alt_data = DateTime.Today,
                            master = false,
                            web_auto_aceitar = false,
                            web_solicitacoes = false,
                            web_somente_visto = false,
                            web_nao_incluir_manual = false,
                            n_folha = cont.ToString(),
                        };
                        listaExportacao.Add(novoUsuario);
                        db.funcionarios.Add(novoUsuario);
                    }
                }
                else
                {
                    var func = db.funcionarios.FirstOrDefault(x => x.n_identificador == item.FUN_MATRICULA || x.n_pis == item.FUN_PIS && x.demissao == null);
                    if (func != null)
                    {
                        var vinculo = item.Vinculos.OrderByDescending(x => x.VNC_ID).FirstOrDefault();
                        if (vinculo.VNC_DEMISSAO != null)
                            func.demissao = vinculo.VNC_DEMISSAO; // VNC_DEMISSAO;
                        else if (vinculo.VNC_DATACESSAO != null)
                            func.demissao = vinculo.VNC_DATACESSAO;
                        else if (vinculo.VNC_ENTRADA_APOSENT != null)
                            func.demissao = vinculo.VNC_ENTRADA_APOSENT;
                        else
                            func.demissao = DateTime.Now;

                        db.funcionarios.Attach(func);
                        db.Entry(func).State = EntityState.Modified;



                        listaAtualizacao.Add(func);

                    }
                }
            }
            db.SaveChanges();
            TempData["listDemissao"] = listaAtualizacao;
            return View(listaExportacao);
        }

        // GET: Exportacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exportacao/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,n_folha,nome,n_identificador,carteira,empresa_id,horario_num,estado,funcao_id,departamento_id,admissao,demissao,nao_digital,afast_data_ini,afast_data_fim,afast_motivo,afast_justificativa_id,invisivel,escala_id,escala_mensal,t_horario,senha_equipamento,filtro1_id,filtro2_id,obs,xcampo1,alt2_horario_num,alt3_horario_num,alt4_horario_num,endereco,bairro,cidade,uf,cep,telefone,celular,email,rg,expedicao,ssp,cpf,mae,pai,nascimento,sexo_masculino,estado_civil_id,nacionalidade,naturalidade,web_senha,web_nao_altera,web_bloqueado,alt_usuario_id,alt_data,master,n_provisorio,provisorio_data_ini,provisorio_data_fim,cidade_empresa_id,estrutura_id,assinatura_eletronica,integracao_id,bh_inicio,centro_custos,nivel_id,web_auto_aceitar,n_pis,web_solicitacoes,data_alteracao_portaria,web_data_fechamento,especial_insight_sinha,motivo_demissao_id,web_somente_visto,web_nao_incluir_manual")] funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                db.funcionarios.Add(funcionarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funcionarios);
        }

        // GET: Exportacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            funcionarios funcionarios = db.funcionarios.Find(id);
            if (funcionarios == null)
            {
                return HttpNotFound();
            }
            return View(funcionarios);
        }

        // POST: Exportacao/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,n_folha,nome,n_identificador,carteira,empresa_id,horario_num,estado,funcao_id,departamento_id,admissao,demissao,nao_digital,afast_data_ini,afast_data_fim,afast_motivo,afast_justificativa_id,invisivel,escala_id,escala_mensal,t_horario,senha_equipamento,filtro1_id,filtro2_id,obs,xcampo1,alt2_horario_num,alt3_horario_num,alt4_horario_num,endereco,bairro,cidade,uf,cep,telefone,celular,email,rg,expedicao,ssp,cpf,mae,pai,nascimento,sexo_masculino,estado_civil_id,nacionalidade,naturalidade,web_senha,web_nao_altera,web_bloqueado,alt_usuario_id,alt_data,master,n_provisorio,provisorio_data_ini,provisorio_data_fim,cidade_empresa_id,estrutura_id,assinatura_eletronica,integracao_id,bh_inicio,centro_custos,nivel_id,web_auto_aceitar,n_pis,web_solicitacoes,data_alteracao_portaria,web_data_fechamento,especial_insight_sinha,motivo_demissao_id,web_somente_visto,web_nao_incluir_manual")] funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionarios);
        }

        // GET: Exportacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            funcionarios funcionarios = db.funcionarios.Find(id);
            if (funcionarios == null)
            {
                return HttpNotFound();
            }
            return View(funcionarios);
        }

        // POST: Exportacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            funcionarios funcionarios = db.funcionarios.Find(id);
            db.funcionarios.Remove(funcionarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
