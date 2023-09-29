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
    public class CargoBusiness : ICargoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CargoRepository _cargoRepository;

        public CargoBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _cargoRepository = new CargoRepository(_unitOfWork);
        }
        public void AddUpdateCargo(CargoDomainModel cargo)
        {
            if (cargo.CRG_ID == 0)
            {


                ap_cargo _cargo = new ap_cargo();
                _cargo.CRG_CODIGO = cargo.CRG_CODIGO;
                _cargo.CRG_NOME = cargo.CRG_NOME.ToUpper();
                _cargo.CRG_DESCRICAO = cargo.CRG_DESCRICAO;
                _cargo.CRG_ESCOLARIDADE = cargo.CRG_ESCOLARIDADE;
                _cargo.CRG_REFCARGO = cargo.CRG_REFCARGO;
                _cargo.CRG_REFSALARIAL = cargo.CRG_REFSALARIAL;
                _cargo.CRG_RISCOS = cargo.CRG_RISCOS;
                _cargo.CRG_BASESALARIAL = cargo.CRG_BASESALARIAL;
                _cargo.CRG_REGDATE = DateTime.Now;
                _cargo.CRG_REGUSER = cargo.CRG_REGUSER;
                _cargo.CRG_STATUS = "A";

                _cargoRepository.Insert(_cargo);
            }
            else
            {
                ap_cargo _cargo = _cargoRepository.SingleOrDefault(x => x.CRG_STATUS == "A" && x.CRG_ID == cargo.CRG_ID);

                if (_cargo != null)
                {
                    _cargo.CRG_NOME = cargo.CRG_NOME.ToUpper();
                    _cargo.CRG_BASESALARIAL = cargo.CRG_BASESALARIAL;
                    _cargo.CRG_CODIGO = cargo.CRG_CODIGO;
                    _cargo.CRG_ESCOLARIDADE = cargo.CRG_ESCOLARIDADE;
                    _cargo.CRG_REFCARGO = cargo.CRG_REFCARGO;
                    _cargo.CRG_REFSALARIAL = cargo.CRG_REFSALARIAL;
                    _cargo.CRG_RISCOS = cargo.CRG_RISCOS;
                    if (cargo.CRG_DESCRICAO != null)
                    { _cargo.CRG_DESCRICAO = cargo.CRG_DESCRICAO.ToUpper(); }
                    else
                    { _cargo.CRG_DESCRICAO = cargo.CRG_DESCRICAO; }
                    _cargo.CRG_REGDATE = DateTime.Now;
                    cargo.CRG_REGUSER = cargo.CRG_REGUSER;
                    _cargo.CRG_STATUS = "A";


                    _cargoRepository.Update(_cargo);
                }

            }
        }

        public List<CargoDomainModel> DdlCargo()
        {
            return _cargoRepository.GetAll(x => x.CRG_STATUS == "A").Select(x => new CargoDomainModel
            {
                CRG_ID = x.CRG_ID,
                CRG_CODIGO = x.CRG_CODIGO,
                CRG_NOME = x.CRG_NOME.ToUpper() + " - " + x.CRG_REFSALARIAL,
                CRG_DESCRICAO = x.CRG_DESCRICAO,
                CRG_ESCOLARIDADE = x.CRG_ESCOLARIDADE.Value,
                CRG_REFCARGO = x.CRG_REFCARGO,
                CRG_REFSALARIAL = x.CRG_REFSALARIAL,
                CRG_RISCOS = x.CRG_RISCOS,
                CRG_BASESALARIAL = x.CRG_BASESALARIAL.Value,
                CRG_REGUSER = x.CRG_REGUSER,
            }).ToList();
        }

        public bool DeleteBanco(CargoDomainModel cargo)
        {
            bool result = false;
            ap_cargo _cargo = _cargoRepository.SingleOrDefault(x => x.CRG_STATUS == "A" && x.CRG_ID == cargo.CRG_ID);

            if (_cargo != null)
            {
                _cargo.CRG_REGUSER = cargo.CRG_REGUSER;
                _cargo.CRG_REGDATE = DateTime.Now;
                _cargo.CRG_STATUS = "I";
                _cargoRepository.Update(_cargo);
                result = true;
            }

            return result;
        }

        public List<CargoDomainModel> GetCargo()
        {
            return _cargoRepository.GetAll(x => x.CRG_STATUS == "A").Select(x => new CargoDomainModel
            {
                CRG_ID = x.CRG_ID,
                CRG_CODIGO = x.CRG_CODIGO,
                CRG_NOME = x.CRG_NOME.ToUpper(),
                CRG_DESCRICAO = x.CRG_DESCRICAO,
                CRG_ESCOLARIDADE = x.CRG_ESCOLARIDADE.Value,
                CRG_REFCARGO = x.CRG_REFCARGO,
                CRG_REFSALARIAL = x.CRG_REFSALARIAL,
                CRG_RISCOS = x.CRG_RISCOS,
                CRG_BASESALARIAL = x.CRG_BASESALARIAL.Value,
                CRG_REGUSER = x.CRG_REGUSER,
            }
            ).ToList();
        }

        public CargoDomainModel GetCargoById(int Id)
        {
            ap_cargo cargo = _cargoRepository.SingleOrDefault(x => x.CRG_STATUS == "A" && x.CRG_ID == Id);

            CargoDomainModel _domainModel = new CargoDomainModel()
            {
                CRG_ID = cargo.CRG_ID,
                CRG_CODIGO = cargo.CRG_CODIGO,
                CRG_NOME = cargo.CRG_NOME.ToUpper(),
                CRG_DESCRICAO = cargo.CRG_DESCRICAO,
                CRG_ESCOLARIDADE = cargo.CRG_ESCOLARIDADE.Value,
                CRG_REFCARGO = cargo.CRG_REFCARGO,
                CRG_REFSALARIAL = cargo.CRG_REFSALARIAL,
                CRG_RISCOS = cargo.CRG_RISCOS,
                CRG_BASESALARIAL = cargo.CRG_BASESALARIAL.Value,
                CRG_REGUSER = cargo.CRG_REGUSER,
            };

            return _domainModel;
        }

        public List<CargoDomainModel> GetCargoByName(string name)
        {
            return _cargoRepository.GetAll(x => x.CRG_STATUS == "A" && x.CRG_NOME.Contains(name))
                .Select(x => new CargoDomainModel
                {
                    CRG_ID = x.CRG_ID,
                    CRG_CODIGO = x.CRG_CODIGO,
                    CRG_NOME = x.CRG_NOME.ToUpper(),
                    CRG_DESCRICAO = x.CRG_DESCRICAO,
                    CRG_ESCOLARIDADE = x.CRG_ESCOLARIDADE.Value,
                    CRG_REFCARGO = x.CRG_REFCARGO,
                    CRG_REFSALARIAL = x.CRG_REFSALARIAL,
                    CRG_RISCOS = x.CRG_RISCOS,
                    CRG_BASESALARIAL = x.CRG_BASESALARIAL.Value,
                    CRG_REGUSER = x.CRG_REGUSER,
                }).ToList();

        }

        public List<CargoDomainModel> GetCargoRecords(string name, int pageStart, int pageSize)
        {
            List<ap_cargo> listDomain = new List<ap_cargo>();


            if (!String.IsNullOrWhiteSpace(name))
                listDomain = _cargoRepository.GetPagedRecords(x => x.CRG_STATUS == "A", x => x.CRG_NOME, pageStart, pageSize).ToList();

            else
                name = name.ToUpper();
            listDomain = _cargoRepository.GetPagedRecords(x => x.CRG_STATUS == "A" && x.CRG_NOME.ToUpper().Contains(name) || x.CRG_REFSALARIAL.ToUpper().Contains(name), x => x.CRG_NOME, pageStart, pageSize).ToList();


            return listDomain.Select(x => new CargoDomainModel
            {
                CRG_ID = x.CRG_ID,
                CRG_CODIGO = x.CRG_CODIGO,
                CRG_NOME = x.CRG_NOME.ToUpper(),
                CRG_DESCRICAO = x.CRG_DESCRICAO,
                CRG_ESCOLARIDADE = x.CRG_ESCOLARIDADE.Value,
                CRG_REFCARGO = x.CRG_REFCARGO,
                CRG_REFSALARIAL = x.CRG_REFSALARIAL,
                CRG_RISCOS = x.CRG_RISCOS,
                CRG_BASESALARIAL = x.CRG_BASESALARIAL.Value,
                CRG_REGUSER = x.CRG_REGUSER,
            }).ToList();
        }

        public IEnumerable<ValidationResult> IsValid(CargoDomainModel cargo)
        {
            throw new NotImplementedException();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _cargoRepository.Save();
                return result;
            }
            catch (Exception ex)
            {

                var mensagem = ex.Message;
                return !result;
            }
        }

        public int TotalRegistros()
        {
            return _cargoRepository.Count(x => x.CRG_STATUS == "A");
        }
    }
}
