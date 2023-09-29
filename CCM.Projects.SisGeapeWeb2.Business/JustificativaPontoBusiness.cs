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
    public class JustificativaPontoBusiness : IJustificativaPontoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JustificativaPontoRepository _justificativaRepository;

        private readonly MotivoJustificativaRepository _motivoJustificativaRepository;

        public JustificativaPontoBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _justificativaRepository = new JustificativaPontoRepository(_unitOfWork);
            _motivoJustificativaRepository = new MotivoJustificativaRepository(_unitOfWork);
        }

        public bool AddUpdateJustificativa(JustificativaPontoDomainModel _domainModel)
        {
            try
            {
                pon_justificativaponto justificativa = new pon_justificativaponto();

                if (_domainModel.JUSPT_ID == 0)
                {

                    justificativa.CID_ID = _domainModel.CID_ID;
                    justificativa.JUSPT_DATAFIM = _domainModel.JUSPT_DATAFIM;
                    justificativa.JUSPT_DATAINICIO = _domainModel.JUSPT_DATAINICIO;
                    justificativa.JUSPT_DATARECEBIMENTO = _domainModel.JUSPT_DATARECEBIMENTO;
                    justificativa.JUSPT_OBSERVACAO = _domainModel.JUSPT_OBSERVACAO;
                    justificativa.MTVJUS_ID = _domainModel.MTVJUS_ID;
                    justificativa.VNC_ID = _domainModel.VNC_ID;
                    justificativa.JUSPT_REGDATE = DateTime.Now;
                    justificativa.JUSPT_REGUSER = _domainModel.JUSPT_REGUSER;
                    justificativa.JUSPT_STATUS = "A";

                    _justificativaRepository.Insert(justificativa);
                    return true;
                }
                else
                {
                    justificativa = _justificativaRepository.SingleOrDefault(x => x.JUSPT_STATUS == "A" && x.JUSPT_ID == _domainModel.JUSPT_ID);

                    justificativa.CID_ID = _domainModel.CID_ID;
                    justificativa.JUSPT_DATAFIM = _domainModel.JUSPT_DATAFIM;
                    justificativa.JUSPT_DATAINICIO = _domainModel.JUSPT_DATAINICIO;
                    justificativa.JUSPT_DATARECEBIMENTO = _domainModel.JUSPT_DATARECEBIMENTO;
                    justificativa.JUSPT_OBSERVACAO = _domainModel.JUSPT_OBSERVACAO;
                    justificativa.MTVJUS_ID = _domainModel.MTVJUS_ID;
                    justificativa.VNC_ID = _domainModel.VNC_ID;
                    justificativa.JUSPT_REGDATE = DateTime.Now;
                    justificativa.JUSPT_REGUSER = _domainModel.JUSPT_REGUSER;
                    justificativa.JUSPT_STATUS = "A";

                    _justificativaRepository.Update(justificativa);
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddUpdateMotivoJustificativa(MotivoJustificativaDomainModel _domainModel)
        {
            try
            {
                pon_motivojustificativa motivo = new pon_motivojustificativa();

                if (_domainModel.MTVJUS_ID == 0)
                {

                    motivo.MTVJUS_NOME = _domainModel.MTVJUS_NOME.ToUpper();
                    motivo.MTVJUS_REGDATE = DateTime.Now;
                    motivo.MTVJUS_REGUSER = _domainModel.MTVJUS_REGUSER;
                    motivo.MTVJUS_STATUS = "A";

                    _motivoJustificativaRepository.Insert(motivo);
                    return true;
                }
                else
                {
                    motivo = _motivoJustificativaRepository.SingleOrDefault(x => x.MTVJUS_STATUS == "A" && x.MTVJUS_ID == _domainModel.MTVJUS_ID);

                    motivo.MTVJUS_NOME = _domainModel.MTVJUS_NOME.ToUpper();
                    motivo.MTVJUS_REGDATE = DateTime.Now;
                    motivo.MTVJUS_REGUSER = _domainModel.MTVJUS_REGUSER;

                    _motivoJustificativaRepository.Update(motivo);

                    return true;
                }
            }
            catch { return false; }
        }

        public List<JustificativaPontoDomainModel> BuscarJustificativaPorParametros(int? vinculo, int? cargo, int? unidade, int? motivo, string matricula, string nome, DateTime? inicio, DateTime? fim)
        {
            List<pon_justificativaponto> ListJustificativa = _justificativaRepository.GetAll(x => x.JUSPT_STATUS == "A" /*&& x.ap_vinculo.VNC_STATUS == "A" && x.ap_vinculo.ap_funcionario.FUN_STATUS == "A"*/).ToList();


            if (vinculo.HasValue)
                ListJustificativa = ListJustificativa.Where(x => x.VNC_ID == vinculo).ToList();
            if (cargo.HasValue)
                ListJustificativa = ListJustificativa.Where(x => x.ap_vinculo.CRG_ID == cargo).ToList();

            if (unidade.HasValue)
                ListJustificativa = ListJustificativa.Where(z => z.ap_vinculo.ap_vinculoxunidade.Any(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null && x.UND_ID == unidade)).ToList();

            if (motivo.HasValue)
                ListJustificativa = ListJustificativa.Where(x => x.MTVJUS_ID == motivo).ToList();

            if (matricula != "")
                ListJustificativa = ListJustificativa.Where(x => x.ap_vinculo.ap_funcionario.FUN_MATRICULA.Contains(matricula)).ToList();

            if (nome != "")
                ListJustificativa = ListJustificativa.Where(x => x.ap_vinculo.ap_funcionario.FUN_NOME.ToUpper().Contains(nome.ToUpper())).ToList();


            if (inicio.HasValue || fim.HasValue)
            {
                if (inicio.HasValue && !fim.HasValue)
                    ListJustificativa = ListJustificativa.Where(x => x.JUSPT_DATAINICIO >= inicio && x.JUSPT_DATAFIM <= DateTime.Today).ToList();
                else
                    ListJustificativa = ListJustificativa.Where(x => x.JUSPT_DATAINICIO >= inicio && x.JUSPT_DATAFIM <= fim || (x.JUSPT_DATAINICIO <= inicio && x.JUSPT_DATAFIM >= inicio)).ToList();
            }


            return ListJustificativa.Select(x => new JustificativaPontoDomainModel()
            {
                CID_ID = x.CID_ID,
                CID_NOME = (x.CID_ID != null ? x.ap_cid10.CID_CODIGO : null),
                JUSPT_DATAFIM = x.JUSPT_DATAFIM,
                JUSPT_DATAINICIO = x.JUSPT_DATAINICIO,
                JUSPT_DATARECEBIMENTO = x.JUSPT_DATARECEBIMENTO,
                JUSPT_ID = x.JUSPT_ID,
                JUSPT_OBSERVACAO = x.JUSPT_OBSERVACAO,
                JUSPT_REGUSER = x.JUSPT_REGUSER,
                MTVJUS_ID = x.MTVJUS_ID.Value,
                MTVJUS_NOME = x.pon_motivojustificativa.MTVJUS_NOME,
                VNC_ID = x.VNC_ID,
                UND_NOME = x.ap_vinculo.ap_vinculoxunidade.Any(z => z.VNCU_DATAFIM == null) ? x.ap_vinculo.ap_vinculoxunidade.FirstOrDefault(z => z.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA : "",
                VNC_NOME = x.ap_vinculo.VNC_ID + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                FUN_ID = x.ap_vinculo.FUN_ID.Value,
                FUN_MATRICULA = x.ap_vinculo.ap_funcionario.FUN_MATRICULA,
                FUN_NOME = x.ap_vinculo.ap_funcionario.FUN_NOME,
            }).ToList();
        }

        public bool DeleteJustificativa(JustificativaPontoDomainModel _domainModel)
        {
            pon_justificativaponto justificativa = _justificativaRepository.SingleOrDefault(x => x.JUSPT_STATUS == "A" && x.JUSPT_ID == _domainModel.JUSPT_ID);
            if (justificativa != null)
            {

                justificativa.JUSPT_REGDATE = DateTime.Now;
                justificativa.JUSPT_REGUSER = _domainModel.JUSPT_REGUSER;
                justificativa.JUSPT_STATUS = "I";

                _justificativaRepository.Update(justificativa);
                return true;
            }
            return false;
        }

        public bool DeleteMotivoJustificativa(MotivoJustificativaDomainModel _domainModel)
        {
            pon_motivojustificativa motivo = _motivoJustificativaRepository.SingleOrDefault(x => x.MTVJUS_STATUS == "A" && x.MTVJUS_ID == _domainModel.MTVJUS_ID);

            motivo.MTVJUS_STATUS = "I";
            motivo.MTVJUS_REGDATE = DateTime.Now;
            motivo.MTVJUS_REGUSER = _domainModel.MTVJUS_REGUSER;

            _motivoJustificativaRepository.Update(motivo);

            return true;
        }

        public List<JustificativaPontoDomainModel> GetJustificativaByFuncionario(int func)
        {
            var listDomain = _justificativaRepository.GetAll(x => x.JUSPT_STATUS == "A" && x.ap_vinculo.FUN_ID == func).ToList();

            return listDomain.Select(x => new JustificativaPontoDomainModel
            {
                CID_ID = x.CID_ID,
                CID_NOME = (x.CID_ID != null ? x.ap_cid10.CID_CODIGO : null),
                JUSPT_DATAFIM = x.JUSPT_DATAFIM,
                JUSPT_DATAINICIO = x.JUSPT_DATAINICIO,
                JUSPT_DATARECEBIMENTO = x.JUSPT_DATARECEBIMENTO,
                JUSPT_ID = x.JUSPT_ID,
                JUSPT_OBSERVACAO = x.JUSPT_OBSERVACAO,
                JUSPT_REGUSER = x.JUSPT_REGUSER,
                MTVJUS_ID = x.MTVJUS_ID.Value,
                MTVJUS_NOME = x.pon_motivojustificativa.MTVJUS_NOME,
                VNC_ID = x.VNC_ID,
                VNC_NOME = x.ap_vinculo.VNC_ID + " - " + x.ap_vinculo.ap_cargo.CRG_NOME + " - " + x.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO
            }).ToList();

        }

        public JustificativaPontoDomainModel GetJustificativaById(int id)
        {
            pon_justificativaponto justificativa = _justificativaRepository.SingleOrDefault(x => x.JUSPT_STATUS == "A" && x.JUSPT_ID == id);

            return new JustificativaPontoDomainModel()
            {
                CID_ID = justificativa.CID_ID,
                CID_NOME = justificativa.CID_ID != null ? justificativa.ap_cid10.CID_CODIGO : null,
                FUN_ID = justificativa.ap_vinculo.FUN_ID.Value,
                JUSPT_DATAFIM = justificativa.JUSPT_DATAFIM,
                JUSPT_DATAINICIO = justificativa.JUSPT_DATAINICIO,
                JUSPT_DATARECEBIMENTO = justificativa.JUSPT_DATARECEBIMENTO,
                JUSPT_ID = justificativa.JUSPT_ID,
                JUSPT_OBSERVACAO = justificativa.JUSPT_OBSERVACAO,
                JUSPT_REGUSER = justificativa.JUSPT_REGUSER,
                MTVJUS_ID = justificativa.MTVJUS_ID.Value,
                MTVJUS_NOME = justificativa.pon_motivojustificativa.MTVJUS_NOME,
                VNC_ID = justificativa.VNC_ID,
                VNC_NOME = justificativa.ap_vinculo.VNC_ID + " - " + justificativa.ap_vinculo.ap_cargo.CRG_NOME + " - " + justificativa.ap_vinculo.ap_vinculotipo.VNCTP_DESCRICAO


            };
        }

        public List<JustificativaPontoDomainModel> GetJustificativaByVinculo(int vnc)
        {
            throw new NotImplementedException();
        }

        public List<MotivoJustificativaDomainModel> GetMotivoJustificativa()
        {
            return _motivoJustificativaRepository.GetAll(x => x.MTVJUS_STATUS == "A").OrderBy(x => x.MTVJUS_NOME).Select(x => new MotivoJustificativaDomainModel
            {
                MTVJUS_ID = x.MTVJUS_ID,
                MTVJUS_NOME = x.MTVJUS_NOME,
                MTVJUS_REGUSER = x.MTVJUS_REGUSER
            }).ToList();
        }

        public MotivoJustificativaDomainModel GetMotivoJustificativaById(int id)
        {
            pon_motivojustificativa motivo = _motivoJustificativaRepository.SingleOrDefault(x => x.MTVJUS_STATUS == "A" && x.MTVJUS_ID == id);
            return new MotivoJustificativaDomainModel { MTVJUS_ID = motivo.MTVJUS_ID, MTVJUS_NOME = motivo.MTVJUS_NOME, MTVJUS_REGUSER = motivo.MTVJUS_REGUSER };
        }

        public bool Salvar()
        {

            bool result = true;
            try
            {
                _justificativaRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public int TotalRegistro()
        {
            throw new NotImplementedException();
        }

        public int TotalRegistroByFuncionario(int id)
        {
            throw new NotImplementedException();
        }
    }
}
