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
    public class FuncionarioDependenteBusiness : IFuncionarioDependenteBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FuncionarioxDepententeRepository _dependenteRepository;

        public FuncionarioDependenteBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dependenteRepository = new FuncionarioxDepententeRepository(_unitOfWork);
        }

        public bool AddUpdateDependente(FuncionarioDependenteDomainModel _domainModel)
        {
            try
            {

                ap_funcionarioxdependente _dependente = new ap_funcionarioxdependente();

                if (_domainModel.FUNDEP_ID == 0)
                {

                    _dependente.FUNDEP_CPF = _domainModel.FUNDEP_CPF;
                    _dependente.FUNDEP_DATANASCIMENTO = _domainModel.FUNDEP_DATANASCIMENTO;
                    _dependente.FUNDEP_NOME = _domainModel.FUNDEP_NOME;
                    _dependente.FUNDEP_TIPO = _domainModel.FUNDEP_TIPO;
                    _dependente.FUN_ID = _domainModel.FUN_ID;
                    _dependente.FUNDEP_REGUSER = _domainModel.FUNDEP_REGUSER;
                    _dependente.FUNDEP_REGDATE = DateTime.Now;
                    _dependente.FUNDEP_STATUS = "A";

                    _dependenteRepository.Insert(_dependente);
                    return true;

                }
                else
                {
                    _dependente = _dependenteRepository.SingleOrDefault(x => x.FUNDEP_STATUS == "A" && x.FUNDEP_ID == _domainModel.FUNDEP_ID);

                    if (_dependente != null)
                    {
                        _dependente.FUNDEP_CPF = _domainModel.FUNDEP_CPF;
                        _dependente.FUNDEP_DATANASCIMENTO = _domainModel.FUNDEP_DATANASCIMENTO;
                        _dependente.FUNDEP_NOME = _domainModel.FUNDEP_NOME;
                        _dependente.FUNDEP_TIPO = _domainModel.FUNDEP_TIPO;
                        _dependente.FUN_ID = _domainModel.FUN_ID;
                        _dependente.FUNDEP_REGUSER = _domainModel.FUNDEP_REGUSER;
                        _dependente.FUNDEP_REGDATE = DateTime.Now;
                        _dependente.FUNDEP_STATUS = "A";

                        _dependenteRepository.Update(_dependente);
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        public bool DeleteDependente(FuncionarioDependenteDomainModel _domainModel)
        {
            ap_funcionarioxdependente _dependente = _dependenteRepository.SingleOrDefault(x => x.FUNDEP_STATUS == "A" && x.FUNDEP_ID == _domainModel.FUNDEP_ID);

            if (_dependente != null)
            {

                _dependente.FUNDEP_REGUSER = _domainModel.FUNDEP_REGUSER;
                _dependente.FUNDEP_REGDATE = DateTime.Now;
                _dependente.FUNDEP_STATUS = "I";

                _dependenteRepository.Update(_dependente);
                return true;
            }
            return false;
        }

        public List<FuncionarioDependenteDomainModel> GetAll()
        {

            List<ap_funcionarioxdependente> _listDomain = _dependenteRepository.GetAll(x => x.FUNDEP_STATUS == "A").ToList();

            var listDomain = _listDomain.Select(x => new FuncionarioDependenteDomainModel
            {
                FUNDEP_CPF = x.FUNDEP_CPF,
                FUNDEP_DATANASCIMENTO = x.FUNDEP_DATANASCIMENTO,
                FUNDEP_ID = x.FUNDEP_ID,
                FUNDEP_NOME = x.FUNDEP_NOME,
                FUNDEP_TIPO = x.FUNDEP_TIPO.Value,
                FUNDEP_REGUSER = x.FUNDEP_REGUSER,
                FUN_ID = x.FUN_ID.Value
            }).ToList();

            return listDomain;
        }

        public List<FuncionarioDependenteDomainModel> GetDependenteByFuncionario(int func)
        {
            List<ap_funcionarioxdependente> _listDomain = _dependenteRepository.GetAll(x => x.FUNDEP_STATUS == "A" && x.FUN_ID == func).ToList();

            var listDomain = _listDomain.Select(x => new FuncionarioDependenteDomainModel
            {
                FUNDEP_CPF = x.FUNDEP_CPF,
                FUNDEP_DATANASCIMENTO = x.FUNDEP_DATANASCIMENTO,
                FUNDEP_ID = x.FUNDEP_ID,
                FUNDEP_NOME = x.FUNDEP_NOME,
                FUNDEP_TIPO = x.FUNDEP_TIPO.Value,
                FUNDEP_REGUSER = x.FUNDEP_REGUSER,
                FUN_ID = x.FUN_ID.Value
            }).ToList();

            return listDomain;
        }

        public FuncionarioDependenteDomainModel GetDependentebyId(int Id)
        {
            ap_funcionarioxdependente _dependente = _dependenteRepository.SingleOrDefault(x => x.FUNDEP_STATUS == "A" && x.FUNDEP_ID == Id);

            FuncionarioDependenteDomainModel _domainModel = new FuncionarioDependenteDomainModel()
            {
                FUNDEP_CPF = _dependente.FUNDEP_CPF,
                FUNDEP_DATANASCIMENTO = _dependente.FUNDEP_DATANASCIMENTO,
                FUN_ID = _dependente.FUN_ID.Value,
                FUNDEP_ID = _dependente.FUNDEP_ID,
                FUNDEP_NOME = _dependente.FUNDEP_NOME,
                FUNDEP_REGUSER = _dependente.FUNDEP_REGUSER,
                FUNDEP_TIPO = _dependente.FUNDEP_TIPO.Value,

            };


            return _domainModel;
        }

        public List<FuncionarioDependenteDomainModel> GetPageRecords(int pageStart, int pageSize)
        {
            throw new NotImplementedException();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _dependenteRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalCount()
        {
            return _dependenteRepository.Count(x => x.FUNDEP_STATUS == "A");
        }

        public int TotalCountByFuncionario(int id)
        {
            return _dependenteRepository.Count(x => x.FUNDEP_STATUS == "A" && x.FUN_ID == id);
        }
    }
}
