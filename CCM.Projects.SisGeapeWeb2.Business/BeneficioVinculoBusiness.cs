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
    public class BeneficioVinculoBusiness : IBeneficioVinculoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeneficioVinculoRepository _beneficioVinculoRepository;

        private readonly BeneficioRepository _beneficioRepository;

        public BeneficioVinculoBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _beneficioVinculoRepository = new BeneficioVinculoRepository(_unitOfWork);
            _beneficioRepository = new BeneficioRepository(_unitOfWork);
        }

        public bool AddUpdateBeneficioVinculo(BeneficioVinculoDomainModel _domainModel)
        {
            ap_beneficioxvinculo beneficioVinculo;
            if (_domainModel.BNFVNC_ID == 0)
            {
                beneficioVinculo = new ap_beneficioxvinculo();


                beneficioVinculo.BNFVNC_NUMPORTARIA = _domainModel.BNFVNC_NUMPORTARIA;
                beneficioVinculo.BNFVNC_DATAPORTARIA = _domainModel.BNFVNC_DATAPORTARIA;
                beneficioVinculo.BNFVNC_DATAFIM = _domainModel.BNFVNC_DATAFIM;
                beneficioVinculo.BNFVNC_DATAINICIO = _domainModel.BNFVNC_DATAINICIO;
                beneficioVinculo.BNFVNC_REGDATE = DateTime.Now;
                beneficioVinculo.BNFVNC_REGUSER = _domainModel.BNFVNC_REGUSER;
                beneficioVinculo.BNFVNC_STATUS = "A";
                beneficioVinculo.BNF_ID = _domainModel.BNF_ID;
                beneficioVinculo.VNC_ID = _domainModel.VNC_ID;


                _beneficioVinculoRepository.Insert(beneficioVinculo);
                return true;
            }
            else
            {
                beneficioVinculo = _beneficioVinculoRepository.SingleOrDefault(x => x.BNFVNC_STATUS == "A" && x.BNFVNC_ID == _domainModel.BNFVNC_ID);

                if (beneficioVinculo != null)
                {
                    beneficioVinculo.BNFVNC_NUMPORTARIA = _domainModel.BNFVNC_NUMPORTARIA;
                    beneficioVinculo.BNFVNC_DATAPORTARIA = _domainModel.BNFVNC_DATAPORTARIA;
                    beneficioVinculo.BNFVNC_DATAFIM = _domainModel.BNFVNC_DATAFIM;
                    beneficioVinculo.BNFVNC_DATAINICIO = _domainModel.BNFVNC_DATAINICIO;
                    beneficioVinculo.BNFVNC_REGDATE = DateTime.Now;
                    beneficioVinculo.BNFVNC_REGUSER = _domainModel.BNFVNC_REGUSER;
                    beneficioVinculo.BNF_ID = _domainModel.BNF_ID;
                    beneficioVinculo.VNC_ID = _domainModel.VNC_ID;

                    _beneficioVinculoRepository.Update(beneficioVinculo);
                    return true;

                }
                return false;
            }
        }

        public bool DeleteBeneficioVinculo(BeneficioVinculoDomainModel _domainModel)
        {
            ap_beneficioxvinculo beneficioVinculo = _beneficioVinculoRepository.SingleOrDefault(x => x.BNFVNC_STATUS == "A" && x.BNFVNC_ID == _domainModel.BNFVNC_ID);

            if (beneficioVinculo != null)
            {
                beneficioVinculo.BNFVNC_STATUS = "I";
                beneficioVinculo.BNFVNC_REGDATE = DateTime.Now;
                beneficioVinculo.BNFVNC_REGUSER = _domainModel.BNFVNC_REGUSER;


                _beneficioVinculoRepository.Update(beneficioVinculo);
                return true;

            }
            return false;
        }

        public BeneficioVinculoDomainModel GetBeneficioVinculoById(int id)
        {
            ap_beneficioxvinculo beneficioVinculo = _beneficioVinculoRepository.SingleOrDefault(x => x.BNFVNC_STATUS == "A" && x.BNFVNC_ID == id);

            BeneficioVinculoDomainModel _domainModel = new BeneficioVinculoDomainModel
            {
                BNFVNC_DATAFIM = beneficioVinculo.BNFVNC_DATAFIM,
                BNFVNC_DATAINICIO = beneficioVinculo.BNFVNC_DATAINICIO,
                BNFVNC_DATAPORTARIA = beneficioVinculo.BNFVNC_DATAPORTARIA,
                BNFVNC_NUMPORTARIA = beneficioVinculo.BNFVNC_NUMPORTARIA,
                BNFVNC_ID = beneficioVinculo.BNFVNC_ID,
                BNFVNC_REGUSER = beneficioVinculo.BNFVNC_REGUSER,
                BNF_ID = beneficioVinculo.BNF_ID.Value,
                BNF_NOME = beneficioVinculo.ap_beneficio.BNF_DESCRICAO,
                FUN_ID = beneficioVinculo.ap_vinculo.FUN_ID.Value,
                VNC_ID = beneficioVinculo.VNC_ID.Value,
                VNC_NOME = beneficioVinculo.VNC_ID + " - " + beneficioVinculo.ap_vinculo.ap_cargo.CRG_NOME + " - " + beneficioVinculo.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,

            };
            return _domainModel;
        }

        public List<CountBeneficioVinculoDomainModel> GetcountByBeneficio()
        {
            return _beneficioRepository.GetAll(x => x.BNF_STATUS == "A" && x.ap_beneficioxvinculo.Any(p => p.BNFVNC_STATUS == "A" && p.BNFVNC_DATAFIM == null)).ToList().Select(x => new CountBeneficioVinculoDomainModel
            {
                BENEFICIO = x.BNF_DESCRICAO,
                BNF_ID = x.BNF_ID,
                QUANTIDADE = x.ap_beneficioxvinculo.Where(z => z.BNFVNC_STATUS == "A" && z.BNFVNC_DATAFIM == null && z.BNF_ID == x.BNF_ID).Count()

            }).ToList();
        }

        public List<BeneficioVinculoDomainModel> GetFuncaoVinculoByFuncionario(int func)
        {
            var listDomain = _beneficioVinculoRepository.GetAll(x => x.BNFVNC_STATUS == "A" && x.ap_vinculo.FUN_ID == func).ToList();
            return listDomain.Select(x => new BeneficioVinculoDomainModel
            {
                BNFVNC_DATAFIM = x.BNFVNC_DATAFIM,
                BNFVNC_DATAINICIO = x.BNFVNC_DATAINICIO,
                BNFVNC_DATAPORTARIA = x.BNFVNC_DATAPORTARIA,
                BNFVNC_NUMPORTARIA = x.BNFVNC_NUMPORTARIA,
                BNFVNC_ID = x.BNFVNC_ID,
                BNF_ID = x.BNF_ID.Value,
                BNF_NOME = x.ap_beneficio.BNF_DESCRICAO,
                VNC_ID = x.VNC_ID.Value,
                FUN_ID = x.ap_vinculo.FUN_ID.Value,
                VNC_NOME = x.VNC_ID + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,

            }).OrderBy(x => x.BNFVNC_DATAINICIO).ToList();
        }

        public List<BeneficioVinculoDomainModel> GetFuncaoVinculoByVinculo(int vnc)
        {
            var listDomain = _beneficioVinculoRepository.GetAll(x => x.BNFVNC_STATUS == "A" && x.VNC_ID == vnc).ToList();
            return listDomain.Select(x => new BeneficioVinculoDomainModel
            {
                BNFVNC_DATAFIM = x.BNFVNC_DATAFIM,
                BNFVNC_DATAINICIO = x.BNFVNC_DATAINICIO,
                BNFVNC_DATAPORTARIA = x.BNFVNC_DATAPORTARIA,
                BNFVNC_NUMPORTARIA = x.BNFVNC_NUMPORTARIA,
                BNFVNC_ID = x.BNFVNC_ID,
                BNF_ID = x.BNF_ID.Value,
                BNF_NOME = x.ap_beneficio.BNF_DESCRICAO,
                VNC_ID = x.VNC_ID.Value,
                FUN_ID = x.ap_vinculo.FUN_ID.Value,
                VNC_NOME = x.VNC_ID + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,

            }).OrderBy(x => x.BNFVNC_DATAINICIO).ToList();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _beneficioVinculoRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistro()
        {
            return _beneficioVinculoRepository.Count(x => x.BNFVNC_STATUS == "A");
        }

        public int TotalRegistroByFuncionario(int id)
        {
            return _beneficioVinculoRepository.Count(x => x.BNFVNC_STATUS == "A" && x.ap_vinculo.FUN_ID == id);
        }
    }
}
