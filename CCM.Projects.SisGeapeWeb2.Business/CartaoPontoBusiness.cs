using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Entities;
using CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface;
using CCM.Projects.SisGeapeWeb2.Repository.Repository;
using FluentDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business
{
    public class CartaoPontoBusiness : ICartaoPontoBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PontoRepository _pontoRepository;
        private readonly VinculoRepository _vinculoRepository;
        private readonly JustificativaPontoRepository _justificativaRepository;
        private readonly UnidadeRepository _unidadeRepository;
        private readonly FeriasRepository _feriasRepository;
        private readonly FeriadoRepository _feriadoRepository;
        private readonly FuncionarioRepository _funcionarioRepository;

        private readonly byte[] imagemByteDados = System.IO.File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath(@"~/Imagem/DefaultServidor.png"));

        public CartaoPontoBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pontoRepository = new PontoRepository(_unitOfWork);
            _vinculoRepository = new VinculoRepository(_unitOfWork);
            _justificativaRepository = new JustificativaPontoRepository(_unitOfWork);
            _unidadeRepository = new UnidadeRepository(_unitOfWork);
            _feriasRepository = new FeriasRepository(_unitOfWork);
            _feriadoRepository = new FeriadoRepository(_unitOfWork);
            _funcionarioRepository = new FuncionarioRepository(_unitOfWork);
        }



        public List<PesquisaCartaoPontoDomainModel> GetPesquisa(PesquisaCartaoPontoDomainModel pesquisa)
        {
            List<PesquisaCartaoPontoDomainModel> listDomain = new List<PesquisaCartaoPontoDomainModel>();
            if (pesquisa.IDCARGO == null && pesquisa.IDUNIDADE == null)
            {

                var query = _funcionarioRepository.GetAll(x => x.FUN_STATUS == "A" && (x.FUN_MATRICULA != "" && x.FUN_MATRICULA != null));
                if (pesquisa.MATRICULA != null)
                    query = query.AsParallel().Where(x => x.FUN_MATRICULA.Contains(pesquisa.MATRICULA));
                if (pesquisa.NOME != null)
                    query = query.AsParallel().Where(x => x.FUN_NOME.ToUpper().Contains(pesquisa.NOME.ToUpper()));

                listDomain = query.Count() > 0 ? query.ToList().Select(x => new PesquisaCartaoPontoDomainModel
                {
                    CARGO = x.ap_vinculo.Any(z => z.VNC_STATUS == "A") ? x.ap_vinculo.OrderByDescending(z => z.VNC_ID).FirstOrDefault().ap_cargo.CRG_NOME : "",
                    UNIDADE = x.ap_vinculo.Any(z => z.VNC_STATUS == "A" && z.ap_vinculoxunidade.Any(s => s.VNCU_STATUS == "A")) ?
                    x.ap_vinculo.OrderByDescending(z => z.VNC_ID).Where(p => p.VNC_STATUS == "A" && p.ap_vinculoxunidade.Any(s => s.VNCU_STATUS == "A")).FirstOrDefault().ap_vinculoxunidade.OrderByDescending(o => o.UND_ID).FirstOrDefault().ap_unidade.UND_SIGLA : "",
                    MATRICULA = x.FUN_MATRICULA,
                    NOME = x.FUN_NOME,
                    FUN_ID = x.FUN_ID

                }).ToList() : null;

            }
            else
            {
                var query = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.cedido) && x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null));

                if (pesquisa.IDCARGO != null)
                    query = query.Where(x => x.CRG_ID == pesquisa.IDCARGO);
                if (pesquisa.IDUNIDADE != null)
                    query = query.AsParallel().ToList().Where(x => x.ap_vinculoxunidade.Any(z => z.VNCU_STATUS == "A" && z.UND_ID == pesquisa.IDUNIDADE));
                if (pesquisa.MATRICULA != null)
                    query = query.AsParallel().ToList().Where(x => x.ap_funcionario.FUN_STATUS == "A" && x.ap_funcionario.FUN_MATRICULA.Contains(pesquisa.MATRICULA));
                if (pesquisa.NOME != null)
                    query = query.AsParallel().ToList().Where(x => x.ap_funcionario.FUN_STATUS == "A" && x.ap_funcionario.FUN_NOME.ToUpper().Contains(pesquisa.NOME.ToUpper()));

                listDomain = query.ToList().Select(x => new PesquisaCartaoPontoDomainModel
                {
                    CARGO = x.ap_cargo.CRG_NOME,
                    UNIDADE = x.ap_vinculoxunidade.FirstOrDefault(z => z.VNCU_STATUS == "A" && z.VNCU_DATAFIM == null).ap_unidade.UND_SIGLA,
                    MATRICULA = x.ap_funcionario.FUN_MATRICULA,
                    NOME = x.ap_funcionario.FUN_NOME,
                    FUN_ID = x.FUN_ID

                }).ToList();

            }
            return listDomain;

        }

        public List<string> ddlAno()
        {
            int anocorrente = DateTime.Today.Year;
            List<string> lstAno = new List<string>();
            for (int i = anocorrente; i >= anocorrente - 20; i--)
            {
                lstAno.Add(Convert.ToString(i));
            }
            return lstAno;
        }

        public List<string> ddlMes()
        {
            var culture = new System.Globalization.CultureInfo("pt-br");
            List<string> lstMes = new List<string>();
            for (int i = 0; i < 12; i++)
            {
                lstMes.Add(Convert.ToString(culture.DateTimeFormat.MonthNames[i]));
            }
            return lstMes;
        }

        public List<CartaoPontoUnidadeDomainModel> GetPointCardByUnidade(UnidadeCartaoPontoDomainModel domainModel)
        {
            List<CartaoPontoUnidadeDomainModel> cartaoPonto = new List<CartaoPontoUnidadeDomainModel>();


            if (domainModel != null)
            {
                DateTime dataFim = domainModel.PERIODO.Value.AddMonths(1).AddDays(-1);
                var qList = _vinculoRepository.GetAll(x => x.VNC_STATUS == "A" && x.VNC_DEMISSAO == null && (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.cedido || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio) && x.ap_vinculoxunidade.Any(z => z.UND_ID == domainModel.UND_ID && z.VNCU_DATAFIM == null && z.VNCU_STATUS == "A")).ToList();
                string und = _unidadeRepository.SingleOrDefault(x => x.UND_ID == domainModel.UND_ID).UND_SIGLA;

                foreach (var funcionario in qList.Select(x => new { x.ap_funcionario }).Distinct())
                {
                    int cont = 0;
                    DateTime data;
                    do
                    {
                        var falta = "";
                        var entrada = "";
                        var saida = "";
                        var dif = "";
                        var atraso = "";
                        var justificativa = "";


                        data = domainModel.PERIODO.Value.AddDays(cont++);



                        var culture = new System.Globalization.CultureInfo("pt-br");
                        var diasemana = culture.DateTimeFormat.DayNames[(int)data.DayOfWeek];
                        var batidasDia = _pontoRepository.GetAll(x => (x.FUN_ID == funcionario.ap_funcionario.FUN_ID || x.FUN_MATRICULA == funcionario.ap_funcionario.FUN_MATRICULA) && x.PON_DATA.Value.Year == data.Year
                                    && x.PON_DATA.Value.Month == data.Month
                                    && x.PON_DATA.Value.Day == data.Day).AsParallel().Select(x => new { x.PON_DATA }).ToList();

                        var JustificativaList = _justificativaRepository.GetAll(x => x.JUSPT_STATUS == "A" && x.ap_vinculo.FUN_ID == funcionario.ap_funcionario.FUN_ID && x.JUSPT_DATAINICIO <= data.Date && x.JUSPT_DATAFIM >= data.Date)/*.Where(x => x.VNC_ID == vinculo).Where(x => x.JUSPT_DATAINICIO.Value.Year >= data.Year && x.JUSPT_DATAFIM.Value.Year <= data.Year
                              && x.JUSPT_DATAINICIO.Value.Month >= data.Month && x.JUSPT_DATAFIM.Value.Month <= data.Month
                              && x.JUSPT_DATAINICIO.Value.Day >= data.Day && x.JUSPT_DATAFIM.Value.Day <= data.Day)*/.ToList().AsParallel().Select(x => new { x.pon_motivojustificativa.MTVJUS_NOME });



                        if (batidasDia.Count > 0)
                        {
                            entrada = batidasDia.Min(x => x.PON_DATA).Value.TimeOfDay.ToString();
                            saida = batidasDia.Max(x => x.PON_DATA).Value.TimeOfDay.ToString();


                            //VERIFICA SE A ENTRADA É IGUAL A SAÍDA
                            if (DateTime.Compare(DateTime.Parse(entrada), DateTime.Parse(saida)) == 0)
                            {
                                saida = "";
                                falta = "X";
                            }
                            var min = TimeSpan.Parse(entrada);



                            //DIFERENCA ENTRE ENTRADA E SAIDA
                            if (entrada != "" && saida != "")
                            {
                                if ((TimeSpan.Compare(TimeSpan.Parse(entrada), TimeSpan.Parse(saida)) != 0))
                                {
                                    dif = (TimeSpan.Parse(saida) - TimeSpan.Parse(entrada)).ToString();
                                    // TimeSpan.Compare(TimeSpan.Parse(entrada), TimeSpan.Parse(saida)).ToString();
                                }

                                if (TimeSpan.Parse(saida).Subtract(TimeSpan.Parse(entrada)).TotalMinutes < 10)
                                {
                                    saida = "";
                                    dif = "";
                                    falta = "X";
                                }

                            }

                            ////vERIFICA O HORARIO ATIVO PARA O SERVIDOR PELA DATA DO REGISTRO DE PONTO
                            //var horario = db.horarioxvinculo.Where(x => x.HORVNC_STATUS == "A" && x.HORVNC_DATAINICIO <= data && (x.HORVNC_DATAFIM >= data || x.HORVNC_DATAFIM == null));
                            //if (horario.Count() > 0 && dif != "")
                            //{
                            //    var cargaHoraria = (TimeSpan.Parse(horario.FirstOrDefault().horario.HOR_SAIDA) - TimeSpan.Parse(horario.FirstOrDefault().horario.HOR_ENTRADA)).ToString();

                            //    if (TimeSpan.Compare(TimeSpan.Parse(cargaHoraria), TimeSpan.Parse(dif)) == 1)
                            //    {
                            //        atraso = (TimeSpan.Parse(cargaHoraria) - TimeSpan.Parse(dif)).ToString();
                            //    }

                            //}



                        }
                        else
                        {
                            if ((int)data.DayOfWeek == 6 || (int)data.DayOfWeek == 0)
                            {
                                falta = "";
                            }
                            else
                                falta = "X";
                        }




                        if (JustificativaList.Count() > 0)
                        {
                            justificativa = JustificativaList.FirstOrDefault().MTVJUS_NOME;

                            if (falta == "X" && justificativa != "")
                            {
                                falta = "";
                            }
                        }






                        CartaoPontoUnidadeDomainModel Batida = new CartaoPontoUnidadeDomainModel()
                        {

                            FUN_FOTO = funcionario.ap_funcionario.ap_funcionarioxfoto.AsParallel().Where(x => x.FUNFT_STATUS == "A").Count() > 0 ? funcionario.ap_funcionario.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                            FUN_ID = funcionario.ap_funcionario.FUN_ID,
                            UNIDADE = und,
                            NOME = funcionario.ap_funcionario.FUN_NOME,
                            MATRICULA = funcionario.ap_funcionario.FUN_MATRICULA,
                            DATA = data,
                            DIA = diasemana.ToUpper(),
                            VNC_ID = 0
                            ,

                            ENTRADA1 = entrada,
                            ATRASO = atraso
                            ,
                            DIFERENCA = dif
                            ,
                            FALTA = falta
                            ,
                            JUSTIFICATIVA = justificativa
                            ,
                            SAIDA1 = saida

                        };

                        cartaoPonto.Add(Batida);

                    } while (data < dataFim.Date);


                }
            }
            else
                throw new InvalidOperationException();


            return cartaoPonto;
        }




        public FuncionarioCartaoPontoDomainModel GetPointCardByVnc(int funcionario, int vinculo, DateTime? dataInicio, DateTime? dataFim)
        {
            //List<CartaoPontoDomainModel> listBatidas = new List<CartaoPontoDomainModel>();

            //ap_vinculo vnc = _vinculoRepository.SingleOrDefault(x => x.VNC_STATUS == "A" && x.FUN_ID == funcionario && (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.cedido));
            //FuncionarioCartaoPontoDomainModel funcionarioCartaoPonto = new FuncionarioCartaoPontoDomainModel
            //{

            //    FUN_FOTO = vnc.ap_funcionario.ap_funcionarioxfoto.AsParallel().Where(x => x.FUNFT_STATUS == "A").Count() > 0 ? vnc.ap_funcionario.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
            //    FUN_ID = vnc.FUN_ID.Value,
            //    NOME = vnc.ap_funcionario.FUN_NOME,
            //    MATRICULA = vnc.ap_funcionario.FUN_MATRICULA,
            //};

            //if (dataInicio != null && dataFim != null)
            //{
            //    int cont = 0;
            //    DateTime data;
            //    do
            //    {
            //        var falta = "";
            //        var entrada = "";
            //        var saida = "";
            //        var dif = "";
            //        var atraso = "";
            //        var justificativa = "";


            //        data = dataInicio.Value.AddDays(cont++);



            //        var culture = new System.Globalization.CultureInfo("pt-br");
            //        var diasemana = culture.DateTimeFormat.DayNames[(int)data.DayOfWeek];
            //        var batidasDia = _pontoRepository.GetAll(x => x.FUN_ID == funcionario && x.PON_DATA.Value.Year == data.Year
            //                    && x.PON_DATA.Value.Month == data.Month
            //                    && x.PON_DATA.Value.Day == data.Day).Select(x => new { x.PON_DATA }).ToList();

            //        var JustificativaList = _justificativaRepository.GetAll(x => x.JUSPT_STATUS == "A" && x.ap_vinculo.FUN_ID == funcionario && x.JUSPT_DATAINICIO <= data.Date && x.JUSPT_DATAFIM >= data.Date)/*.Where(x => x.VNC_ID == vinculo).Where(x => x.JUSPT_DATAINICIO.Value.Year >= data.Year && x.JUSPT_DATAFIM.Value.Year <= data.Year
            //                  && x.JUSPT_DATAINICIO.Value.Month >= data.Month && x.JUSPT_DATAFIM.Value.Month <= data.Month
            //                  && x.JUSPT_DATAINICIO.Value.Day >= data.Day && x.JUSPT_DATAFIM.Value.Day <= data.Day)*/.ToList().Select(x => new { x.pon_motivojustificativa.MTVJUS_NOME });



            //        if (batidasDia.Count > 0)
            //        {
            //            entrada = batidasDia.Min(x => x.PON_DATA).Value.TimeOfDay.ToString();
            //            saida = batidasDia.Max(x => x.PON_DATA).Value.TimeOfDay.ToString();


            //            //VERIFICA SE A ENTRADA É IGUAL A SAÍDA
            //            if (DateTime.Compare(DateTime.Parse(entrada), DateTime.Parse(saida)) == 0)
            //            {
            //                saida = "";
            //                falta = "X";
            //            }
            //            var min = TimeSpan.Parse(entrada);



            //            //DIFERENCA ENTRE ENTRADA E SAIDA
            //            if (entrada != "" && saida != "")
            //            {
            //                if ((TimeSpan.Compare(TimeSpan.Parse(entrada), TimeSpan.Parse(saida)) != 0))
            //                {
            //                    dif = (TimeSpan.Parse(saida) - TimeSpan.Parse(entrada)).ToString();
            //                    // TimeSpan.Compare(TimeSpan.Parse(entrada), TimeSpan.Parse(saida)).ToString();
            //                }

            //                if (TimeSpan.Parse(saida).Subtract(TimeSpan.Parse(entrada)).TotalMinutes < 10)
            //                {
            //                    saida = "";
            //                    dif = "";
            //                    falta = "X";
            //                }

            //            }

            //            ////vERIFICA O HORARIO ATIVO PARA O SERVIDOR PELA DATA DO REGISTRO DE PONTO
            //            //var horario = db.horarioxvinculo.Where(x => x.HORVNC_STATUS == "A" && x.HORVNC_DATAINICIO <= data && (x.HORVNC_DATAFIM >= data || x.HORVNC_DATAFIM == null));
            //            //if (horario.Count() > 0 && dif != "")
            //            //{
            //            //    var cargaHoraria = (TimeSpan.Parse(horario.FirstOrDefault().horario.HOR_SAIDA) - TimeSpan.Parse(horario.FirstOrDefault().horario.HOR_ENTRADA)).ToString();

            //            //    if (TimeSpan.Compare(TimeSpan.Parse(cargaHoraria), TimeSpan.Parse(dif)) == 1)
            //            //    {
            //            //        atraso = (TimeSpan.Parse(cargaHoraria) - TimeSpan.Parse(dif)).ToString();
            //            //    }

            //            //}



            //        }
            //        else
            //        {
            //            if ((int)data.DayOfWeek == 6 || (int)data.DayOfWeek == 0)
            //            {
            //                falta = "";
            //            }
            //            else
            //                falta = "X";
            //        }




            //        if (JustificativaList.Count() > 0)
            //        {
            //            justificativa = JustificativaList.FirstOrDefault().MTVJUS_NOME;

            //            if (falta == "X" && justificativa != "")
            //            {
            //                falta = "";
            //            }
            //        }






            //        CartaoPontoDomainModel Batida = new CartaoPontoDomainModel()
            //        {
            //            DATA = data,
            //            DIA = diasemana.ToUpper(),
            //            VNC_ID = vnc.VNC_ID
            //            ,

            //            FUN_ID = funcionario,
            //            ENTRADA1 = entrada,
            //            ATRASO = atraso
            //            ,
            //            DIFERENCA = dif
            //            ,
            //            FALTA = falta
            //            ,
            //            JUSTIFICATIVA = justificativa
            //            ,
            //            SAIDA1 = saida

            //        };



            //        listBatidas.Add(Batida);

            //    } while (data < dataFim.Value.Date);

            //}

            //funcionarioCartaoPonto.CartaoPonto = listBatidas;
            //return funcionarioCartaoPonto;
            throw new NotImplementedException();
        }

        public int TotalRegistros()
        {
            throw new NotImplementedException();
        }

        public async Task<FuncionarioCartaoPontoDomainModel> BuscarCartaoPontoPorFuncionario(int id, DateTime? dataInicio, DateTime? dataFim)
        {
            List<CartaoPontoDomainModel> cartaoPonto = new List<CartaoPontoDomainModel>();
            var fun = await _vinculoRepository.FirstOrDefaultAsync(x => x.VNC_STATUS == "A" && x.ap_funcionario.FUN_STATUS == "A" && x.ap_funcionario.FUN_ID == id).ContinueWith((z) => new FuncionarioCartaoPontoDomainModel()
            {

                FUN_FOTO = z.Result.ap_funcionario.ap_funcionarioxfoto.AsParallel().Any(p => p.FUNFT_STATUS == "A") ? z.Result.ap_funcionario.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                FUN_ID = z.Result.FUN_ID.Value,
                NOME = z.Result.ap_funcionario.FUN_NOME,
                MATRICULA = z.Result.ap_funcionario.FUN_MATRICULA,
                TIPAGEM = z.Result.ap_funcionario.FUN_TIPOSANGUINEO.Value
            });


            if (dataInicio != null && dataFim != null)
            {
                int cont = 0;
                DateTime data;
                DateTime fimPeriodo = dataFim.Value.AddDays(1);

                var listBatidasFuncionario = await _pontoRepository.GetAllAsync(x => (x.FUN_ID == fun.FUN_ID || x.FUN_MATRICULA == fun.MATRICULA) && x.PON_DATA >= dataInicio && x.PON_DATA < fimPeriodo).ContinueWith((x) =>
                 x.Result.Select(z => new { PON_DATA = z.PON_DATA }).ToList()
                );

                var JustificativaList = await _justificativaRepository.GetAllAsync(x => x.JUSPT_STATUS == "A" && x.ap_vinculo.FUN_ID == fun.FUN_ID && ((x.JUSPT_DATAINICIO >= dataInicio && x.JUSPT_DATAFIM <= dataFim) || (x.JUSPT_DATAFIM >= dataInicio && x.JUSPT_DATAFIM <= dataFim))).ContinueWith((z) =>
                 z.Result.ToList().Select(x => new { x.JUSPT_DATAINICIO, x.JUSPT_DATAFIM, x.pon_motivojustificativa.MTVJUS_NOME, x.JUSPT_OBSERVACAO, x.JUSPT_ID })
                 );

                var feriasList = await _feriasRepository.GetAllAsync(x => x.FRS_STATUS == "A" && x.ap_vinculo.FUN_ID == fun.FUN_ID && ((x.FRS_DATA_INICIOGOZO >= dataInicio && x.FRS_DATA_INICIOGOZO <= dataFim) || (x.FRS_DATA_FIMGOZO >= dataInicio && x.FRS_DATA_FIMGOZO <= dataFim)))
                    .ContinueWith((z) => z.Result.ToList().Select(x => new { x.FRS_DATA_INICIOGOZO, x.FRS_DATA_FIMGOZO, x.FRS_OBSERVACAO, x.ap_vinculo.FUN_ID, x.FRS_ID, x.FRS_NUMPORTARIA, x.FRS_DATAPORTARIA }));

                var feriadoList = _feriadoRepository.GetAll(x => x.FER_DATA >= dataInicio && x.FER_DATA <= dataFim && x.FER_STATUS == "A");


                do
                {

                    CartaoPontoDomainModel Batida = new CartaoPontoDomainModel();
                    data = dataInicio.Value.AddDays(cont++);
                    Batida.DATA = data;
                    var batidasDia = listBatidasFuncionario.Where(x => x.PON_DATA.Value.Year == data.Year
                                    && x.PON_DATA.Value.Month == data.Month
                                    && x.PON_DATA.Value.Day == data.Day).ToList();

                    if (batidasDia.Count() > 0)
                    {
                        Batida.ENTRADA1 = batidasDia.Min(x => x.PON_DATA).Value.TimeOfDay;
                        //  if (batidasDia.Any(x => x.PON_DATA.Value.TimeOfDay > Batida.ENTRADA1))
                        if (batidasDia.Any(x => x.PON_DATA.Value.Subtract(Batida.ENTRADA1.Value).TimeOfDay.TotalMinutes > 15))
                            Batida.SAIDA1 = batidasDia.Where(x => x.PON_DATA.Value.TimeOfDay > Batida.ENTRADA1).Max(x => x.PON_DATA).Value.TimeOfDay;
                    }

                    if (feriasList.Any(x => x.FRS_DATA_INICIOGOZO <= data && x.FRS_DATA_FIMGOZO >= data))
                    {
                        var justificativaFerias = feriasList.FirstOrDefault(x => x.FRS_DATA_INICIOGOZO <= data && x.FRS_DATA_FIMGOZO >= data);
                        Batida.FERIAS_OBS = "Ausência no periodo de " + justificativaFerias.FRS_DATA_INICIOGOZO.Value.ToShortDateString() + " até " + justificativaFerias.FRS_DATA_FIMGOZO.Value.ToShortDateString() + " - Motivo: FÉRIAS - Nº Portaria: " + justificativaFerias.FRS_NUMPORTARIA + " - Data Portaria: " + (justificativaFerias.FRS_DATAPORTARIA.HasValue ? justificativaFerias.FRS_DATAPORTARIA.Value.ToShortDateString() : " Não informada ") + " - " + justificativaFerias.FRS_OBSERVACAO;
                        Batida.FERIAS = "FÉRIAS";
                        Batida.FRS_ID = justificativaFerias.FRS_ID;
                    }

                    if (JustificativaList.Any(x => x.JUSPT_DATAINICIO <= data && x.JUSPT_DATAFIM >= data))
                    {
                        var justPonto = JustificativaList.FirstOrDefault(x => x.JUSPT_DATAINICIO <= data && x.JUSPT_DATAFIM >= data);
                        Batida.JUSTIFICATIVAPONTO_OBS = "Ausência no periodo de " + justPonto.JUSPT_DATAINICIO.Value.ToShortDateString() + " até " + justPonto.JUSPT_DATAFIM.Value.ToShortDateString() + " - Motivo: " + justPonto.MTVJUS_NOME + " - " + justPonto.JUSPT_OBSERVACAO;
                        Batida.JUSTIFICATIVA = justPonto.MTVJUS_NOME;
                        Batida.JUSTP_ID = justPonto.JUSPT_ID;
                    }

                    if (feriadoList.Any(x => x.FER_DATA.Value.Date == data))
                    {
                        var justFeriado = feriadoList.FirstOrDefault(x => x.FER_DATA.Value.Date == data);
                        Batida.FERIADO_OBS = "Ausência no dia de " + justFeriado.FER_DATA.Value.ToShortDateString() + "  - Motivo: " + DescriptionExtension.Descricao((TipoFeriado)justFeriado.FER_TIPO) + " - " + justFeriado.FER_DESCRICAO;
                        Batida.FERIADO = justFeriado.FER_DESCRICAO;
                    }


                    cartaoPonto.Add(Batida);

                } while (data < dataFim.Value.Date);
                fun.CartaoPonto = cartaoPonto;
            }

            return fun;
        }

        public async Task<List<UnidadeCartaoPontoDomainModel>> BuscarCartaoPontoPorUnidade(int und, DateTime? Periodo)
        {
            DateTime dataFim = Periodo.Value.LastDayOfMonth();
            DateTime FimPeriodo = Periodo.Value.AddMonths(1);
            List<UnidadeCartaoPontoDomainModel> ListCartaoUnidade = new List<UnidadeCartaoPontoDomainModel>();
            List<ap_unidade> resultpesq = new List<ap_unidade>();


            //Unidade HEMOCENTROCoordenador
            if (und == 76)
            {
                resultpesq = _unidadeRepository.GetAllAsync(x => x.UND_STATUS == "A" && x.UND_DATAFIM == null && x.UND_DIRETORIO != null && x.UND_NIVEL != 5 && x.UND_NIVEL != 6).Result.ToList();

            }
            else
            {
                resultpesq = _unidadeRepository.GetAllAsync(x => x.UND_ID == und).Result.ToList();
            }

            var feriadoList = _feriadoRepository.GetAll(x => x.FER_DATA >= Periodo && x.FER_DATA <= dataFim && x.FER_STATUS == "A");


            foreach (ap_unidade dadosUnidade in resultpesq)
            {


                UnidadeCartaoPontoDomainModel CartaoUnidade = new UnidadeCartaoPontoDomainModel()
                {
                    PERIODO = Periodo,
                    UND_ID = dadosUnidade.UND_ID,
                    UNIDADE = dadosUnidade.UND_SIGLA,
                    CaminhoUnidade = dadosUnidade.UND_DIRETORIO
                };

                var listServidores = await _vinculoRepository.GetAllAsync(x => x.VNC_STATUS == "A" && x.VNC_DEMISSAO == null && (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo
                /*|| x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.cedido*/ || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio)
                && x.ap_vinculoxunidade.Any(z => z.UND_ID == und && z.VNCU_DATAFIM == null && z.VNCU_STATUS == "A")).ContinueWith((x) =>
                x.Result.Select(f => f.ap_funcionario).Distinct().ToList()
                );

                foreach (var fun in listServidores)
                {
                    List<CartaoPontoDomainModel> cartaoPonto = new List<CartaoPontoDomainModel>();
                    FuncionarioCartaoPontoDomainModel funcionario = new FuncionarioCartaoPontoDomainModel()
                    {

                        FUN_FOTO = fun.ap_funcionarioxfoto.AsParallel().Any(p => p.FUNFT_STATUS == "A") ? fun.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                        FUN_ID = fun.FUN_ID,
                        NOME = fun.FUN_NOME,
                        MATRICULA = fun.FUN_MATRICULA,
                        TIPAGEM = fun.FUN_TIPOSANGUINEO.Value
                    };

                    var listBatidasFuncionario = await _pontoRepository.GetAllAsync(x => (x.FUN_ID == fun.FUN_ID || x.FUN_MATRICULA == fun.FUN_MATRICULA)
                    && (x.PON_DATA.Value >= Periodo && x.PON_DATA.Value < FimPeriodo)).ContinueWith((x) =>
                     x.Result.Select(z => new { PON_DATA = z.PON_DATA }).ToList()
                    );

                    var JustificativaList = await _justificativaRepository.GetAllAsync(x => x.JUSPT_STATUS == "A" && x.ap_vinculo.FUN_ID == fun.FUN_ID && ((x.JUSPT_DATAINICIO >= Periodo && x.JUSPT_DATAFIM <= dataFim) || (x.JUSPT_DATAFIM >= Periodo && x.JUSPT_DATAFIM <= dataFim))).ContinueWith((z) =>
                     z.Result.ToList().Select(x => new { x.JUSPT_DATAINICIO, x.JUSPT_DATAFIM, x.pon_motivojustificativa.MTVJUS_NOME, x.JUSPT_OBSERVACAO, x.JUSPT_ID })
                     );

                    var feriasList = await _feriasRepository.GetAllAsync(x => x.FRS_STATUS == "A" && x.ap_vinculo.FUN_ID == fun.FUN_ID && ((x.FRS_DATA_INICIOGOZO >= Periodo && x.FRS_DATA_INICIOGOZO <= dataFim) || (x.FRS_DATA_FIMGOZO >= Periodo && x.FRS_DATA_FIMGOZO <= dataFim)))
                        .ContinueWith((z) => z.Result.ToList().Select(x => new { x.FRS_DATA_INICIOGOZO, x.FRS_DATA_FIMGOZO, x.FRS_OBSERVACAO, x.ap_vinculo.FUN_ID, x.FRS_ID, x.FRS_NUMPORTARIA, x.FRS_DATAPORTARIA }));

                    int cont = 0;
                    DateTime data;
                    do
                    {

                        CartaoPontoDomainModel Batida = new CartaoPontoDomainModel();
                        data = Periodo.Value.AddDays(cont++);
                        Batida.DATA = data;
                        var batidasDia = listBatidasFuncionario.Where(x => x.PON_DATA.Value.Year == data.Year
                                        && x.PON_DATA.Value.Month == data.Month
                                        && x.PON_DATA.Value.Day == data.Day).ToList();

                        if (batidasDia.Count() > 0)
                        {
                            Batida.ENTRADA1 = batidasDia.Min(x => x.PON_DATA).Value.TimeOfDay;
                            //                if (TimeSpan.Parse(saida).Subtract(TimeSpan.Parse(entrada)).TotalMinutes < 10)

                            if (batidasDia.Any(x => x.PON_DATA.Value.Subtract(Batida.ENTRADA1.Value).TimeOfDay.TotalMinutes > 15))
                                Batida.SAIDA1 = batidasDia.Where(x => x.PON_DATA.Value.TimeOfDay > Batida.ENTRADA1).Max(x => x.PON_DATA).Value.TimeOfDay;
                        }

                        if (feriasList.Any(x => x.FRS_DATA_INICIOGOZO <= data && x.FRS_DATA_FIMGOZO >= data))
                        {
                            var justificativaFerias = feriasList.FirstOrDefault(x => x.FRS_DATA_INICIOGOZO <= data && x.FRS_DATA_FIMGOZO >= data);
                            Batida.FERIAS_OBS = "Ausência no periodo de " + justificativaFerias.FRS_DATA_INICIOGOZO.Value.ToShortDateString() + " até " + justificativaFerias.FRS_DATA_FIMGOZO.Value.ToShortDateString() + " - Motivo: FÉRIAS - Nº Portaria: " + justificativaFerias.FRS_NUMPORTARIA + " - Data Portaria: " + (justificativaFerias.FRS_DATAPORTARIA.HasValue ? justificativaFerias.FRS_DATAPORTARIA.Value.ToShortDateString() : " ") + " - " + justificativaFerias.FRS_OBSERVACAO;
                            Batida.FERIAS = "FÉRIAS";
                            Batida.FRS_ID = justificativaFerias.FRS_ID;
                        }

                        if (JustificativaList.Any(x => x.JUSPT_DATAINICIO <= data && x.JUSPT_DATAFIM >= data))
                        {
                            var justPonto = JustificativaList.FirstOrDefault(x => x.JUSPT_DATAINICIO <= data && x.JUSPT_DATAFIM >= data);
                            Batida.JUSTIFICATIVAPONTO_OBS = "Ausência no periodo de " + justPonto.JUSPT_DATAINICIO.Value.ToShortDateString() + " até " + justPonto.JUSPT_DATAFIM.Value.ToShortDateString() + " - Motivo: " + justPonto.MTVJUS_NOME + " - " + justPonto.JUSPT_OBSERVACAO;
                            Batida.JUSTIFICATIVA = justPonto.MTVJUS_NOME;
                            Batida.JUSTP_ID = justPonto.JUSPT_ID;
                        }

                        if (feriadoList.Any(x => x.FER_DATA.Value.Date == data))
                        {
                            var justFeriado = feriadoList.FirstOrDefault(x => x.FER_DATA.Value.Date == data);
                            Batida.FERIADO_OBS = "Ausência no dia de " + justFeriado.FER_DATA.Value.ToShortDateString() + "  - Motivo: " + DescriptionExtension.Descricao((TipoFeriado)justFeriado.FER_TIPO) + " - " + justFeriado.FER_DESCRICAO;
                            Batida.FERIADO = justFeriado.FER_DESCRICAO;
                        }


                        cartaoPonto.Add(Batida);

                    } while (data < dataFim.Date);
                    funcionario.CartaoPonto = cartaoPonto;


                    CartaoUnidade.Funcionarios.Add(funcionario);

                }

                ListCartaoUnidade.Add(CartaoUnidade);
            }
            return ListCartaoUnidade;
        }

        public async Task<List<UnidadeCartaoPontoFaltasDomainModel>> BuscarCartaoPontoPorUnidadeFaltas(int und, DateTime? PeriodoInicio, DateTime? PeriodoFim)
        {
            PeriodoInicio = PeriodoInicio ?? DateTime.Now.FirstDayOfMonth();
            PeriodoFim = PeriodoFim.HasValue ? PeriodoFim.Value.SetTime(23, 59, 59) : DateTime.Now.LastDayOfMonth().SetTime(23, 59, 59);

            List<UnidadeCartaoPontoFaltasDomainModel> ListCartaoUnidade = new List<UnidadeCartaoPontoFaltasDomainModel>();
            List<ap_unidade> resultpesq = new List<ap_unidade>();


            //Unidade HEMOCENTROCoordenador
            if (und == 76)
            {
                resultpesq = _unidadeRepository.GetAllAsync(x => x.UND_STATUS == "A" && x.UND_DATAFIM == null && x.UND_DIRETORIO != null && x.UND_NIVEL != 5 && x.UND_NIVEL != 6).Result.ToList();

            }
            else
            {
                resultpesq = _unidadeRepository.GetAllAsync(x => x.UND_ID == und).Result.ToList();
            }

            var feriadoList = _feriadoRepository.GetAll(x => x.FER_DATA >= PeriodoInicio && x.FER_DATA <= PeriodoFim && x.FER_STATUS == "A");


            foreach (ap_unidade dadosUnidade in resultpesq)
            {


                UnidadeCartaoPontoFaltasDomainModel CartaoUnidade = new UnidadeCartaoPontoFaltasDomainModel()
                {
                    PERIODO_INICIO = PeriodoInicio,
                    PERIODO_FIM = PeriodoFim,
                    UND_ID = dadosUnidade.UND_ID,
                    UNIDADE = dadosUnidade.UND_SIGLA,
                    CaminhoUnidade = dadosUnidade.UND_DIRETORIO
                };

                var listServidores = await _vinculoRepository.GetAllAsync(x => x.VNC_STATUS == "A" && x.VNC_DEMISSAO == null && (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo
                || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio)
                && x.ap_vinculoxunidade.Any(z => z.UND_ID == und && z.VNCU_DATAFIM == null && z.VNCU_STATUS == "A"))/*.Select(f => f.ap_funcionario).Distinct().ToList()*/
                .ContinueWith((x) => x.Result.Select(f => f.ap_funcionario).Distinct().ToList()
                );

                foreach (var fun in listServidores)
                {
                    List<CartaoPontoDomainModel> cartaoPonto = new List<CartaoPontoDomainModel>();
                    FuncionarioCartaoPontoDomainModel funcionario = new FuncionarioCartaoPontoDomainModel()
                    {

                        FUN_FOTO = fun.ap_funcionarioxfoto.AsParallel().Any(p => p.FUNFT_STATUS == "A") ? fun.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                        FUN_ID = fun.FUN_ID,
                        NOME = fun.FUN_NOME,
                        MATRICULA = fun.FUN_MATRICULA,
                        TIPAGEM = fun.FUN_TIPOSANGUINEO.Value
                    };

                    //Modificado
                    var listBatidasFuncionario = await _pontoRepository.GetAllAsync(x => (x.FUN_ID == fun.FUN_ID || x.FUN_MATRICULA == fun.FUN_MATRICULA)
                    && (x.PON_DATA.Value >= PeriodoInicio.Value && x.PON_DATA.Value < PeriodoFim.Value)).ContinueWith((x) =>
                     x.Result.Select(z => new { PON_DATA = z.PON_DATA }).ToList()
                    );

                    var JustificativaList = await _justificativaRepository.GetAllAsync(x => x.JUSPT_STATUS == "A" && x.ap_vinculo.FUN_ID == fun.FUN_ID && ((x.JUSPT_DATAINICIO >= PeriodoInicio && x.JUSPT_DATAFIM <= PeriodoFim) || (x.JUSPT_DATAFIM >= PeriodoInicio && x.JUSPT_DATAFIM <= PeriodoFim))).ContinueWith((z) =>
                     z.Result.ToList().Select(x => new { x.JUSPT_DATAINICIO, x.JUSPT_DATAFIM, x.pon_motivojustificativa.MTVJUS_NOME, x.JUSPT_OBSERVACAO, x.JUSPT_ID })
                     );

                    var feriasList = await _feriasRepository.GetAllAsync(x => x.FRS_STATUS == "A" && x.ap_vinculo.FUN_ID == fun.FUN_ID && ((x.FRS_DATA_INICIOGOZO >= PeriodoInicio && x.FRS_DATA_INICIOGOZO <= PeriodoFim) || (x.FRS_DATA_FIMGOZO >= PeriodoInicio && x.FRS_DATA_FIMGOZO <= PeriodoFim)))
                        .ContinueWith((z) => z.Result.ToList().Select(x => new { x.FRS_DATA_INICIOGOZO, x.FRS_DATA_FIMGOZO, x.FRS_OBSERVACAO, x.ap_vinculo.FUN_ID, x.FRS_ID, x.FRS_NUMPORTARIA, x.FRS_DATAPORTARIA }));

                    int cont = 0;
                    DateTime data;
                    do
                    {

                        CartaoPontoDomainModel Batida = new CartaoPontoDomainModel();
                        data = PeriodoInicio.Value.AddDays(cont++);
                        Batida.DATA = data;
                        var batidasDia = listBatidasFuncionario.Where(x => x.PON_DATA.Value.Year == data.Year
                                        && x.PON_DATA.Value.Month == data.Month
                                        && x.PON_DATA.Value.Day == data.Day).ToList();

                        if (batidasDia.Count() > 0)
                        {
                            Batida.ENTRADA1 = batidasDia.Min(x => x.PON_DATA).Value.TimeOfDay;
                            //                if (TimeSpan.Parse(saida).Subtract(TimeSpan.Parse(entrada)).TotalMinutes < 10)

                            if (batidasDia.Any(x => x.PON_DATA.Value.Subtract(Batida.ENTRADA1.Value).TimeOfDay.TotalMinutes > 15))
                                Batida.SAIDA1 = batidasDia.Where(x => x.PON_DATA.Value.TimeOfDay > Batida.ENTRADA1).Max(x => x.PON_DATA).Value.TimeOfDay;
                        }

                        if (feriasList.Any(x => x.FRS_DATA_INICIOGOZO <= data && x.FRS_DATA_FIMGOZO >= data))
                        {
                            var justificativaFerias = feriasList.FirstOrDefault(x => x.FRS_DATA_INICIOGOZO <= data && x.FRS_DATA_FIMGOZO >= data);
                            Batida.FERIAS_OBS = "Ausência no periodo de " + justificativaFerias.FRS_DATA_INICIOGOZO.Value.ToShortDateString() + " até " + justificativaFerias.FRS_DATA_FIMGOZO.Value.ToShortDateString() + " - Motivo: FÉRIAS - Nº Portaria: " + justificativaFerias.FRS_NUMPORTARIA + " - Data Portaria: " + (justificativaFerias.FRS_DATAPORTARIA.HasValue ? justificativaFerias.FRS_DATAPORTARIA.Value.ToShortDateString() : " ") + " - " + justificativaFerias.FRS_OBSERVACAO;
                            Batida.FERIAS = "FÉRIAS";
                            Batida.FRS_ID = justificativaFerias.FRS_ID;
                        }

                        if (JustificativaList.Any(x => x.JUSPT_DATAINICIO <= data && x.JUSPT_DATAFIM >= data))
                        {
                            var justPonto = JustificativaList.FirstOrDefault(x => x.JUSPT_DATAINICIO <= data && x.JUSPT_DATAFIM >= data);
                            Batida.JUSTIFICATIVAPONTO_OBS = "Ausência no periodo de " + justPonto.JUSPT_DATAINICIO.Value.ToShortDateString() + " até " + justPonto.JUSPT_DATAFIM.Value.ToShortDateString() + " - Motivo: " + justPonto.MTVJUS_NOME + " - " + justPonto.JUSPT_OBSERVACAO;
                            Batida.JUSTIFICATIVA = justPonto.MTVJUS_NOME;
                            Batida.JUSTP_ID = justPonto.JUSPT_ID;
                        }

                        if (feriadoList.Any(x => x.FER_DATA.Value.Date == data))
                        {
                            var justFeriado = feriadoList.FirstOrDefault(x => x.FER_DATA.Value.Date == data);
                            Batida.FERIADO_OBS = "Ausência no dia de " + justFeriado.FER_DATA.Value.ToShortDateString() + "  - Motivo: " + DescriptionExtension.Descricao((TipoFeriado)justFeriado.FER_TIPO) + " - " + justFeriado.FER_DESCRICAO;
                            Batida.FERIADO = justFeriado.FER_DESCRICAO;
                        }


                        cartaoPonto.Add(Batida);

                    } while (data < PeriodoFim.Value.Date);
                    funcionario.CartaoPonto = cartaoPonto;


                    CartaoUnidade.Funcionarios.Add(funcionario);

                }

                ListCartaoUnidade.Add(CartaoUnidade);
            }
            return ListCartaoUnidade;
        }

        //Metodo Adicionado em 17/05/2019
        public async Task<List<UnidadeCartaoPontoDomainModel>> BuscarCartaoPontoPorUnidadeFeriadoFimDeSemana(int und, DateTime? Periodo)
        {
            DateTime dataFim = Periodo.Value.LastDayOfMonth();
            DateTime FimPeriodo = Periodo.Value.AddMonths(1);
            List<UnidadeCartaoPontoDomainModel> ListCartaoUnidade = new List<UnidadeCartaoPontoDomainModel>();
            List<ap_unidade> resultpesq = new List<ap_unidade>();

            //Unidade HEMOCENTROCoordenador
            if (und == 76)
            {
                resultpesq = _unidadeRepository.GetAllAsync(x => x.UND_STATUS == "A" && x.UND_DATAFIM == null && x.UND_DIRETORIO != null && x.UND_NIVEL != 5 && x.UND_NIVEL != 6).Result.ToList();

            }
            else
            {
                resultpesq = _unidadeRepository.GetAllAsync(x => x.UND_ID == und).Result.ToList();
            }

            var feriadoList = _feriadoRepository.GetAll(x => x.FER_DATA >= Periodo && x.FER_DATA <= dataFim && x.FER_STATUS == "A").ToList().Where(x => (int)x.FER_DATA.Value.DayOfWeek != 6 && (int)x.FER_DATA.Value.DayOfWeek != 0);

            //IEnumerable<ap_feriado> feriadoListTest = feriadoList.Where(x => (int)x.FER_DATA.Value.DayOfWeek != 6 && (int)x.FER_DATA.Value.DayOfWeek != 0);

            foreach (ap_unidade dadosUnidade in resultpesq)
            {

                UnidadeCartaoPontoDomainModel CartaoUnidade = new UnidadeCartaoPontoDomainModel()
                {
                    PERIODO = Periodo,
                    UND_ID = dadosUnidade.UND_ID,
                    UNIDADE = dadosUnidade.UND_SIGLA,
                    CaminhoUnidade = dadosUnidade.UND_DIRETORIO
                };

                var listServidores = await _vinculoRepository.GetAllAsync(x => x.VNC_STATUS == "A" && x.VNC_DEMISSAO == null && (x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.ativo
                /*|| x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.cedido*/ || x.VNCST_ID == (int)VinculoSituacaoDomainModel.Situacao.aguardandoExercicio)
                && x.ap_vinculoxunidade.Any(z => z.UND_ID == und && z.VNCU_DATAFIM == null && z.VNCU_STATUS == "A")).ContinueWith((x) =>
                x.Result.Select(f => f.ap_funcionario).Distinct().ToList()
                );

                foreach (var fun in listServidores)
                {
                    List<CartaoPontoDomainModel> cartaoPonto = new List<CartaoPontoDomainModel>();

                    FuncionarioCartaoPontoDomainModel funcionario = new FuncionarioCartaoPontoDomainModel()
                    {

                        FUN_FOTO = fun.ap_funcionarioxfoto.AsParallel().Any(p => p.FUNFT_STATUS == "A") ? fun.ap_funcionarioxfoto.AsParallel().FirstOrDefault(x => x.FUNFT_STATUS == "A").FUNFT_ARQUIVO : imagemByteDados,
                        FUN_ID = fun.FUN_ID,
                        NOME = fun.FUN_NOME,
                        MATRICULA = fun.FUN_MATRICULA,
                        TIPAGEM = fun.FUN_TIPOSANGUINEO.Value
                    };


                    var listBatidasFuncionario = await _pontoRepository.GetAllAsync(x => (x.FUN_ID == fun.FUN_ID || x.FUN_MATRICULA == fun.FUN_MATRICULA)
                    && (x.PON_DATA.Value >= Periodo && x.PON_DATA.Value < FimPeriodo)).ContinueWith((x) =>
                     x.Result.Select(z => new { PON_DATA = z.PON_DATA }).ToList()
                    );

                    int cont = 0;
                    DateTime data;
                    do
                    {

                        CartaoPontoDomainModel Batida = new CartaoPontoDomainModel();
                        data = Periodo.Value.AddDays(cont++);

                        if (((int)data.DayOfWeek == 6 || (int)data.DayOfWeek == 0 || feriadoList.Any(x => x.FER_DATA.Value.Date == data.Date)) && listBatidasFuncionario.Any(x => x.PON_DATA.Value.Date == data))
                        {
                            Batida.DATA = data;
                            var batidasDia = listBatidasFuncionario.Where(x => x.PON_DATA.Value.Year == data.Year
                                            && x.PON_DATA.Value.Month == data.Month
                                            && x.PON_DATA.Value.Day == data.Day).ToList();

                            if (batidasDia.Count() > 0)
                            {
                                Batida.ENTRADA1 = batidasDia.Min(x => x.PON_DATA).Value.TimeOfDay;
                                //                if (TimeSpan.Parse(saida).Subtract(TimeSpan.Parse(entrada)).TotalMinutes < 10)

                                if (batidasDia.Any(x => x.PON_DATA.Value.Subtract(Batida.ENTRADA1.Value).TimeOfDay.TotalMinutes > 15))
                                    Batida.SAIDA1 = batidasDia.Where(x => x.PON_DATA.Value.TimeOfDay > Batida.ENTRADA1).Max(x => x.PON_DATA).Value.TimeOfDay;
                            }

                            if (feriadoList.Any(x => x.FER_DATA.Value.Date == data.Date))
                            {
                                var fer = (feriadoList.Where(x => x.FER_DATA.Value.Date == data.Date && x.FER_STATUS == "A").FirstOrDefault());
                                Batida.JUSTIFICATIVA = ((TipoFeriado)fer.FER_TIPO).Descricao() + " - " + fer.FER_DESCRICAO;
                            }
                            else
                            {
                                Batida.JUSTIFICATIVA = "Fim de Semana";
                            }

                            cartaoPonto.Add(Batida);

                        }
                    } while (data < dataFim.Date);

                    funcionario.CartaoPonto = cartaoPonto;


                    CartaoUnidade.Funcionarios.Add(funcionario);

                }

                ListCartaoUnidade.Add(CartaoUnidade);
            }
            return ListCartaoUnidade;
        }


    }
}