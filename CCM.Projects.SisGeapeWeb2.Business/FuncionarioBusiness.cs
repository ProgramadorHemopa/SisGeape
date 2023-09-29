using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeape2.Domain.SASS;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Business.ServiceCorreios;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class FuncionarioBusiness : IFuncionarioBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FuncionarioRepository _funcionarioRepository;
        private readonly VinculoRepository _vinculoRepository;
        private readonly VinculoSituacaoRepository _vinculoSituacaoRepository;

        private readonly VinculoUnidadeRepository _vinculoUnidadeRepository;
        private readonly FuncionarioCrachaRepository _crachaRepository;
        private readonly byte[] imagemByteDados = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));

        public FuncionarioBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _funcionarioRepository = new FuncionarioRepository(_unitOfWork);
            _vinculoRepository = new VinculoRepository(_unitOfWork);
            _vinculoUnidadeRepository = new VinculoUnidadeRepository(_unitOfWork);
            _crachaRepository = new FuncionarioCrachaRepository(_unitOfWork);
            _vinculoSituacaoRepository = new VinculoSituacaoRepository(_unitOfWork);
        }

        //Adiocinado por Angelo Matos em 19082019:1209
        public bool ExistFuncionario(string matricula, int id)
        {
            return _funcionarioRepository.Exists(x => x.FUN_STATUS == "A" && x.FUN_MATRICULA.Equals(matricula) && x.FUN_ID != id);
        }

        public FuncionarioDomainModel AddUpdateFuncionario(FuncionarioDomainModel modelDomain)
        {
            if (modelDomain.FUN_ID == 0)
            {

                ap_funcionario _funcionario = new ap_funcionario()
                {
                    FUN_MATRICULA = modelDomain.FUN_MATRICULA,
                    BNC_ID = modelDomain.BNC_ID,
                    FUN_AGENCIA = modelDomain.FUN_AGENCIA,
                    FUN_ASSISTENCIAMEDICA = modelDomain.FUN_ASSISTENCIAMEDICA,
                    FUN_BAIRRO = modelDomain.FUN_BAIRRO,
                    FUN_CELULAR = modelDomain.FUN_CELULAR,
                    FUN_CEP = (modelDomain.FUN_CEP != null ? Regex.Replace(modelDomain.FUN_CEP, "[^0-9a-zA-Z]+", "") : modelDomain.FUN_CEP),
                    FUN_CERTIFICADOMILITAR = modelDomain.FUN_CERTIFICADOMILITAR,
                    FUN_CIDADE = modelDomain.FUN_CIDADE,
                    FUN_CIDADENATAL = modelDomain.FUN_CIDADENATAL,
                    FUN_CMCORPORACAO = modelDomain.FUN_CMCORPORACAO,
                    FUN_CMSERIE = modelDomain.FUN_CMSERIE,
                    FUN_CNH = modelDomain.FUN_CNH,
                    FUN_CNHCATEGORIA = modelDomain.FUN_CNHCATEGORIA,
                    FUN_CNHVALIDADE = modelDomain.FUN_CNHVALIDADE,
                    FUN_COMPLEMENTO = modelDomain.FUN_COMPLEMENTO,
                    FUN_CONJUGE = modelDomain.FUN_CONJUGE,
                    FUN_CONTACORRENTE = modelDomain.FUN_CONTACORRENTE,
                    FUN_CNS = modelDomain.FUN_CNS,
                    FUN_CPF = (modelDomain.FUN_CPF != null ? Regex.Replace(modelDomain.FUN_CPF, "[^0-9a-zA-Z]+", "") : modelDomain.FUN_CPF),
                    FUN_CTPS = modelDomain.FUN_CTPS,
                    FUN_CTPSDATAEMISSAO = modelDomain.FUN_CTPSDATAEMISSAO,
                    FUN_DATANASCIMENTO = modelDomain.FUN_DATANASCIMENTO,
                    FUN_EMAIL = modelDomain.FUN_EMAIL,
                    FUN_ENDERECO = modelDomain.FUN_ENDERECO,
                    FUN_ESCOLARIDADE = modelDomain.FUN_ESCOLARIDADE,
                    FUN_ESTADOCIVIL = modelDomain.FUN_ESTADOCIVIL,
                    FUN_IDIOMA = modelDomain.FUN_IDIOMA,
                    FUN_IMOVEL = modelDomain.FUN_IMOVEL,
                    FUN_LOGRADOURO = modelDomain.FUN_LOGRADOURO,
                    FUN_NACIONALIDADE = modelDomain.FUN_NACIONALIDADE,
                    FUN_NECESSIDADE_ESPECIAL = modelDomain.FUN_NECESSIDADE_ESPECIAL,
                    FUN_NOME = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modelDomain.FUN_NOME),
                    FUN_NOMECRACHA = GetNomeCracha(modelDomain.FUN_NOME),
                    FUN_NOMEMAE = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modelDomain.FUN_MAE),
                    FUN_NOMEPAI = modelDomain.FUN_PAI != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modelDomain.FUN_PAI) : null,
                    FUN_NUMEROEND = modelDomain.FUN_NUMEROEND,
                    FUN_OBSERVACAO = modelDomain.FUN_OBSERVACAO,
                    FUN_PIS = (modelDomain.FUN_PIS != null ? Regex.Replace(modelDomain.FUN_PIS, "[^0-9a-zA-Z]+", "") : modelDomain.FUN_PIS),
                    FUN_RG = modelDomain.FUN_RG,
                    FUN_RGDATAEMISSAO = modelDomain.FUN_RGDATAEMISSAO,
                    FUN_RGEMISSOR = modelDomain.FUN_RGEMISSOR,
                    FUN_RGESTADO = modelDomain.FUN_RGESTADO,
                    FUN_SEXO = modelDomain.FUN_SEXO,
                    FUN_TELEFONE = modelDomain.FUN_TELEFONE,
                    FUN_TIPOSANGUINEO = modelDomain.FUN_TIPOSANGUINEO,
                    FUN_TITULODATAEMISSAO = modelDomain.FUN_TITULODATAEMISSAO,
                    FUN_TITULOELEITOR = modelDomain.FUN_TITULOELEITOR,
                    FUN_TITULOSESSAO = modelDomain.FUN_TITULOSESSAO,
                    FUN_TITULOZONA = modelDomain.FUN_TITULOZONA,
                    FUN_REGDATE = DateTime.Now,
                    FUN_REGUSER = modelDomain.FUN_REGUSER,
                    FUN_STATUS = "A"
                };
                ap_funcionarioxfoto _funcionarioFoto = new ap_funcionarioxfoto
                {
                    FUNFT_ARQUIVO = modelDomain.FUN_FOTO,
                    FUNFT_REGDATE = DateTime.Now,
                    FUNFT_REGUSER = modelDomain.FUN_REGUSER,
                    FUNFT_STATUS = "A"

                };

                _funcionario.ap_funcionarioxfoto.Add(_funcionarioFoto);

                var result = _funcionarioRepository.Insert(_funcionario);
                _funcionarioRepository.Save();
                modelDomain.FUN_ID = result.FUN_ID;
            }
            else
            {
                ap_funcionario _funcionario = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_ID == modelDomain.FUN_ID);
                {
                    if (_funcionario != null)
                    {
                        _funcionario.FUN_ID = modelDomain.FUN_ID;
                        _funcionario.BNC_ID = modelDomain.BNC_ID;
                        _funcionario.FUN_AGENCIA = modelDomain.FUN_AGENCIA;
                        _funcionario.FUN_ASSISTENCIAMEDICA = modelDomain.FUN_ASSISTENCIAMEDICA;
                        _funcionario.FUN_BAIRRO = modelDomain.FUN_BAIRRO;
                        _funcionario.FUN_CELULAR = modelDomain.FUN_CELULAR;
                        _funcionario.FUN_CEP = (modelDomain.FUN_CEP != null ? Regex.Replace(modelDomain.FUN_CEP, "[^0-9a-zA-Z]+", "") : null);//modelDomain.FUN_CEP;
                        _funcionario.FUN_CNS = modelDomain.FUN_CNS;
                        _funcionario.FUN_CERTIFICADOMILITAR = modelDomain.FUN_CERTIFICADOMILITAR;
                        _funcionario.FUN_CIDADE = modelDomain.FUN_CIDADE;
                        _funcionario.FUN_CIDADENATAL = modelDomain.FUN_CIDADENATAL;
                        _funcionario.FUN_CMCORPORACAO = modelDomain.FUN_CMCORPORACAO;
                        _funcionario.FUN_CMSERIE = modelDomain.FUN_CMSERIE;
                        _funcionario.FUN_CNH = modelDomain.FUN_CNH;
                        _funcionario.FUN_CNHCATEGORIA = modelDomain.FUN_CNHCATEGORIA;
                        _funcionario.FUN_CNHVALIDADE = modelDomain.FUN_CNHVALIDADE;
                        _funcionario.FUN_COMPLEMENTO = modelDomain.FUN_COMPLEMENTO;
                        _funcionario.FUN_CONJUGE = modelDomain.FUN_CONJUGE;
                        _funcionario.FUN_CONTACORRENTE = modelDomain.FUN_CONTACORRENTE;
                        _funcionario.FUN_CPF = (modelDomain.FUN_CPF != null ? Regex.Replace(modelDomain.FUN_CPF, "[^0-9a-zA-Z]+", "") : null);
                        _funcionario.FUN_CTPS = modelDomain.FUN_CTPS;
                        _funcionario.FUN_CTPSDATAEMISSAO = modelDomain.FUN_CTPSDATAEMISSAO;
                        _funcionario.FUN_DATANASCIMENTO = modelDomain.FUN_DATANASCIMENTO;
                        _funcionario.FUN_EMAIL = modelDomain.FUN_EMAIL;
                        _funcionario.FUN_ENDERECO = modelDomain.FUN_ENDERECO;
                        _funcionario.FUN_ESCOLARIDADE = modelDomain.FUN_ESCOLARIDADE;
                        _funcionario.FUN_ESTADOCIVIL = modelDomain.FUN_ESTADOCIVIL;
                        _funcionario.FUN_IDIOMA = modelDomain.FUN_IDIOMA;
                        _funcionario.FUN_IMOVEL = modelDomain.FUN_IMOVEL;
                        _funcionario.FUN_LOGRADOURO = modelDomain.FUN_LOGRADOURO;
                        _funcionario.FUN_MATRICULA = modelDomain.FUN_MATRICULA;
                        _funcionario.FUN_NACIONALIDADE = modelDomain.FUN_NACIONALIDADE;
                        _funcionario.FUN_NECESSIDADE_ESPECIAL = modelDomain.FUN_NECESSIDADE_ESPECIAL;
                        _funcionario.FUN_NOME = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modelDomain.FUN_NOME);
                        _funcionario.FUN_NOMECRACHA = _funcionario.FUN_NOMECRACHA == null ? GetNomeCracha(modelDomain.FUN_NOME) : _funcionario.FUN_NOMECRACHA;
                        _funcionario.FUN_PIS = (modelDomain.FUN_PIS != null ? Regex.Replace(modelDomain.FUN_PIS, "[^0-9a-zA-Z]+", "") : null);
                        _funcionario.FUN_NOMEMAE = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modelDomain.FUN_MAE);
                        _funcionario.FUN_NOMEPAI = modelDomain.FUN_PAI != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modelDomain.FUN_PAI) : null;
                        _funcionario.FUN_NUMEROEND = modelDomain.FUN_NUMEROEND;
                        _funcionario.FUN_OBSERVACAO = modelDomain.FUN_OBSERVACAO;
                        _funcionario.FUN_RG = modelDomain.FUN_RG;
                        _funcionario.FUN_RGDATAEMISSAO = modelDomain.FUN_RGDATAEMISSAO;
                        _funcionario.FUN_RGEMISSOR = modelDomain.FUN_RGEMISSOR;
                        _funcionario.FUN_RGESTADO = modelDomain.FUN_RGESTADO;
                        _funcionario.FUN_SEXO = modelDomain.FUN_SEXO;
                        _funcionario.FUN_TELEFONE = modelDomain.FUN_TELEFONE;
                        _funcionario.FUN_TIPOSANGUINEO = modelDomain.FUN_TIPOSANGUINEO;
                        _funcionario.FUN_TITULODATAEMISSAO = modelDomain.FUN_TITULODATAEMISSAO;
                        _funcionario.FUN_TITULOELEITOR = modelDomain.FUN_TITULOELEITOR;
                        _funcionario.FUN_TITULOSESSAO = modelDomain.FUN_TITULOSESSAO;
                        _funcionario.FUN_TITULOSESSAO = modelDomain.FUN_TITULOSESSAO;
                        _funcionario.FUN_TITULOZONA = modelDomain.FUN_TITULOZONA;
                        _funcionario.FUN_REGUSER = modelDomain.FUN_REGUSER;
                        _funcionario.FUN_REGDATE = DateTime.Now;
                        _funcionario.FUN_STATUS = "A";

                    }

                    if (modelDomain.FUN_FOTO != null)
                    {
                        ap_funcionarioxfoto foto;

                        if (_funcionario.ap_funcionarioxfoto.Count > 0)
                        {
                            foto = _funcionario.ap_funcionarioxfoto.Single();
                        }
                        else
                        {
                            foto = new ap_funcionarioxfoto();

                        }
                        foto.FUNFT_ARQUIVO = modelDomain.FUN_FOTO;
                        foto.FUNFT_REGDATE = DateTime.Now;
                        foto.FUNFT_REGUSER = modelDomain.FUN_REGUSER;
                        foto.FUNFT_STATUS = "A";

                        if (_funcionario.ap_funcionarioxfoto.Count == 0)
                            _funcionario.ap_funcionarioxfoto.Add(foto);


                        _funcionarioRepository.Update(_funcionario);
                        _funcionarioRepository.Save();

                    }
                };
            }

            return modelDomain;
        }

        public FuncionarioEndereco buscarCep(string cep)
        {
            FuncionarioEndereco funcionarioEndereco = new FuncionarioEndereco();

            using (var ws = new AtendeClienteClient())
            {
                var resp = ws.consultaCEP(cep);

                funcionarioEndereco.FUN_BAIRRO = resp.bairro;
                funcionarioEndereco.FUN_CEP = resp.cep;
                funcionarioEndereco.FUN_ENDERECO = resp.end;
                funcionarioEndereco.FUN_MUNICIPIO = resp.cidade;

            }

            return funcionarioEndereco;


        }

        public List<FuncionarioDomainModel> GetAllFuncionario()
        {
            return _funcionarioRepository.GetAll(x => x.FUN_STATUS == "A").ToList().Select(fun => new FuncionarioDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_MATRICULA = fun.FUN_MATRICULA,
                BNC_ID = fun.BNC_ID,
                FUN_AGENCIA = fun.FUN_AGENCIA,
                FUN_ASSISTENCIAMEDICA = fun.FUN_ASSISTENCIAMEDICA,
                FUN_BAIRRO = fun.FUN_BAIRRO,
                FUN_CELULAR = fun.FUN_CELULAR,
                FUN_CEP = fun.FUN_CEP,
                FUN_CNS = fun.FUN_CNS,
                FUN_CERTIFICADOMILITAR = fun.FUN_CERTIFICADOMILITAR,
                FUN_CIDADE = fun.FUN_CIDADE,
                FUN_CIDADENATAL = fun.FUN_CIDADENATAL,
                FUN_CMCORPORACAO = fun.FUN_CMCORPORACAO,
                FUN_CMSERIE = fun.FUN_CMSERIE,
                FUN_CNH = fun.FUN_CNH,
                FUN_CNHCATEGORIA = fun.FUN_CNHCATEGORIA,
                FUN_CNHVALIDADE = fun.FUN_CNHVALIDADE,
                FUN_COMPLEMENTO = fun.FUN_COMPLEMENTO,
                FUN_CONJUGE = fun.FUN_CONJUGE,
                FUN_CONTACORRENTE = fun.FUN_CONTACORRENTE,
                FUN_CPF = fun.FUN_CPF,
                FUN_CTPS = fun.FUN_CTPS,
                FUN_CTPSDATAEMISSAO = fun.FUN_CTPSDATAEMISSAO,
                FUN_DATANASCIMENTO = fun.FUN_DATANASCIMENTO,
                FUN_EMAIL = fun.FUN_EMAIL,
                FUN_ENDERECO = fun.FUN_ENDERECO,
                FUN_ESCOLARIDADE = fun.FUN_ESCOLARIDADE,
                FUN_ESTADOCIVIL = fun.FUN_ESTADOCIVIL,
                FUN_IDIOMA = fun.FUN_IDIOMA,
                FUN_IMOVEL = fun.FUN_IMOVEL,
                FUN_LOGRADOURO = fun.FUN_LOGRADOURO,
                FUN_NACIONALIDADE = fun.FUN_NACIONALIDADE,
                FUN_NECESSIDADE_ESPECIAL = fun.FUN_NECESSIDADE_ESPECIAL,
                FUN_NOME = fun.FUN_NOME,
                FUN_NOMECRACHA = GetNomeCracha(fun.FUN_NOME),
                FUN_MAE = fun.FUN_NOMEMAE,
                FUN_PAI = fun.FUN_NOMEPAI,
                FUN_NUMEROEND = fun.FUN_NUMEROEND,
                FUN_OBSERVACAO = fun.FUN_OBSERVACAO,
                FUN_PIS = fun.FUN_PIS,
                FUN_RG = fun.FUN_RG,
                FUN_RGDATAEMISSAO = fun.FUN_RGDATAEMISSAO,
                FUN_RGEMISSOR = fun.FUN_RGEMISSOR,
                FUN_RGESTADO = fun.FUN_RGESTADO,
                FUN_SEXO = fun.FUN_SEXO,
                FUN_TELEFONE = fun.FUN_TELEFONE,
                FUN_TIPOSANGUINEO = fun.FUN_TIPOSANGUINEO,
                FUN_TITULODATAEMISSAO = fun.FUN_TITULODATAEMISSAO,
                FUN_TITULOELEITOR = fun.FUN_TITULOELEITOR,
                FUN_TITULOSESSAO = fun.FUN_TITULOSESSAO,
                FUN_TITULOZONA = fun.FUN_TITULOZONA,
                FUN_FOTO = fun.ap_funcionarioxfoto.Any(x => x.FUNFT_STATUS == "A") ? fun.ap_funcionarioxfoto.FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                Vinculos = fun.ap_vinculo.Any(x => x.VNC_STATUS == "A") ? fun.ap_vinculo.Select(x => new VinculoDomainModel()
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
                    }).ToList()
                    : null
                }).ToList() : null
            }).ToList();
        }

        public List<FuncionarioDomainModel> GetAllFuncionarioPage(int pageStart, int pageSize)
        {
            return _funcionarioRepository.GetPagedRecords(x => x.FUN_STATUS == "A", x => x.FUN_NOME, pageStart, pageSize).Select(fun => new FuncionarioDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_MATRICULA = fun.FUN_MATRICULA,
                FUN_CNS = fun.FUN_CNS,
                BNC_ID = fun.BNC_ID,
                FUN_AGENCIA = fun.FUN_AGENCIA,
                FUN_ASSISTENCIAMEDICA = fun.FUN_ASSISTENCIAMEDICA,
                FUN_BAIRRO = fun.FUN_BAIRRO,
                FUN_CELULAR = fun.FUN_CELULAR,
                FUN_CEP = fun.FUN_CEP,
                FUN_CERTIFICADOMILITAR = fun.FUN_CERTIFICADOMILITAR,
                FUN_CIDADE = fun.FUN_CIDADE,
                FUN_CIDADENATAL = fun.FUN_CIDADENATAL,
                FUN_CMCORPORACAO = fun.FUN_CMCORPORACAO,
                FUN_CMSERIE = fun.FUN_CMSERIE,
                FUN_CNH = fun.FUN_CNH,
                FUN_CNHCATEGORIA = fun.FUN_CNHCATEGORIA.ToString(),
                FUN_CNHVALIDADE = fun.FUN_CNHVALIDADE,
                FUN_COMPLEMENTO = fun.FUN_COMPLEMENTO,
                FUN_CONJUGE = fun.FUN_CONJUGE,
                FUN_CONTACORRENTE = fun.FUN_CONTACORRENTE,
                FUN_CPF = fun.FUN_CPF,
                FUN_CTPS = fun.FUN_CTPS,
                FUN_CTPSDATAEMISSAO = fun.FUN_CTPSDATAEMISSAO,
                FUN_DATANASCIMENTO = fun.FUN_DATANASCIMENTO,
                FUN_EMAIL = fun.FUN_EMAIL,
                FUN_ENDERECO = fun.FUN_ENDERECO,
                FUN_ESCOLARIDADE = fun.FUN_ESCOLARIDADE,
                FUN_ESTADOCIVIL = fun.FUN_ESTADOCIVIL,
                FUN_IDIOMA = fun.FUN_IDIOMA,
                FUN_IMOVEL = fun.FUN_IMOVEL,
                FUN_LOGRADOURO = fun.FUN_LOGRADOURO,
                FUN_NACIONALIDADE = fun.FUN_NACIONALIDADE,
                FUN_NECESSIDADE_ESPECIAL = fun.FUN_NECESSIDADE_ESPECIAL,
                FUN_NOME = fun.FUN_NOME,
                FUN_NOMECRACHA = GetNomeCracha(fun.FUN_NOME),
                FUN_MAE = fun.FUN_NOMEMAE,
                FUN_PAI = fun.FUN_NOMEPAI,
                FUN_NUMEROEND = fun.FUN_NUMEROEND,
                FUN_OBSERVACAO = fun.FUN_OBSERVACAO,
                FUN_PIS = fun.FUN_PIS,
                FUN_RG = fun.FUN_RG,
                FUN_RGDATAEMISSAO = fun.FUN_RGDATAEMISSAO,
                FUN_RGEMISSOR = fun.FUN_RGEMISSOR,
                FUN_RGESTADO = fun.FUN_RGESTADO,
                FUN_SEXO = fun.FUN_SEXO,
                FUN_TELEFONE = fun.FUN_TELEFONE,
                FUN_TIPOSANGUINEO = fun.FUN_TIPOSANGUINEO,
                FUN_TITULODATAEMISSAO = fun.FUN_TITULODATAEMISSAO,
                FUN_TITULOELEITOR = fun.FUN_TITULOELEITOR,
                FUN_TITULOSESSAO = fun.FUN_TITULOSESSAO,
                FUN_TITULOZONA = fun.FUN_TITULOZONA,
                FUN_FOTO = fun.ap_funcionarioxfoto.Where(x => x.FUNFT_STATUS == "A").Count() > 0 ? fun.ap_funcionarioxfoto.FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : null
            }).ToList();
        }

        public void GetCracha(int Id)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioEmitirCrachaDomainModel> GetDadosCracha(int id)
        {
            var listDomain = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.VNC_ID == id).ToList();
            return listDomain.Select(x => new FuncionarioEmitirCrachaDomainModel
            {
                CRG_NOME = x.ap_cargo.CRG_NOME,
                FUNFT_ARQUIVO = x.ap_funcionario.ap_funcionarioxfoto.FirstOrDefault(z => z.FUNFT_STATUS == "A").FUNFT_ARQUIVO,
                FUN_CODIGOBARRA = GerarBarCode(x.ap_funcionario.FUN_MATRICULA),
                FUN_MATRICULA = x.ap_funcionario.FUN_MATRICULA,
                FUN_NOME = x.ap_funcionario.FUN_NOMECRACHA,
                FUN_TIPAGEM = x.ap_funcionario.FUN_TIPOSANGUINEO.Value,
                FUN_TIPOSANGUINEO = GetTipagem(x.ap_funcionario.FUN_TIPOSANGUINEO.Value)
            }).ToList();
        }

        private byte[] GerarBarCode(string pMATRICULA)
        {
            var bc = new BarcodeLib.Barcode();
            System.Drawing.Image BcImage = bc.Encode(BarcodeLib.TYPE.CODE128, pMATRICULA);
            using (var ms = new System.IO.MemoryStream())
            {
                BcImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private string GetTipagem(int? FUN_TIPO)
        {
            int? esc = FUN_TIPO;
            if (esc == 1)
                return "A-";
            else if (esc == 2)
                return "A+";
            else if (esc == 3)
                return "B-";
            else if (esc == 4)
                return "B+";
            else if (esc == 5)
                return "AB-";
            else if (esc == 6)
                return "AB+";
            else if (esc == 7)
                return "O-";
            else if (esc == 8)
                return "O+";
            else
                return "";
        }

        public FuncionarioDomainModel GetFuncionarioById(int Id)
        {
            ap_funcionario fun = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_ID == Id);
            byte[] imagemByteDados = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));

            FuncionarioDomainModel _domainModel = new FuncionarioDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_MATRICULA = fun.FUN_MATRICULA.ToString(),
                BNC_ID = fun.BNC_ID,
                FUN_AGENCIA = fun.FUN_AGENCIA,
                FUN_CNS = fun.FUN_CNS,
                FUN_ASSISTENCIAMEDICA = fun.FUN_ASSISTENCIAMEDICA,
                FUN_BAIRRO = fun.FUN_BAIRRO,
                FUN_CELULAR = fun.FUN_CELULAR,
                FUN_CEP = fun.FUN_CEP,
                FUN_CERTIFICADOMILITAR = fun.FUN_CERTIFICADOMILITAR,
                FUN_CIDADE = fun.FUN_CIDADE,
                FUN_CIDADENATAL = fun.FUN_CIDADENATAL,
                FUN_CMCORPORACAO = fun.FUN_CMCORPORACAO,
                FUN_CMSERIE = fun.FUN_CMSERIE,
                FUN_CNH = fun.FUN_CNH,
                FUN_CNHCATEGORIA = fun.FUN_CNHCATEGORIA,
                FUN_CNHVALIDADE = fun.FUN_CNHVALIDADE,
                FUN_COMPLEMENTO = fun.FUN_COMPLEMENTO,
                FUN_CONJUGE = fun.FUN_CONJUGE,
                FUN_CONTACORRENTE = fun.FUN_CONTACORRENTE,
                FUN_CPF = fun.FUN_CPF,
                FUN_CTPS = fun.FUN_CTPS,
                FUN_CTPSDATAEMISSAO = fun.FUN_CTPSDATAEMISSAO,
                FUN_DATANASCIMENTO = fun.FUN_DATANASCIMENTO,
                FUN_EMAIL = fun.FUN_EMAIL,
                FUN_ENDERECO = fun.FUN_ENDERECO,
                FUN_ESCOLARIDADE = fun.FUN_ESCOLARIDADE,
                FUN_ESTADOCIVIL = fun.FUN_ESTADOCIVIL,
                FUN_IDIOMA = fun.FUN_IDIOMA,
                FUN_IMOVEL = fun.FUN_IMOVEL,
                FUN_LOGRADOURO = fun.FUN_LOGRADOURO,
                FUN_NACIONALIDADE = fun.FUN_NACIONALIDADE,
                FUN_NECESSIDADE_ESPECIAL = fun.FUN_NECESSIDADE_ESPECIAL,
                FUN_NOME = fun.FUN_NOME,
                FUN_NOMECRACHA = fun.FUN_NOMECRACHA,
                FUN_MAE = fun.FUN_NOMEMAE,
                FUN_PAI = fun.FUN_NOMEPAI,
                FUN_NUMEROEND = fun.FUN_NUMEROEND,
                FUN_OBSERVACAO = fun.FUN_OBSERVACAO,
                FUN_PIS = fun.FUN_PIS,
                FUN_RG = fun.FUN_RG,
                FUN_RGDATAEMISSAO = fun.FUN_RGDATAEMISSAO,
                FUN_RGEMISSOR = fun.FUN_RGEMISSOR,
                FUN_RGESTADO = fun.FUN_RGESTADO,
                FUN_SEXO = fun.FUN_SEXO,
                FUN_TELEFONE = fun.FUN_TELEFONE,
                FUN_TIPOSANGUINEO = fun.FUN_TIPOSANGUINEO,
                FUN_TITULODATAEMISSAO = fun.FUN_TITULODATAEMISSAO,
                FUN_TITULOELEITOR = fun.FUN_TITULOELEITOR,
                FUN_TITULOSESSAO = fun.FUN_TITULOSESSAO,
                FUN_TITULOZONA = fun.FUN_TITULOZONA,
                FUN_FOTO = fun.ap_funcionarioxfoto.Where(x => x.FUNFT_STATUS == "A").Count() > 0 ? fun.ap_funcionarioxfoto.FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados
            };

            return _domainModel;
        }

        public List<FuncionarioIndexDomainModel> GetFuncionarioByNameOrMatricula(FuncionarioPesquisaDomainModel domainModel)
        {
            return _funcionarioRepository.GetAll(x => x.FUN_STATUS == "A" && (x.FUN_MATRICULA.ToString().Contains(domainModel.MATRICULA) || x.FUN_NOME.Contains(domainModel.NOME))).Select(fun => new FuncionarioIndexDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_MATRICULA = fun.FUN_MATRICULA.ToString(),
                FUN_NOME = fun.FUN_NOME
            }).ToList();
        }

        public List<FuncionarioDomainModel> GetFuncionarioByPIS(string _pis)
        {
            var lista = _funcionarioRepository.GetAll(x => x.FUN_STATUS == "A" && x.FUN_PIS.Contains(_pis)).ToList().Select(fun => new FuncionarioDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_MATRICULA = fun.FUN_MATRICULA.ToString(),
                BNC_ID = fun.BNC_ID,
                FUN_AGENCIA = fun.FUN_AGENCIA,
                FUN_CNS = fun.FUN_CNS,
                FUN_ASSISTENCIAMEDICA = fun.FUN_ASSISTENCIAMEDICA,
                FUN_BAIRRO = fun.FUN_BAIRRO,
                FUN_CELULAR = fun.FUN_CELULAR,
                FUN_CEP = fun.FUN_CEP,
                FUN_CERTIFICADOMILITAR = fun.FUN_CERTIFICADOMILITAR,
                FUN_CIDADE = fun.FUN_CIDADE,
                FUN_CIDADENATAL = fun.FUN_CIDADENATAL,
                FUN_CMCORPORACAO = fun.FUN_CMCORPORACAO,
                FUN_CMSERIE = fun.FUN_CMSERIE,
                FUN_CNH = fun.FUN_CNH,
                FUN_CNHCATEGORIA = fun.FUN_CNHCATEGORIA,
                FUN_CNHVALIDADE = fun.FUN_CNHVALIDADE,
                FUN_COMPLEMENTO = fun.FUN_COMPLEMENTO,
                FUN_CONJUGE = fun.FUN_CONJUGE,
                FUN_CONTACORRENTE = fun.FUN_CONTACORRENTE,
                FUN_CPF = fun.FUN_CPF,
                FUN_CTPS = fun.FUN_CTPS,
                FUN_CTPSDATAEMISSAO = fun.FUN_CTPSDATAEMISSAO,
                FUN_DATANASCIMENTO = fun.FUN_DATANASCIMENTO,
                FUN_EMAIL = fun.FUN_EMAIL,
                FUN_ENDERECO = fun.FUN_ENDERECO,
                FUN_ESCOLARIDADE = fun.FUN_ESCOLARIDADE,
                FUN_ESTADOCIVIL = fun.FUN_ESTADOCIVIL,
                FUN_IDIOMA = fun.FUN_IDIOMA,
                FUN_IMOVEL = fun.FUN_IMOVEL,
                FUN_LOGRADOURO = fun.FUN_LOGRADOURO,
                FUN_NACIONALIDADE = fun.FUN_NACIONALIDADE,
                FUN_NECESSIDADE_ESPECIAL = fun.FUN_NECESSIDADE_ESPECIAL,
                FUN_NOME = fun.FUN_NOME,
                FUN_NOMECRACHA = GetNomeCracha(fun.FUN_NOME),
                FUN_MAE = fun.FUN_NOMEMAE,
                FUN_PAI = fun.FUN_NOMEPAI,
                FUN_NUMEROEND = fun.FUN_NUMEROEND,
                FUN_OBSERVACAO = fun.FUN_OBSERVACAO,
                FUN_PIS = fun.FUN_PIS,
                FUN_RG = fun.FUN_RG,
                FUN_RGDATAEMISSAO = fun.FUN_RGDATAEMISSAO,
                FUN_RGEMISSOR = fun.FUN_RGEMISSOR,
                FUN_RGESTADO = fun.FUN_RGESTADO,
                FUN_SEXO = fun.FUN_SEXO,
                FUN_TELEFONE = fun.FUN_TELEFONE,
                FUN_TIPOSANGUINEO = fun.FUN_TIPOSANGUINEO,
                FUN_TITULODATAEMISSAO = fun.FUN_TITULODATAEMISSAO,
                FUN_TITULOELEITOR = fun.FUN_TITULOELEITOR,
                FUN_TITULOSESSAO = fun.FUN_TITULOSESSAO,
                FUN_TITULOZONA = fun.FUN_TITULOZONA,
                FUN_FOTO = (fun.ap_funcionarioxfoto.Where(x => x.FUNFT_STATUS == "A").Count() > 0 ? fun.ap_funcionarioxfoto.FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : null)
            }).ToList();
            return lista;
        }

        public List<FuncionarioDomainModel> GetFuncionarioPage(int pageStart, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioIndexDomainModel> GetFuncionarioPageByParameters(string nome, string matricula, int? unidade, int? tipoVinculo, int? situacaoVinculo, int? cargo, int? beneficio, int? funcao, int pageStart, int pageSize)
        {


            var _list = _funcionarioRepository.GetAll(x => x.FUN_STATUS == "A").ToList()
                .GroupJoin(_vinculoRepository.GetAll(x => x.VNC_STATUS == "A").ToList(), fun => fun.FUN_ID, vnc => vnc.FUN_ID, (fun, vnc) => new { fun.FUN_MATRICULA, fun.FUN_ID, fun.FUN_NOME, vinculo = vnc })
                .SelectMany(x => x.vinculo.DefaultIfEmpty(), (x, y) => new { x.FUN_ID, x.FUN_MATRICULA, x.FUN_NOME, x.vinculo });

            if (matricula != "")
            {
                _list = _list.Where(x => x.FUN_MATRICULA.ToString().Contains(matricula));
            }
            if (nome != "")
            {
                _list = _list.Where(x => x.FUN_NOME.ToUpper().Contains(nome.ToUpper()));
            }


            if (cargo != null)
            {
                _list = _list.Where(z => z.vinculo.Any(x => x.CRG_ID == cargo && x.CRG_ID.Value == cargo.Value));

            }

            if (tipoVinculo != null)
            {
                _list = _list.Where(x => x.vinculo.Any(z => z.VNCTP_ID == tipoVinculo));

            }
            if (situacaoVinculo != null)
            {
                _list = _list.Where(x => x.vinculo.Any(z => z.VNCST_ID == situacaoVinculo));

            }
            if (unidade != null)
            {
                if (situacaoVinculo == (int)VinculoSituacaoDomainModel.Situacao.ativo)
                    _list = _list.Where(x => x.vinculo.Any(z => z.ap_vinculoxunidade.Any(p => p.UND_ID == unidade && p.VNCU_DATAFIM == null && p.VNCU_STATUS == "A")));
                else
                    _list = _list.Where(x => x.vinculo.Any(z => z.ap_vinculoxunidade.Any(p => p.UND_ID == unidade && p.VNCU_STATUS == "A")));

            }

            if (beneficio != null)
            {
                _list = _list.Where(x => x.vinculo.Any(z => z.ap_beneficioxvinculo.Any(p => p.BNF_ID == beneficio)));
            }



            return _list.Distinct().Select(fun => new FuncionarioIndexDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_MATRICULA = fun.FUN_MATRICULA,
                FUN_NOME = fun.FUN_NOME,
                //FUN_FOTO = fun.ap_funcionarioxfoto.Any(x => x.FUNFT_STATUS == "A") ? fun.ap_funcionarioxfoto.FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO.ToArray() : imagemByteDados.ToArray()

            }).ToList();
        }
        //Traz a Idade a partir de uma data
        public int Idade(DateTime idade)
        {
            int ano = DateTime.Now.Year;
            return (ano - idade.Year);
        }

        //Modificado por Angelo Matos em 24092019
        public List<FuncionarioRelatorioDomainModel> GetFuncionarioPageByParametersRelatorio(string nome, string matricula, int?[] unidade, int?[] tipoVinculo, int?[] situacaoVinculo, int?[] cargo, int?[] beneficio, int pageStart, int pageSize, string sexo, int? idade, int?[] funcao, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? inicioDemissao, DateTime? fimDemissao)
        {
            List<ap_vinculo> _list = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A").ToList();

            if (matricula != "")
            {
                _list = _list.Where(x => x.ap_funcionario.FUN_MATRICULA.ToString().Contains(matricula)).ToList();
            }
            if (nome != "")
            {
                _list = _list.Where(x => x.ap_funcionario.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList();
            }
            if (sexo != "")
            {
                _list = _list.Where(x => x.ap_funcionario.FUN_SEXO.ToUpper().Contains(sexo.ToUpper())).ToList();
            }
            if (cargo != null)
            {
                if (cargo[0] != null)
                {
                    _list = _list.Where(x => cargo.Contains(x.CRG_ID)).ToList();
                }
            }
            if (idade != null)
            {
                switch (idade)
                {
                    case 1:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 25).ToList();
                        break;
                    case 2:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 25 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 40).ToList();
                        break;
                    case 3:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 40 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 55).ToList();
                        break;
                    case 4:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 55 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 70).ToList();
                        break;
                    case 5:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 70).ToList();
                        break;
                }
            }

            if (tipoVinculo != null)
            {
                if (tipoVinculo[0] != null)
                {
                    _list = _list.Where(x => tipoVinculo.Contains(x.VNCTP_ID)).ToList();
                }
            }
            if (situacaoVinculo != null)
            {
                if (situacaoVinculo[0] != null)
                {
                    _list = _list.Where(x => situacaoVinculo.Contains(x.VNCST_ID)).ToList();

                }
            }

            if (unidade != null)
            {
                if (unidade[0] != null)
                {
                    //76 - Representa op Hemocentro coordenador
                    if (unidade.Contains(76))
                    {
                        _list = _list.Where(x => x.ap_vinculoxunidade.Any(y => y.ap_unidade.UND_NIVEL != 6 && y.ap_unidade.UND_NIVEL != 7)).ToList();
                    }
                    else
                    {
                        _list = _list.Where(x => x.ap_vinculoxunidade.Any(y => unidade.Contains(y.UND_ID))).ToList();
                    }
                }
            }

            if (funcao != null)
            {
                if (funcao[0] != null)
                {
                    _list = _list.Where(x => x.ap_funcaoxvinculo.Any(y => funcao.Contains(y.FNC_ID))).ToList();
                }
            }

            if (inicioAdmissao != null)
            {
                if (fimAdmissao == null)
                {
                    _list = _list.Where(z => z.VNC_ADMISSAO >= inicioAdmissao).ToList();
                }
                else
                {
                    _list = _list.Where(z => z.VNC_ADMISSAO >= inicioAdmissao && z.VNC_ADMISSAO <= fimAdmissao).ToList();
                }
            }
            else
            {
                if (fimAdmissao != null)
                {
                    _list = _list.Where(z => z.VNC_ADMISSAO <= fimAdmissao).ToList();
                }
            }

            if (inicioDemissao != null)
            {
                if (fimDemissao == null)
                {
                    _list = _list.Where(z => z.VNC_DEMISSAO >= inicioDemissao).ToList();
                }
                else
                {
                    _list = _list.Where(z => z.VNC_DEMISSAO >= inicioDemissao && z.VNC_DEMISSAO <= fimDemissao).ToList();
                }
            }
            else
            {
                if (fimDemissao != null)
                {
                    _list = _list.Where(z => z.VNC_DEMISSAO <= fimDemissao).ToList();
                }
            }

            if (beneficio != null)
            {
                if (beneficio[0] != null)
                {
                    _list = _list.Where(x => x.ap_beneficioxvinculo.Any(p => beneficio.Contains(p.BNF_ID))).ToList();
                }
            }

            if (beneficio != null)
            {
                if (beneficio[0] != null)
                {
                    _list = _list.Where(x => x.ap_beneficioxvinculo.Any(p => beneficio.Contains(p.BNF_ID))).ToList();
                }
            }



            IOrderedEnumerable<FuncionarioRelatorioDomainModel> lista = _list.Select(fun => new FuncionarioRelatorioDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_NOME = fun.ap_funcionario.FUN_NOME,
                FUN_MATRICULA = fun.ap_funcionario.FUN_MATRICULA,
                CARGO = fun.ap_cargo.CRG_NOME,
                FUN_UNIDADE = fun.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).Count() > 0 ?
                    fun.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).LastOrDefault().ap_unidade.UND_SIGLA : "---",
                FUN_DATA_ADMISSAO = fun.VNC_ADMISSAO,
                FUN_DATA_DEMISSAO = fun.VNC_DEMISSAO,
                SITUACAO = fun.VNCST_ID.Value
            }).OrderBy(x => x.FUN_NOME);

            return lista.ToList();
        }

        //Adicionado por Angelo Matos em 25092019
        public List<FuncionarioRelatorioFuncaoDomainModel> GetFuncionarioPageByParametersRelatorioFuncao(string nome, string matricula, int?[] unidade, int?[] situacaoVinculo, int?[] cargo, int pageStart, int pageSize, string sexo, int? idade, int?[] funcao, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? inicioDemissao, DateTime? fimDemissao, DateTime? inicioFuncao, DateTime? fimFuncao)
        {
            var _listf = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A").ToList().Select(x => x.ap_funcaoxvinculo);
            List<ap_funcaoxvinculo> _list = new List<ap_funcaoxvinculo>();

            foreach (var item in _listf)
            {
                foreach (var item2 in item)
                {
                    if (item2.FNCVNC_STATUS == "A")
                    {
                        _list.Add(item2);
                    }

                }
            }

            if (matricula != "")
            {
                _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_MATRICULA.ToString().Contains(matricula)).ToList();
            }

            if (nome != "")
            {
                _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList();
            }

            if (sexo != "")
            {
                _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_SEXO.ToUpper().Contains(sexo.ToUpper())).ToList();
            }

            if (cargo != null)
            {
                if (cargo[0] != null)
                {
                    _list = _list.Where(x => cargo.Contains(x.ap_vinculo.CRG_ID)).ToList();
                }
            }
            if (idade != null)
            {
                switch (idade)
                {
                    case 1:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 25).ToList();
                        break;
                    case 2:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 25 && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 40).ToList();
                        break;
                    case 3:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 40 && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 55).ToList();
                        break;
                    case 4:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 55 && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 70).ToList();
                        break;
                    case 5:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 70).ToList();
                        break;
                }
            }

            if (situacaoVinculo != null)
            {
                if (situacaoVinculo[0] != null)
                {
                    _list = _list.Where(x => situacaoVinculo.Contains(x.ap_vinculo.VNCST_ID)).ToList();
                }
            }

            if (unidade != null)
            {
                if (unidade[0] != null)
                {
                    _list = _list.Where(x => x.ap_vinculo.ap_vinculoxunidade.Any(y => unidade.Contains(y.UND_ID))).ToList();
                }
            }

            if (inicioAdmissao != null)
            {
                if (fimAdmissao != null)
                {
                    _list = _list.Where(x => x.ap_vinculo.VNC_ADMISSAO >= inicioAdmissao && x.ap_vinculo.VNC_ADMISSAO <= fimAdmissao).ToList();
                }
                else
                {
                    _list = _list.Where(x => x.ap_vinculo.VNC_ADMISSAO >= inicioAdmissao).ToList();
                }
            }

            if (funcao != null)
            {
                if (funcao[0] != null)
                {
                    if (inicioFuncao != null || fimFuncao != null)
                    {
                        if (inicioFuncao != null)
                        {
                            if (fimFuncao == null)
                            {
                                _list = _list.Where(x => funcao.Contains(x.FNC_ID) && x.FNCVNC_DATAINICIO >= inicioFuncao).ToList();
                            }
                            else
                            {
                                _list = _list.Where(x => funcao.Contains(x.FNC_ID) && x.FNCVNC_DATAINICIO >= inicioFuncao && x.FNCVNC_DATAINICIO <= fimFuncao).ToList();
                            }
                        }
                        else
                        {
                            if (fimFuncao != null)
                            {
                                _list = _list.Where(x => funcao.Contains(x.FNC_ID) && x.FNCVNC_DATAINICIO <= fimFuncao).ToList();
                            }
                            else
                            {
                                _list = _list.Where(x => funcao.Contains(x.FNC_ID)).ToList();
                            }
                        }
                    }
                }
            }

            IOrderedEnumerable<FuncionarioRelatorioFuncaoDomainModel> lista = _list.Select(fun => new FuncionarioRelatorioFuncaoDomainModel
            {
                FUN_ID = fun.ap_vinculo.FUN_ID,
                FUN_NOME = fun.ap_vinculo.ap_funcionario.FUN_NOME,
                FUN_MATRICULA = fun.ap_vinculo.ap_funcionario.FUN_MATRICULA,
                CARGO = fun.ap_vinculo.ap_cargo.CRG_NOME,
                FUN_UNIDADE = fun.ap_vinculo.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).Count() > 0 ?
                    fun.ap_vinculo.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).LastOrDefault().ap_unidade.UND_SIGLA : "---",
                FUN_DATA_ADMISSAO = fun.ap_vinculo.VNC_ADMISSAO,
                FUN_DATA_DEMISSAO = fun.ap_vinculo.VNC_DEMISSAO,
                SITUACAO = fun.ap_vinculo.VNCST_ID.Value,
                FUNCAO = fun.ap_funcao.FNC_NOME,
                FUNCAO_DATA_INICIO = fun.FNCVNC_DATAINICIO,
                FUNCAO_DATA_FIM = fun.FNCVNC_DATAFIM
            }).OrderBy(x => x.FUN_NOME);

            return lista.ToList();
        }

        //Adicionado por Angelo Matos em 25092019
        public List<FuncionarioRelatorioAposentadoriaDomainModel> GetFuncionarioPageByParametersRelatorioAposentadoria(string nome, string matricula, int?[] unidade, int?[] situacaoVinculo, int?[] cargo, int pageStart, int pageSize, string sexo, int? idade, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? entradaAposentadoriaInicio, DateTime? entradaAposentadoriaFim, int?[] tipoAposentadoria)
        {
            List<ap_vinculo> _list = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A").ToList();

            if (matricula != "") { _list = _list.Where(x => x.ap_funcionario.FUN_MATRICULA.ToString().Contains(matricula)).ToList(); }

            if (nome != "") { _list = _list.Where(x => x.ap_funcionario.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList(); }

            if (sexo != "") { _list = _list.Where(x => x.ap_funcionario.FUN_SEXO.ToUpper().Contains(sexo.ToUpper())).ToList(); }

            if (cargo != null)
            {
                if (cargo[0] != null) { _list = _list.Where(x => cargo.Contains(x.CRG_ID)).ToList(); }
            }
            if (idade != null)
            {
                switch (idade)
                {
                    case 1:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 25).ToList();
                        break;
                    case 2:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 25 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 40).ToList();
                        break;
                    case 3:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 40 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 55).ToList();
                        break;
                    case 4:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 55 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 70).ToList();
                        break;
                    case 5:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 70).ToList();
                        break;
                }
            }

            if (tipoAposentadoria != null)
            {
                if (tipoAposentadoria[0] != null)
                {
                    _list = _list.Where(x => tipoAposentadoria.Contains(x.VNC_TIPO_APOSENT)).ToList();
                }
            }

            if (situacaoVinculo != null)
            {
                if (situacaoVinculo[0] != null)
                {
                    if (entradaAposentadoriaInicio.HasValue)
                    {
                        if (entradaAposentadoriaFim.HasValue)
                        {
                            _list = _list.Where(x => situacaoVinculo.Contains(x.VNCST_ID) && x.VNC_ENTRADA_APOSENT >= entradaAposentadoriaInicio && x.VNC_ENTRADA_APOSENT <= entradaAposentadoriaFim).ToList();
                        }
                        else
                        {
                            _list = _list.Where(x => situacaoVinculo.Contains(x.VNCST_ID) && x.VNC_ENTRADA_APOSENT >= entradaAposentadoriaInicio).ToList();
                        }
                    }
                    else
                    {
                        if (entradaAposentadoriaFim.HasValue)
                        {
                            _list = _list.Where(x => situacaoVinculo.Contains(x.VNCST_ID) && x.VNC_ENTRADA_APOSENT <= entradaAposentadoriaFim).ToList();
                        }
                        else
                        {
                            _list = _list.Where(x => situacaoVinculo.Contains(x.VNCST_ID)).ToList();
                        }
                    }

                }
            }

            if (unidade != null)
            {
                if (unidade[0] != null)
                { _list = _list.Where(x => x.ap_vinculoxunidade.Any(y => unidade.Contains(y.UND_ID))).ToList(); }
            }

            IOrderedEnumerable<FuncionarioRelatorioAposentadoriaDomainModel> lista = _list.Select(fun => new FuncionarioRelatorioAposentadoriaDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_NOME = fun.ap_funcionario.FUN_NOME,
                FUN_MATRICULA = fun.ap_funcionario.FUN_MATRICULA,
                CARGO = fun.ap_cargo.CRG_NOME,
                FUN_UNIDADE = fun.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).Count() > 0 ?
                    fun.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).LastOrDefault().ap_unidade.UND_SIGLA : "---",
                FUN_DATA_ADMISSAO = fun.VNC_ADMISSAO,
                FUN_DATA_DEMISSAO = fun.VNC_DEMISSAO,
                SITUACAO = fun.VNCST_ID.Value,
                SITUACAO_NOME = fun.ap_vinculosituacao.VNCST_DESCRICAO,
                ENTRADA_APOSENTADORIA = fun.VNC_ENTRADA_APOSENT,
                TIPO_APOSENTADORIA = fun.VNC_TIPO_APOSENT != null ? ((TipoAposentadoria)fun.VNC_TIPO_APOSENT).Descricao() : null
            }).OrderBy(x => x.FUN_NOME);

            return lista.ToList();
        }

        //Adicionado por Angelo Matos em 25092019
        public List<FuncionarioRelatorioBeneficioDomainModel> GetFuncionarioPageByParametersRelatorioBeneficio(string nome, string matricula, int?[] unidade, int?[] situacaoVinculo, int?[] cargo, int?[] beneficio, int pageStart, int pageSize, string sexo, int? idade, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? inicioDemissao, DateTime? fimDemissao, DateTime? beneficioInicio, DateTime? beneficioFim)
        {
            var listf = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A").ToList().Select(x => x.ap_beneficioxvinculo);
            List<ap_beneficioxvinculo> _list = new List<ap_beneficioxvinculo>();
            foreach (var item in listf)
            {
                foreach (var item2 in item)
                {
                    if (item2.BNFVNC_STATUS == "A")
                    {
                        _list.Add(item2);
                    }

                }
            }

            if (matricula != "")
            {
                _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_MATRICULA.ToString().Contains(matricula)).ToList();

            }

            if (nome != "")
            {
                _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList();
            }

            if (sexo != "")
            {
                _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_SEXO.ToUpper().Contains(sexo.ToUpper())).ToList();
            }

            if (cargo != null)
            {
                if (cargo[0] != null)
                {
                    _list = _list.Where(x => cargo.Contains(x.ap_vinculo.CRG_ID)).ToList();
                }
            }
            if (idade != null)
            {
                switch (idade)
                {
                    case 1:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 25).ToList();
                        break;
                    case 2:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 25 && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 40).ToList();
                        break;
                    case 3:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 40 && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 55).ToList();
                        break;
                    case 4:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 55 && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 70).ToList();
                        break;
                    case 5:
                        _list = _list.Where(x => x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_vinculo.ap_funcionario.FUN_DATANASCIMENTO.Value) > 70).ToList();
                        break;
                }
            }

            if (situacaoVinculo != null)
            {
                if (situacaoVinculo[0] != null)
                {
                    _list = _list.Where(x => situacaoVinculo.Contains(x.ap_vinculo.VNCST_ID)).ToList();
                }
            }

            if (unidade != null)
            {
                if (unidade[0] != null)
                {
                    _list = _list.Where(x => x.ap_vinculo.ap_vinculoxunidade.Any(y => unidade.Contains(y.UND_ID))).ToList();
                }
            }

            if (beneficio != null)
            {
                if (beneficio[0] != null)
                {
                    if (beneficioInicio.HasValue)
                    {
                        if (beneficioFim.HasValue)
                        {
                            _list = _list.Where(x => beneficio.Contains(x.BNF_ID) && x.BNFVNC_DATAINICIO >= beneficioInicio && x.BNFVNC_DATAINICIO <= beneficioFim).ToList();
                        }
                        else
                        {
                            _list = _list.Where(x => beneficio.Contains(x.BNF_ID) && x.BNFVNC_DATAINICIO >= beneficioInicio).ToList();
                        }
                    }
                    else
                    {
                        if (beneficioFim.HasValue)
                        {
                            _list = _list.Where(x => beneficio.Contains(x.BNF_ID) && x.BNFVNC_DATAINICIO >= beneficioFim).ToList();
                        }
                        else
                        {
                            _list = _list.Where(x => beneficio.Contains(x.BNF_ID)).ToList();
                        }
                    }

                }
            }

            IOrderedEnumerable<FuncionarioRelatorioBeneficioDomainModel> lista = _list.Select(fun => new FuncionarioRelatorioBeneficioDomainModel
            {
                FUN_ID = fun.ap_vinculo.FUN_ID,
                FUN_NOME = fun.ap_vinculo.ap_funcionario.FUN_NOME,
                FUN_MATRICULA = fun.ap_vinculo.ap_funcionario.FUN_MATRICULA,
                CARGO = fun.ap_vinculo.ap_cargo.CRG_NOME,
                FUN_UNIDADE = fun.ap_vinculo.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).Count() > 0 ?
                  fun.ap_vinculo.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).LastOrDefault().ap_unidade.UND_SIGLA : "---",
                FUN_DATA_ADMISSAO = fun.ap_vinculo.VNC_ADMISSAO,
                FUN_DATA_DEMISSAO = fun.ap_vinculo.VNC_DEMISSAO,
                SITUACAO = fun.ap_vinculo.VNCST_ID.Value,
                BENEFICIO_NOME = fun.ap_beneficio.BNF_DESCRICAO,
                BENEFICIO_DATA_INICIO = fun.BNFVNC_DATAINICIO,
                BENEFICIO_DATA_FIM = fun.BNFVNC_DATAFIM
            }).OrderBy(x => x.FUN_NOME);

            return lista.ToList();
        }

        //Adicionado por Angelo Matos em 25092019
        public List<FuncionarioRelatorioCessaoDomainModel> GetFuncionarioPageByParametersRelatorioCessao(string nome, string matricula, int?[] unidade, int?[] cargo, int?[] beneficio, int pageStart, int pageSize, string sexo, int? idade, string onus, DateTime? inicioAdmissao, DateTime? fimAdmissao, DateTime? cessaoInicio, DateTime? cessaoFim)
        {
            List<ap_vinculo> _list = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A" && x.VNCST_ID == 6).ToList();

            if (matricula != "")
            {
                _list = _list.Where(x => x.ap_funcionario.FUN_MATRICULA.ToString().Contains(matricula)).ToList();
            }
            if (nome != "")
            {
                _list = _list.Where(x => x.ap_funcionario.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList();
            }
            if (sexo != "")
            {
                _list = _list.Where(x => x.ap_funcionario.FUN_SEXO.ToUpper().Contains(sexo.ToUpper())).ToList();
            }
            if (cargo != null)
            {
                if (cargo[0] != null)
                {
                    _list = _list.Where(x => cargo.Contains(x.CRG_ID)).ToList();
                }
            }
            if (idade != null)
            {
                switch (idade)
                {
                    case 1:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 25).ToList();
                        break;
                    case 2:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 25 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 40).ToList();
                        break;
                    case 3:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 40 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 55).ToList();
                        break;
                    case 4:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 55 && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) <= 70).ToList();
                        break;
                    case 5:
                        _list = _list.Where(x => x.ap_funcionario.FUN_DATANASCIMENTO != null && Idade(x.ap_funcionario.FUN_DATANASCIMENTO.Value) > 70).ToList();
                        break;
                }
            }

            if (unidade != null)
            {
                if (unidade[0] != null)
                {
                    _list = _list.Where(x => x.ap_vinculoxunidade.Any(y => unidade.Contains(y.UND_ID))).ToList();

                }
            }

            if (onus != "")
            {
                _list = _list.Where(x => x.VNC_ONUS.Contains(onus)).ToList();
            }

            if (cessaoInicio.HasValue)
            {
                if (cessaoFim.HasValue)
                {
                    _list = _list.Where(x => x.VNC_DATACESSAO >= cessaoInicio && x.VNC_DATACESSAO <= cessaoFim).ToList();
                }
                else
                {
                    _list = _list.Where(x => x.VNC_DATACESSAO >= cessaoInicio).ToList();
                }
            }
            else
            {
                if (cessaoFim.HasValue)
                {
                    _list = _list.Where(x => x.VNC_DATACESSAO <= cessaoFim).ToList();
                }
            }

            IOrderedEnumerable<FuncionarioRelatorioCessaoDomainModel> lista = _list.Select(fun => new FuncionarioRelatorioCessaoDomainModel
            {
                FUN_ID = fun.FUN_ID,
                FUN_NOME = fun.ap_funcionario.FUN_NOME,
                FUN_MATRICULA = fun.ap_funcionario.FUN_MATRICULA,
                CARGO = fun.ap_cargo.CRG_NOME,
                FUN_UNIDADE = fun.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).Count() > 0 ?
                    fun.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A").OrderBy(x => x.VNCU_DATAINICIO).LastOrDefault().ap_unidade.UND_SIGLA : "---",
                FUN_DATA_ADMISSAO = fun.VNC_ADMISSAO,
                FUN_DATA_DEMISSAO = fun.VNC_DEMISSAO,
                SITUACAO = fun.VNCST_ID.Value,
                DATA_CESSAO = fun.VNC_DATACESSAO,
                ORGAO_DESTINO = fun.VNC_ORGAO_DESTINO,
                ONUS = fun.VNC_ONUS == TipoOnus.D.ToString() ? TipoOnus.D.Descricao() : TipoOnus.O.Descricao(),
            }).OrderBy(x => x.FUN_NOME);

            return lista.ToList();
        }


        //public List<FuncionarioRelatorioDomainModel> GetFuncionarioPageByParametersRelatorioOLD(string nome, string matricula, int?[] unidade, int?[] tipoVinculo, int?[] situacaoVinculo, int?[] cargo, int?[] beneficio, int pageStart, int pageSize, string sexo, int? idade, int?[] funcao, DateTime? inicioAdmissao, DateTime? fimAdmissao)
        //{
        //    var _list = _funcionarioRepository.GetAll(x => x.FUN_STATUS == "A").ToList()
        //        .GroupJoin(_vinculoRepository.GetAll(x => x.VNC_STATUS == "A").ToList(), fun => fun.FUN_ID, vnc => vnc.FUN_ID, (fun, vnc) => new { fun.FUN_MATRICULA, fun.FUN_ID, fun.FUN_NOME, fun.FUN_SEXO, fun.FUN_DATANASCIMENTO, vinculo = vnc })
        //        .SelectMany(x => x.vinculo.DefaultIfEmpty(), (x, y) => new { x.FUN_ID, x.FUN_MATRICULA, x.FUN_NOME, x.FUN_SEXO, x.FUN_DATANASCIMENTO, x.vinculo }).ToList();

        //    if (matricula != "")
        //    {
        //        _list = _list.Where(x => x.FUN_MATRICULA.ToString().Contains(matricula)).ToList();
        //    }
        //    if (nome != "")
        //    {
        //        _list = _list.Where(x => x.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList();
        //    }
        //    if (sexo != "")
        //    {
        //        _list = _list.Where(x => x.FUN_SEXO.ToUpper().Contains(sexo.ToUpper())).ToList();
        //    }
        //    if (cargo != null)
        //    {
        //        if (cargo[0] != null)
        //        {
        //            _list = _list.Select(x => new { x.FUN_ID, x.FUN_MATRICULA, x.FUN_NOME, x.FUN_SEXO, x.FUN_DATANASCIMENTO, vinculo = x.vinculo.Where(z => cargo.Contains(z.CRG_ID)) }).Where(x => x.vinculo.Any()).ToList();
        //        }
        //    }
        //    if (idade != null)
        //    {
        //        switch (idade)
        //        {
        //            case 1:
        //                _list = _list.Where(x => x.FUN_DATANASCIMENTO != null && Idade(x.FUN_DATANASCIMENTO.Value) <= 25).ToList();
        //                break;
        //            case 2:
        //                _list = _list.Where(x => x.FUN_DATANASCIMENTO != null && Idade(x.FUN_DATANASCIMENTO.Value) > 25 && Idade(x.FUN_DATANASCIMENTO.Value) <= 40).ToList();
        //                break;
        //            case 3:
        //                _list = _list.Where(x => x.FUN_DATANASCIMENTO != null && Idade(x.FUN_DATANASCIMENTO.Value) > 40 && Idade(x.FUN_DATANASCIMENTO.Value) <= 55).ToList();
        //                break;
        //            case 4:
        //                _list = _list.Where(x => x.FUN_DATANASCIMENTO != null && Idade(x.FUN_DATANASCIMENTO.Value) > 55 && Idade(x.FUN_DATANASCIMENTO.Value) <= 70).ToList();
        //                break;
        //            case 5:
        //                _list = _list.Where(x => x.FUN_DATANASCIMENTO != null && Idade(x.FUN_DATANASCIMENTO.Value) > 70).ToList();
        //                break;
        //        }
        //    }

        //    if (tipoVinculo != null)
        //    {
        //        if (tipoVinculo[0] != null)
        //        {
        //            _list = _list.Distinct().Select(x => new { x.FUN_ID, x.FUN_MATRICULA, x.FUN_NOME, x.FUN_SEXO, x.FUN_DATANASCIMENTO, vinculo = x.vinculo.Where(z => tipoVinculo.Contains(z.VNCTP_ID)) }).Where(x => x.vinculo.Any()).ToList();
        //        }
        //    }
        //    if (situacaoVinculo != null)
        //    {
        //        if (situacaoVinculo[0] != null)
        //        {
        //            _list = _list.Distinct().Select(x => new { x.FUN_ID, x.FUN_MATRICULA, x.FUN_NOME, x.FUN_SEXO, x.FUN_DATANASCIMENTO, vinculo = x.vinculo.Any(z => z.VNC_DEMISSAO == null) ? x.vinculo.Where(z => situacaoVinculo.Contains(z.VNCST_ID) && z.VNC_DEMISSAO == null) : x.vinculo.Where(z => situacaoVinculo.Contains(z.VNCST_ID) && z.VNC_DEMISSAO != null) }).Where(x => x.vinculo.Any()).ToList();
        //        }

        //    }
        //    if (unidade != null)
        //    {
        //        if (unidade[0] != null)
        //        {
        //            _list = _list.Distinct().Select(x => new { x.FUN_ID, x.FUN_MATRICULA, x.FUN_NOME, x.FUN_SEXO, x.FUN_DATANASCIMENTO, vinculo = x.vinculo.Where(z => z.ap_vinculoxunidade.Any(p => unidade.Contains(p.UND_ID) && p.VNCU_STATUS == "A")) }).Where(x => x.vinculo.Any()).ToList();
        //        }

        //    }

        //    if (funcao != null)
        //    {
        //        if (funcao[0] != null)
        //        {
        //            _list = _list.Distinct().Where(x => x.vinculo.Any(z => z.ap_funcaoxvinculo.Any(p => funcao.Contains(p.FNC_ID) && p.FNCVNC_STATUS == "A"))).ToList();
        //        }
        //    }

        //    if (inicioAdmissao != null)
        //    {
        //        if (fimAdmissao == null)
        //        {
        //            //_list = _list.Distinct().Where(x => x.vinculo.Any(z => z.ap_vinculoxunidade.Any(p => p.VNCU_DATAINICIO >= inicioAdmissao && p.VNCU_STATUS == "A"))).ToList();
        //            _list = _list.Distinct().Where(x => x.vinculo.Any(z => z.VNC_ADMISSAO >= inicioAdmissao)).ToList();
        //        }
        //        else
        //        {
        //            //_list = _list.Distinct().Where(x => x.vinculo.Any(z => z.ap_vinculoxunidade.Any(p => p.VNCU_DATAINICIO >= inicioAdmissao && p.VNCU_DATAINICIO <= fimAdmissao && p.VNCU_STATUS == "A"))).ToList();
        //            _list = _list.Distinct().Where(x => x.vinculo.Any(z => z.VNC_ADMISSAO >= inicioAdmissao && z.VNC_ADMISSAO <= fimAdmissao)).ToList();
        //        }
        //    }
        //    else
        //    {
        //        if (fimAdmissao != null)
        //        {
        //            _list = _list.Distinct().Where(x => x.vinculo.Any(z => z.VNC_ADMISSAO <= fimAdmissao)).ToList();
        //        }
        //    }

        //    if (beneficio != null)
        //    {
        //        if (beneficio[0] != null)
        //        {
        //            _list = _list.Distinct().Where(x => x.vinculo.Any(z => z.ap_beneficioxvinculo.Any(p => beneficio.Contains(p.BNF_ID)))).ToList();
        //        }

        //    }

        //    var lista = _list.Distinct().Select(fun => new FuncionarioRelatorioDomainModel
        //    {
        //        FUN_ID = fun.FUN_ID,
        //        FUN_MATRICULA = fun.FUN_MATRICULA,
        //        FUN_NOME = fun.FUN_NOME,
        //        FUN_UNIDADE =
        //         fun.vinculo.Any(x => (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoAposentadoria) && x.VNC_DEMISSAO == null) ?

        //         (
        //             fun.vinculo.Where(x => x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoAposentadoria).Any(x => x.ap_vinculoxunidade.Any(z => z.VNCU_DATAFIM == null)) ?

        //                 fun.vinculo.Where(x => x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoAposentadoria).Select(y => y.ap_vinculoxunidade.Where(a => a.VNCU_DATAFIM == null)).ToList()
        //                 .Where(z => z.Any(w => w.VNCU_DATAFIM == null)).Select(c => c.FirstOrDefault().ap_unidade.UND_SIGLA).FirstOrDefault()
        //             :

        //             fun.vinculo.Where(x => x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoAposentadoria).Any(x => !x.ap_vinculoxunidade.Any(z => z.VNCU_DATAFIM == null)) ?
        //                 "SEM LOTAÇÃO"
        //             :
        //                 ""
        //         )
        //         :
        //         fun.vinculo.OrderByDescending(x => x.VNC_ID).FirstOrDefault().ap_vinculoxunidade.Count() > 0 ?
        //             fun.vinculo.OrderByDescending(x => x.VNC_ID).FirstOrDefault().ap_vinculoxunidade.OrderByDescending(x => x.VNCU_ID).FirstOrDefault().ap_unidade.UND_SIGLA
        //             //"AQUI"
        //             :
        //             "SEM VINCULO",

        //        SITUACAO = !fun.vinculo.Any(x => x.VNC_DEMISSAO == null) ?
        //             fun.vinculo.Where(x => x.VNC_DEMISSAO != null).FirstOrDefault().VNCST_ID.Value
        //         :
        //             fun.vinculo.Where(x => x.VNC_DEMISSAO == null).FirstOrDefault().VNCST_ID.Value,
        //        CARGO = inicioAdmissao == null && fimAdmissao == null ?
        //            !fun.vinculo.Any(x => x.VNC_DEMISSAO == null) ?
        //                fun.vinculo.Where(x => x.VNC_DEMISSAO != null).FirstOrDefault().ap_cargo.CRG_NOME
        //            :
        //                fun.vinculo.Where(x => x.VNC_DEMISSAO == null).FirstOrDefault().ap_cargo.CRG_NOME
        //         :
        //            fun.vinculo.Where(x => x.VNC_ADMISSAO >= inicioAdmissao && x.VNC_ADMISSAO <= fimAdmissao).LastOrDefault().ap_cargo.CRG_NOME
        //         ,

        //        FUN_DATA_ADMISSAO = inicioAdmissao == null && fimAdmissao == null ?
        //                fun.vinculo.Any(x => x.VNC_DEMISSAO == null && (x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.desligado || x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.aposentado || x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.falecimento)) ?
        //                     fun.vinculo.Where(x => x.VNC_DEMISSAO == null && (x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.desligado || x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.aposentado || x.VNCST_ID != (int)VinculoSituacaoDomainModel.Situacao.falecimento)).FirstOrDefault(z => z.VNC_DEMISSAO == null).VNC_ADMISSAO
        //                :
        //                     fun.vinculo.OrderByDescending(x => x.VNC_ID).Where(x => x.VNC_DEMISSAO != null).FirstOrDefault().VNC_ADMISSAO
        //            :
        //                fun.vinculo.Where(x => x.VNC_ADMISSAO >= inicioAdmissao && x.VNC_ADMISSAO <= fimAdmissao).LastOrDefault().VNC_ADMISSAO,

        //        FUN_DATA_DEMISSAO = inicioAdmissao == null && fimAdmissao == null ?
        //                !fun.vinculo.Any(x => x.VNC_DEMISSAO == null) ?
        //                    fun.vinculo.Where(x => x.VNC_DEMISSAO != null).FirstOrDefault(z => z.VNC_DEMISSAO != null).VNC_DEMISSAO
        //                :
        //                    null
        //            :
        //                fun.vinculo.Where(x => x.VNC_ADMISSAO >= inicioAdmissao && x.VNC_ADMISSAO <= fimAdmissao).LastOrDefault().VNC_DEMISSAO
        //    }).ToList();


        //    return lista;
        //}

        private string GetNomeCracha(string nome)
        {
            string[] lName;
            lName = nome.Split(' ');

            nome = lName[0];
            nome = nome + " " + lName[lName.Length - 1];

            return nome;
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _funcionarioRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalFuncionario()
        {
            return _funcionarioRepository.Count(x => x.FUN_STATUS == "A");
        }

        public bool ValidaCracha(int Id)
        {
            throw new NotImplementedException();
        }

        public bool AddUpdateSolicitacaoCracha(FuncionarioSolicitarCrachaDomainModel modelDomain)
        {
            try
            {
                ap_funcionariocracha cracha = new ap_funcionariocracha();
                if (modelDomain.FUNCRC_ID == 0)
                {

                    cracha.FUNCRC_DATASOLICITACAO = DateTime.Now;
                    cracha.FUNCRC_OBSERVACAO = modelDomain.FUNCRC_OBSERVACAO;
                    cracha.FUNCRC_SITUACAO = modelDomain.FUNCRC_SITUACAO;
                    cracha.FUNCRC_TIPO = modelDomain.FUNCRC_TIPO;
                    cracha.VNC_ID = modelDomain.VNC_ID;
                    cracha.FUNCRC_REGDATE = DateTime.Now;
                    cracha.FUNCRC_STATUS = "A";
                    cracha.FUNCRC_REGUSER = modelDomain.FUNCRC_REGUSER;

                    _crachaRepository.Insert(cracha);

                }
                else
                {
                    cracha = _crachaRepository.SingleOrDefault(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_ID == modelDomain.FUNCRC_ID);
                    if (cracha != null)
                    {
                        cracha.FUNCRC_DATAEMISSAO = modelDomain.FUNCRC_DATAEMISSAO;
                        cracha.FUNCRC_NUMERO = modelDomain.FUNCRC_NUMERO;
                        cracha.FUNCRC_OBSERVACAO = modelDomain.FUNCRC_OBSERVACAO;
                        cracha.FUNCRC_SITUACAO = modelDomain.FUNCRC_SITUACAO;
                        cracha.FUNCRC_TIPO = modelDomain.FUNCRC_TIPO;
                        cracha.VNC_ID = modelDomain.VNC_ID;
                        cracha.FUNCRC_REGDATE = DateTime.Now;
                        cracha.FUNCRC_STATUS = "A";
                        cracha.FUNCRC_REGUSER = modelDomain.FUNCRC_REGUSER;

                        _crachaRepository.Update(cracha);
                    }
                }



                return true;
            }
            catch { return false; }
        }

        public List<FuncionarioSolicitarCrachaDomainModel> LoadCrachaByParameters(int? fun_id, string matricula, string nome, string numero, int? cargo, int? unidade, int? tipo, int? situacao, DateTime? dataInicio, DateTime? dataFim, DateTime? inicioEmissao, DateTime? fimEmissao)
        {

            var listAllDomain = _crachaRepository.GetAll(x => x.FUNCRC_STATUS == "A").ToList();
            var listDomain = listAllDomain;
            if (fun_id != null)
            {
                listDomain = listDomain.Where(x => x.FUNCRC_ID == fun_id.Value).ToList();
            }

            if (matricula != null && matricula != "")
            {
                listDomain = listDomain.Where(x => x.ap_vinculo.ap_funcionario.FUN_MATRICULA.Contains(matricula)).ToList();
            }

            if (nome != null && nome != "")
            {
                listDomain = listDomain.Where(x => x.ap_vinculo.ap_funcionario.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList();
            }
            if (numero != null && numero != "")
            {
                listDomain = listDomain.Where(x => x.FUNCRC_NUMERO != null && x.FUNCRC_NUMERO.Contains(numero)).ToList();

            }
            if (cargo != null)
            {
                listDomain = listDomain.Where(z => z.ap_vinculo.CRG_ID == cargo.Value).ToList();

            }
            if (unidade != null)
            {

                listDomain = listDomain.Where(x => x.ap_vinculo.ap_vinculoxunidade.Any(y => y.UND_ID == unidade)).ToList();
            }
            if (tipo != null)
            {
                listDomain = listDomain.Where(x => x.FUNCRC_TIPO == tipo).ToList();

            }
            if (situacao != null)
            {
                listDomain = listDomain.Where(x => x.FUNCRC_SITUACAO == situacao).ToList();

            }
            if (dataInicio != null)
            {
                listDomain = listDomain.Where(x => x.FUNCRC_DATASOLICITACAO >= dataInicio).ToList();
            }
            if (dataFim != null)
            {
                listDomain = listDomain.Where(x => x.FUNCRC_DATASOLICITACAO <= dataFim).ToList();
            }

            if (inicioEmissao.HasValue)
            {
                listDomain = listDomain.Where(x => x.FUNCRC_DATAEMISSAO.Value.Date >= inicioEmissao.Value.Date).ToList();
            }
            if (fimEmissao.HasValue)
            {
                listDomain = listDomain.Where(x => x.FUNCRC_DATAEMISSAO.Value.Date <= fimEmissao.Value.Date).ToList();
            }

            return listDomain.Count > 0 ? listDomain.Select(x => new FuncionarioSolicitarCrachaDomainModel
            {
                FUNCRC_DATASOLICITACAO = x.FUNCRC_DATASOLICITACAO,
                FUNCRC_DATAEMISSAO = x.FUNCRC_DATAEMISSAO,
                FUNCRC_ID = x.FUNCRC_ID,
                FUNCRC_NUMERO = x.FUNCRC_NUMERO,
                FUNCRC_OBSERVACAO = x.FUNCRC_OBSERVACAO,
                FUNCRC_REGUSER = x.FUNCRC_REGUSER,
                FUNCRC_SITUACAO = x.FUNCRC_SITUACAO,
                FUNCRC_TIPO = x.FUNCRC_TIPO,
                FUN_ID = x.ap_vinculo.FUN_ID,
                VNC_ID = x.VNC_ID,
                VIA = (listAllDomain.Where(z => z.FUNCRC_DATASOLICITACAO <= x.FUNCRC_DATASOLICITACAO && z.VNC_ID == x.VNC_ID && z.FUNCRC_ID != x.FUNCRC_ID).Count() + 1),
                MATRICULA = x.ap_vinculo.ap_funcionario.FUN_MATRICULA,
                VNC_NOME = x.ap_vinculo.VNC_ID.ToString() + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                FUN_NOME = x.ap_vinculo.ap_funcionario.FUN_NOMECRACHA,
                FUN_FOTO = x.ap_vinculo.ap_funcionario.ap_funcionarioxfoto.Any(z => z.FUNFT_STATUS == "A") ? x.ap_vinculo.ap_funcionario.ap_funcionarioxfoto.FirstOrDefault(z => z.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados
            }).ToList() : new List<FuncionarioSolicitarCrachaDomainModel>();

        }

        public List<FuncionarioCartaoPontoDomainModel> GetCartaoPontoByVinculo(int vinculo, DateTime dataInicio, DateTime dataFim)
        {
            throw new NotImplementedException();
        }

        public FuncionarioCartaoPontoDomainModel GetDadosCartaoPonto(int id)
        {
            ap_funcionario funcionario = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_ID == id);
            byte[] imagemByteDados = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));

            if (funcionario.ap_funcionarioxfoto.Where(x => x.FUNFT_STATUS == "A").Count() > 0)
            {
                imagemByteDados = funcionario.ap_funcionarioxfoto.FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO;
            }

            return new FuncionarioCartaoPontoDomainModel { FUN_FOTO = imagemByteDados, FUN_ID = funcionario.FUN_ID, MATRICULA = funcionario.FUN_MATRICULA, NOME = funcionario.FUN_NOME };
        }

        public bool EditarNomeCracha(FuncionarioNomeCrachaDomainModel modelDomain)
        {
            try
            {
                ap_funcionario func = _funcionarioRepository.SingleOrDefault(x => x.FUN_ID == modelDomain.FUN_ID);
                func.FUN_NOMECRACHA = modelDomain.FUN_NOMECRACHA;
                func.FUN_REGDATE = DateTime.Now;
                func.FUN_REGUSER = modelDomain.FUN_REGUSER;

                _funcionarioRepository.Update(func);
                _funcionarioRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public FuncionarioNomeCrachaDomainModel GetNomeCrachaByFuncionario(int id)
        {
            ap_funcionario funcionario = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_ID == id);
            FuncionarioNomeCrachaDomainModel domainModel = new FuncionarioNomeCrachaDomainModel()
            { FUN_ID = funcionario.FUN_ID, FUN_NOMECRACHA = funcionario.FUN_NOMECRACHA, FUN_REGUSER = funcionario.FUN_REGUSER };
            return domainModel;
        }

        public FuncionarioSolicitarCrachaDomainModel GetSolicitacaoCrachaByID(int id)
        {
            ap_funcionariocracha solicitacao = _crachaRepository.SingleOrDefault(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_ID == id);

            FuncionarioSolicitarCrachaDomainModel domainModel = new FuncionarioSolicitarCrachaDomainModel()
            {
                FUN_ID = solicitacao.ap_vinculo.FUN_ID,
                FUNCRC_DATAEMISSAO = solicitacao.FUNCRC_DATAEMISSAO,
                FUNCRC_DATASOLICITACAO = solicitacao.FUNCRC_DATASOLICITACAO,
                FUNCRC_ID = solicitacao.FUNCRC_ID,
                FUNCRC_NUMERO = solicitacao.FUNCRC_NUMERO,
                FUNCRC_OBSERVACAO = solicitacao.FUNCRC_OBSERVACAO,
                FUNCRC_REGUSER = solicitacao.FUNCRC_REGUSER,
                FUNCRC_SITUACAO = solicitacao.FUNCRC_SITUACAO,
                FUNCRC_TIPO = solicitacao.FUNCRC_TIPO,
                FUN_NOME = solicitacao.ap_vinculo.ap_funcionario.FUN_NOMECRACHA,
                VNC_ID = solicitacao.ap_vinculo.VNC_ID,
                VNC_NOME = (solicitacao.ap_vinculo.VNC_ID + " - " + solicitacao.ap_vinculo.ap_cargo.CRG_NOME + " - " + solicitacao.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO)
            };

            return domainModel;
        }

        public bool DeleteFuncionario(FuncionarioDomainModel modelDomain)
        {
            bool result = false;
            ap_funcionario funcionario = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_ID == modelDomain.FUN_ID);

            if (funcionario != null)
            {
                funcionario.FUN_REGUSER = modelDomain.FUN_REGUSER;
                funcionario.FUN_REGDATE = DateTime.Now;
                funcionario.FUN_STATUS = "I";
                _funcionarioRepository.Update(funcionario);
                result = true;
            }
            return result;
        }

        public bool DeleteSolicitacaoCracha(FuncionarioSolicitarCrachaDomainModel modelDomain)
        {
            bool result = false;

            ap_funcionariocracha solicitacao = _crachaRepository.SingleOrDefault(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_ID == modelDomain.FUNCRC_ID);

            if (solicitacao != null)
            {
                solicitacao.FUNCRC_REGDATE = DateTime.Now;
                solicitacao.FUNCRC_STATUS = "I";
                solicitacao.FUNCRC_REGUSER = modelDomain.FUNCRC_REGUSER;
                _crachaRepository.Update(solicitacao);
                result = true;

            }
            return result;
        }

        public int TotalSolicitacaoCrachaPendente()
        {
            return _crachaRepository.GetAll(x => x.FUNCRC_STATUS == "A" && x.FUNCRC_SITUACAO == 1).Count();
        }

        public List<CountFuncionarioSituacaoDomainModel> TotalRegistroPorSituacao()
        {
            throw new NotImplementedException();
            //return _vinculoSituacaoRepository.GetAll(x=>x.VNCST_STATUS=="A" && x.ap_vinculo.Any(z=>z.VNC_STATUS=="A" && z.))
        }

        public FuncionarioCartaoSaudeDomainModel buscarFuncionarioCartaoSaudePorId(int id)
        {
            ap_funcionario fun = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_ID == id);

            FuncionarioCartaoSaudeDomainModel domainModel = new FuncionarioCartaoSaudeDomainModel()
            {
                FUN_ID = fun.FUN_ID,
                FUN_MATRICULA = fun.FUN_MATRICULA,
                FUN_NOME = fun.FUN_NOME,
                FUN_NOMECRACHA = fun.FUN_NOMECRACHA,
                FUN_NUMEROEND = fun.FUN_NUMEROEND,
                FUN_SEXO = fun.FUN_SEXO,
                FUN_TIPOSANGUINEO = fun.FUN_TIPOSANGUINEO,
                FUN_BAIRRO = fun.FUN_BAIRRO,
                FUN_COMPLEMENTO = fun.FUN_COMPLEMENTO,
                FUN_ENDERECO = fun.FUN_ENDERECO,
                FUN_FOTO = fun.ap_funcionarioxfoto.Any(x => x.FUNFT_STATUS == "A") ? fun.ap_funcionarioxfoto.FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados
            };

            return domainModel;
        }

        public List<FuncionarioIndexDomainModel> ddlFuncionarioAtendimentoSASS()
        {
            return _vinculoRepository.GetAll(x => x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.ap_unidade.UND_SIGLA.Contains("SASS")) && x.ap_cargo.CRG_STATUS == "A"
            && x.ap_cargo.CRG_NOME.ToLower().Contains("medico")
            && x.VNC_STATUS == "A"
            && x.ap_funcionario.FUN_STATUS == "A"
            && x.VNCTP_ID != (int)VinculoTipoDomainModel.Tipo.estagiario
            ).ToList().Select(x => new FuncionarioIndexDomainModel()
            {
                FUN_ID = x.FUN_ID.Value,
                FUN_MATRICULA = x.ap_funcionario.FUN_MATRICULA.ToString(),
                FUN_NOME = x.ap_funcionario.FUN_NOME,
                SITUACAO = x.VNCST_ID.Value,
                Ativo = x.ap_vinculoxunidade.Any(z => z.ap_unidade.UND_SIGLA.Contains("SASS") && z.VNCU_DATAFIM == null)
            }
            ).ToList();
        }

        public int TotalFuncionarioPorParametros(DateTime? vinculoInicio, DateTime? vinculoFim, int? situacao, int? cargo)
        {
            var query = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A");

            if (vinculoInicio.HasValue)
                query.Where(x => x.VNC_ADMISSAO.Value.Date >= vinculoInicio);
            if (vinculoFim.HasValue)
                query.Where(x => x.VNC_DEMISSAO.Value.Date <= vinculoFim || (x.VNC_ADMISSAO <= vinculoFim && x.VNC_DEMISSAO == null));
            else
                query.Where(x => x.VNC_DEMISSAO == null);

            //Ativo
            if (situacao == (int)VinculoSituacaoDomainModel.Situacao.ativo)
                query.Where(x => x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo);
            else if (situacao.HasValue)
                query.Where(x => x.VNCST_ID == situacao);

            if (cargo.HasValue)
                query.Where(x => x.CRG_ID == cargo);

            return query.ToList().Count();

        }

    }

    public static class DescriptionExtension
    {
        public static string Descricao<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(
            typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Name;
            else return source.ToString();
        }
    }
}
