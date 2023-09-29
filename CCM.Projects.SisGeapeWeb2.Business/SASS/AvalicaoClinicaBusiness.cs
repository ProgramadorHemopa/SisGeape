using CCM.Projects.SisGeape2.Domain.SASS;
using CCM.Projects.SisGeapeWeb2.Business.Interface.SASS;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository.SASS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCM.Projects.SisGeapeWeb2.Business.SASS
{
    public class AvaliacaoClinicaBusiness : IAvaliacaoClinicaBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AvaliacaoClinicaRepository _avaliacaoClinicaRepository;

        public AvaliacaoClinicaBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _avaliacaoClinicaRepository = new AvaliacaoClinicaRepository(_unitOfWork);
        }

        public bool AddUpdateAvalicaoClinica(AvaliacaoClinicaDomainModel domainModel)
        {
            try
            {
                sass_avaliacaoclinica avalicao;
                if (domainModel.AVC_ID == 0)
                {
                    avalicao = new sass_avaliacaoclinica()
                    {
                        AVC_DESCRICAO = domainModel.AVC_DESCRICAO,
                        AVC_ID = domainModel.AVC_ID,
                        AVC_REGDATE = DateTime.Now,
                        AVC_REGUSER = domainModel.AVC_REGUSER,
                        AVC_STATUS = "A"
                    };
                    _avaliacaoClinicaRepository.Insert(avalicao);

                }
                else
                {
                    avalicao = _avaliacaoClinicaRepository.SingleOrDefault(x => x.AVC_STATUS == "A" && x.AVC_ID == domainModel.AVC_ID);

                    avalicao.AVC_DESCRICAO = domainModel.AVC_DESCRICAO;
                    avalicao.AVC_REGDATE = DateTime.Now;
                    avalicao.AVC_REGUSER = domainModel.AVC_REGUSER;
                    _avaliacaoClinicaRepository.Update(avalicao);

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public AvaliacaoClinicaDomainModel AvaliacaoClinicaById(int id)
        {
            sass_avaliacaoclinica avaliacao = _avaliacaoClinicaRepository.SingleOrDefault(x => x.AVC_STATUS == "A" && x.AVC_ID == id);

            AvaliacaoClinicaDomainModel objAvaliacao = new AvaliacaoClinicaDomainModel
            {
                AVC_DESCRICAO = avaliacao.AVC_DESCRICAO,
                AVC_ID = avaliacao.AVC_ID,
                AVC_REGUSER = avaliacao.AVC_REGUSER
            };

            return objAvaliacao;
        }

        public bool DeleteAvaliacaoClinica(AvaliacaoClinicaDomainModel domainModel)
        {
            try
            {
                sass_avaliacaoclinica avaliacao = _avaliacaoClinicaRepository.SingleOrDefault(x => x.AVC_STATUS == "A" && x.AVC_ID == domainModel.AVC_ID);

                avaliacao.AVC_STATUS = "I";
                avaliacao.AVC_REGDATE = DateTime.Now;
                avaliacao.AVC_REGUSER = domainModel.AVC_REGUSER;
                _avaliacaoClinicaRepository.Update(avaliacao);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AvaliacaoClinicaDomainModel> ListarAvaliacaoClinica()
        {
            return _avaliacaoClinicaRepository.GetAll(x => x.AVC_STATUS == "A").Select(x => new AvaliacaoClinicaDomainModel
            {
                AVC_DESCRICAO = x.AVC_DESCRICAO,
                AVC_ID = x.AVC_ID
            }).ToList();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _avaliacaoClinicaRepository.Save();
                return result;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return !result;
            }
        }
    }
}
