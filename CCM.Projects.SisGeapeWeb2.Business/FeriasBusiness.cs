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
    public class FeriasBusiness : IFeriasBusiness
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly FeriasRepository _feriasRepository;
        private IValidationDictionary _validationDictionary;

        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        public FeriasBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _feriasRepository = new FeriasRepository(_unitOfWork);

        }

        public bool AddUpdateFerias(FeriasDomainModel _domainModel)
        {
            ap_ferias ferias = new ap_ferias();
            try
            {
                if (_domainModel.FRS_ID == 0)
                {
                    ferias.VNC_ID = _domainModel.VNC_ID;
                    ferias.FRS_DATAPORTARIA = _domainModel.FRS_DATAPORTARIA;
                    ferias.FRS_DATA_FIMAQUISITIVO = _domainModel.FRS_DATA_FIMAQUISITIVO;
                    ferias.FRS_DATA_INICIOAQUISITIVO = _domainModel.FRS_DATA_INICIOAQUISITIVO;
                    ferias.FRS_DATA_INICIOGOZO = _domainModel.FRS_DATA_INICIOGOZO;

                    ferias.FRS_DATA_RETORNO = _domainModel.FRS_DATA_RETORNO;
                    ferias.FRS_ID = _domainModel.FRS_ID;
                    ferias.FRS_DATA_FIMGOZO = _domainModel.FRS_DATA_FIMGOZO;
                    ferias.FRS_NUMPORTARIA = _domainModel.FRS_NUMPORTARIA;
                    ferias.FRS_OBSERVACAO = _domainModel.FRS_OBSERVACAO;
                    ferias.FRS_REGDATE = DateTime.Now;
                    ferias.FRS_REGUSER = _domainModel.FRS_REGUSER;
                    ferias.FRS_STATUS = "A";

                    _feriasRepository.Insert(ferias);
                    return true;
                }
                else
                {
                    ferias = _feriasRepository.SingleOrDefault(x => x.FRS_STATUS == "A" && x.FRS_ID == _domainModel.FRS_ID);

                    ferias.VNC_ID = _domainModel.VNC_ID;
                    ferias.FRS_DATAPORTARIA = _domainModel.FRS_DATAPORTARIA;
                    ferias.FRS_DATA_FIMAQUISITIVO = _domainModel.FRS_DATA_FIMAQUISITIVO;
                    ferias.FRS_DATA_INICIOAQUISITIVO = _domainModel.FRS_DATA_INICIOAQUISITIVO;
                    ferias.FRS_DATA_INICIOGOZO = _domainModel.FRS_DATA_INICIOGOZO;
                    ferias.FRS_DATA_RETORNO = _domainModel.FRS_DATA_RETORNO;
                    ferias.FRS_DATA_FIMGOZO = _domainModel.FRS_DATA_FIMGOZO;
                    ferias.FRS_ID = _domainModel.FRS_ID;
                    ferias.FRS_NUMPORTARIA = _domainModel.FRS_NUMPORTARIA;
                    ferias.FRS_OBSERVACAO = _domainModel.FRS_OBSERVACAO;
                    ferias.FRS_REGDATE = DateTime.Now;
                    ferias.FRS_REGUSER = _domainModel.FRS_REGUSER;

                    _feriasRepository.Update(ferias);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFerias(FeriasDomainModel _domainModel)
        {
            ap_ferias ferias = _feriasRepository.SingleOrDefault(x => x.FRS_STATUS == "A" && x.FRS_ID == _domainModel.FRS_ID);

            if (ferias != null)
            {
                ferias.FRS_REGDATE = DateTime.Now;
                ferias.FRS_REGUSER = _domainModel.FRS_REGUSER;
                ferias.FRS_STATUS = "I";

                _feriasRepository.Update(ferias);
                return true;
            }
            return false;
        }

        public List<FeriasDomainModel> GetFeriasByFuncionario(int func)
        {

            var listDomain = _feriasRepository.GetAll(x => x.FRS_STATUS == "A" && x.ap_vinculo.FUN_ID == func).ToList();

            return listDomain.Select(x => new FeriasDomainModel
            {
                FRS_DATAPORTARIA = x.FRS_DATAPORTARIA,
                FRS_DATA_FIMAQUISITIVO = x.FRS_DATA_FIMAQUISITIVO,
                FRS_DATA_INICIOAQUISITIVO = x.FRS_DATA_INICIOAQUISITIVO,
                FRS_DATA_INICIOGOZO = x.FRS_DATA_INICIOGOZO,
                FRS_DATA_RETORNO = x.FRS_DATA_RETORNO,
                FRS_DATA_FIMGOZO = x.FRS_DATA_FIMGOZO,
                FRS_ID = x.FRS_ID,
                FRS_NUMPORTARIA = x.FRS_NUMPORTARIA,
                FRS_OBSERVACAO = x.FRS_OBSERVACAO,
                FRS_REGUSER = x.FRS_REGUSER,
                FUN_ID = x.ap_vinculo.FUN_ID.Value,
                VNC_ID = x.VNC_ID.Value,
                VNC_NOME = x.ap_vinculo.VNC_ID.ToString() + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                FRS_PERIODOAQUISITIVO = x.FRS_DATA_INICIOAQUISITIVO.Value.ToShortDateString() + " - " + x.FRS_DATA_FIMAQUISITIVO.Value.ToShortDateString()
            }).ToList();
        }

        public FeriasDomainModel GetFeriasById(int id)
        {
            ap_ferias ferias = _feriasRepository.SingleOrDefault(x => x.FRS_STATUS == "A" && x.FRS_ID == id);

            FeriasDomainModel domainModel = new FeriasDomainModel()
            {
                FUN_ID = ferias.ap_vinculo.FUN_ID.Value,
                VNC_ID = ferias.VNC_ID.Value,
                FRS_DATAPORTARIA = ferias.FRS_DATAPORTARIA,
                FRS_DATA_FIMAQUISITIVO = ferias.FRS_DATA_FIMAQUISITIVO,
                FRS_DATA_INICIOAQUISITIVO = ferias.FRS_DATA_INICIOAQUISITIVO,
                FRS_DATA_INICIOGOZO = ferias.FRS_DATA_INICIOGOZO,
                FRS_DATA_RETORNO = ferias.FRS_DATA_RETORNO,
                FRS_DATA_FIMGOZO = ferias.FRS_DATA_FIMGOZO,
                FRS_ID = ferias.FRS_ID,
                FRS_NUMPORTARIA = ferias.FRS_NUMPORTARIA,
                FRS_OBSERVACAO = ferias.FRS_OBSERVACAO,
                FRS_REGUSER = ferias.FRS_REGUSER,
                VNC_NOME = ferias.ap_vinculo.VNC_ID.ToString() + " - " + ferias.ap_vinculo.ap_cargo.CRG_NOME + " - " + ferias.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                FRS_PERIODOAQUISITIVO = ferias.FRS_DATA_INICIOAQUISITIVO.Value.ToShortDateString() + " - " + ferias.FRS_DATA_FIMAQUISITIVO.Value.ToShortDateString()
            };

            return domainModel;
        }

        public List<FeriasDomainModel> GetFeriasByVinculo(int vnc)
        {
            var listDomain = _feriasRepository.GetAll(x => x.FRS_STATUS == "A" && x.VNC_ID == vnc).ToList();

            return listDomain.Select(x => new FeriasDomainModel
            {
                FRS_DATAPORTARIA = x.FRS_DATAPORTARIA,
                FRS_DATA_FIMAQUISITIVO = x.FRS_DATA_FIMAQUISITIVO,
                FRS_DATA_INICIOAQUISITIVO = x.FRS_DATA_INICIOAQUISITIVO,
                FRS_DATA_INICIOGOZO = x.FRS_DATA_INICIOGOZO,
                FRS_DATA_RETORNO = x.FRS_DATA_RETORNO,
                FRS_ID = x.FRS_ID,
                FRS_DATA_FIMGOZO = x.FRS_DATA_FIMGOZO,
                FRS_NUMPORTARIA = x.FRS_NUMPORTARIA,
                FRS_OBSERVACAO = x.FRS_OBSERVACAO,
                FRS_REGUSER = x.FRS_REGUSER,
                FUN_ID = x.ap_vinculo.FUN_ID.Value,
                VNC_ID = x.VNC_ID.Value,
                VNC_NOME = x.ap_vinculo.VNC_ID.ToString() + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                FRS_PERIODOAQUISITIVO = x.FRS_DATA_INICIOAQUISITIVO.Value.ToShortDateString() + " - " + x.FRS_DATA_FIMAQUISITIVO.Value.ToShortDateString()
            }).ToList();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _feriasRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistro()
        {
            return _feriasRepository.Count(x => x.FRS_STATUS == "A");
        }

        public int TotalRegistroByFuncionario(int id)
        {
            return _feriasRepository.Count(x => x.FRS_STATUS == "A" && x.ap_vinculo.FUN_ID == id);
        }

        public List<ComunicadoFeriasDomainModel> GetDadosComunicado(int id)
        {
            var listDomain = _feriasRepository.GetAll(x => x.FRS_STATUS == "A" && x.FRS_ID == id).ToList().Select(ferias => new ComunicadoFeriasDomainModel
            {
                CRG_NOME = ferias.ap_vinculo.ap_cargo.CRG_NOME,
                FRS_DATA_INICIOGOZO = ferias.FRS_DATA_INICIOGOZO.Value.ToShortDateString(),
                FRS_DATA_RETORNO = ferias.FRS_DATA_RETORNO.Value.ToShortDateString(),
                FRS_ID = ferias.FRS_ID.ToString(),
                FRS_NUMPORTARIA = ferias.FRS_NUMPORTARIA,
                FRS_DATA_FIMGOZO = ferias.FRS_DATA_FIMGOZO.Value.ToShortDateString(),
                FRS_PERIODOAQUISITIVO = ferias.FRS_DATA_INICIOAQUISITIVO.Value.ToShortDateString() + " - " + ferias.FRS_DATA_FIMAQUISITIVO.Value.ToShortDateString(),
                FUN_MATRICULA = ferias.ap_vinculo.ap_funcionario.FUN_MATRICULA,
                FUN_NOME = ferias.ap_vinculo.ap_funcionario.FUN_NOME,
                UND_NOME = (ferias.ap_vinculo.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null).Count() > 0 ? ferias.ap_vinculo.ap_vinculoxunidade.Where(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null).FirstOrDefault().ap_unidade.UND_NOME : "____________________")
            });

            return listDomain.ToList();
        }

        public int TotalServidoresFerias()
        {
            return _feriasRepository.Count(x => x.FRS_STATUS == "A" && x.FRS_DATA_INICIOGOZO <= DateTime.Today && x.FRS_DATA_FIMGOZO >= DateTime.Today);
        }

        public List<FeriasDomainModel> BuscaServidoresEmFerias(DateTime inicio, DateTime fim)
        {
            var listDomain = _feriasRepository.GetAll(x => x.FRS_STATUS == "A" && x.FRS_DATA_INICIOGOZO >= inicio && x.FRS_DATA_INICIOGOZO <= fim).ToList();

            return listDomain.Select(x => new FeriasDomainModel
            {
                FRS_DATAPORTARIA = x.FRS_DATAPORTARIA,
                FRS_DATA_FIMAQUISITIVO = x.FRS_DATA_FIMAQUISITIVO,
                FRS_DATA_INICIOAQUISITIVO = x.FRS_DATA_INICIOAQUISITIVO,
                FRS_DATA_INICIOGOZO = x.FRS_DATA_INICIOGOZO,
                FRS_DATA_RETORNO = x.FRS_DATA_RETORNO,
                FRS_ID = x.FRS_ID,
                FRS_DATA_FIMGOZO = x.FRS_DATA_FIMGOZO,
                FRS_NUMPORTARIA = x.FRS_NUMPORTARIA,
                FRS_OBSERVACAO = x.FRS_OBSERVACAO,
                FRS_REGUSER = x.FRS_REGUSER,
                FUN_ID = x.ap_vinculo.FUN_ID.Value,
                VNC_ID = x.VNC_ID.Value,
                MATRICULA = x.ap_vinculo.ap_funcionario.FUN_MATRICULA,
                VNC_NOME = x.ap_vinculo.VNC_ID.ToString() + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                FRS_PERIODOAQUISITIVO = x.FRS_DATA_INICIOAQUISITIVO.Value.ToShortDateString() + " - " + x.FRS_DATA_FIMAQUISITIVO.Value.ToShortDateString(),
                UNIDADE = x.ap_vinculo.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null) ? x.ap_vinculo.ap_vinculoxunidade.FirstOrDefault(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA : (x.ap_vinculo.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.VNCU_DATAINICIO <= x.FRS_DATA_INICIOGOZO && z.VNCU_DATAINICIO <= fim) ? x.ap_vinculo.ap_vinculoxunidade.FirstOrDefault(z => z.VNCU_STATUS == "A" && z.VNCU_DATAINICIO <= x.FRS_DATA_INICIOGOZO && z.VNCU_DATAINICIO <= fim).ap_unidade.UND_SIGLA : ""),
                FUN_NOME = x.ap_vinculo.ap_funcionario.FUN_NOMECRACHA,
            }).ToList();
        }
    }
}
