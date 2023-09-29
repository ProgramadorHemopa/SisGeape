using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class BeneficioBusiness : IBeneficioBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeneficioRepository _beneficioRepository;

        public BeneficioBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _beneficioRepository = new BeneficioRepository(_unitOfWork);
        }



        public void AddUpdateBeneficio(BeneficioDomainModel _domainModel)
        {
            if (_domainModel.BNF_ID == 0)
            {
                ap_beneficio _beneficio = new ap_beneficio()
                {
                    BNF_DESCRICAO = _domainModel.BNF_DESCRICAO.ToUpper(),
                    BNF_VALOR = _domainModel.BNF_VALOR,
                    BNF_REGDATE = DateTime.Now,
                    BNF_REGUSER = _domainModel.BNF_REGUSER,
                    BNF_STATUS = "A"

                };
                _beneficioRepository.Insert(_beneficio);

            }
            else
            {
                ap_beneficio _beneficio = _beneficioRepository.SingleOrDefault(x => x.BNF_STATUS == "A" && x.BNF_ID == _domainModel.BNF_ID);

                _beneficio.BNF_DESCRICAO = _domainModel.BNF_DESCRICAO.ToUpper();
                _beneficio.BNF_VALOR = _domainModel.BNF_VALOR;
                _beneficio.BNF_REGUSER = _domainModel.BNF_REGUSER;
                _beneficio.BNF_REGDATE = DateTime.Now;

                _beneficioRepository.Update(_beneficio);
            }
        }

        public bool DeleteBeneficio(BeneficioDomainModel _domainModel)
        {
            bool result = false;
            ap_beneficio _beneficio = _beneficioRepository.SingleOrDefault(x => x.BNF_STATUS == "A" && x.BNF_ID == _domainModel.BNF_ID);

            if (_beneficio != null)
            {
                _beneficio.BNF_REGUSER = _domainModel.BNF_REGUSER;
                _beneficio.BNF_REGDATE = DateTime.Now;
                _beneficio.BNF_STATUS = "I";
                _beneficioRepository.Update(_beneficio);

                result = true;
            }

            return result;
        }

        public List<BeneficioDomainModel> GetAllBeneficio()
        {
            return _beneficioRepository.GetAll(x => x.BNF_STATUS == "A").Select(x => new BeneficioDomainModel { BNF_DESCRICAO = x.BNF_DESCRICAO, BNF_VALOR = x.BNF_VALOR.Value, BNF_ID = x.BNF_ID }).ToList();
        }

        public BeneficioDomainModel GetBeneficioById(int Id)
        {
            var beneficio = _beneficioRepository.SingleOrDefault(x => x.BNF_STATUS == "A" && x.BNF_ID == Id);
            BeneficioDomainModel _modelDomain = new BeneficioDomainModel();
            _modelDomain.BNF_DESCRICAO = beneficio.BNF_DESCRICAO;
            _modelDomain.BNF_VALOR = beneficio.BNF_VALOR.Value;
            _modelDomain.BNF_ID = beneficio.BNF_ID;

            return _modelDomain;
        }

        public List<BeneficioDomainModel> GetBeneficioByName(string name)
        {
            return _beneficioRepository.GetAll(x => x.BNF_STATUS == "A" && x.BNF_DESCRICAO.Contains(name)).Select(x => new BeneficioDomainModel { BNF_DESCRICAO = x.BNF_DESCRICAO, BNF_VALOR = x.BNF_VALOR.Value, BNF_ID = x.BNF_ID }).ToList();
        }

        public List<BeneficioDomainModel> GetBeneficioRecords(int pageStart, int pageSize)
        {
            return _beneficioRepository.GetPagedRecords(x => x.BNF_STATUS == "A", x => x.BNF_DESCRICAO, pageStart, pageSize).Select(x => new BeneficioDomainModel { BNF_DESCRICAO = x.BNF_DESCRICAO, BNF_VALOR = x.BNF_VALOR.Value, BNF_ID = x.BNF_ID }).ToList();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _beneficioRepository.Save();
                return result;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return !result;
            }
        }


        public int TotalRegistros()
        {
            return _beneficioRepository.Count(x => x.BNF_STATUS == "A");
        }
    }
}
