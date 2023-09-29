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
    public class FuncaoVinculoBusiness : IFuncaoVinculoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FuncaoVinculoRepository _funcaoVinculoRepository;

        public FuncaoVinculoBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _funcaoVinculoRepository = new FuncaoVinculoRepository(_unitOfWork);
        }

        public bool AddUpdateFuncaoVinculo(FuncaoVinculoDomainModel _domainModel)
        {
            ap_funcaoxvinculo funcaoxvinculo = new ap_funcaoxvinculo();
            try
            {
                if (_domainModel.FNCVNC_ID == 0)
                {

                    funcaoxvinculo.FNCVNC_DATAFIM = _domainModel.FNCVNC_DATAFIM;
                    funcaoxvinculo.FNCVNC_DATAINICIO = _domainModel.FNCVNC_DATAINICIO;
                    funcaoxvinculo.FNCVNC_DATAPORTARIA = _domainModel.FNCVNC_DATAPORTARIA;
                    funcaoxvinculo.FNCVNC_NUMPORTARIA = _domainModel.FNCVNC_NUMPORTARIA;
                    funcaoxvinculo.VNC_ID = _domainModel.VNC_ID;
                    funcaoxvinculo.FNC_ID = _domainModel.FNC_ID;
                    funcaoxvinculo.FNCVNC_STATUS = "A";
                    funcaoxvinculo.FNCVNC_REGUSER = _domainModel.FNCVNC_REGUSER;
                    funcaoxvinculo.FNCVNC_REGDATE = DateTime.Now;


                    _funcaoVinculoRepository.Insert(funcaoxvinculo);
                    return true;
                }
                else
                {
                    funcaoxvinculo = _funcaoVinculoRepository.SingleOrDefault(x => x.FNCVNC_STATUS == "A" && x.FNCVNC_ID == _domainModel.FNCVNC_ID);


                    funcaoxvinculo.FNCVNC_DATAFIM = _domainModel.FNCVNC_DATAFIM;
                    funcaoxvinculo.FNCVNC_DATAINICIO = _domainModel.FNCVNC_DATAINICIO;
                    funcaoxvinculo.FNCVNC_DATAPORTARIA = _domainModel.FNCVNC_DATAPORTARIA;
                    funcaoxvinculo.FNCVNC_NUMPORTARIA = _domainModel.FNCVNC_NUMPORTARIA;
                    funcaoxvinculo.VNC_ID = _domainModel.VNC_ID;
                    funcaoxvinculo.FNC_ID = _domainModel.FNC_ID;
                    funcaoxvinculo.FNCVNC_REGUSER = _domainModel.FNCVNC_REGUSER;
                    funcaoxvinculo.FNCVNC_REGDATE = DateTime.Now;


                    _funcaoVinculoRepository.Update(funcaoxvinculo);
                    return true;


                }
            }
            catch { return false; }
        }

        public bool DeleteFuncaoVinculo(FuncaoVinculoDomainModel _domainModel)
        {
            ap_funcaoxvinculo funcaoxvinculo = _funcaoVinculoRepository.SingleOrDefault(x => x.FNCVNC_STATUS == "A" && x.FNCVNC_ID == _domainModel.FNCVNC_ID);

            if (funcaoxvinculo != null)
            {

                funcaoxvinculo.FNCVNC_REGUSER = _domainModel.FNCVNC_REGUSER;
                funcaoxvinculo.FNCVNC_REGDATE = DateTime.Now;
                funcaoxvinculo.FNCVNC_STATUS = "I";

                _funcaoVinculoRepository.Update(funcaoxvinculo);
                return true;
            }

            return false;

        }

        public List<FuncaoVinculoDomainModel> GetFuncaoVinculoByFuncionario(int func)
        {
            var listDomain = _funcaoVinculoRepository.GetAll(x => x.FNCVNC_STATUS == "A" && x.ap_vinculo.FUN_ID == func).ToList();
            return listDomain.Select(x => new FuncaoVinculoDomainModel
            {

                FNCVNC_DATAFIM = x.FNCVNC_DATAFIM,
                FNCVNC_DATAINICIO = x.FNCVNC_DATAINICIO,
                FNCVNC_DATAPORTARIA = x.FNCVNC_DATAPORTARIA,
                FNCVNC_ID = x.FNCVNC_ID,
                FNCVNC_NUMPORTARIA = x.FNCVNC_NUMPORTARIA,
                FNCVNC_REGUSER = x.FNCVNC_REGUSER,
                FNC_ID = x.FNC_ID.Value,
                VNC_ID = x.VNC_ID.Value,
                FUN_ID = x.ap_vinculo.FUN_ID.Value,
                FNC_NOME = x.ap_funcao.FNC_NOME,
                VNC_NOME = x.VNC_ID + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,

            }).OrderBy(x => x.FNCVNC_DATAINICIO).ToList();
        }

        public FuncaoVinculoDomainModel GetFuncaoVinculoById(int id)
        {
            ap_funcaoxvinculo funcaoxvinculo = _funcaoVinculoRepository.SingleOrDefault(x => x.FNCVNC_STATUS == "A" && x.FNCVNC_ID == id);


            FuncaoVinculoDomainModel funcaoVinculoDomain = new FuncaoVinculoDomainModel
            {
                FNCVNC_DATAFIM = funcaoxvinculo.FNCVNC_DATAFIM,
                FNCVNC_DATAINICIO = funcaoxvinculo.FNCVNC_DATAINICIO,
                FNCVNC_DATAPORTARIA = funcaoxvinculo.FNCVNC_DATAPORTARIA,
                FNCVNC_ID = funcaoxvinculo.FNCVNC_ID,
                FNCVNC_NUMPORTARIA = funcaoxvinculo.FNCVNC_NUMPORTARIA,
                FNCVNC_REGUSER = funcaoxvinculo.FNCVNC_REGUSER,
                FNC_ID = funcaoxvinculo.FNC_ID.Value,
                VNC_ID = funcaoxvinculo.VNC_ID.Value,
                FUN_ID = funcaoxvinculo.ap_vinculo.FUN_ID.Value,
                FNC_NOME = funcaoxvinculo.ap_funcao.FNC_NOME,
                VNC_NOME = funcaoxvinculo.VNC_ID + " - " + funcaoxvinculo.ap_vinculo.ap_cargo.CRG_NOME + " - " + funcaoxvinculo.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,

            };



            return funcaoVinculoDomain;

        }

        public List<FuncaoVinculoDomainModel> GetFuncaoVinculoByVinculo(int vnc)
        {
            return _funcaoVinculoRepository.GetAll(x => x.FNCVNC_STATUS == "A" && x.VNC_ID == vnc)
                           .Select(x => new FuncaoVinculoDomainModel
                           {
                               FNCVNC_DATAFIM = x.FNCVNC_DATAFIM,
                               FNCVNC_DATAINICIO = x.FNCVNC_DATAINICIO,
                               FNCVNC_DATAPORTARIA = x.FNCVNC_DATAPORTARIA,
                               FNCVNC_ID = x.FNCVNC_ID,
                               FNCVNC_NUMPORTARIA = x.FNCVNC_NUMPORTARIA,
                               FNCVNC_REGUSER = x.FNCVNC_REGUSER,
                               FNC_ID = x.FNC_ID.Value,
                               VNC_ID = x.VNC_ID.Value,
                               FUN_ID = x.ap_vinculo.FUN_ID.Value,
                               FNC_NOME = x.ap_funcao.FNC_NOME,
                               VNC_NOME = x.VNC_ID + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,

                           }).ToList();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _funcaoVinculoRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistro()
        {
            return _funcaoVinculoRepository.Count(x => x.FNCVNC_STATUS == "A");
        }

        public int TotalRegistroByFuncionario(int id)
        {
            return _funcaoVinculoRepository.Count(x => x.FNCVNC_STATUS == "A" && x.FNCVNC_ID == id);
        }
    }
}
