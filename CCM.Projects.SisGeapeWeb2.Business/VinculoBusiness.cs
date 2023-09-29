using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class VinculoBusiness : IVinculoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FuncionarioRepository _funcionarioRepository;

        private readonly VinculoRepository _vinculoRepository;
        private readonly VinculoSituacaoRepository _vinculoSituacaoRepository;
        private readonly VinculoTipoRepository _vinculoTipoRepository;
        private readonly CargoRepository _cargoRepository;
        private readonly SystemUserRepository _systemUserRepository;

        public VinculoBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _funcionarioRepository = new FuncionarioRepository(_unitOfWork);
            _vinculoRepository = new VinculoRepository(_unitOfWork);
            _vinculoSituacaoRepository = new VinculoSituacaoRepository(_unitOfWork);
            _vinculoTipoRepository = new VinculoTipoRepository(_unitOfWork);
            _cargoRepository = new CargoRepository(_unitOfWork);
            _systemUserRepository = new SystemUserRepository(_unitOfWork);
        }

        //metodo pra gerar o nome do login do usuario. Padrão fundacional, PRIMEIRO.ULTIMO_NOME
        private string GetLoginName(string nome)
        {
            string[] lUsuario = nome.Split(' ');

            nome = lUsuario[0] + "." + lUsuario[lUsuario.Length - 1];

            return nome.ToUpper();
        }
        public VinculoDomainModel AddUpdateVinculo(VinculoDomainModel _domainModel)
        {
            try
            {
                if (_domainModel.VNC_ID == 0)
                {


                    ap_vinculo _vinculo = new ap_vinculo();
                    _vinculo.VNCST_ID = _domainModel.VNCST_ID;
                    _vinculo.VNCTP_ID = _domainModel.VNCTP_ID;
                    _vinculo.VNC_ADMISSAO = _domainModel.VNC_ADMISSAO;
                    _vinculo.VNC_AREA = _domainModel.VNC_AREA;
                    _vinculo.VNC_ORGAO_DESTINO = _domainModel.VNC_ORGAO_DESTINO;
                    _vinculo.VNC_QTD = _domainModel.VNC_QTD;
                    _vinculo.VNC_CARGAHORARIA = _domainModel.VNC_CARGAHORARIA;
                    _vinculo.VNC_CONCURSO = _domainModel.VNC_CONCURSO;
                    _vinculo.VNC_DATACESSAO = _domainModel.VNC_DATACESSAO;
                    _vinculo.VNC_DEMISSAO = _domainModel.VNC_DEMISSAO;
                    _vinculo.VNC_ENTRADA_APOSENT = _domainModel.VNC_ENTRADA_APOSENT;
                    _vinculo.VNC_HISTORICO = _domainModel.VNC_HISTORICO;
                    _vinculo.VNC_ONUS = _domainModel.VNC_ONUS;
                    _vinculo.VNC_ORGAO_ORIGEM = _domainModel.VNC_ORGAO_ORIGEM;
                    _vinculo.VNC_REGDATE = DateTime.Now;
                    _vinculo.VNC_REGUSER = _domainModel.VNC_REGUSER;
                    _vinculo.VNC_TIPO_APOSENT = _domainModel.VNC_TIPO_APOSENT;
                    _vinculo.VNC_STATUS = "A";
                    _vinculo.FUN_ID = _domainModel.FUN_ID;
                    _vinculo.CRG_ID = _domainModel.CRG_ID;



                    var result = _vinculoRepository.Insert(_vinculo);

                    var funcionario = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_ID == _domainModel.FUN_ID);
                    if (funcionario != null)
                    {
                        //verifica se não existe usuario do funcionario do vinculo.
                        if (!_systemUserRepository.Exists(x => x.SUSR_STATUS == "A" && x.matricula.ToString() == funcionario.FUN_MATRICULA))
                        {
                            //cria novo usuario com a senha padrão.
                            systemuser novoUsuario = new systemuser()
                            {
                                matricula = Convert.ToInt32(funcionario.FUN_MATRICULA),
                                SUSR_LOGIN = GetLoginName(funcionario.FUN_NOME),
                                SUSR_PASSWORD = APB.Framework.Encryption.Crypto.Encode(funcionario.FUN_MATRICULA),
                                SUSR_NAME = funcionario.FUN_NOME,
                                SUSR_REGDATE = DateTime.Now,
                                SUSR_REGUSER = _domainModel.VNC_REGUSER,
                                SUSR_STATUS = "A",
                            };

                            //adiciona o novo usuario.
                            _systemUserRepository.Insert(novoUsuario);



                        }
                    }
                    _vinculoRepository.Save();

                    _domainModel.VNC_ID = result.VNC_ID;
                }
                else
                {
                    ap_vinculo _vinculo = _vinculoRepository.SingleOrDefault(x => x.VNC_STATUS == "A" && x.VNC_ID == _domainModel.VNC_ID);

                    if (_vinculo != null)
                    {

                        _vinculo.VNCST_ID = _domainModel.VNCST_ID;
                        _vinculo.VNCTP_ID = _domainModel.VNCTP_ID;
                        _vinculo.VNC_ADMISSAO = _domainModel.VNC_ADMISSAO;
                        _vinculo.VNC_AREA = _domainModel.VNC_AREA;
                        _vinculo.VNC_QTD = _domainModel.VNC_QTD;
                        _vinculo.VNC_CARGAHORARIA = _domainModel.VNC_CARGAHORARIA;
                        _vinculo.VNC_CONCURSO = _domainModel.VNC_CONCURSO;
                        _vinculo.VNC_DATACESSAO = _domainModel.VNC_DATACESSAO;
                        _vinculo.VNC_DEMISSAO = _domainModel.VNC_DEMISSAO;
                        _vinculo.VNC_ENTRADA_APOSENT = _domainModel.VNC_ENTRADA_APOSENT;
                        _vinculo.VNC_HISTORICO = _domainModel.VNC_HISTORICO;
                        _vinculo.VNC_ONUS = _domainModel.VNC_ONUS;
                        _vinculo.VNC_ORGAO_ORIGEM = _domainModel.VNC_ORGAO_ORIGEM;
                        _vinculo.VNC_ORGAO_DESTINO = _domainModel.VNC_ORGAO_DESTINO;
                        _vinculo.VNC_REGDATE = DateTime.Now;
                        _vinculo.VNC_REGUSER = _domainModel.VNC_REGUSER;
                        _vinculo.VNC_TIPO_APOSENT = _domainModel.VNC_TIPO_APOSENT;
                        _vinculo.FUN_ID = _domainModel.FUN_ID;
                        _vinculo.CRG_ID = _domainModel.CRG_ID;


                        //verifica se a situação é != de ATIVO e inativa todas as solicitações de crachas pendentes.
                        if (_domainModel.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.ativo && _domainModel.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio)
                        {
                            if (_vinculo.ap_funcionariocracha.Any(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_SITUACAO == 1))
                                foreach (var solCracha in _vinculo.ap_funcionariocracha.Where(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_SITUACAO == 1))
                                {
                                    solCracha.FUNCRC_REGDATE = DateTime.Now;
                                    solCracha.FUNCRC_REGUSER = _domainModel.VNC_REGUSER;
                                    solCracha.FUNCRC_STATUS = "I";
                                }
                        }


                        //busca o login do funcionario do vinculo que tá sendo atualizado
                        var user = _systemUserRepository.SingleOrDefault(x => x.matricula.ToString() == _vinculo.ap_funcionario.FUN_MATRICULA);
                        if (user != null)
                        {
                            //verifica se o funcionario não possui um outro vinculo (ATIVO OU AGUARDANDO EM EXERCICIO) diferente deste que tá atualizando e verifica se a situacao do vinculo que está sendo atualizado não é diferente de (ATIVO OU AGUARDANDO EM EXERCICIO)
                            if ((_vinculo.ap_funcionario.ap_vinculo.Any(x => x.VNC_STATUS == "A" && x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.ativo && x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio && x.VNC_ID != _domainModel.VNC_ID)) && _domainModel.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.ativo && _domainModel.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio)
                                user.SUSR_STATUS = "I";//se verdade, inativa o login do funcionario;
                            else
                                user.SUSR_STATUS = "A";//permanece ativo;

                            user.SUSR_REGDATE = DateTime.Now;
                            user.SUSR_REGUSER = _domainModel.VNC_REGUSER;

                            //atualiza usuario
                            _systemUserRepository.Update(user);

                        }
                        else // se não existir, cria um novo.
                        {
                            if (!_systemUserRepository.Exists(x => x.SUSR_STATUS == "A" && x.matricula.ToString() == _vinculo.ap_funcionario.FUN_MATRICULA))
                            {
                                //cria novo usuario com a senha padrão.
                                systemuser novoUsuario = new systemuser()
                                {
                                    matricula = Convert.ToInt32(_vinculo.ap_funcionario.FUN_MATRICULA),
                                    SUSR_LOGIN = GetLoginName(_vinculo.ap_funcionario.FUN_NOME),
                                    SUSR_PASSWORD = APB.Framework.Encryption.Crypto.Encode(_vinculo.ap_funcionario.FUN_MATRICULA),
                                    SUSR_NAME = _vinculo.ap_funcionario.FUN_NOME,
                                    SUSR_REGDATE = DateTime.Now,
                                    SUSR_REGUSER = _domainModel.VNC_REGUSER,
                                    SUSR_STATUS = "A",
                                };

                                //adiciona o novo usuario.
                                _systemUserRepository.Insert(novoUsuario);



                            }
                        }
                        //atualiza o vinculo;
                        _vinculoRepository.Update(_vinculo);
                        //salva
                        _vinculoRepository.Save();


                    }
                }
                return _domainModel;

            }
            catch { throw new InvalidOperationException(); }
        }


        public bool AddUpdateVinculoSituacao(VinculoSituacaoDomainModel _domainModel)
        {

            try
            {
                ap_vinculosituacao _vncst;
                if (_domainModel.VNCST_ID == 0)
                {
                    _vncst = new ap_vinculosituacao();

                    _vncst.VNCST_DESCRICAO = _domainModel.VNCST_DESCRICAO.ToUpper();
                    _vncst.VNCST_REGUSER = _domainModel.VNCST_REGUSER;
                    _vncst.VNCST_REGDATE = DateTime.Now;
                    _vncst.VNCST_STATUS = "A";

                    _vinculoSituacaoRepository.Insert(_vncst);
                    return true;

                }
                else
                {
                    _vncst = _vinculoSituacaoRepository.SingleOrDefault(x => x.VNCST_STATUS == "A" && x.VNCST_ID == _domainModel.VNCST_ID);

                    if (_vncst != null)
                    {
                        _vncst.VNCST_DESCRICAO = _domainModel.VNCST_DESCRICAO.ToUpper();
                        _vncst.VNCST_REGUSER = _domainModel.VNCST_REGUSER;
                        _vncst.VNCST_REGDATE = DateTime.Now;
                        _vncst.VNCST_STATUS = "A";

                        _vinculoSituacaoRepository.Update(_vncst);
                        return true;

                    }
                    return false;
                }

            }
            catch { return false; }
        }

        public bool AddUpdateVinculoTipo(VinculoTipoDomainModel _domainModel)
        {
            try
            {
                ap_vinculotipo _vnctp;
                if (_domainModel.VNCTP_ID == 0)
                {
                    _vnctp = new ap_vinculotipo();
                    _vnctp.VNCTP_DESCRICAO = _domainModel.VNCTP_DESCRICAO.ToUpper();
                    _vnctp.VNCTP_REGUSER = _domainModel.VNCTP_REGUSER;
                    _vnctp.VNCTP_REGDATE = DateTime.Now;
                    _vnctp.VNCTP_STATUS = "A";

                    _vinculoTipoRepository.Insert(_vnctp);
                    return true;
                }
                else
                {
                    _vnctp = _vinculoTipoRepository.SingleOrDefault(x => x.VNCTP_STATUS == "A" && x.VNCTP_ID == _domainModel.VNCTP_ID);
                    if (_vnctp != null)
                    {
                        _vnctp.VNCTP_DESCRICAO = _domainModel.VNCTP_DESCRICAO.ToUpper();
                        _vnctp.VNCTP_REGUSER = _domainModel.VNCTP_REGUSER;
                        _vnctp.VNCTP_REGDATE = DateTime.Now;
                        _vinculoTipoRepository.Update(_vnctp);

                        return true;
                    }
                    return false;
                }

            }
            catch { return false; }
        }

        public List<VinculoDomainModel> ddlVinculoByFuncionario(int id)
        {
            var listDomain = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.FUN_ID == id /* && x.VNC_DEMISSAO != null*/).ToList();


            return listDomain.Select(x => new VinculoDomainModel
            {
                VNC_NOME = x.VNC_ID + " - " + x.ap_cargo.CRG_NOME + " - " + x.ap_vinculotipo.VNCTP_DESCRICAO,
                CRG_NOME = x.ap_cargo.CRG_NOME,
                FUN_ID = x.FUN_ID.Value,
                VNC_QTD = x.VNC_QTD,
                VNC_ORGAO_DESTINO = x.VNC_ORGAO_DESTINO,
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

            }).ToList();
        }

        public bool DeleteVinculo(VinculoDomainModel _domainModel)
        {

            ap_vinculo _vinculo = _vinculoRepository.SingleOrDefault(x => x.VNC_STATUS == "A" && x.VNC_ID == _domainModel.VNC_ID);

            if (_vinculo != null)
            {
                _vinculo.VNC_REGDATE = DateTime.Now;
                _vinculo.VNC_REGUSER = _domainModel.VNC_REGUSER;
                _vinculo.VNC_STATUS = "I";

                //se existir solicitações de crachás pendentes, inativa todas.
                if (_vinculo.ap_funcionariocracha.Any(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_SITUACAO == 1))
                    foreach (var solCracha in _vinculo.ap_funcionariocracha.Where(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_SITUACAO == 1))
                    {
                        solCracha.FUNCRC_REGDATE = DateTime.Now;
                        solCracha.FUNCRC_REGUSER = _domainModel.VNC_REGUSER;
                        solCracha.FUNCRC_STATUS = "I";
                    }
                //busca usuário do funcionario do vinculo a ser ocultado.
                var user = _systemUserRepository.SingleOrDefault(x => x.SUSR_STATUS == "A" && x.matricula.ToString() == _vinculo.ap_funcionario.FUN_MATRICULA);
                if (user != null)
                {
                    //se não existir outro vinculo ativo ou aguardando em exercicio, inativa o usuario do funcionario.
                    if (!(_vinculo.ap_funcionario.ap_vinculo.Any(x => x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.ativo && x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio && x.VNC_ID != _domainModel.VNC_ID)))
                    {
                        user.SUSR_STATUS = "I";
                        user.SUSR_REGDATE = DateTime.Now;
                        user.SUSR_REGUSER = _domainModel.VNC_REGUSER;

                        _systemUserRepository.Update(user);
                    }

                }

                _vinculoRepository.Update(_vinculo);

                return true;
            }
            return false;
        }

        public bool DeleteVinculoSituacao(VinculoSituacaoDomainModel _domainModel)
        {
            ap_vinculosituacao _vncst = _vinculoSituacaoRepository.SingleOrDefault(x => x.VNCST_STATUS == "A" && x.VNCST_ID == _domainModel.VNCST_ID);

            if (_vncst != null)
            {
                _vncst.VNCST_REGUSER = _domainModel.VNCST_REGUSER;
                _vncst.VNCST_REGDATE = DateTime.Now;
                _vncst.VNCST_STATUS = "I";

                _vinculoSituacaoRepository.Update(_vncst);
                return true;

            }
            return false;
        }

        public bool DeleteVinculoTipo(VinculoTipoDomainModel _domainModel)
        {
            ap_vinculotipo _vnctp = _vinculoTipoRepository.SingleOrDefault(x => x.VNCTP_STATUS == "A" && x.VNCTP_ID == _domainModel.VNCTP_ID);
            if (_vnctp != null)
            {
                _vnctp.VNCTP_REGUSER = _domainModel.VNCTP_REGUSER;
                _vnctp.VNCTP_REGDATE = DateTime.Now;
                _vnctp.VNCTP_STATUS = "I";
                _vinculoTipoRepository.Update(_vnctp);

                return true;
            }
            return false;
        }

        public List<VinculoDomainModel> BuscarTodosAtivos()
        {
            return _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A" && (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio)).ToList().Select(x => new VinculoDomainModel
            {

                CRG_ID = x.CRG_ID.Value,
                CRG_NOME = x.ap_cargo.CRG_NOME,
                FUN_ID = x.FUN_ID.Value,
                VNCST_ID = x.VNCST_ID.Value,
                VNCTP_ID = x.VNCTP_ID.Value,
                VNC_ADMISSAO = x.VNC_ADMISSAO,
                VNC_AREA = x.VNC_AREA.Value,
                VNC_CARGAHORARIA = x.VNC_CARGAHORARIA,
                VNC_CONCURSO = x.VNC_CONCURSO,
                VNC_DATACESSAO = x.VNC_DATACESSAO,
                VNC_DEMISSAO = x.VNC_DEMISSAO,
                VNC_ENTRADA_APOSENT = x.VNC_ENTRADA_APOSENT,
                VNC_HISTORICO = x.VNC_HISTORICO,
                VNC_ID = x.VNC_ID,
                VNC_ONUS = x.VNC_ONUS,
                VNC_ORGAO_DESTINO = x.VNC_ORGAO_DESTINO,
                VNC_ORGAO_ORIGEM = x.VNC_ORGAO_ORIGEM,
                VNC_NOME = x.VNC_ID + " - " + x.ap_cargo.CRG_NOME + " - " + x.ap_vinculotipo.VNCTP_DESCRICAO,
                VNC_SITUACAO = x.ap_vinculosituacao.VNCST_DESCRICAO,
                VNC_TIPO = x.ap_vinculotipo.VNCTP_DESCRICAO,
                Lotacao = x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A") ? x.ap_vinculoxunidade.AsParallel().Where(p => p.VNCU_STATUS == "A").Select(_lotacao => new VinculoUnidadeDomainModel()
                {
                    UND_ID = _lotacao.UND_ID.Value,
                    UND_NOME = _lotacao.ap_unidade != null ? _lotacao.ap_unidade.UND_SIGLA + " - " + _lotacao.ap_unidade.UND_NOME : null,
                    VNCU_ATRIBUICAO = _lotacao.VNCU_ATRIBUICAO,
                    VNCU_DATAFIM = _lotacao.VNCU_DATAFIM,
                    VNCU_DATAINICIO = _lotacao.VNCU_DATAINICIO,
                    VNCU_ID = _lotacao.VNCU_ID,
                }).ToList() : null,
                Funcionario = new FuncionarioDomainModel
                {
                    FUN_ID = x.ap_funcionario.FUN_ID,
                    FUN_MATRICULA = x.ap_funcionario.FUN_MATRICULA,
                    BNC_ID = x.ap_funcionario.BNC_ID,
                    FUN_AGENCIA = x.ap_funcionario.FUN_AGENCIA,
                    FUN_ASSISTENCIAMEDICA = x.ap_funcionario.FUN_ASSISTENCIAMEDICA,
                    FUN_BAIRRO = x.ap_funcionario.FUN_BAIRRO,
                    FUN_CELULAR = x.ap_funcionario.FUN_CELULAR,
                    FUN_CEP = x.ap_funcionario.FUN_CEP,
                    FUN_CERTIFICADOMILITAR = x.ap_funcionario.FUN_CERTIFICADOMILITAR,
                    FUN_CIDADE = x.ap_funcionario.FUN_CIDADE,
                    FUN_CIDADENATAL = x.ap_funcionario.FUN_CIDADENATAL,
                    FUN_CMCORPORACAO = x.ap_funcionario.FUN_CMCORPORACAO,
                    FUN_CMSERIE = x.ap_funcionario.FUN_CMSERIE,
                    FUN_CNH = x.ap_funcionario.FUN_CNH,
                    FUN_CNHCATEGORIA = x.ap_funcionario.FUN_CNHCATEGORIA,
                    FUN_CNHVALIDADE = x.ap_funcionario.FUN_CNHVALIDADE,
                    FUN_COMPLEMENTO = x.ap_funcionario.FUN_COMPLEMENTO,
                    FUN_CONJUGE = x.ap_funcionario.FUN_CONJUGE,
                    FUN_CONTACORRENTE = x.ap_funcionario.FUN_CONTACORRENTE,
                    FUN_CPF = x.ap_funcionario.FUN_CPF,
                    FUN_CTPS = x.ap_funcionario.FUN_CTPS,
                    FUN_CTPSDATAEMISSAO = x.ap_funcionario.FUN_CTPSDATAEMISSAO,
                    FUN_DATANASCIMENTO = x.ap_funcionario.FUN_DATANASCIMENTO,
                    FUN_EMAIL = x.ap_funcionario.FUN_EMAIL,
                    FUN_ENDERECO = x.ap_funcionario.FUN_ENDERECO,
                    FUN_ESCOLARIDADE = x.ap_funcionario.FUN_ESCOLARIDADE,
                    FUN_ESTADOCIVIL = x.ap_funcionario.FUN_ESTADOCIVIL,
                    FUN_IDIOMA = x.ap_funcionario.FUN_IDIOMA,
                    FUN_IMOVEL = x.ap_funcionario.FUN_IMOVEL,
                    FUN_LOGRADOURO = x.ap_funcionario.FUN_LOGRADOURO,
                    FUN_NACIONALIDADE = x.ap_funcionario.FUN_NACIONALIDADE,
                    FUN_NECESSIDADE_ESPECIAL = x.ap_funcionario.FUN_NECESSIDADE_ESPECIAL,
                    FUN_NOME = x.ap_funcionario.FUN_NOME,
                    FUN_MAE = x.ap_funcionario.FUN_NOMEMAE,
                    FUN_PAI = x.ap_funcionario.FUN_NOMEPAI,
                    FUN_NUMEROEND = x.ap_funcionario.FUN_NUMEROEND,
                    FUN_OBSERVACAO = x.ap_funcionario.FUN_OBSERVACAO,
                    FUN_PIS = x.ap_funcionario.FUN_PIS,
                    FUN_RG = x.ap_funcionario.FUN_RG,
                    FUN_RGDATAEMISSAO = x.ap_funcionario.FUN_RGDATAEMISSAO,
                    FUN_RGEMISSOR = x.ap_funcionario.FUN_RGEMISSOR,
                    FUN_RGESTADO = x.ap_funcionario.FUN_RGESTADO,
                    FUN_SEXO = x.ap_funcionario.FUN_SEXO,
                    FUN_TELEFONE = x.ap_funcionario.FUN_TELEFONE,
                    FUN_TIPOSANGUINEO = x.ap_funcionario.FUN_TIPOSANGUINEO,
                    FUN_TITULODATAEMISSAO = x.ap_funcionario.FUN_TITULODATAEMISSAO,
                    FUN_TITULOELEITOR = x.ap_funcionario.FUN_TITULOELEITOR,
                    FUN_TITULOSESSAO = x.ap_funcionario.FUN_TITULOSESSAO,
                    FUN_TITULOZONA = x.ap_funcionario.FUN_TITULOZONA,

                },

            }).ToList();

        }

        public async Task<List<VinculoDomainModel>> GetAllAsyncVinculoByUnidade(int id)
        {
            return await _vinculoRepository.GetAllAsync(v => v.VNC_STATUS == "A" && v.ap_vinculoxunidade.Any(u => u.UND_ID == id && u.VNCU_STATUS == "A"))
                .ContinueWith((t) =>
               t.Result.Count() > 0 ? t.Result.Select(x => new VinculoDomainModel()
               {
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
                   VNC_QTD = x.VNC_QTD,
                   CRG_ID = x.CRG_ID.Value,
                   VNCST_ID = x.VNCST_ID.Value,
                   VNC_ID = x.VNC_ID,
                   VNCTP_ID = x.VNCTP_ID.Value,
                   VNC_ONUS = x.VNC_ONUS,
                   VNC_ORGAO_ORIGEM = x.VNC_ORGAO_ORIGEM,
                   VNC_TIPO = x.ap_vinculotipo.VNCTP_DESCRICAO,
                   VNC_TIPO_APOSENT = x.VNC_TIPO_APOSENT,
                   FUN_NOME = x.ap_funcionario.FUN_NOME,
                   Lotacao = x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A") ? x.ap_vinculoxunidade.AsParallel().Where(p => p.VNCU_STATUS == "A").Select(_lotacao => new VinculoUnidadeDomainModel()
                   {
                       UND_ID = _lotacao.UND_ID.Value,
                       UND_NOME = _lotacao.ap_unidade != null ? _lotacao.ap_unidade.UND_SIGLA + " - " + _lotacao.ap_unidade.UND_NOME : null,
                       VNCU_ATRIBUICAO = _lotacao.VNCU_ATRIBUICAO,
                       VNCU_DATAFIM = _lotacao.VNCU_DATAFIM,
                       VNCU_DATAINICIO = _lotacao.VNCU_DATAINICIO,
                       VNCU_ID = _lotacao.VNCU_ID,
                   }).ToList() : null
               }).ToList() : null);
        }

        public List<VinculoTipoDomainModel> GetAllTipoVinculo()
        {
            return _vinculoTipoRepository.GetAll(x => x.VNCTP_STATUS == "A").Select(x => new VinculoTipoDomainModel
            {
                VNCTP_DESCRICAO = x.VNCTP_DESCRICAO,
                VNCTP_ID = x.VNCTP_ID,
                VNCTP_REGUSER = x.VNCTP_REGUSER
            }).ToList();
        }

        public List<VinculoSituacaoDomainModel> GetAllVinculoSituacao()
        {
            return _vinculoSituacaoRepository.GetAll(x => x.VNCST_STATUS == "A").Select(x => new VinculoSituacaoDomainModel
            {
                VNCST_DESCRICAO = x.VNCST_DESCRICAO,
                VNCST_ID = x.VNCST_ID,
                VNCST_REGUSER = x.VNCST_REGUSER
            }).ToList();
        }

        public List<VinculoDomainModel> GetPageRecords(int pageStart, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<VinculoDomainModel>> GetVinculobyDate(DateTime? inicio, DateTime? fim)
        {
            //return _vinculoRepository.GetAllAsync(z => z.VNC_ADMISSAO.Value.Date >= inicio && z.VNC_ADMISSAO.Value.Date <= fim).ContinueWith(v => v.Result.Select(x => new
            //  VinculoDomainModel
            //{
            //    VNC_NOME = x.VNC_ID + " - " + x.ap_cargo.CRG_NOME + " - " + x.ap_vinculotipo.VNCTP_DESCRICAO,
            //    CRG_NOME = x.ap_cargo.CRG_NOME,
            //    FUN_ID = x.FUN_ID.Value,
            //    VNC_SITUACAO = x.ap_vinculosituacao.VNCST_DESCRICAO,
            //    VNC_ADMISSAO = x.VNC_ADMISSAO,
            //    VNC_AREA = x.VNC_AREA.Value,
            //    VNC_CARGAHORARIA = x.VNC_CARGAHORARIA,
            //    VNC_CONCURSO = x.VNC_CONCURSO,
            //    VNC_DATACESSAO = x.VNC_DATACESSAO,
            //    VNC_DEMISSAO = x.VNC_DEMISSAO,
            //    VNC_ENTRADA_APOSENT = x.VNC_ENTRADA_APOSENT,
            //    VNC_HISTORICO = x.VNC_HISTORICO,
            //    CRG_ID = x.CRG_ID.Value,
            //    VNCST_ID = x.VNCST_ID.Value,
            //    VNC_ID = x.VNC_ID,
            //    VNCTP_ID = x.VNCTP_ID.Value,
            //    VNC_ONUS = x.VNC_ONUS,
            //    VNC_ORGAO_ORIGEM = x.VNC_ORGAO_ORIGEM,
            //    VNC_TIPO = x.ap_vinculotipo.VNCTP_DESCRICAO,
            //    VNC_TIPO_APOSENT = x.VNC_TIPO_APOSENT,
            //    FUN_NOME = x.ap_funcionario.FUN_NOME,
            //}
            //  ).ToList());

            throw new NotImplementedException();
        }

        public List<VinculoDomainModel> GetVinculoByFuncionario(int func)
        {
            var listDomain = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.FUN_ID == func).ToList();


            return listDomain.Select(x => new VinculoDomainModel
            {
                CRG_NOME = x.ap_cargo.CRG_NOME,
                FUN_ID = x.FUN_ID.Value,
                VNC_ORGAO_DESTINO = x.VNC_ORGAO_DESTINO,
                VNC_NOME = x.VNC_ORGAO_DESTINO,
                VNC_QTD = x.VNC_QTD,
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

            }).ToList();

        }

        public VinculoDomainModel GetVinculobyId(int Id)
        {
            var _vinculo = _vinculoRepository.SingleOrDefault(x => x.VNC_STATUS == "A" && x.VNC_ID == Id);


            VinculoDomainModel _domainModel = new VinculoDomainModel()
            {
                CRG_NOME = _vinculo.ap_cargo.CRG_NOME,
                FUN_ID = _vinculo.FUN_ID.Value,
                VNC_SITUACAO = _vinculo.ap_vinculosituacao.VNCST_DESCRICAO,
                VNC_ADMISSAO = _vinculo.VNC_ADMISSAO,
                VNC_AREA = _vinculo.VNC_AREA.Value,
                VNC_CARGAHORARIA = _vinculo.VNC_CARGAHORARIA,
                VNC_CONCURSO = _vinculo.VNC_CONCURSO,
                VNC_DATACESSAO = _vinculo.VNC_DATACESSAO,
                VNC_QTD = _vinculo.VNC_QTD,
                VNC_DEMISSAO = _vinculo.VNC_DEMISSAO,
                VNC_ENTRADA_APOSENT = _vinculo.VNC_ENTRADA_APOSENT,
                VNC_HISTORICO = _vinculo.VNC_HISTORICO,
                CRG_ID = _vinculo.CRG_ID.Value,
                VNCST_ID = _vinculo.VNCST_ID.Value,
                VNC_ID = _vinculo.VNC_ID,
                VNCTP_ID = _vinculo.VNCTP_ID.Value,
                VNC_ONUS = _vinculo.VNC_ONUS,
                VNC_ORGAO_ORIGEM = _vinculo.VNC_ORGAO_ORIGEM,
                VNC_TIPO = _vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                VNC_TIPO_APOSENT = _vinculo.VNC_TIPO_APOSENT,
                VNC_ORGAO_DESTINO = _vinculo.VNC_ORGAO_DESTINO,

                FUN_NOME = _vinculo.ap_funcionario.FUN_NOME,
            };
            return _domainModel;
        }

        public VinculoSituacaoDomainModel GetVinculoSituacaobyId(int Id)
        {
            var vncst = _vinculoSituacaoRepository.SingleOrDefault(x => x.VNCST_STATUS == "A" && x.VNCST_ID == Id);
            VinculoSituacaoDomainModel _domainModel = new VinculoSituacaoDomainModel()
            {
                VNCST_DESCRICAO = vncst.VNCST_DESCRICAO,
                VNCST_ID = vncst.VNCST_ID,
                VNCST_REGUSER = vncst.VNCST_REGUSER
            };
            return _domainModel;
        }

        public VinculoTipoDomainModel GetVinculoTipobyId(int Id)
        {
            var vnctp = _vinculoTipoRepository.SingleOrDefault(x => x.VNCTP_STATUS == "A" && x.VNCTP_ID == Id);

            VinculoTipoDomainModel _domainModel = new VinculoTipoDomainModel()
            {
                VNCTP_DESCRICAO = vnctp.VNCTP_DESCRICAO,
                VNCTP_ID = vnctp.VNCTP_ID,
                VNCTP_REGUSER = vnctp.VNCTP_REGUSER
            };

            return _domainModel;
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _vinculoRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalCount()
        {
            return _vinculoRepository.Count(x => x.VNC_STATUS == "A");
        }

        public int TotalCountVinculoSituacao()
        {
            return _vinculoSituacaoRepository.Count(x => x.VNCST_STATUS == "A");
        }

        public int TotalCountVinculoTipo()
        {
            return _vinculoTipoRepository.Count(x => x.VNCTP_STATUS == "A");
        }

        public bool ValidarDelete(int vinculo)
        {
            return !(_vinculoRepository.Exists(x => x.VNC_ID == vinculo && x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A")));
        }

        public List<VinculoDomainModel> BuscarTodos()
        {
            return _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A").ToList().Select(x => new VinculoDomainModel
            {

                CRG_ID = x.CRG_ID.Value,
                CRG_NOME = x.ap_cargo.CRG_NOME,
                FUN_ID = x.FUN_ID.Value,
                VNCST_ID = x.VNCST_ID.Value,
                VNCTP_ID = x.VNCTP_ID.Value,
                VNC_ADMISSAO = x.VNC_ADMISSAO,
                VNC_AREA = x.VNC_AREA.Value,
                VNC_CARGAHORARIA = x.VNC_CARGAHORARIA,
                VNC_CONCURSO = x.VNC_CONCURSO,
                VNC_DATACESSAO = x.VNC_DATACESSAO,
                VNC_DEMISSAO = x.VNC_DEMISSAO,
                VNC_ENTRADA_APOSENT = x.VNC_ENTRADA_APOSENT,
                VNC_HISTORICO = x.VNC_HISTORICO,
                VNC_QTD = x.VNC_QTD,
                VNC_TIPO_APOSENT = x.VNC_TIPO_APOSENT,
                VNC_ID = x.VNC_ID,
                VNC_ONUS = x.VNC_ONUS,
                VNC_ORGAO_DESTINO = x.VNC_ORGAO_DESTINO,
                VNC_ORGAO_ORIGEM = x.VNC_ORGAO_ORIGEM,
                VNC_NOME = x.VNC_ID + " - " + x.ap_cargo.CRG_NOME + " - " + x.ap_vinculotipo.VNCTP_DESCRICAO,
                VNC_SITUACAO = x.ap_vinculosituacao.VNCST_DESCRICAO,
                VNC_TIPO = x.ap_vinculotipo.VNCTP_DESCRICAO,
                Lotacao = x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A") ? x.ap_vinculoxunidade.AsParallel().Where(p => p.VNCU_STATUS == "A").Select(_lotacao => new VinculoUnidadeDomainModel()
                {
                    UND_ID = _lotacao.UND_ID.Value,
                    UND_NOME = _lotacao.ap_unidade != null ? _lotacao.ap_unidade.UND_SIGLA + " - " + _lotacao.ap_unidade.UND_NOME : null,
                    VNCU_ATRIBUICAO = _lotacao.VNCU_ATRIBUICAO,
                    VNCU_DATAFIM = _lotacao.VNCU_DATAFIM,
                    VNCU_DATAINICIO = _lotacao.VNCU_DATAINICIO,
                    VNCU_ID = _lotacao.VNCU_ID,
                }).ToList() : null,
                Funcionario = new FuncionarioDomainModel
                {
                    FUN_ID = x.ap_funcionario.FUN_ID,
                    FUN_MATRICULA = x.ap_funcionario.FUN_MATRICULA,
                    BNC_ID = x.ap_funcionario.BNC_ID,
                    FUN_AGENCIA = x.ap_funcionario.FUN_AGENCIA,
                    FUN_ASSISTENCIAMEDICA = x.ap_funcionario.FUN_ASSISTENCIAMEDICA,
                    FUN_BAIRRO = x.ap_funcionario.FUN_BAIRRO,
                    FUN_CELULAR = x.ap_funcionario.FUN_CELULAR,
                    FUN_CEP = x.ap_funcionario.FUN_CEP,
                    FUN_CERTIFICADOMILITAR = x.ap_funcionario.FUN_CERTIFICADOMILITAR,
                    FUN_CIDADE = x.ap_funcionario.FUN_CIDADE,
                    FUN_CIDADENATAL = x.ap_funcionario.FUN_CIDADENATAL,
                    FUN_CMCORPORACAO = x.ap_funcionario.FUN_CMCORPORACAO,
                    FUN_CMSERIE = x.ap_funcionario.FUN_CMSERIE,
                    FUN_CNH = x.ap_funcionario.FUN_CNH,
                    FUN_CNHCATEGORIA = x.ap_funcionario.FUN_CNHCATEGORIA,
                    FUN_CNHVALIDADE = x.ap_funcionario.FUN_CNHVALIDADE,
                    FUN_COMPLEMENTO = x.ap_funcionario.FUN_COMPLEMENTO,
                    FUN_CONJUGE = x.ap_funcionario.FUN_CONJUGE,
                    FUN_CONTACORRENTE = x.ap_funcionario.FUN_CONTACORRENTE,
                    FUN_CPF = x.ap_funcionario.FUN_CPF,
                    FUN_CTPS = x.ap_funcionario.FUN_CTPS,
                    FUN_CTPSDATAEMISSAO = x.ap_funcionario.FUN_CTPSDATAEMISSAO,
                    FUN_DATANASCIMENTO = x.ap_funcionario.FUN_DATANASCIMENTO,
                    FUN_EMAIL = x.ap_funcionario.FUN_EMAIL,
                    FUN_ENDERECO = x.ap_funcionario.FUN_ENDERECO,
                    FUN_ESCOLARIDADE = x.ap_funcionario.FUN_ESCOLARIDADE,
                    FUN_ESTADOCIVIL = x.ap_funcionario.FUN_ESTADOCIVIL,
                    FUN_IDIOMA = x.ap_funcionario.FUN_IDIOMA,
                    FUN_IMOVEL = x.ap_funcionario.FUN_IMOVEL,
                    FUN_LOGRADOURO = x.ap_funcionario.FUN_LOGRADOURO,
                    FUN_NACIONALIDADE = x.ap_funcionario.FUN_NACIONALIDADE,
                    FUN_NECESSIDADE_ESPECIAL = x.ap_funcionario.FUN_NECESSIDADE_ESPECIAL,
                    FUN_NOME = x.ap_funcionario.FUN_NOME,
                    FUN_MAE = x.ap_funcionario.FUN_NOMEMAE,
                    FUN_PAI = x.ap_funcionario.FUN_NOMEPAI,
                    FUN_NUMEROEND = x.ap_funcionario.FUN_NUMEROEND,
                    FUN_OBSERVACAO = x.ap_funcionario.FUN_OBSERVACAO,
                    FUN_PIS = x.ap_funcionario.FUN_PIS,
                    FUN_RG = x.ap_funcionario.FUN_RG,
                    FUN_RGDATAEMISSAO = x.ap_funcionario.FUN_RGDATAEMISSAO,
                    FUN_RGEMISSOR = x.ap_funcionario.FUN_RGEMISSOR,
                    FUN_RGESTADO = x.ap_funcionario.FUN_RGESTADO,
                    FUN_SEXO = x.ap_funcionario.FUN_SEXO,
                    FUN_TELEFONE = x.ap_funcionario.FUN_TELEFONE,
                    FUN_TIPOSANGUINEO = x.ap_funcionario.FUN_TIPOSANGUINEO,
                    FUN_TITULODATAEMISSAO = x.ap_funcionario.FUN_TITULODATAEMISSAO,
                    FUN_TITULOELEITOR = x.ap_funcionario.FUN_TITULOELEITOR,
                    FUN_TITULOSESSAO = x.ap_funcionario.FUN_TITULOSESSAO,
                    FUN_TITULOZONA = x.ap_funcionario.FUN_TITULOZONA,

                },

            }).ToList();
        }
    }
}
