using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.InfraValidation.Interface;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class VinculoUnidadeBusiness : IVinculoUnidadeBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidationDictionary _validationDictionary;
        private readonly VinculoUnidadeRepository _lotacaoRepository;

        public VinculoUnidadeBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _lotacaoRepository = new VinculoUnidadeRepository(_unitOfWork);
        }
        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        protected bool validarInsert(VinculoUnidadeDomainModel _domainModel)
        {
            var list = _lotacaoRepository.GetAll(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null && x.VNCU_ID != _domainModel.VNCU_ID && x.VNC_ID == _domainModel.VNC_ID).ToList();

            if (list.Count() > 0)
                _validationDictionary.AddError("VNC_ID", "Este Vínculo já possuí uma lotação ativa.");


            return _validationDictionary.IsValid;
        }

        public bool AddUpdateLotacao(VinculoUnidadeDomainModel _domainModel)
        {
            if (!validarInsert(_domainModel))
                return false;

            try
            {

                ap_vinculoxunidade _lotacao = new ap_vinculoxunidade();
                if (_domainModel.VNCU_ID == 0)
                {
                    _lotacao.VNC_ID = _domainModel.VNC_ID;
                    _lotacao.UND_ID = _domainModel.UND_ID;
                    _lotacao.VNCU_ATRIBUICAO = _domainModel.VNCU_ATRIBUICAO;
                    _lotacao.VNCU_DATAFIM = _domainModel.VNCU_DATAFIM;
                    _lotacao.VNCU_DATAINICIO = _domainModel.VNCU_DATAINICIO;
                    _lotacao.VNCU_REGDATE = DateTime.Now;
                    _lotacao.VNCU_REGUSER = _domainModel.VNCU_REGUSER;
                    _lotacao.VNCU_STATUS = "A";

                    _lotacaoRepository.Insert(_lotacao);
                    return true;

                }
                else
                {
                    _lotacao = _lotacaoRepository.SingleOrDefault(x => x.VNCU_STATUS == "A" && x.VNCU_ID == _domainModel.VNCU_ID);

                    //verifica se a lotação está sendo fechada
                    if (_domainModel.VNCU_DATAFIM != null)
                    {
                        //Verifica se o vinculo da lotação não possui algum beneficio.
                        if (_lotacao.ap_vinculo.ap_beneficioxvinculo.Any(x => x.BNFVNC_STATUS == "A" && x.BNFVNC_DATAFIM == null))
                        {
                            //fecha os beneficios existentes com a data informada para fechamento da lotação
                            foreach (var beneficio in _lotacao.ap_vinculo.ap_beneficioxvinculo.Where(x => x.BNFVNC_STATUS == "A" && x.BNFVNC_DATAFIM == null))
                            {
                                beneficio.BNFVNC_DATAFIM = _domainModel.VNCU_DATAFIM;
                                beneficio.BNFVNC_REGDATE = DateTime.Now;
                                beneficio.BNFVNC_REGUSER = _domainModel.VNCU_REGUSER;

                            }
                        }
                        //Verifica se o vinculo da lotação não possui alguma função.
                        if (_lotacao.ap_vinculo.ap_funcaoxvinculo.Any(x => x.FNCVNC_STATUS == "A" && x.FNCVNC_DATAFIM == null))
                        {
                            //fecha as funções existentes com a data informada para fechamento da lotação
                            foreach (var funcao in _lotacao.ap_vinculo.ap_funcaoxvinculo.Where(x => x.FNCVNC_STATUS == "A" && x.FNCVNC_DATAFIM == null))
                            {
                                funcao.FNCVNC_DATAFIM = _domainModel.VNCU_DATAFIM;
                                funcao.FNCVNC_REGDATE = DateTime.Now;
                                funcao.FNCVNC_REGUSER = _domainModel.VNCU_REGUSER;

                            }
                        }

                    }
                    _lotacao.VNC_ID = _domainModel.VNC_ID;
                    _lotacao.UND_ID = _domainModel.UND_ID;
                    _lotacao.VNCU_ATRIBUICAO = _domainModel.VNCU_ATRIBUICAO;
                    _lotacao.VNCU_DATAFIM = _domainModel.VNCU_DATAFIM;
                    _lotacao.VNCU_DATAINICIO = _domainModel.VNCU_DATAINICIO;
                    _lotacao.VNCU_REGDATE = DateTime.Now;
                    _lotacao.VNCU_REGUSER = _domainModel.VNCU_REGUSER;



                    _lotacaoRepository.Update(_lotacao);
                    return true;
                }

            }
            catch { return false; }
        }

        public bool DeleteLotacao(VinculoUnidadeDomainModel _domainModel)
        {
            ap_vinculoxunidade _lotacao = _lotacaoRepository.SingleOrDefault(x => x.VNCU_STATUS == "A" && x.VNCU_ID == _domainModel.VNCU_ID);

            if (_lotacao != null)
            {
                _lotacao.VNCU_REGDATE = DateTime.Now;
                _lotacao.VNCU_REGUSER = _domainModel.VNCU_REGUSER;
                _lotacao.VNCU_STATUS = "I";

                _lotacaoRepository.Update(_lotacao);
                return true;
            }
            return false;
        }

        public List<VinculoUnidadeDomainModel> GetLotacaoByFuncionario(int func)
        {
            var list = _lotacaoRepository.GetAll(x => x.VNCU_STATUS == "A" && x.ap_vinculo.FUN_ID == func).ToList();
            var listDomain = list.Select(_lotacao => new VinculoUnidadeDomainModel
            {
                FUN_ID = _lotacao.ap_vinculo.FUN_ID.Value,
                UND_ID = _lotacao.UND_ID.Value,
                UND_NOME = _lotacao.ap_unidade.UND_SIGLA + " - " + _lotacao.ap_unidade.UND_NOME,
                VNCU_ATRIBUICAO = _lotacao.VNCU_ATRIBUICAO,
                VNCU_DATAFIM = _lotacao.VNCU_DATAFIM,
                VNCU_DATAINICIO = _lotacao.VNCU_DATAINICIO,
                VNCU_ID = _lotacao.VNCU_ID,
                VNC_ID = _lotacao.VNC_ID.Value,
                VNCU_REGUSER = _lotacao.VNCU_REGUSER,
                VNC_NOME = _lotacao.ap_vinculo.VNC_ID.ToString() + " - " + _lotacao.ap_vinculo.ap_cargo.CRG_NOME + " - " + _lotacao.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,

            }).ToList();

            return listDomain;
        }

        public VinculoUnidadeDomainModel GetLotacaoById(int id)
        {
            ap_vinculoxunidade _lotacao = _lotacaoRepository.SingleOrDefault(x => x.VNCU_STATUS == "A" && x.VNCU_ID == id);


            VinculoUnidadeDomainModel _domainModel = new VinculoUnidadeDomainModel()
            {
                FUN_ID = _lotacao.ap_vinculo.FUN_ID.Value,
                UND_ID = _lotacao.UND_ID.Value,
                UND_NOME = _lotacao.ap_unidade.UND_NOME,
                VNCU_ATRIBUICAO = _lotacao.VNCU_ATRIBUICAO,
                VNCU_DATAFIM = _lotacao.VNCU_DATAFIM,
                VNCU_DATAINICIO = _lotacao.VNCU_DATAINICIO,
                VNCU_ID = _lotacao.VNCU_ID,
                VNC_ID = _lotacao.VNC_ID.Value,
                VNCU_REGUSER = _lotacao.VNCU_REGUSER,
                VNC_NOME = _lotacao.ap_vinculo.VNC_ID + " - " + _lotacao.ap_vinculo.ap_cargo.CRG_NOME + " - " + _lotacao.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO
            };

            return _domainModel;
        }

        public List<VinculoUnidadeDomainModel> GetLotacaoByUnidade(int und)
        {
            throw new NotImplementedException();
        }


        public bool Salvar()
        {
            bool result = true;
            try
            {
                _lotacaoRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistro()
        {
            return _lotacaoRepository.Count(x => x.VNCU_STATUS == "A");
        }

        public int TotalRegistroByFuncionario(int id)
        {
            return _lotacaoRepository.Count(x => x.VNCU_STATUS == "A" && x.ap_vinculo.FUN_ID == id);
        }
    }
}
