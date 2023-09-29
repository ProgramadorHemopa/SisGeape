using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class PontoImportacaoBusiness : IPontoImportacaoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PontoImportacaoRepository _importacaoRepository;
        private readonly FuncionarioRepository _funcionarioRepository;

        //private PontoSecullum4Entities db = new PontoSecullum4Entities();



        public PontoImportacaoBusiness(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _importacaoRepository = new PontoImportacaoRepository(_unitOfWork);
            _funcionarioRepository = new FuncionarioRepository(_unitOfWork);
        }

        protected int GetFuncionario(string pis)
        {
            try
            {
                int id = _funcionarioRepository.SingleOrDefault(x => x.FUN_STATUS == "A" && x.FUN_PIS == pis).FUN_ID;
                return id;
            }
            catch { return 0; }
        }

        public bool AddImportacao(PontoImportacaoDomainModel domainModel)
        {
            try
            {
                pon_pontoimportacao importacao = new pon_pontoimportacao();
                importacao.PONIMP_DATAINICIO = domainModel.PONIMP_DATAINICIO;
                importacao.PONIMP_DATAFIM = domainModel.PONIMP_DATAFIM;
                importacao.PONIMP_OBSERVACAO = domainModel.PONIMP_OBSERVACAO;
                importacao.PONEQP_ID = domainModel.PONEQP_ID;
                importacao.PONIMP_REGUSER = domainModel.PONIMP_REGUSER;
                importacao.PONIMP_STATUS = "A";
                importacao.PONIMP_TOTAL = domainModel.pontos.Count;
                if (domainModel.pontos.Count > 0)
                {
                    foreach (var item in domainModel.pontos.ToList())
                    {
                        pon_ponto ponto = new pon_ponto();
                        ponto.PON_DATA = item.PON_DATA;
                        ponto.FUN_PIS = item.IDENTIFICADOR_FUNCIONARIO;
                        ponto.FUN_MATRICULA = item.MATRICULA_FUNCIONARIO;
                        ponto.PON_ENTRADA1 = item.PON_ENTRADA1;
                        ponto.PON_ENTRADA2 = item.PON_ENTRADA2;
                        ponto.PON_SAIDA1 = item.PON_SAIDA1;
                        ponto.PON_SAIDA2 = item.PON_SAIDA2;
                        ponto.FUN_ID = item.FUN_ID.HasValue ? item.FUN_ID : GetFuncionario(item.IDENTIFICADOR_FUNCIONARIO);
                        importacao.pon_ponto.Add(ponto);
                    }

                    _importacaoRepository.Insert(importacao);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public List<PontoImportacaoDomainModel> GetAll()
        {
            return _importacaoRepository.GetAll(x => x.PONIMP_STATUS == "A").Select(x => new PontoImportacaoDomainModel
            {
                PONEQP_ID = x.PONEQP_ID,
                PONIMP_DATAFIM = x.PONIMP_DATAFIM,
                PONIMP_DATAINICIO = x.PONIMP_DATAINICIO,
                PONIMP_ID = x.PONIMP_ID,
                PONIMP_OBSERVACAO = x.PONIMP_OBSERVACAO,
                PONIMP_REGUSER = x.PONIMP_REGUSER,
                PONIMP_TOTAL = x.PONIMP_TOTAL
            }).ToList();
        }

        public bool Salvar()
        {
            bool result = true;
            try
            {
                _importacaoRepository.Save();
                return result;
            }
            catch
            {

                return !result;
            }
        }

        public bool ImportarBatidasPorArquivoTxt(Stream arquivo, DateTime? inicio, DateTime? fim, string observacao)
        {
            FuncionarioBusiness funcionario = new FuncionarioBusiness(_unitOfWork);
            using (StreamReader sr = new StreamReader(arquivo))
            {

                string linha;
                List<PontoDomainModel> pontosNaoEncontrados = new List<PontoDomainModel>();
                PontoImportacaoDomainModel pontoImportacaoDomain = new PontoImportacaoDomainModel()
                {
                    PONIMP_DATAFIM = fim,
                    PONIMP_DATAINICIO = inicio,
                    PONIMP_OBSERVACAO = observacao,
                    PONEQP_ID = 3,
                };

                while ((linha = sr.ReadLine()) != null)
                {
                    if (Convert.ToInt32(linha.Substring(9, 1).ToString()) == 3)
                    {
                        string dateTimeString = Regex.Replace(linha.Substring(10, 12), @"[^\u0000-\u007F]", string.Empty);
                        string inputFormat = "ddMMyyyyHHmm";
                        var dateTime = DateTime.ParseExact(dateTimeString, inputFormat, System.Globalization.CultureInfo.InvariantCulture);
                        var pis = linha.Substring(22, 12);
                        pis = pis.StartsWith("0") ? pis.Substring(1, 11) : pis.Trim();
                        var dadosFuncionario = funcionario.GetFuncionarioByPIS(pis);
                        if (dadosFuncionario.Count > 0 && (dateTime.Date >= inicio && dateTime.Date <= fim))
                        {
                            var regPonto = new PontoDomainModel()
                            {
                                PON_DATA = dateTime,
                                IDENTIFICADOR_FUNCIONARIO = dadosFuncionario.FirstOrDefault().FUN_PIS,
                                MATRICULA_FUNCIONARIO = dadosFuncionario.FirstOrDefault().FUN_MATRICULA,
                                FUN_ID = dadosFuncionario.FirstOrDefault().FUN_ID

                            };
                            pontoImportacaoDomain.pontos.Add(regPonto);
                        }

                    }

                }
                return AddImportacao(pontoImportacaoDomain);
            }
        }
    }
}
