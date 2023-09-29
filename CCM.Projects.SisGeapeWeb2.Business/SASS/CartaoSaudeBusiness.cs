using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeape2.Domain.SASS;
using CCM.Projects.SisGeapeWeb2.Business.Interface.SASS;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using CCM.Projects.SisGeapeWeb2.Repository.Repository.SASS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business.SASS
{
    public class CartaoSaudeBusiness : ICartaoSaudeBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FuncionarioRepository _funcionarioRepository;
        private readonly VinculoRepository _vinculoRepository;
        private readonly JustificativaPontoRepository _justificativaRepository;
        private readonly UnidadeRepository _unidadeRepository;
        private readonly ConsultaRepository _consultaRepository;
        private readonly byte[] imagemByteDados = System.IO.File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));

        public CartaoSaudeBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _funcionarioRepository = new FuncionarioRepository(_unitOfWork);
            _vinculoRepository = new VinculoRepository(_unitOfWork);
            _justificativaRepository = new JustificativaPontoRepository(_unitOfWork);
            _unidadeRepository = new UnidadeRepository(_unitOfWork);
            _consultaRepository = new ConsultaRepository(_unitOfWork);
        }

        public bool AddUpdateConsulta(ConsultaDomainModel domainModel)
        {
            try
            {
                sass_consulta consulta;
                if (domainModel.CON_ID == 0)
                {
                    consulta = new sass_consulta()
                    {
                        CON_ACIDOURICO = domainModel.CON_ACIDOURICO,
                        CON_ALTURA = domainModel.CON_ALTURA,
                        CON_ID = domainModel.CON_ID,
                        CON_CIRCUNFERENCIAABDOMINAL = domainModel.CON_CIRCUNFERENCIAABDOMINAL,
                        CON_COLESTEROLTOTAL = domainModel.CON_COLESTEROLTOTAL,
                        CON_DATA = domainModel.CON_DATA,
                        CON_DIABETICO = domainModel.CON_DIABETICO,
                        CON_ETILISMO = domainModel.CON_ETILISMO,
                        CON_FUMA = domainModel.CON_FUMA,
                        CON_GLICEMIA = domainModel.CON_GLICEMIA,
                        CON_HDL = domainModel.CON_HDL,
                        CON_ANTIHBS = domainModel.CON_ANTIHBS,
                        CON_HIPERTENSO = domainModel.CON_HIPERTENSO,
                        CON_IMC = domainModel.CON_IMC,
                        CON_LDL = domainModel.CON_LDL,
                        CON_OBSERVACAO = domainModel.CON_OBSERVACAO,
                        CON_PESO = domainModel.CON_PESO,
                        CON_PRESSAOARTERIALMAX = domainModel.CON_PRESSAOARTERIALMAX,
                        CON_PRESSAOARTERIALMIN = domainModel.CON_PRESSAOARTERIALMIN,
                        CON_RESPATENDIMENTO = domainModel.CON_RESPATENDIMENTO,
                        CON_SEDENTARISMO = domainModel.CON_SEDENTARISMO,
                        CON_TRIGLICERIDEOS = domainModel.CON_TRIGLICERIDEOS,
                        CON_TIPO = domainModel.CON_TIPO,
                        FUN_ID = domainModel.FUN_ID,
                        CON_REGDATE = DateTime.Now,
                        CON_REGUSER = domainModel.CON_REGUSER,
                        CON_STATUS = "A"

                    };
                    _consultaRepository.Insert(consulta);
                }
                else
                {
                    consulta = _consultaRepository.SingleOrDefault(x => x.CON_STATUS == "A" && x.CON_ID == domainModel.CON_ID);
                    consulta.CON_ACIDOURICO = domainModel.CON_ACIDOURICO;
                    consulta.CON_ALTURA = domainModel.CON_ALTURA;
                    consulta.CON_CIRCUNFERENCIAABDOMINAL = domainModel.CON_CIRCUNFERENCIAABDOMINAL;
                    consulta.CON_COLESTEROLTOTAL = domainModel.CON_COLESTEROLTOTAL;
                    consulta.CON_DATA = domainModel.CON_DATA;
                    consulta.CON_ANTIHBS = domainModel.CON_ANTIHBS;
                    consulta.CON_DIABETICO = domainModel.CON_DIABETICO;
                    consulta.CON_ETILISMO = domainModel.CON_ETILISMO;
                    consulta.CON_FUMA = domainModel.CON_FUMA;
                    consulta.CON_GLICEMIA = domainModel.CON_GLICEMIA;
                    consulta.CON_HDL = domainModel.CON_HDL;
                    consulta.CON_HIPERTENSO = domainModel.CON_HIPERTENSO;
                    consulta.CON_IMC = domainModel.CON_IMC;
                    consulta.CON_LDL = domainModel.CON_LDL;
                    consulta.CON_OBSERVACAO = domainModel.CON_OBSERVACAO;
                    consulta.CON_PESO = domainModel.CON_PESO;
                    consulta.CON_PRESSAOARTERIALMAX = domainModel.CON_PRESSAOARTERIALMAX;
                    consulta.CON_PRESSAOARTERIALMIN = domainModel.CON_PRESSAOARTERIALMIN;
                    consulta.CON_RESPATENDIMENTO = domainModel.CON_RESPATENDIMENTO;
                    consulta.CON_SEDENTARISMO = domainModel.CON_SEDENTARISMO;
                    consulta.CON_TRIGLICERIDEOS = domainModel.CON_TRIGLICERIDEOS;
                    consulta.CON_TIPO = domainModel.CON_TIPO;
                    consulta.CON_REGDATE = DateTime.Now;
                    consulta.CON_REGUSER = domainModel.CON_REGUSER;
                    _consultaRepository.Update(consulta);
                };
                return true;
            }
            catch
            { return false; }
        }

        public async Task<FuncionarioCartaoSaudeDomainModel> buscarConsultaPorFuncionario(int id)
        {
            try
            {

                return await _funcionarioRepository.FirstOrDefaultAsync(z => z.FUN_STATUS == "A" && z.FUN_ID == id).ContinueWith(x => new FuncionarioCartaoSaudeDomainModel
                {
                    FUN_ID = x.Result.FUN_ID,
                    FUN_MATRICULA = x.Result.FUN_MATRICULA,
                    FUN_NOME = x.Result.FUN_NOME,
                    FUN_NOMECRACHA = x.Result.FUN_NOMECRACHA,
                    FUN_NUMEROEND = x.Result.FUN_NUMEROEND,
                    FUN_SEXO = x.Result.FUN_SEXO,
                    FUN_TIPOSANGUINEO = x.Result.FUN_TIPOSANGUINEO,
                    FUN_BAIRRO = x.Result.FUN_BAIRRO,
                    FUN_COMPLEMENTO = x.Result.FUN_COMPLEMENTO,
                    FUN_ENDERECO = x.Result.FUN_ENDERECO,
                    FUN_DATANASCIMENTO = x.Result.FUN_DATANASCIMENTO,
                    FUN_FOTO = x.Result.ap_funcionarioxfoto.Any(z => z.FUNFT_STATUS == "A") ? x.Result.ap_funcionarioxfoto.FirstOrDefault(s => s.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                    Consulta = x.Result.sass_consulta.Where(c => c.CON_STATUS == "A").OrderByDescending(c => c.CON_DATA).Select(c => new ConsultaDomainModel
                    {
                        CON_ACIDOURICO = c.CON_ACIDOURICO,
                        CON_ALTURA = c.CON_ALTURA,
                        CON_CIRCUNFERENCIAABDOMINAL = c.CON_CIRCUNFERENCIAABDOMINAL,
                        CON_COLESTEROLTOTAL = c.CON_COLESTEROLTOTAL,
                        CON_DATA = c.CON_DATA,
                        CON_DIABETICO = c.CON_DIABETICO,
                        CON_ETILISMO = c.CON_ETILISMO,
                        CON_FUMA = c.CON_FUMA,
                        CON_ANTIHBS = c.CON_ANTIHBS,
                        CON_GLICEMIA = c.CON_GLICEMIA,
                        CON_HDL = c.CON_HDL,
                        CON_HIPERTENSO = c.CON_HIPERTENSO,
                        CON_ID = c.CON_ID,
                        CON_IMC = c.CON_IMC,
                        CON_LDL = c.CON_LDL,
                        CON_OBSERVACAO = c.CON_OBSERVACAO,
                        CON_PESO = c.CON_PESO,
                        CON_PRESSAOARTERIALMAX = c.CON_PRESSAOARTERIALMAX,
                        CON_PRESSAOARTERIALMIN = c.CON_PRESSAOARTERIALMIN,
                        CON_RESPATENDIMENTO = c.CON_RESPATENDIMENTO,
                        CON_REGUSER = c.CON_REGUSER,
                        CON_SEDENTARISMO = c.CON_SEDENTARISMO,
                        CON_TIPO = c.CON_TIPO,
                        CON_TRIGLICERIDEOS = c.CON_TRIGLICERIDEOS,
                        FUN_ID = c.FUN_ID,
                        FUN_NOME = c.ap_funcionario.FUN_NOMECRACHA
                    }).ToList(),

                });
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public ConsultaDomainModel buscarPorId(int id)
        {
            try
            {
                sass_consulta consulta = _consultaRepository.SingleOrDefault(x => x.CON_STATUS == "A" && x.CON_ID == id);
                return new ConsultaDomainModel()
                {
                    CON_ACIDOURICO = consulta.CON_ACIDOURICO,
                    CON_ALTURA = consulta.CON_ALTURA,
                    CON_CIRCUNFERENCIAABDOMINAL = consulta.CON_CIRCUNFERENCIAABDOMINAL,
                    CON_COLESTEROLTOTAL = consulta.CON_COLESTEROLTOTAL,
                    CON_DATA = consulta.CON_DATA,
                    CON_DIABETICO = consulta.CON_DIABETICO,
                    CON_ETILISMO = consulta.CON_ETILISMO,
                    CON_FUMA = consulta.CON_FUMA,
                    CON_ANTIHBS = consulta.CON_ANTIHBS,
                    CON_GLICEMIA = consulta.CON_GLICEMIA,
                    CON_HDL = consulta.CON_HDL,
                    CON_HIPERTENSO = consulta.CON_HIPERTENSO,
                    CON_ID = consulta.CON_ID,
                    CON_IMC = consulta.CON_IMC,
                    CON_LDL = consulta.CON_LDL,
                    CON_OBSERVACAO = consulta.CON_OBSERVACAO,
                    CON_PESO = consulta.CON_PESO,
                    CON_PRESSAOARTERIALMAX = consulta.CON_PRESSAOARTERIALMAX,
                    CON_PRESSAOARTERIALMIN = consulta.CON_PRESSAOARTERIALMIN,
                    CON_RESPATENDIMENTO = consulta.CON_RESPATENDIMENTO,
                    CON_REGUSER = consulta.CON_REGUSER,
                    CON_SEDENTARISMO = consulta.CON_SEDENTARISMO,
                    CON_TIPO = consulta.CON_TIPO,
                    CON_TRIGLICERIDEOS = consulta.CON_TRIGLICERIDEOS,
                    FUN_ID = consulta.FUN_ID,
                    FUN_NOME = consulta.ap_funcionario.FUN_NOMECRACHA,
                    Funcionario = new FuncionarioCartaoSaudeDomainModel
                    {
                        FUN_ID = consulta.ap_funcionario.FUN_ID,
                        FUN_MATRICULA = consulta.ap_funcionario.FUN_MATRICULA,
                        FUN_NOME = consulta.ap_funcionario.FUN_NOME,
                        FUN_NOMECRACHA = consulta.ap_funcionario.FUN_NOMECRACHA,
                        FUN_NUMEROEND = consulta.ap_funcionario.FUN_NUMEROEND,
                        FUN_SEXO = consulta.ap_funcionario.FUN_SEXO,
                        FUN_TIPOSANGUINEO = consulta.ap_funcionario.FUN_TIPOSANGUINEO,
                        FUN_BAIRRO = consulta.ap_funcionario.FUN_BAIRRO,
                        FUN_COMPLEMENTO = consulta.ap_funcionario.FUN_COMPLEMENTO,
                        FUN_ENDERECO = consulta.ap_funcionario.FUN_ENDERECO,
                        FUN_DATANASCIMENTO = consulta.ap_funcionario.FUN_DATANASCIMENTO,
                        FUN_FOTO = consulta.ap_funcionario.ap_funcionarioxfoto.Any(z => z.FUNFT_STATUS == "A") ? consulta.ap_funcionario.ap_funcionarioxfoto.FirstOrDefault(s => s.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,

                    }
                };
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public bool DeleteConsulta(ConsultaDomainModel domainModel)
        {
            try
            {
                sass_consulta consulta = _consultaRepository.FirstOrDefaultAsync(x => x.CON_STATUS == "A" && x.CON_ID == domainModel.CON_ID).Result;
                consulta.CON_REGDATE = DateTime.Now;
                consulta.CON_REGUSER = domainModel.CON_REGUSER;
                consulta.CON_STATUS = "I";
                _consultaRepository.Update(consulta);
                return true;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public List<PesquisaCartaoPontoDomainModel> getPesquisa(PesquisaCartaoPontoDomainModel pesquisa)
        {

            List<PesquisaCartaoPontoDomainModel> listDomain = new List<PesquisaCartaoPontoDomainModel>();

            var query = _funcionarioRepository.GetAll(x => x.FUN_STATUS == "A").ToList().Select(x => new { x.FUN_ID, x.FUN_MATRICULA, x.FUN_NOME, x.ap_funcionarioxfoto, x.ap_vinculo });


            if (pesquisa.IDCARGO != null)
                query = query.AsParallel().Where(x => x.ap_vinculo.Any(z => z.VNC_STATUS == "A" && z.CRG_ID == pesquisa.IDCARGO && (z.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio || z.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo)));
            if (pesquisa.IDUNIDADE != null)
                query = query.AsParallel().ToList().Where(x => x.ap_vinculo.Any(z => z.VNC_STATUS == "A" && (z.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio || z.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo) && z.ap_vinculoxunidade.Any(p => p.VNCU_STATUS == "A" && p.UND_ID == pesquisa.IDUNIDADE && p.VNCU_DATAFIM == null)));
            if (pesquisa.MATRICULA != null)
                query = query.AsParallel().Where(x => x.FUN_MATRICULA == pesquisa.MATRICULA);
            if (pesquisa.NOME != null)
                query = query.AsParallel().Where(x => x.FUN_NOME.ToUpper().Contains(pesquisa.NOME.ToUpper()));

            listDomain = query.Select(x => new PesquisaCartaoPontoDomainModel
            {
                //CARGO = x.ap_vinculo.CRG_NOME,
                //UNIDADE = x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null) ? x.ap_vinculoxunidade.FirstOrDefault(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA : x.ap_vinculoxunidade.OrderByDescending(z => z.VNCU_ID).FirstOrDefault().ap_unidade.UND_SIGLA,
                MATRICULA = x.FUN_MATRICULA,
                NOME = x.FUN_NOME,
                FUN_ID = x.FUN_ID,
                FUN_FOTO = x.ap_funcionarioxfoto.Where(z => z.FUNFT_STATUS == "A").Count() > 0 ? x.ap_funcionarioxfoto.FirstOrDefault(z => z.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados
            }).Distinct().ToList();

            return listDomain;

            //var query = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A")).ToList().Select(z => new {  z.ap_funcionario, z.ap_vinculoxunidade, z.VNCST_ID,z.CRG_ID,z.FUN_ID, z.ap_cargo});

            //if (pesquisa.IDCARGO != null || pesquisa.IDUNIDADE != null)
            //    query = query.Where(x => x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo);

            //if (pesquisa.IDCARGO != null)
            //    query = query.Where(x => x.CRG_ID == pesquisa.IDCARGO);
            //if (pesquisa.IDUNIDADE != null)
            //    query = query.AsParallel().ToList().Where(x => x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.UND_ID == pesquisa.IDUNIDADE));
            //if (pesquisa.MATRICULA != null)
            //    query = query.AsParallel().ToList().Where(x => x.ap_funcionario.FUN_STATUS == "A" && x.ap_funcionario.FUN_MATRICULA == pesquisa.MATRICULA);
            //if (pesquisa.NOME != null)
            //    query = query.AsParallel().ToList().Where(x => x.ap_funcionario.FUN_STATUS == "A" && x.ap_funcionario.FUN_NOME.ToUpper().Contains(pesquisa.NOME.ToUpper()));

            //listDomain = query.ToList().Select(x => new PesquisaCartaoPontoDomainModel
            //{
            //    CARGO = x.ap_cargo.CRG_NOME,
            //    UNIDADE = x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null) ? x.ap_vinculoxunidade.FirstOrDefault(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA : x.ap_vinculoxunidade.OrderByDescending(z => z.VNCU_ID).FirstOrDefault().ap_unidade.UND_SIGLA,
            //    MATRICULA = x.ap_funcionario.FUN_MATRICULA,
            //    NOME = x.ap_funcionario.FUN_NOME,
            //    FUN_ID = x.FUN_ID,
            //    FUN_FOTO = x.ap_funcionario.ap_funcionarioxfoto.Where(z => z.FUNFT_STATUS == "A").Count() > 0 ? x.ap_funcionario.ap_funcionarioxfoto.FirstOrDefault(z => z.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados
            //}).Distinct().ToList();

            //return listDomain;
        }

        public IEnumerable<ConsultaDomainModel> ListarTodos()
        {
            try
            {
                return _consultaRepository.GetAll(x => x.CON_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A").AsQueryable().Select(c => new ConsultaDomainModel
                {
                    CON_ACIDOURICO = c.CON_ACIDOURICO,
                    CON_ALTURA = c.CON_ALTURA,
                    CON_CIRCUNFERENCIAABDOMINAL = c.CON_CIRCUNFERENCIAABDOMINAL,
                    CON_COLESTEROLTOTAL = c.CON_COLESTEROLTOTAL,
                    CON_DATA = c.CON_DATA,
                    CON_DIABETICO = c.CON_DIABETICO,
                    CON_ETILISMO = c.CON_ETILISMO,
                    CON_FUMA = c.CON_FUMA,
                    CON_GLICEMIA = c.CON_GLICEMIA,
                    CON_HDL = c.CON_HDL,
                    CON_HIPERTENSO = c.CON_HIPERTENSO,
                    CON_ID = c.CON_ID,
                    CON_ANTIHBS = c.CON_ANTIHBS,
                    CON_IMC = c.CON_IMC,
                    CON_LDL = c.CON_LDL,
                    CON_OBSERVACAO = c.CON_OBSERVACAO,
                    CON_PESO = c.CON_PESO,
                    CON_PRESSAOARTERIALMAX = c.CON_PRESSAOARTERIALMAX,
                    CON_PRESSAOARTERIALMIN = c.CON_PRESSAOARTERIALMIN,
                    CON_RESPATENDIMENTO = c.CON_RESPATENDIMENTO,
                    CON_REGUSER = c.CON_REGUSER,
                    CON_SEDENTARISMO = c.CON_SEDENTARISMO,
                    CON_TIPO = c.CON_TIPO,
                    CON_TRIGLICERIDEOS = c.CON_TRIGLICERIDEOS,
                    FUN_ID = c.FUN_ID,
                    FUN_NOME = c.ap_funcionario.FUN_NOMECRACHA,
                    Funcionario = new FuncionarioCartaoSaudeDomainModel
                    {
                        FUN_ID = c.ap_funcionario.FUN_ID,
                        FUN_MATRICULA = c.ap_funcionario.FUN_MATRICULA,
                        FUN_NOME = c.ap_funcionario.FUN_NOME,
                        FUN_NOMECRACHA = c.ap_funcionario.FUN_NOMECRACHA,
                        FUN_NUMEROEND = c.ap_funcionario.FUN_NUMEROEND,
                        FUN_SEXO = c.ap_funcionario.FUN_SEXO,
                        FUN_TIPOSANGUINEO = c.ap_funcionario.FUN_TIPOSANGUINEO,
                        FUN_BAIRRO = c.ap_funcionario.FUN_BAIRRO,
                        FUN_COMPLEMENTO = c.ap_funcionario.FUN_COMPLEMENTO,
                        FUN_ENDERECO = c.ap_funcionario.FUN_ENDERECO,
                        FUN_DATANASCIMENTO = c.ap_funcionario.FUN_DATANASCIMENTO,
                        FUN_FOTO = c.ap_funcionario.ap_funcionarioxfoto.Any(z => z.FUNFT_STATUS == "A") ? c.ap_funcionario.ap_funcionarioxfoto.FirstOrDefault(s => s.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,

                    },
                });


            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<List<VinculoDomainModel>> buscarVinculoPendenteDeAtendimentoPorData(DateTime? inicio, DateTime? fim)
        {
            try
            {

                var list = await _vinculoRepository.GetAllAsync(z => z.VNC_STATUS == "A" && z.VNC_ADMISSAO.Value >= inicio && z.VNC_ADMISSAO.Value <= (fim.HasValue ? fim : DateTime.Today) && !z.ap_funcionario.sass_consulta.Any(x => x.CON_STATUS == "A")).
                     ContinueWith(v => v.Result.Select(x => new VinculoDomainModel()
                     {
                         VNC_NOME = x.VNC_ID + " - " + x.ap_cargo.CRG_NOME + " - " + x.ap_vinculotipo.VNCTP_DESCRICAO,
                         CRG_NOME = x.ap_cargo.CRG_NOME,
                         FUN_ID = x.FUN_ID.Value,
                         VNC_SITUACAO = x.ap_vinculosituacao.VNCST_DESCRICAO,
                         VNC_ADMISSAO = x.VNC_ADMISSAO,
                         VNC_AREA = x.VNC_AREA.Value,
                         VNC_CARGAHORARIA = x.VNC_CARGAHORARIA,
                         VNC_CONCURSO = x.VNC_CONCURSO,
                         VNC_DATACESSAO = x.VNC_DATACESSAO,
                         VNC_DEMISSAO = x.VNC_DEMISSAO,
                         VNC_ENTRADA_APOSENT = x.VNC_ENTRADA_APOSENT,
                         VNC_HISTORICO = x.VNC_HISTORICO,
                         CRG_ID = x.CRG_ID.Value,
                         VNCST_ID = x.VNCST_ID.Value,
                         VNC_ID = x.VNC_ID,
                         VNCTP_ID = x.VNCTP_ID.Value,
                         VNC_ONUS = x.VNC_ONUS,
                         VNC_ORGAO_ORIGEM = x.VNC_ORGAO_ORIGEM,
                         VNC_TIPO = x.ap_vinculotipo.VNCTP_DESCRICAO,
                         VNC_TIPO_APOSENT = x.VNC_TIPO_APOSENT,
                         FUN_NOME = x.ap_funcionario.FUN_NOME,
                     }).ToList());


                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }


        public async Task<bool> Salvar()
        {

            bool result = true;
            try
            {
                await _consultaRepository.SaveAsync();
                return result;
            }
            catch (Exception e)
            {

                throw new InvalidOperationException(e.Message);
            }
        }

        public int TotalRegistro()
        {
            throw new NotImplementedException();

        }


    }
}
