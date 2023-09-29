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
    public class CidBusiness : ICidBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CidRepository _cidRepository;

        public CidBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cidRepository = new CidRepository(_unitOfWork);
        }

        public bool AddUpdateCid(CidDomainModel domainModel)
        {
            ap_cid10 cid = new ap_cid10();
            try
            {
                if (domainModel.CID_ID == 0)
                {

                    cid.CID_CODIGO = domainModel.CID_CODIGO.ToUpper();
                    cid.CID_DESCRICAO = domainModel.CID_DESCRICAO;
                    cid.CID_REGDATE = DateTime.Now;
                    cid.CID_REGUSER = domainModel.CID_REGUSER;
                    cid.CID_STATUS = "A";

                    _cidRepository.Insert(cid);
                    return true;
                }
                else
                {
                    cid = _cidRepository.SingleOrDefault(x => x.CID_STATUS == "A" && x.CID_ID == domainModel.CID_ID);

                    cid.CID_CODIGO = domainModel.CID_CODIGO.ToUpper();
                    cid.CID_DESCRICAO = domainModel.CID_DESCRICAO;
                    cid.CID_REGDATE = DateTime.Now;
                    cid.CID_REGUSER = domainModel.CID_REGUSER;

                    _cidRepository.Update(cid);
                    return true;
                }
            }
            catch { return false; }
        }

        public bool DeleteCid(CidDomainModel domainModel)
        {
            ap_cid10 cid = _cidRepository.SingleOrDefault(x => x.CID_STATUS == "A" && x.CID_ID == domainModel.CID_ID);

            if (cid != null)
            {

                cid.CID_REGDATE = DateTime.Now;
                cid.CID_REGUSER = domainModel.CID_REGUSER;
                cid.CID_STATUS = "I";

                _cidRepository.Update(cid);
                return true;
            }
            return false;
        }

        public List<CidDomainModel> GetCid()
        {
            return _cidRepository.GetAll(x => x.CID_STATUS == "A").Select(x => new CidDomainModel
            {
                CID_CODIGO = x.CID_CODIGO,
                CID_DESCRICAO = x.CID_DESCRICAO,
                CID_ID = x.CID_ID,
                CID_REGUSER = x.CID_REGUSER
            }).ToList();
        }

        public CidDomainModel GetCidById(int id)
        {
            ap_cid10 cid = _cidRepository.SingleOrDefault(x => x.CID_STATUS == "A" && x.CID_ID == id);

            return new CidDomainModel { CID_CODIGO = cid.CID_CODIGO, CID_DESCRICAO = cid.CID_DESCRICAO, CID_ID = cid.CID_ID, CID_REGUSER = cid.CID_REGUSER };
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _cidRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistros()
        {
            return _cidRepository.Count(x => x.CID_STATUS == "A");
        }
    }
}
