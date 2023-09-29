using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class UnidadeBusiness : IUnidadeBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UnidadeRepository _unidadeRepository;


        public UnidadeBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _unidadeRepository = new UnidadeRepository(_unitOfWork);
        }



        //protected bool ValidAddUpdateUnidade(UnidadeDomainModel _domainModel)
        //{
        //    if (_unidadeRepository.GetAll(x => x.UND_STATUS == "A" && x.UND_SIGLA.Contains(_domainModel.UND_SIGLA)).Count() > 0)
        //        _validatonDictionary.AddError("UND_SIGLA", "Já existe uma UNIDADE com esta SIGLA");

        //    return _validatonDictionary.IsValid;
        //}

        public bool AddUpdateUnidade(UnidadeDomainModel _domainModel)
        {
            //if (!ValidAddUpdateUnidade(_domainModel))
            //    return false;

            try
            {
                if (_domainModel.UND_ID == 0)
                {
                    var unidade = new ap_unidade()
                    {
                        UND_SIGLA = _domainModel.UND_SIGLA.ToUpper(),
                        UND_NOME = _domainModel.UND_NOME,
                        UND_NIVEL = _domainModel.UND_NIVEL,
                        UND_REGDATE = DateTime.Now,
                        UND_PAI = _domainModel.UND_PAI,
                        UND_DIRETORIO = _domainModel.UND_DIRETORIO,
                        UND_CODIGO = _domainModel.UND_CODIGO,
                        UND_DATAFIM = _domainModel.UND_DATAFIM,
                        UND_DATAINICIO = _domainModel.UND_DATAINICIO,
                        UND_RAMAL = _domainModel.UND_RAMAL,
                        UND_COMPETENCIA = _domainModel.UND_COMPETENCIA,
                        UND_STATUS = "A",
                        UND_REGUSER = _domainModel.UND_REGUSER
                    };
                    _unidadeRepository.Insert(unidade);
                    return true;

                }
                else
                {
                    var unidade = _unidadeRepository.SingleOrDefault(x => x.UND_ID == _domainModel.UND_ID && x.UND_STATUS == "A");

                    if (unidade != null)
                    {
                        unidade.UND_SIGLA = _domainModel.UND_SIGLA.ToUpper();
                        unidade.UND_NOME = _domainModel.UND_NOME;
                        unidade.UND_NIVEL = _domainModel.UND_NIVEL;
                        unidade.UND_REGDATE = DateTime.Now;
                        unidade.UND_PAI = _domainModel.UND_PAI;
                        unidade.UND_DIRETORIO = _domainModel.UND_DIRETORIO;
                        unidade.UND_CODIGO = _domainModel.UND_CODIGO;
                        unidade.UND_DATAFIM = _domainModel.UND_DATAFIM;
                        unidade.UND_DATAINICIO = _domainModel.UND_DATAINICIO;
                        unidade.UND_RAMAL = _domainModel.UND_RAMAL;
                        unidade.UND_COMPETENCIA = _domainModel.UND_COMPETENCIA;
                        unidade.UND_STATUS = "A";
                        unidade.UND_REGUSER = _domainModel.UND_REGUSER;
                    };
                    _unidadeRepository.Update(unidade);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUnidade(UnidadeDomainModel _domainModel)
        {
            var unidade = _unidadeRepository.SingleOrDefault(x => x.UND_ID == _domainModel.UND_ID && x.UND_STATUS == "A");

            if (unidade != null)
            {

                unidade.UND_REGDATE = DateTime.Now;
                unidade.UND_STATUS = "I";
                unidade.UND_REGUSER = _domainModel.UND_REGUSER;
                _unidadeRepository.Update(unidade);
                return true;

            };
            return false;
        }

        public async Task<List<UnidadeDomainModel>> GetAllAsync()
        {
            List<UnidadeDomainModel> listDomain = await _unidadeRepository.GetAllAsync(x => x.UND_STATUS == "A").ContinueWith(x =>
           x.Result.Count() > 0 ? x.Result.Select(z => z).Select(und => new UnidadeDomainModel()
           {
               UND_CODIGO = und.UND_CODIGO,
               UND_ID = und.UND_ID,
               UND_COMPETENCIA = und.UND_COMPETENCIA,
               UND_DATAFIM = und.UND_DATAFIM,
               UND_DATAINICIO = und.UND_DATAINICIO,
               UND_DIRETORIO = und.UND_DIRETORIO,
               UND_NIVEL = und.UND_NIVEL,
               UND_NOME = und.UND_NOME,
               UND_PAI = und.UND_PAI,
               UND_RAMAL = und.UND_RAMAL,
               UND_SIGLA = und.UND_SIGLA,
               UND_PAI_NOME = und.UND_PAI.HasValue ? _unidadeRepository.FirstOrDefaultAsync(z => z.UND_ID == und.UND_PAI).Result.UND_SIGLA : null,
           }).ToList() : null);

            //foreach (var item in listDomain)
            //{
            //    if (item.UND_PAI != null)
            //        item.UND_PAI_NOME = _unidadeRepository.SingleOrDefault(x => x.UND_ID == item.UND_PAI).UND_SIGLA;
            //}

            return listDomain;
        }

        public List<UnidadeDomainModel> ddlUnidade()
        {
            List<UnidadeDomainModel> listDomain = _unidadeRepository.GetAll(x => x.UND_STATUS == "A" && x.UND_DATAFIM == null).OrderBy(x => x.UND_SIGLA).Select(und => new UnidadeDomainModel()
            {
                UND_CODIGO = und.UND_CODIGO,
                UND_ID = und.UND_ID,
                UND_COMPETENCIA = und.UND_COMPETENCIA,
                UND_DATAFIM = und.UND_DATAFIM,
                UND_DATAINICIO = und.UND_DATAINICIO,
                UND_DIRETORIO = und.UND_DIRETORIO,
                UND_NIVEL = und.UND_NIVEL,
                UND_NOME = und.UND_SIGLA + " - " + und.UND_NOME,
                UND_PAI = und.UND_PAI,
                UND_RAMAL = und.UND_RAMAL,
                UND_SIGLA = und.UND_SIGLA,

            }).ToList();


            return listDomain;
        }

        public List<UnidadeDomainModel> GetUnidadeByName(string name)
        {
            return _unidadeRepository.GetAll(x => x.UND_STATUS == "A" && x.UND_SIGLA.Contains(name)).Select(und => new UnidadeDomainModel()
            {
                UND_CODIGO = und.UND_CODIGO,
                UND_ID = und.UND_ID,
                UND_COMPETENCIA = und.UND_COMPETENCIA,
                UND_DATAFIM = und.UND_DATAFIM,
                UND_DATAINICIO = und.UND_DATAINICIO,
                UND_DIRETORIO = und.UND_DIRETORIO,
                UND_NIVEL = und.UND_NIVEL,
                UND_NOME = und.UND_NOME,
                UND_PAI = und.UND_PAI,
                UND_RAMAL = und.UND_RAMAL,
                UND_SIGLA = und.UND_SIGLA,


            }).ToList();
        }

        public List<UnidadeDomainModel> GetUnidadePageRecords(int pageStart, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<UnidadeDomainModel> GetUnidadeyId(int Id)
        {
            var unidade = await _unidadeRepository.FirstOrDefaultAsync(x => x.UND_STATUS == "A" && x.UND_ID == Id).ContinueWith(und => new UnidadeDomainModel()
            {
                UND_CODIGO = und.Result.UND_CODIGO,
                UND_ID = und.Result.UND_ID,
                UND_COMPETENCIA = und.Result.UND_COMPETENCIA,
                UND_DATAFIM = und.Result.UND_DATAFIM,
                UND_DATAINICIO = und.Result.UND_DATAINICIO,
                UND_DIRETORIO = und.Result.UND_DIRETORIO,
                UND_NIVEL = und.Result.UND_NIVEL,
                UND_NOME = und.Result.UND_NOME,
                UND_PAI = und.Result.UND_PAI,
                UND_PAI_NOME = (und.Result.UND_PAI > 0 ? _unidadeRepository.SingleOrDefault(x => x.UND_STATUS == "A" && x.UND_ID == und.Result.UND_PAI).UND_SIGLA : null),
                UND_RAMAL = und.Result.UND_RAMAL,
                UND_SIGLA = und.Result.UND_SIGLA,

            });

            return unidade;
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _unidadeRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalUnidadeCount()
        {
            return _unidadeRepository.Count(x => x.UND_STATUS == "A");
        }

        //Pendencia a Implementar
        public bool ValidarDelete(int Id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsUnidade(string name, int id)
        {
            return _unidadeRepository.Exists(x => x.UND_STATUS == "A" && x.UND_SIGLA.Equals(name) && x.UND_ID != id);
        }
    }
}
