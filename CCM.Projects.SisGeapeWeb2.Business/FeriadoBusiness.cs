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
    public class FeriadoBusiness : IFeriadoBusiness
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly FeriadoRepository _feriadoRepository;
        private IValidationDictionary _validationDictionary;

        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }

        public FeriadoBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _feriadoRepository = new FeriadoRepository(_unitOfWork);

        }

        public int TotalRegistro()
        {
            return _feriadoRepository.Count(x => x.FER_STATUS == "A");
        }

        private List<FeriadoDomainModel> GetFeriadoPorDataEspecifico(DateTime dataInicio)
        {

            var listDomain = _feriadoRepository.GetAll(x => x.FER_STATUS == "A" && x.FER_DATA == dataInicio).ToList();

            return listDomain.Select(x => new FeriadoDomainModel
            {
                FER_DESCRICAO = x.FER_DESCRICAO,
                FER_TIPO = (TipoFeriado)x.FER_TIPO,
                FER_DATA = x.FER_DATA
            }).ToList();
        }

        private List<FeriadoDomainModel> GetFeriadoPorDataIniFim(DateTime? dataInicio, DateTime? dataFim)
        {
            var listDomain = _feriadoRepository.GetAll(x => x.FER_STATUS == "A" && (x.FER_DATA >= dataInicio && x.FER_DATA <= dataFim)).ToList();

            return listDomain.Select(x => new FeriadoDomainModel
            {
                FER_DESCRICAO = x.FER_DESCRICAO,
                FER_TIPO = (TipoFeriado)x.FER_TIPO,
                FER_DATA = x.FER_DATA
            }).ToList();
        }

        public List<FeriadoDomainModel> GetFeriadoPorData(DateTime? dataInicio, DateTime? dataFim)
        {
            List<FeriadoDomainModel> retorno = null;
            if (dataInicio.HasValue && !dataFim.HasValue)
            {
                retorno = GetFeriadoPorDataEspecifico(dataInicio.Value);
            }
            else if (dataInicio.HasValue && dataFim.HasValue)
            {
                retorno = GetFeriadoPorDataIniFim(dataInicio.Value, dataFim.Value);
            }
            else
            {
                var listDomain = _feriadoRepository.GetAll(x => x.FER_STATUS == "A").ToList();
                retorno = listDomain.Select(x => new FeriadoDomainModel
                {
                    FER_ID = x.FER_ID,
                    FER_DESCRICAO = x.FER_DESCRICAO,
                    FER_TIPO = (TipoFeriado)x.FER_TIPO,
                    FER_DATA = x.FER_DATA
                }).ToList();
            }

            return retorno;
        }

        public FeriadoDomainModel GetFeriadoById(int id)
        {
            ap_feriado feriado = _feriadoRepository.SingleOrDefault(x => x.FER_STATUS == "A" && x.FER_ID == id);

            FeriadoDomainModel domainModel = new FeriadoDomainModel()
            {
                FER_ID = feriado.FER_ID,
                FER_DESCRICAO = feriado.FER_DESCRICAO,
                FER_TIPO = (TipoFeriado)feriado.FER_TIPO,
                FER_DATA = feriado.FER_DATA
            };

            return domainModel;
        }

        public bool AddUpdateFeriado(FeriadoDomainModel _domainModel)
        {
            ap_feriado feriado = new ap_feriado();
            try
            {
                if (_domainModel.FER_ID == 0)
                {
                    feriado.FER_ID = _domainModel.FER_ID;
                    feriado.FER_DESCRICAO = _domainModel.FER_DESCRICAO;
                    feriado.FER_TIPO = (int)_domainModel.FER_TIPO;
                    feriado.FER_DATA = _domainModel.FER_DATA;
                    feriado.FER_REGDATE = DateTime.Now;
                    feriado.FER_REGUSER = _domainModel.FER_REGUSER;
                    feriado.FER_STATUS = "A";

                    _feriadoRepository.Insert(feriado);
                    return true;
                }
                else
                {
                    feriado = _feriadoRepository.SingleOrDefault(x => x.FER_STATUS == "A" && x.FER_ID == _domainModel.FER_ID);

                    feriado.FER_ID = _domainModel.FER_ID;
                    feriado.FER_DESCRICAO = _domainModel.FER_DESCRICAO;
                    feriado.FER_TIPO = (int)_domainModel.FER_TIPO;
                    feriado.FER_DATA = _domainModel.FER_DATA;
                    feriado.FER_REGDATE = DateTime.Now;
                    feriado.FER_REGUSER = _domainModel.FER_REGUSER;
                    feriado.FER_STATUS = "A";

                    _feriadoRepository.Update(feriado);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFeriado(FeriadoDomainModel _domainModel)
        {
            ap_feriado feriado = _feriadoRepository.SingleOrDefault(x => x.FER_STATUS == "A" && x.FER_ID == _domainModel.FER_ID);

            if (feriado != null)
            {
                feriado.FER_REGDATE = DateTime.Now;
                feriado.FER_REGUSER = _domainModel.FER_REGUSER;
                feriado.FER_STATUS = "I";

                _feriadoRepository.Update(feriado);
                return true;
            }
            return false;
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _feriadoRepository.Save();
                return result;
            }
            catch (Exception e)
            {
                throw e;
                // return !result;
            }
        }


    }
}
