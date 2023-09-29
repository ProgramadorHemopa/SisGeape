using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class ConselhoClasseBusiness : IConselhoClasseBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ConselhoClasseRepository _conselhoClasseRepository;
        private readonly ConselhoRepository _conselhoRepository;
        private readonly VinculoRepository _vinculoRepository;

        public ConselhoClasseBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _conselhoClasseRepository = new ConselhoClasseRepository(_unitOfWork);
            _conselhoRepository = new ConselhoRepository(_unitOfWork);
            _vinculoRepository = new VinculoRepository(_unitOfWork);
        }
        public bool AddUpdateConselho(ConselhoDomainModel domainModel)
        {
            try
            {
                if (domainModel.CON_ID == 0)
                {
                    ap_conselho _conselho = new ap_conselho()
                    {
                        CON_NOME = domainModel.CON_NOME.ToUpper(),
                        CON_REGDATE = DateTime.Now,
                        CON_REGUSER = domainModel.CON_REGUSER,
                        CRG_ID = domainModel.CRG_ID,
                        CON_STATUS = "A",


                    };
                    _conselhoRepository.Insert(_conselho);
                    return true;

                }
                else
                {
                    ap_conselho _conselho = _conselhoRepository.SingleOrDefault(x => x.CON_STATUS == "A" && x.CON_ID == domainModel.CON_ID);
                    _conselho.CON_NOME = domainModel.CON_NOME.ToUpper();
                    _conselho.CON_REGDATE = DateTime.Now;
                    _conselho.CON_REGUSER = domainModel.CON_REGUSER;
                    _conselho.CRG_ID = domainModel.CRG_ID;


                    _conselhoRepository.Update(_conselho);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool AddUpdateConselhoClasse(ConselhoClasseDomainModel domainModel)
        {
            try
            {
                if (domainModel.CONCLA_ID == 0)
                {
                    ap_conselhoclasse conselhoclasse = new ap_conselhoclasse()
                    {
                        CONCLA_DATAQUITACAO = Convert.ToDateTime(domainModel.CONCLA_DATAQUITACAO),
                        CONCLA_REFANO = domainModel.CONCLA_REFANO,
                        CONCLA_REGDATE = DateTime.Now,
                        CONCLA_REGUSER = domainModel.CONCLA_REGUSER,
                        CON_ID = domainModel.CON_ID,
                        VNC_ID = domainModel.VNC_ID,
                        CONCLA_DATARECEBIMENTO = Convert.ToDateTime(domainModel.CONCLA_DATARECEBIMENTO),
                        CONCLA_STATUS = "A",

                    };

                    _conselhoClasseRepository.Insert(conselhoclasse);
                    return true;
                }
                else
                {
                    ap_conselhoclasse conselhoclasse = _conselhoClasseRepository.SingleOrDefault(x => x.CONCLA_STATUS == "A" && x.CONCLA_ID == domainModel.CONCLA_ID);

                    conselhoclasse.CONCLA_DATAQUITACAO = Convert.ToDateTime(domainModel.CONCLA_DATAQUITACAO);
                    conselhoclasse.CONCLA_REFANO = domainModel.CONCLA_REFANO;
                    conselhoclasse.CONCLA_REGDATE = DateTime.Now;
                    conselhoclasse.CONCLA_REGUSER = domainModel.CONCLA_REGUSER;
                    conselhoclasse.CON_ID = domainModel.CON_ID;

                    _conselhoClasseRepository.Update(conselhoclasse);
                    return true;
                }
            }
            catch { return false; }
        }

        public List<ConselhoDomainModel> ddlConselhoByCargo(int cargo)
        {
            return _conselhoRepository.GetAll(x => x.CON_STATUS == "A" && x.CRG_ID == cargo).AsParallel().ToList().Select(x => new ConselhoDomainModel
            {
                CON_ID = x.CON_ID,
                CON_NOME = x.CON_NOME,
                CON_REGUSER = x.CON_REGUSER,
                CRG_ID = x.CRG_ID,
                CRG_NOME = x.ap_cargo.CRG_NOME
            }).ToList();
        }

        public bool DeleteConselho(ConselhoDomainModel domainModel)
        {
            try
            {
                ap_conselho _conselho = _conselhoRepository.SingleOrDefault(x => x.CON_STATUS == "A" && x.CON_ID == domainModel.CON_ID);
                _conselho.CON_REGDATE = DateTime.Now;
                _conselho.CON_REGUSER = domainModel.CON_REGUSER;
                _conselho.CON_STATUS = "I";


                _conselhoRepository.Update(_conselho);
                return true;
            }
            catch { return false; }
        }

        public bool DeleteConselhoClasse(ConselhoClasseDomainModel domainModel)
        {
            try
            {
                ap_conselhoclasse conselhoclasse = _conselhoClasseRepository.SingleOrDefault(x => x.CONCLA_STATUS == "A" && x.CONCLA_ID == domainModel.CONCLA_ID);


                conselhoclasse.CONCLA_REGDATE = DateTime.Now;
                conselhoclasse.CONCLA_REGUSER = domainModel.CONCLA_REGUSER;
                conselhoclasse.CONCLA_STATUS = "I";

                _conselhoClasseRepository.Update(conselhoclasse);
                return true;
            }
            catch { return false; }
        }

        public List<ConselhoDomainModel> GetConselho()
        {
            return _conselhoRepository.GetAll(x => x.CON_STATUS == "A").AsParallel().ToList().Select(x => new ConselhoDomainModel
            {
                CON_ID = x.CON_ID,
                CON_NOME = x.CON_NOME,
                CON_REGUSER = x.CON_REGUSER,
                CRG_ID = x.CRG_ID,
                CRG_NOME = x.ap_cargo.CRG_NOME
            }).ToList();
        }

        public ConselhoDomainModel GetConselhoById(int id)
        {
            ap_conselho conselho = _conselhoRepository.SingleOrDefault(x => x.CON_STATUS == "A" && x.CON_ID == id);


            if (conselho != null)
            {
                ConselhoDomainModel domainModel = new ConselhoDomainModel
                {
                    CON_ID = conselho.CON_ID,
                    CON_NOME = conselho.CON_NOME,
                    CON_REGUSER = conselho.CON_REGUSER,
                    CRG_ID = conselho.CRG_ID,
                    CRG_NOME = conselho.ap_cargo.CRG_NOME
                };
                return domainModel;
            }
            else
                throw new Exception();
        }

        public List<ConselhoClasseDomainModel> GetConselhoClasse()
        {
            throw new NotImplementedException();
        }

        public ConselhoClasseDomainModel GetConselhoClasseById(int id)
        {
            ap_conselhoclasse conselho = _conselhoClasseRepository.SingleOrDefault(x => x.CONCLA_STATUS == "A" && x.CONCLA_ID == id);


            if (conselho != null)
            {
                ConselhoClasseDomainModel domainModel = new ConselhoClasseDomainModel
                {
                    CON_ID = conselho.CON_ID.Value,
                    CONCLA_DATAQUITACAO = conselho.CONCLA_DATAQUITACAO.Value.ToString("yyyy-MM-dd"),
                    CONCLA_DATARECEBIMENTO = conselho.CONCLA_DATARECEBIMENTO.Value.ToString("yyyy-MM-dd"),
                    CONCLA_ID = conselho.CONCLA_ID,
                    CONCLA_REFANO = conselho.CONCLA_REFANO,
                    CONSELHO = conselho.ap_conselho.CON_NOME,
                    VNC_ID = conselho.VNC_ID.Value

                };
                return domainModel;
            }
            else
                throw new Exception();
        }

        public List<ConselhoClasseDomainModel> GetConselhoClasseByVinculo(int vnc)
        {
            throw new NotImplementedException();
        }

        public ConselhoClasseVinculoDomainModel GetFuncionarioConselhoByVinculo(int vnc)
        {

            byte[] imagemByteDados = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));
            // List<ap_conselhoclasse> conselhoclasse = _conselhoClasseRepository.GetAll(x => x.CONCLA_STATUS == "A" && x.VNC_ID == vnc).AsParallel().ToList().Select;

            ap_vinculo vinculo = _vinculoRepository.SingleOrDefault(x => x.VNC_STATUS == "A" && x.VNC_ID == vnc);
            return new ConselhoClasseVinculoDomainModel
            {
                VNC_ID = vinculo.VNC_ID,
                CARGO = vinculo.ap_cargo.CRG_NOME,
                FUN_FOTO = vinculo.ap_funcionario.ap_funcionarioxfoto.AsParallel().Where(x => x.FUNFT_STATUS == "A").Count() > 0 ? vinculo.ap_funcionario.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                ConselhoClasse = vinculo.ap_conselhoclasse.Where(x => x.CONCLA_STATUS == "A").ToList().Select(x => new ConselhoClasseDomainModel { CONCLA_DATAQUITACAO = x.CONCLA_DATAQUITACAO.Value.ToShortDateString(), CONCLA_ID = x.CONCLA_ID, CONCLA_REFANO = x.CONCLA_REFANO, CON_ID = x.CON_ID.Value, VNC_ID = x.VNC_ID.Value, CONCLA_DATARECEBIMENTO = x.CONCLA_DATARECEBIMENTO.Value.ToShortDateString(), CONSELHO = x.ap_conselho.CON_NOME }).ToList(),
                FUN_ID = vinculo.FUN_ID.Value,
                NOME = vinculo.ap_funcionario.FUN_NOME,
                MATRICULA = vinculo.ap_funcionario.FUN_MATRICULA,
                SITUACAO = vinculo.ap_vinculosituacao.VNCST_DESCRICAO,
                TIPO = vinculo.ap_vinculotipo.VNCTP_DESCRICAO,
                CRG_ID = vinculo.CRG_ID.Value,
                UNIDADE = vinculo.ap_vinculoxunidade.AsParallel().Any(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null) ? vinculo.ap_vinculoxunidade.AsParallel().FirstOrDefault(x => x.VNCU_STATUS == "A" && x.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA : "",

            };

        }

        public List<PesquisaConselhoClasseListDomainModel> GetPesquisaConselho(PesquisaConselhoClasseDomainModel domainModel)
        {
            List<PesquisaConselhoClasseListDomainModel> listDomain = new List<PesquisaConselhoClasseListDomainModel>();


            var query = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo && x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null) && x.ap_cargo.ap_conselho.Any(z => z.CON_STATUS == "A"));

            if (domainModel.IDCARGO != null)
                query = query.Where(x => x.CRG_ID == domainModel.IDCARGO);
            if (domainModel.IDUNIDADE != null)
                query = query.AsParallel().ToList().Where(x => x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.UND_ID == domainModel.IDUNIDADE));
            if (domainModel.MATRICULA != null)
                query = query.AsParallel().ToList().Where(x => x.ap_funcionario.FUN_STATUS == "A" && x.ap_funcionario.FUN_MATRICULA == domainModel.MATRICULA);
            if (domainModel.NOME != null)
                query = query.AsParallel().ToList().Where(x => x.ap_funcionario.FUN_STATUS == "A" && x.ap_funcionario.FUN_NOME.ToUpper().Contains(domainModel.NOME.ToUpper()));

            if (domainModel.SITUACAO == 0)//PENDENTE
                query = query.AsParallel().ToList().Where(x => !(x.ap_conselhoclasse.Any(z => z.CONCLA_REFANO == domainModel.ANO && z.CONCLA_STATUS == "A")));
            else if (domainModel.SITUACAO == 1)// QUITE
                query = query.AsParallel().ToList().Where(x => x.ap_conselhoclasse.Any(z => z.CONCLA_REFANO == domainModel.ANO && z.CONCLA_STATUS == "A"));



            listDomain = query.ToList().Select(x => new PesquisaConselhoClasseListDomainModel
            {
                CARGO = x.ap_cargo.CRG_NOME,
                VNC_ID = x.VNC_ID,
                UNIDADE = x.ap_vinculoxunidade.FirstOrDefault(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA,
                MATRICULA = x.ap_funcionario.FUN_MATRICULA,
                NOME = x.ap_funcionario.FUN_NOME,
                SITUACAO = x.ap_conselhoclasse.Any(z => z.CONCLA_STATUS == "A" && z.CONCLA_REFANO == domainModel.ANO) ? 1 : 0

            }).ToList();

            return listDomain;

        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _conselhoClasseRepository.Save();
                return result;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return !result;
            }
        }

        public int TotalRegistroConselho()
        {
            throw new NotImplementedException();
        }

        public int TotalRegistroConselhoClasse()
        {
            throw new NotImplementedException();
        }
    }
}
