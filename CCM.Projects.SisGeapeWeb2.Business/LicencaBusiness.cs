using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class LicencaBusiness : ILicencaBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LicencaRepository _licencaRepository;

        public LicencaBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _licencaRepository = new LicencaRepository(_unitOfWork);
        }

        public LicencaDomainModel AddUpdateLicenca(LicencaDomainModel modelDomain)
        {

            if (modelDomain.LIC_ID == 0)
            {
                ap_licenca licenca = new ap_licenca();
                licenca.LICTP_ID = modelDomain.LICTP_ID;
                licenca.LIC_DATAENTRADA = modelDomain.LIC_DATAENTRADA;
                licenca.LIC_DATAFIM = modelDomain.LIC_DATAFIM;
                licenca.LIC_DATAINICIO = modelDomain.LIC_DATAINICIO;
                licenca.LIC_DATAPORTARIA = modelDomain.LIC_DATAPORTARIA;
                licenca.LIC_DATARETORNO = modelDomain.LIC_DATARETORNO;
                licenca.LIC_NOMEARQUIVO = modelDomain.ANEXO.FileName;
                licenca.LIC_ARQUIVO = GetImageByte(modelDomain.ANEXO.InputStream);
                licenca.LIC_NUMPORTARIA = modelDomain.LIC_NUMPORTARIA;
                licenca.LIC_REGDATE = DateTime.Now;
                licenca.VNC_ID = modelDomain.VNC_ID;
                licenca.LIC_REGUSER = modelDomain.LIC_REGUSER;
                licenca.LIC_STATUS = "A";
                _licencaRepository.Insert(licenca);
            }
            else
            {
                ap_licenca licenca = _licencaRepository.SingleOrDefault(x => x.LIC_STATUS == "A" && x.LIC_ID == modelDomain.LIC_ID);

                if (licenca != null)
                {
                    licenca.LICTP_ID = modelDomain.LICTP_ID;
                    licenca.LIC_DATAENTRADA = modelDomain.LIC_DATAENTRADA;
                    licenca.LIC_DATAFIM = modelDomain.LIC_DATAFIM;
                    licenca.LIC_DATAINICIO = modelDomain.LIC_DATAINICIO;
                    licenca.LIC_DATAPORTARIA = modelDomain.LIC_DATAPORTARIA;
                    licenca.LIC_DATARETORNO = modelDomain.LIC_DATARETORNO;
                    licenca.LIC_NOMEARQUIVO = modelDomain.ANEXO.FileName;
                    licenca.LIC_ARQUIVO = GetImageByte(modelDomain.ANEXO.InputStream);
                    licenca.LIC_NUMPORTARIA = modelDomain.LIC_NUMPORTARIA;
                    licenca.LIC_REGDATE = DateTime.Now;
                    licenca.VNC_ID = modelDomain.VNC_ID;
                    licenca.LIC_REGUSER = modelDomain.LIC_REGUSER;
                    licenca.LIC_STATUS = "A";
                    _licencaRepository.Update(licenca);
                }

            }
            return modelDomain;
        }

        public bool DeleteLicenca(LicencaDomainModel modelDomain)
        {
            bool result = false;
            ap_licenca licenca = _licencaRepository.SingleOrDefault(x => x.LIC_STATUS == "A" && x.LIC_ID == modelDomain.LIC_ID);
            if (licenca != null)
            {

                licenca.LIC_REGDATE = DateTime.Now;
                licenca.LIC_REGUSER = modelDomain.LIC_REGUSER;
                licenca.LIC_STATUS = "I";
                _licencaRepository.Update(licenca);

                result = true;
            }

            return result;
        }

        public List<LicencaDomainModel> GetAll()
        {
            return _licencaRepository.GetAll(x => x.LIC_STATUS == "A").ToList().Select(x => new LicencaDomainModel
            {
                LICTP_ID = x.LICTP_ID.Value,
                LIC_DATAENTRADA = x.LIC_DATAENTRADA,
                LIC_DATAFIM = x.LIC_DATAFIM,
                LIC_DATAINICIO = x.LIC_DATAINICIO,
                LIC_DATAPORTARIA = x.LIC_DATAPORTARIA,
                LIC_DATARETORNO = x.LIC_DATARETORNO,
                LIC_ID = x.LIC_ID,
                LIC_NOMEARQUIVO = x.LIC_NOMEARQUIVO,
                LIC_NUMPORTARIA = x.LIC_NUMPORTARIA,
                LIC_REGUSER = x.LIC_REGUSER,
                VNC_ID = x.VNC_ID.Value
            }).ToList();
        }

        public LicencaDomainModel GetLicencaById(int Id)
        {
            ap_licenca licenca = _licencaRepository.SingleOrDefault(x => x.LIC_STATUS == "A" && x.LIC_ID == Id);


            LicencaDomainModel _domainModel = new LicencaDomainModel();
            _domainModel.LICTP_ID = licenca.LICTP_ID.Value;
            _domainModel.LIC_DATAENTRADA = licenca.LIC_DATAENTRADA;
            _domainModel.LIC_DATAFIM = licenca.LIC_DATAFIM;
            _domainModel.LIC_DATAINICIO = licenca.LIC_DATAINICIO;
            _domainModel.LIC_DATAPORTARIA = licenca.LIC_DATAPORTARIA;
            _domainModel.LIC_DATARETORNO = licenca.LIC_DATARETORNO;
            _domainModel.LIC_ID = licenca.LIC_ID;
            _domainModel.LIC_NOMEARQUIVO = licenca.LIC_NOMEARQUIVO;
            _domainModel.LIC_NUMPORTARIA = licenca.LIC_NUMPORTARIA;
            _domainModel.LIC_REGUSER = licenca.LIC_REGUSER;
            _domainModel.VNC_ID = licenca.VNC_ID.Value;

            return _domainModel;

        }



        public List<LicencaDomainModel> GetAllLicencaPage(int pageStart, int pageSize)
        {
            return _licencaRepository.GetPagedRecords(x => x.LIC_STATUS == "A", x => x.LIC_DATAENTRADA.Value.ToShortDateString(), pageStart, pageSize).
                Select(x => new LicencaDomainModel
                {
                    LICTP_ID = x.LICTP_ID.Value,
                    LIC_DATAENTRADA = x.LIC_DATAENTRADA,
                    LIC_DATAFIM = x.LIC_DATAFIM,
                    LIC_DATAINICIO = x.LIC_DATAINICIO,
                    LIC_DATAPORTARIA = x.LIC_DATAPORTARIA,
                    LIC_DATARETORNO = x.LIC_DATARETORNO,
                    LIC_ID = x.LIC_ID,
                    LIC_NOMEARQUIVO = x.LIC_NOMEARQUIVO,
                    LIC_NUMPORTARIA = x.LIC_NUMPORTARIA,
                    LIC_REGUSER = x.LIC_REGUSER,
                    VNC_ID = x.VNC_ID.Value
                }).ToList();


        }

        public List<LicencaDomainModel> GetLicencaByFuncionario(int funcionario)
        {
            return _licencaRepository.GetAll(x => x.LIC_STATUS == "A" && x.ap_vinculo.FUN_ID == funcionario).ToList().Select(x => new LicencaDomainModel
            {
                LICTP_ID = x.LICTP_ID.Value,
                LIC_DATAENTRADA = x.LIC_DATAENTRADA,
                LIC_DATAFIM = x.LIC_DATAFIM,
                LIC_DATAINICIO = x.LIC_DATAINICIO,
                LIC_DATAPORTARIA = x.LIC_DATAPORTARIA,
                LIC_DATARETORNO = x.LIC_DATARETORNO,
                LIC_ID = x.LIC_ID,
                LIC_NOMEARQUIVO = x.LIC_NOMEARQUIVO,
                LIC_NUMPORTARIA = x.LIC_NUMPORTARIA,
                LIC_REGUSER = x.LIC_REGUSER,
                VNC_ID = x.VNC_ID.Value
            }).ToList();
        }

        public List<LicencaDomainModel> GetLicencaByFuncionarioPage(int func, int pageStart, int pageSize)
        {
            return _licencaRepository.GetPagedRecords(x => x.LIC_STATUS == "A" && x.ap_vinculo.FUN_ID == func, x => x.LIC_DATAENTRADA.Value.ToShortDateString(), pageStart, pageSize).
                Select(x => new LicencaDomainModel
                {
                    LICTP_ID = x.LICTP_ID.Value,
                    LIC_DATAENTRADA = x.LIC_DATAENTRADA,
                    LIC_DATAFIM = x.LIC_DATAFIM,
                    LIC_DATAINICIO = x.LIC_DATAINICIO,
                    LIC_DATAPORTARIA = x.LIC_DATAPORTARIA,
                    LIC_DATARETORNO = x.LIC_DATARETORNO,
                    LIC_ID = x.LIC_ID,
                    LIC_NOMEARQUIVO = x.LIC_NOMEARQUIVO,
                    LIC_NUMPORTARIA = x.LIC_NUMPORTARIA,
                    LIC_REGUSER = x.LIC_REGUSER,
                    VNC_ID = x.VNC_ID.Value
                }).ToList();

        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _licencaRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }


        public int TotalLicenca()
        {
            return _licencaRepository.Count(x => x.LIC_STATUS == "A");
        }

        public int TotalLicencaByFuncionario(int funcionario)
        {
            return _licencaRepository.Count(x => x.LIC_STATUS == "A" && x.ap_vinculo.FUN_ID == funcionario);
        }

        protected byte[] GetImageByte(Stream arquivo)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                arquivo.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }

        }



    }
}
