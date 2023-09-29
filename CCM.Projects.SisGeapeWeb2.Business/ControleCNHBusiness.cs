using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.InfraValidation.Interface;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class ControleCNHBusiness : IControleCNHBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VinculoRepository _vinculoRepository;
        private readonly ControleCNHRepository _controleCNHRepository;
        private readonly ControleCNHAuxiliarRepository _controleCNHAuxiliarRepository;

        public ControleCNHBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _vinculoRepository = new VinculoRepository(_unitOfWork);
            _controleCNHRepository = new ControleCNHRepository(_unitOfWork);
            _controleCNHAuxiliarRepository = new ControleCNHAuxiliarRepository(_unitOfWork);
        }

        //FUNÇÃO QUE ADICIONA UM NOVO REGISTRO DE CNH
        public bool AddUpdateControleCNH(ControleCNHDomainModel domainModel)
        {
            try
            {
                if (domainModel.CNH_ID == 0)
                {
                    ap_controlecnh _cnh = new ap_controlecnh()
                    {
                        CNH_NREG = domainModel.CNH_NREG,
                        CNH_REGDATE = DateTime.Now,
                        CNH_REGUSER = domainModel.CNH_REGUSER,
                        VNC_ID = domainModel.VNC_ID,
                        CNH_STATUS = "A",
                        ap_controlecnh_aux = domainModel.auxiliar.Select(x => new ap_controlecnh_aux()
                        {
                            CNHA_CATEGORIA = x.CNHA_CATEGORIA,
                            CNHA_EMISSAO = x.CNHA_EMISSAO,
                            CNHA_RECEBIMENTO = x.CNHA_DATARECEBIMENTO,
                            CNHA_REGUSER = (int?)x.CNHA_REGUSER,
                            CNHA_STATUS = "A",
                            CNHA_REGDATE = DateTime.Now,
                            CNHA_VALIDADE = x.CNHA_VALIDADE,
                        }).ToList()
                    };
                    _controleCNHRepository.Insert(_cnh);
                    return true;
                }
                else
                {
                    ap_controlecnh _cnh = _controleCNHRepository.SingleOrDefault(x => x.CNH_STATUS == "A" && x.CNH_ID == domainModel.CNH_ID);
                    _cnh.CNH_NREG = domainModel.CNH_NREG;
                    _cnh.CNH_REGDATE = DateTime.Now;
                    _cnh.CNH_REGUSER = domainModel.CNH_REGUSER;
                    _cnh.VNC_ID = domainModel.VNC_ID;

                    _controleCNHRepository.Update(_cnh);
                    return true;
                }
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        //FUNÇÃO QUE ADICIONA OU ATUALIZAR UM NOVO REGISTRO DE DADOS DE CNH
        public bool AddUpdateControleCNHFilho(ControleCNHAuxiliarDomainModel domainModel)
        {
            try
            {
                if (domainModel.CNHA_ID == 0)
                {
                    ap_controlecnh_aux _cnh = new ap_controlecnh_aux()
                    {
                        CNHA_VALIDADE = domainModel.CNHA_VALIDADE,
                        CNH_ID = domainModel.CNH_ID,
                        CNHA_EMISSAO = domainModel.CNHA_EMISSAO,
                        CNHA_RECEBIMENTO = DateTime.Now,
                        CNHA_CATEGORIA = domainModel.CNHA_CATEGORIA,
                        CNHA_REGDATE = DateTime.Now,
                        CNHA_REGUSER = domainModel.CNHA_REGUSER,
                        CNHA_STATUS = "A"
                    };
                    //Carrega informações dos dados de CNH PAI e atualiza
                    if (domainModel.CNH.CNH_NREG != null)
                    {
                        ap_controlecnh _cnhPai;


                        if (domainModel.CNH_ID != 0)
                        {
                            _cnhPai = _controleCNHRepository.SingleOrDefault(x => x.CNH_ID == domainModel.CNH_ID);

                            _cnhPai.CNH_ID = domainModel.CNH_ID.Value;
                            _cnhPai.CNH_NREG = domainModel.CNH.CNH_NREG;
                            _cnhPai.CNH_REGDATE = DateTime.Now;
                            _cnhPai.CNH_REGUSER = domainModel.CNHA_REGUSER;
                            _cnhPai.CNH_STATUS = "A";
                            _cnhPai.VNC_ID = domainModel.CNH.VNC_ID;
                            _controleCNHRepository.Update(_cnhPai);
                        }
                        else
                        {
                            _cnhPai = new ap_controlecnh()
                            {
                                CNH_ID = domainModel.CNH_ID.Value,
                                CNH_NREG = domainModel.CNH.CNH_NREG,
                                CNH_REGDATE = DateTime.Now,
                                CNH_REGUSER = domainModel.CNHA_REGUSER,
                                CNH_STATUS = "A",
                                VNC_ID = domainModel.CNH.VNC_ID
                            };
                            _controleCNHRepository.Insert(_cnhPai);

                        }

                    }

                    _controleCNHAuxiliarRepository.Insert(_cnh);

                    return true;
                }
                else
                {

                    ap_controlecnh_aux _cnh = _controleCNHAuxiliarRepository.SingleOrDefault(x => x.CNHA_STATUS == "A" && x.CNHA_ID == domainModel.CNHA_ID);
                    _cnh.CNHA_VALIDADE = domainModel.CNHA_VALIDADE;
                    _cnh.CNHA_EMISSAO = domainModel.CNHA_EMISSAO;
                    _cnh.CNHA_RECEBIMENTO = _cnh.CNHA_RECEBIMENTO;
                    _cnh.CNHA_CATEGORIA = domainModel.CNHA_CATEGORIA;
                    _cnh.CNHA_REGDATE = DateTime.Now;
                    _cnh.CNHA_REGUSER = domainModel.CNHA_REGUSER;
                    _cnh.ap_controlecnh.CNH_NREG = domainModel.CNH.CNH_NREG;

                    _controleCNHAuxiliarRepository.Update(_cnh);

                    return true;
                }
            }
            catch (System.Exception e)
            {
                var msg = e;
                return false;
            }
        }

        //FUNÇÃO QUE REMOVE CNH
        public bool DeleteControleCNH(ControleCNHDomainModel domainModel)
        {
            try
            {
                ap_controlecnh _cnh = _controleCNHRepository.SingleOrDefault(x => x.CNH_STATUS == "A" && x.CNH_ID == domainModel.CNH_ID);
                _cnh.CNH_REGDATE = DateTime.Now;
                _cnh.CNH_REGUSER = domainModel.CNH_REGUSER;
                _cnh.CNH_STATUS = "I";

                _controleCNHRepository.Update(_cnh);
                return true;
            }
            catch { return false; }
        }

        //FUNÇÃO QUE REMOVE DADOS DE CNH
        public bool DeleteControleCNHFilho(ControleCNHAuxiliarDomainModel domainModel)
        {
            try
            {
                ap_controlecnh_aux _cnh = _controleCNHAuxiliarRepository.SingleOrDefault(x => x.CNHA_STATUS == "A" && x.CNHA_ID == domainModel.CNHA_ID);
                _cnh.CNHA_REGDATE = DateTime.Now;
                _cnh.CNHA_REGUSER = domainModel.CNHA_REGUSER;
                _cnh.CNHA_STATUS = "I";


                _controleCNHAuxiliarRepository.Update(_cnh);
                return true;
            }
            catch { return false; }
        }

        //FUNÇÃO QUE RETORNA TODOS AS CNHs
        public List<ControleCNHDomainModel> GetAllControleCNH()
        {
            return _controleCNHRepository.GetAll(x => x.CNH_STATUS == "A").AsParallel().ToList().Select(x => new ControleCNHDomainModel
            {
                CNH_ID = x.CNH_ID,
                CNH_NREG = x.CNH_NREG,
                CNH_REGUSER = x.CNH_REGUSER.Value,
                VNC_ID = x.VNC_ID.Value,
                auxiliar = x.ap_controlecnh_aux.Where(z => z.CNHA_STATUS == "A").Select(y => new ControleCNHAuxiliarDomainModel
                {
                    CNHA_ID = y.CNHA_ID,
                    CNH_ID = y.CNH_ID.Value,
                    CNHA_CATEGORIA = y.CNHA_CATEGORIA,
                    CNHA_DATARECEBIMENTO = y.CNHA_RECEBIMENTO,
                    CNHA_EMISSAO = y.CNHA_EMISSAO,
                    CNHA_REGDATE = y.CNHA_REGDATE.Value,
                    CNHA_REGUSER = y.CNHA_REGUSER.Value,
                    CNHA_STATUS = y.CNHA_STATUS,
                    CNHA_VALIDADE = y.CNHA_VALIDADE
                }).ToList()
            }).ToList();
        }

        //FUNÇÃO QUE RETORNA UM REGISTRO DE CNH
        public ControleCNHDomainModel GetControleCNHById(int id)
        {
            ap_controlecnh _cnh = _controleCNHRepository.SingleOrDefault(x => x.CNH_STATUS == "A" && x.CNH_ID == id);

            ControleCNHDomainModel domainModel = new ControleCNHDomainModel()
            {
                auxiliar = _cnh.ap_controlecnh_aux.Select(x => new ControleCNHAuxiliarDomainModel
                {
                    CNHA_ID = x.CNHA_ID,
                    CNH_ID = x.CNH_ID,
                    CNHA_CATEGORIA = x.CNHA_CATEGORIA,
                    CNHA_DATARECEBIMENTO = x.CNHA_RECEBIMENTO,
                    CNHA_EMISSAO = x.CNHA_EMISSAO,
                    CNHA_REGDATE = x.CNHA_REGDATE.Value,
                    CNHA_REGUSER = x.CNHA_REGUSER,
                    CNHA_STATUS = x.CNHA_STATUS,
                    CNHA_VALIDADE = x.CNHA_VALIDADE
                }).ToList(),
                CNH_ID = _cnh.CNH_ID,
                CNH_NREG = _cnh.CNH_NREG,
                CNH_REGUSER = _cnh.CNH_REGUSER.Value,
                VNC_ID = _cnh.VNC_ID.Value
            };

            return domainModel;
        }

        //FUNÇÃO QUE RETORNA UM REGISTRO DE DADOS DE CNH
        public ControleCNHAuxiliarDomainModel GetControleCNHAById(int id)
        {
            ap_controlecnh_aux _cnh = _controleCNHAuxiliarRepository.SingleOrDefault(x => x.CNHA_STATUS == "A" && x.CNHA_ID == id);

            ControleCNHAuxiliarDomainModel domainModel = new ControleCNHAuxiliarDomainModel()
            {
                CNHA_ID = _cnh.CNHA_ID,
                CNH_ID = _cnh.CNH_ID.Value,
                CNHA_CATEGORIA = _cnh.CNHA_CATEGORIA,
                CNHA_VALIDADE = _cnh.CNHA_VALIDADE,
                CNHA_EMISSAO = _cnh.CNHA_EMISSAO,
                CNHA_DATARECEBIMENTO = _cnh.CNHA_RECEBIMENTO,
                CNHA_REGUSER = _cnh.CNHA_REGUSER.Value,
                CNH = new ControleCNHDomainModel { CNH_ID = _cnh.ap_controlecnh.CNH_ID, CNH_NREG = _cnh.ap_controlecnh.CNH_NREG, CNH_REGDATE = _cnh.ap_controlecnh.CNH_REGDATE.Value, CNH_STATUS = _cnh.ap_controlecnh.CNH_STATUS, VNC_ID = _cnh.ap_controlecnh.VNC_ID.Value }
            };

            return domainModel;
        }

        //FUNÇÃO QUE RETORNA CNH POR DADE DE VALIDADE
        public List<ControleCNHDomainModel> GetControleCNHValidade(DateTime? validade)
        {
            var listDomain = _controleCNHRepository.GetAll(x => x.CNH_STATUS == "A" && (x.ap_controlecnh_aux.LastOrDefault().CNHA_VALIDADE <= validade)).ToList();

            return listDomain.Select(x => new ControleCNHDomainModel() { CNH_ID = x.CNH_ID, CNH_NREG = x.CNH_NREG }).ToList();
        }

        //FUNÇÃO QUE RETORNA CNH POR VINCULO
        public ControleCNHVinculoDomainModel GetControleCNHByVinculo(int vnc)
        {

            byte[] imagemByteDados = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));

            ap_vinculo vinculo = _vinculoRepository.SingleOrDefault(x => x.VNC_STATUS == "A" && x.VNC_ID == vnc);
            return new ControleCNHVinculoDomainModel
            {
                VNC_ID = vinculo.VNC_ID,
                CARGO = vinculo.ap_cargo.CRG_NOME,
                FUN_FOTO = vinculo.ap_funcionario.ap_funcionarioxfoto.AsParallel().Where(x => x.FUNFT_STATUS == "A").Count() > 0 ? vinculo.ap_funcionario.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                ControleCNH = vinculo.ap_controlecnh.ToList().Select(x => new ControleCNHDomainModel()
                {
                    CNH_ID = x.CNH_ID,
                    CNH_NREG = x.CNH_NREG,
                    CNH_REGUSER = x.CNH_REGUSER.Value,
                    VNC_ID = x.VNC_ID.Value,
                    CNH_STATUS = x.CNH_STATUS,
                    auxiliar = x.ap_controlecnh_aux.Where(a => a.CNHA_STATUS == "A").Select(z => new ControleCNHAuxiliarDomainModel()
                    {
                        CNHA_CATEGORIA = z.CNHA_CATEGORIA,
                        CNHA_DATARECEBIMENTO = z.CNHA_RECEBIMENTO,
                        CNHA_EMISSAO = z.CNHA_EMISSAO,
                        CNHA_ID = z.CNHA_ID,
                        CNHA_REGUSER = z.CNHA_REGUSER,
                        CNHA_VALIDADE = z.CNHA_VALIDADE,
                        CNH_ID = z.CNH_ID,
                        CNHA_STATUS = z.CNHA_STATUS,
                        CNH = new ControleCNHDomainModel { CNH_NREG = x.CNH_NREG, CNH_ID = x.CNH_ID, VNC_ID = x.VNC_ID.Value }

                    }).ToList(),
                }).ToList(),
                FUN_ID = vinculo.FUN_ID.Value,
                FUN_NOME = vinculo.ap_funcionario.FUN_NOME,
                MATRICULA = vinculo.ap_funcionario.FUN_MATRICULA,
                SITUACAO = vinculo.ap_vinculosituacao.VNCST_DESCRICAO,
                TIPO = vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                CRG_ID = vinculo.CRG_ID.Value,
                UNIDADE = vinculo.ap_vinculoxunidade.AsParallel().Any(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null) ?
                    vinculo.ap_vinculoxunidade.AsParallel().FirstOrDefault(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA : "",

            };
        }

        //FUNÇÃO QUE REALIZA AS BUSCAS NO BD COM BASE NOS PARAMETROS REPASSADOS
        public List<ControleCNHPesquisaDomainModel> GetControleCNHPesquisa(string Matricula, string Nome, int? Situacao, int? Unidade)
        {
            byte[] imagemByteDados = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));
            //CARGOS DE MOTORISTAS ID'S 53 e 54
            var _list = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && (x.ap_cargo.CRG_ID == 53 || x.ap_cargo.CRG_ID == 54)).ToList()
            .Select(x => new
            {
                x.VNC_ID,
                x.VNC_ADMISSAO,
                x.VNC_DEMISSAO,
                x.FUN_ID,
                x.CRG_ID,
                x.ap_funcionario.FUN_NOME,
                x.ap_funcionario.FUN_MATRICULA,
                x.ap_cargo.CRG_NOME,

                unidade = x.ap_vinculoxunidade.Any(y => y.VNCU_STATUS == "A" && y.VNCU_DATAFIM == null) ?
                    x.ap_vinculoxunidade.Where(y => y.VNCU_STATUS == "A" && y.VNCU_DATAFIM == null).Select(z => z.ap_unidade).SingleOrDefault() :
                    x.ap_vinculoxunidade.Where(y => y.VNCU_STATUS == "A").Select(a => a.ap_unidade).FirstOrDefault(),

                situacaovinculo = x.ap_vinculosituacao.VNCST_DESCRICAO,
                tipovinculo = x.ap_vinculotipo.VNCTP_DESCRICAO,

                CNH_NREG = x.ap_controlecnh.Count() > 0 ?
                    x.ap_controlecnh.Where(z => z.CNH_NREG != null).SingleOrDefault().CNH_NREG : null,
                /*
                controlecnh = x.ap_controlecnh.Where(z => z.ap_controlecnh_aux != null).Count() > 0 ?
                    x.ap_controlecnh.Where(z => z.ap_controlecnh_aux != null)
                    .Select(a => a.ap_controlecnh_aux.Where(z => z.CNHA_VALIDADE == a.ap_controlecnh_aux.Where(s => s.CNHA_STATUS == "A").Max(s => s.CNHA_VALIDADE) || z.CNHA_VALIDADE == a.ap_controlecnh_aux.LastOrDefault().CNHA_VALIDADE).ToList()).SingleOrDefault() : null,
                
                */

                controlecnh = x.ap_controlecnh.Where(z => z.ap_controlecnh_aux != null).Count() > 0 ?
                    x.ap_controlecnh.Where(z => z.ap_controlecnh_aux != null).SingleOrDefault().ap_controlecnh_aux : null,


                FUN_FOTO = x.ap_funcionario.ap_funcionarioxfoto.AsParallel().Where(y => y.FUNFT_STATUS == "A").Count() > 0 ?
                    x.ap_funcionario.ap_funcionarioxfoto.AsParallel().FirstOrDefault(y => y.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados

            }).ToList();

            if (Matricula != null)
            {
                _list = _list.Where(x => x.FUN_MATRICULA != null && x.FUN_MATRICULA.ToString().Contains(Matricula)).ToList();
            }

            if (Nome != null)
            {
                _list = _list.Where(x => x.FUN_NOME.ToUpper().Contains(Nome.ToUpper())).ToList();
                //.OrderBy(x => x.controlecnh.Max(a => a.));
            }

            if (Situacao != null)
            {
                switch (Situacao)
                {
                    //Há 1 ano do vencimento
                    case 0:
                        _list = _list.Where(x => x.controlecnh != null && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days <= 365 && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days >= 0).ToList();
                        //_list = _list.Where(x => x.controlecnh != null && x.controlecnh.LastOrDefault(z => z.CNHA_VALIDADE != null && z.CNHA_STATUS == "A").CNHA_VALIDADE.Value.Subtract(DateTime.Now).Days <= 365).ToList();
                        break;
                    //Há 6 meses do vencimento
                    case 1:
                        _list = _list.Where(x => x.controlecnh != null && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days <= 180 && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days >= 0).ToList();
                        break;
                    //Há 3 meses do vencimento
                    case 2:
                        _list = _list.Where(x => x.controlecnh != null && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days <= 90 && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days >= 0).ToList();
                        break;
                    //Há 1 mes do vencimento
                    case 3:
                        _list = _list.Where(x => x.controlecnh != null && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days <= 30 && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days >= 0).ToList();
                        break;
                    //Vencido
                    case 4:
                        //_list = _list.Where(x => x.controlecnh != null && x.controlecnh.Where(z => z.CNHA_VALIDADE != null && z.CNHA_STATUS == "A" && z.CNHA_VALIDADE.Value.Subtract(DateTime.Now).Days < 0).Count() > 0).ToList();
                        //_list = _list.Where(x => x.controlecnh != null && x.controlecnh.Where(z => z.CNHA_VALIDADE != null && z.CNHA_STATUS == "A").LastOrDefault().CNHA_VALIDADE.Value.Subtract(DateTime.Now).Days < 0).ToList();
                        _list = _list.Where(x => x.controlecnh != null && x.controlecnh.Where(s => s.CNHA_VALIDADE != null && s.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days < 0).ToList();
                        break;
                }
            }

            if (Unidade != null)
            {
                _list = _list.Where(x => x.unidade != null && x.unidade.UND_ID == Unidade).ToList();
            }

            var lista = _list.Distinct().Select(
                fun => new ControleCNHPesquisaDomainModel()
                {
                    CNH_NREG = fun.CNH_NREG,
                    CNH_VNC_ID = fun.VNC_ID,
                    CNH_VNC_ADMISSAO = fun.VNC_ADMISSAO,
                    CNH_VNC_DEMISSAO = fun.VNC_DEMISSAO,
                    CNH_FUN_ID = fun.FUN_ID,
                    CNH_FUN_MATRICULA = fun.FUN_MATRICULA,
                    CNH_FUN_NOME = fun.FUN_NOME,
                    CNH_FUN_FOTO = fun.FUN_FOTO.Count() > 0 ?
                        fun.FUN_FOTO : null,
                    CNH_CRG_ID = fun.CRG_ID,
                    CNH_CRG_NOME = fun.CRG_NOME,
                    CNH_UND_ID = fun.unidade != null ?
                        fun.unidade.UND_ID : 0,
                    CNH_UND_SIGLA = fun.unidade != null ?
                        fun.unidade.UND_SIGLA : "",

                    CNH_DATARECEBIMENTO = fun.controlecnh != null && fun.controlecnh.Any() ?
                        fun.controlecnh.Select(a => a.CNHA_RECEBIMENTO).FirstOrDefault() : null,

                    CNH_VALIDADE = fun.controlecnh != null && fun.controlecnh.Any() ?
                        fun.controlecnh.Where(x => x.CNHA_VALIDADE != null && x.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE) : null,

                    CNH_DIAS_PARA_VENCER = fun.controlecnh != null && fun.controlecnh.Any() ?
                        fun.controlecnh.Where(x => x.CNHA_VALIDADE != null && x.CNHA_STATUS == "A").Max(a => a.CNHA_VALIDADE).Value.Subtract(DateTime.Now).Days : 0,

                    CNH_VNC_SITUACAO = fun.situacaovinculo,

                    CNH_VNC_TIPO = fun.tipovinculo
                }
            );

            return lista.ToList();
        }

        public void Initialize(IValidationDictionary validationDictionary)
        {
            throw new NotImplementedException();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _controleCNHRepository.Save();
                return result;
            }
            catch (System.Exception ex)
            {
                var erro = ex.Message;
                return !result;
            }
        }

        public int TotalRegistro()
        {
            throw new NotImplementedException();
        }
    }
}
