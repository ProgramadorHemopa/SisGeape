using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class FuncaoBusiness : IFuncaoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FuncaoRepository _funcaoRepository;
        private readonly ReferenciaFuncaoRepository _referenciaFuncaoRepository;

        public FuncaoBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _funcaoRepository = new FuncaoRepository(_unitOfWork);
            _referenciaFuncaoRepository = new ReferenciaFuncaoRepository(_unitOfWork);
        }
        public void AddUpdateFuncao(FuncaoDomainModel modelDomain)
        {
            if (modelDomain.FNC_ID == 0)
            {


                ap_funcao _funcao = new ap_funcao();

                _funcao.FNC_NOME = modelDomain.FNC_NOME.ToUpper();
                _funcao.REFFNC_ID = modelDomain.REFFNC_ID;
                _funcao.FNC_QUANTIDADE = modelDomain.FNC_QUANTIDADE;
                _funcao.FNC_REGDATE = DateTime.Now;
                _funcao.FNC_REGUSER = modelDomain.FNC_REGUSER;
                _funcao.FNC_STATUS = "A";




                _funcaoRepository.Insert(_funcao);
            }
            else
            {
                ap_funcao _funcao = _funcaoRepository.SingleOrDefault(x => x.FNC_STATUS == "A" && x.FNC_ID == modelDomain.FNC_ID);

                if (_funcao != null)
                {

                    _funcao.FNC_NOME = modelDomain.FNC_NOME.ToUpper();
                    _funcao.REFFNC_ID = modelDomain.REFFNC_ID;
                    _funcao.FNC_QUANTIDADE = modelDomain.FNC_QUANTIDADE;
                    _funcao.FNC_REGDATE = DateTime.Now;
                    _funcao.FNC_REGUSER = modelDomain.FNC_REGUSER;

                    _funcaoRepository.Update(_funcao);
                }

            }
        }

        public bool AddUpdateReferenciaFuncao(ReferenciaFuncaoDomainModel modelDomain)
        {
            try
            {
                ap_referenciafuncao _referencia;
                if (modelDomain.REFFNC_ID == 0)
                {
                    _referencia = new ap_referenciafuncao();

                    _referencia.REFFNC_ID = modelDomain.REFFNC_ID;
                    _referencia.REFFNC_NOME = modelDomain.REFFNC_NOME.ToUpper();
                    _referencia.REFFNC_OBSERVACAO = modelDomain.REFFNC_OBSERVACAO;
                    _referencia.REFFNC_VALOR = Convert.ToDouble(modelDomain.REFFNC_VALOR);
                    _referencia.REFFNC_REGUSER = modelDomain.REFFNC_REGUSER;
                    _referencia.REFFNC_REGDATE = DateTime.Now;
                    _referencia.REFFNC_STATUS = "A";

                    _referenciaFuncaoRepository.Insert(_referencia);
                    return true;
                }
                else
                {
                    _referencia = _referenciaFuncaoRepository.SingleOrDefault(x => x.REFFNC_STATUS == "A" && x.REFFNC_ID == modelDomain.REFFNC_ID);
                    if (_referencia != null)
                    {
                        _referencia.REFFNC_NOME = modelDomain.REFFNC_NOME.ToUpper();
                        _referencia.REFFNC_OBSERVACAO = modelDomain.REFFNC_OBSERVACAO;
                        _referencia.REFFNC_VALOR = Convert.ToDouble(modelDomain.REFFNC_VALOR);
                        _referencia.REFFNC_REGUSER = modelDomain.REFFNC_REGUSER;
                        _referencia.REFFNC_REGDATE = DateTime.Now;
                        _referencia.REFFNC_STATUS = "A";

                        _referenciaFuncaoRepository.Update(_referencia);
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        public bool DeleteFuncao(FuncaoDomainModel modelDomain)
        {
            bool result = false;
            ap_funcao _funcao = _funcaoRepository.SingleOrDefault(x => x.FNC_STATUS == "A" && x.FNC_ID == modelDomain.FNC_ID);

            if (_funcao != null)
            {
                _funcao.FNC_REGUSER = modelDomain.FNC_REGUSER;
                _funcao.FNC_REGDATE = DateTime.Now;
                _funcao.FNC_STATUS = "I";
                _funcaoRepository.Update(_funcao);
                result = true;
            }

            return result;
        }

        public bool DeleteReferenciaFuncao(ReferenciaFuncaoDomainModel modelDomain)
        {
            bool result = false;
            var _referencia = _referenciaFuncaoRepository.SingleOrDefault(x => x.REFFNC_STATUS == "A" && x.REFFNC_ID == modelDomain.REFFNC_ID);

            if (_referencia != null)
            {
                _referencia.REFFNC_REGUSER = modelDomain.REFFNC_REGUSER;
                _referencia.REFFNC_REGDATE = DateTime.Now;
                _referencia.REFFNC_STATUS = "I";
                _referenciaFuncaoRepository.Update(_referencia);
                result = true;
            }

            return result;
        }

        public List<ReferenciaFuncaoDomainModel> GetAllReferenciaFuncao()
        {
            return _referenciaFuncaoRepository.GetAll(x => x.REFFNC_STATUS == "A").Select(x => new ReferenciaFuncaoDomainModel
            {
                REFFNC_ID = x.REFFNC_ID,
                REFFNC_NOME = x.REFFNC_NOME,
                REFFNC_OBSERVACAO = x.REFFNC_OBSERVACAO,
                REFFNC_REGUSER = x.REFFNC_REGUSER,
                REFFNC_VALOR = x.REFFNC_VALOR.HasValue ? Convert.ToDecimal(x.REFFNC_VALOR) : 0
            }).ToList();
        }

        public List<FuncaoDomainModel> GetFuncao()
        {
            List<FuncaoDomainModel> listDomain = _funcaoRepository.GetAll(x => x.FNC_STATUS == "A").Select(x => new FuncaoDomainModel
            {
                FNC_NOME = x.FNC_NOME,
                FNC_QUANTIDADE = x.FNC_QUANTIDADE,
                REFFNC_ID = x.REFFNC_ID.Value,
                FNC_ID = x.FNC_ID,
                FNC_REGUSER = x.FNC_REGUSER
            }).ToList();
            foreach (var item in listDomain)
            {
                if (item.REFFNC_ID != 0)
                    item.REF_NOME = _referenciaFuncaoRepository.SingleOrDefault(x => x.REFFNC_ID == item.REFFNC_ID && x.REFFNC_STATUS == "A").REFFNC_NOME;
            }

            return listDomain;
        }

        public FuncaoDomainModel GetFuncaoById(int Id)
        {

            ap_funcao funcao = _funcaoRepository.SingleOrDefault(x => x.FNC_STATUS == "A" && x.FNC_ID == Id);

            FuncaoDomainModel domainModel = new FuncaoDomainModel()
            {
                FNC_NOME = funcao.FNC_NOME,
                FNC_QUANTIDADE = funcao.FNC_QUANTIDADE,
                REFFNC_ID = funcao.REFFNC_ID.Value,
                FNC_ID = funcao.FNC_ID,
                FNC_REGUSER = funcao.FNC_REGUSER
            };

            domainModel.REF_NOME = _referenciaFuncaoRepository.SingleOrDefault(x => x.REFFNC_ID == domainModel.REFFNC_ID && x.REFFNC_STATUS == "A").REFFNC_NOME;
            return domainModel;
        }

        public List<FuncaoDomainModel> GetFuncaoByName(string name)
        {
            List<FuncaoDomainModel> listDomain = _funcaoRepository.GetAll(x => x.FNC_STATUS == "A" && x.FNC_NOME.Contains(name)).Select(x => new FuncaoDomainModel
            {
                FNC_NOME = x.FNC_NOME,
                FNC_QUANTIDADE = x.FNC_QUANTIDADE,
                REFFNC_ID = x.REFFNC_ID.Value,
                FNC_ID = x.FNC_ID,
                FNC_REGUSER = x.FNC_REGUSER
            }).ToList();
            foreach (var item in listDomain)
            {
                if (item.REFFNC_ID != 0)
                    item.REF_NOME = _referenciaFuncaoRepository.SingleOrDefault(x => x.REFFNC_ID == item.REFFNC_ID && x.REFFNC_STATUS == "A").REFFNC_NOME;
            }

            return listDomain;
        }

        public List<FuncaoDomainModel> GetFuncaoRecords(int pageStart, int pageSize)
        {
            return _funcaoRepository.GetPagedRecords(x => x.FNC_STATUS == "A", x => x.FNC_NOME, pageStart, pageSize)
                         .Select(x => new FuncaoDomainModel
                         {
                             FNC_NOME = x.FNC_NOME,
                             FNC_QUANTIDADE = x.FNC_QUANTIDADE,
                             REFFNC_ID = x.REFFNC_ID.Value,
                             FNC_ID = x.FNC_ID,
                             FNC_REGUSER = x.FNC_REGUSER
                         }).ToList();
        }

        public ReferenciaFuncaoDomainModel GetReferenciaFuncaoById(int Id)
        {
            var refe = _referenciaFuncaoRepository.SingleOrDefault(x => x.REFFNC_STATUS == "A" && x.REFFNC_ID == Id);

            ReferenciaFuncaoDomainModel domainModel = new ReferenciaFuncaoDomainModel()
            {
                REFFNC_ID = refe.REFFNC_ID,
                REFFNC_NOME = refe.REFFNC_NOME,
                REFFNC_OBSERVACAO = refe.REFFNC_OBSERVACAO,
                REFFNC_REGUSER = refe.REFFNC_REGUSER,
                REFFNC_VALOR = refe.REFFNC_VALOR.HasValue ? Convert.ToDecimal(refe.REFFNC_VALOR) : 0
            };

            return domainModel;

        }

        public IEnumerable<ValidationResult> IsValid(FuncaoDomainModel cargo)
        {
            throw new NotImplementedException();
        }

        public int ReferenciaFuncaoTotalRegistros()
        {
            return _referenciaFuncaoRepository.Count(x => x.REFFNC_STATUS == "A");
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _funcaoRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistros()
        {
            return _funcaoRepository.Count(x => x.FNC_STATUS == "A");
        }
    }
}
