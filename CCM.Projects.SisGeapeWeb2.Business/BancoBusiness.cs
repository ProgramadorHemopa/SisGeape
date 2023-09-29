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
    public class BancoBusiness : IBancoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BancoRepository _bancoRepository;

        public BancoBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _bancoRepository = new BancoRepository(_unitOfWork);
        }

        public BancoDomainModel AddUpdateBanco(BancoDomainModel _domainModel)
        {

            if (_domainModel.BNC_ID == 0)
            {
                ap_banco banco = new ap_banco();

                banco.BNC_DESCRICAO = _domainModel.BNC_DESCRICAO.ToUpper();
                banco.BNC_REGUSER = _domainModel.BNC_REGUSER;
                banco.BNC_REGDATE = DateTime.Now;
                banco.BNC_STATUS = "A";

                _bancoRepository.Insert(banco);
            }
            else
            {
                ap_banco banco = _bancoRepository.SingleOrDefault(x => x.BNC_STATUS == "A" && x.BNC_ID == _domainModel.BNC_ID);

                if (banco != null)
                {
                    banco.BNC_DESCRICAO = _domainModel.BNC_DESCRICAO.ToUpper();
                    banco.BNC_REGDATE = DateTime.Now;
                    banco.BNC_REGUSER = _domainModel.BNC_REGUSER;
                    _bancoRepository.Update(banco);
                }
            }
            return _domainModel;
        }

        public bool DeleteBanco(BancoDomainModel _domainModel)
        {
            bool result = false;
            ap_banco banco = _bancoRepository.SingleOrDefault(x => x.BNC_STATUS == "A" && x.BNC_ID == _domainModel.BNC_ID);

            if (banco != null)
            {
                banco.BNC_REGUSER = _domainModel.BNC_REGUSER;
                banco.BNC_REGDATE = DateTime.Now;
                banco.BNC_STATUS = "I";
                _bancoRepository.Update(banco);

                result = true;
            }

            return result;
        }

        public List<BancoDomainModel> GetBanco()
        {
            return _bancoRepository.GetAll(x => x.BNC_STATUS == "A").
                Select(x => new BancoDomainModel { BNC_DESCRICAO = x.BNC_DESCRICAO, BNC_ID = x.BNC_ID }).ToList();
        }

        public BancoDomainModel GetBancoById(int Id)
        {

            ap_banco _banco = _bancoRepository.SingleOrDefault(x => x.BNC_STATUS == "A" && x.BNC_ID == Id);

            BancoDomainModel _domainModel = new BancoDomainModel();
            _domainModel.BNC_DESCRICAO = _banco.BNC_DESCRICAO;
            _domainModel.BNC_ID = _banco.BNC_ID;

            return _domainModel;
        }

        public List<BancoDomainModel> GetBancoByName(string banco)
        {
            return _bancoRepository.GetAll(x => x.BNC_STATUS == "A" && x.BNC_DESCRICAO.Contains(banco)).Select(x => new BancoDomainModel { BNC_DESCRICAO = x.BNC_DESCRICAO, BNC_ID = x.BNC_ID }).ToList();
        }

        public List<BancoDomainModel> GetBancoRecords(int pageStart, int pageSize)
        {

            return _bancoRepository.GetPagedRecords(x => x.BNC_STATUS == "A", x => x.BNC_DESCRICAO, pageStart, pageSize).Select(x => new BancoDomainModel { BNC_DESCRICAO = x.BNC_DESCRICAO, BNC_ID = x.BNC_ID }).ToList();
        }

        public IEnumerable<ValidationResult> IsValid(BancoDomainModel banco)
        {
            throw new NotImplementedException();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _bancoRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistros()
        {
            return _bancoRepository.Count(x => x.BNC_STATUS == "A");
        }

    }
}