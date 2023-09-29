using SisGeape2.Models;
using SisGeape2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SisGeape2.InfraRelatorio.Infra
{

    public class RelatorioPonto
    {
        private readonly ModeloDados db = new ModeloDados();

        public List<CartaoPontoViewModel> GetCartao(DateTime? Inicio, DateTime? Fim, int vinculo)
        {
            List<CartaoPontoViewModel> Relatorio = new List<CartaoPontoViewModel>();
            int cont = 0;
            DateTime data;
            var funcionario = db.vinculo.Where(x => x.VNC_STATUS == "A").Where(x => x.VNC_ID == vinculo).Select(x => new { x.FUN_ID }).FirstOrDefault();


            do
            {
                var falta = "";
                var entrada = "";
                var saida = "";
                var dif = "";
                var atraso = "";
                var justificativa = "";


                data = Inicio.Value.AddDays(cont++);



                var culture = new System.Globalization.CultureInfo("pt-br");
                var diasemana = culture.DateTimeFormat.AbbreviatedDayNames[(int)data.DayOfWeek];
                var batidasDia = db.ponto.Where(x => x.VNC_ID == vinculo).Where(x => x.PON_DATAHORA.Value.Year == data.Year
                            && x.PON_DATAHORA.Value.Month == data.Month
                            && x.PON_DATAHORA.Value.Day == data.Day).Select(x => new { x.PON_DATAHORA }).ToList();

                var JustificativaList = db.justificativaponto.Where(x=>x.JUSPT_STATUS=="A").Where(x=>x.JUSPT_DATAINICIO<= data.Date && x.JUSPT_DATAFIM >= data.Date)/*.Where(x => x.VNC_ID == vinculo).Where(x => x.JUSPT_DATAINICIO.Value.Year >= data.Year && x.JUSPT_DATAFIM.Value.Year <= data.Year
                              && x.JUSPT_DATAINICIO.Value.Month >= data.Month && x.JUSPT_DATAFIM.Value.Month <= data.Month
                              && x.JUSPT_DATAINICIO.Value.Day >= data.Day && x.JUSPT_DATAFIM.Value.Day <= data.Day)*/.Select(x=>new {x.motivojustificativa.MTVJUS_NOME }).ToList();



                if (batidasDia.Count > 0)
                {
                    entrada = batidasDia.Min(x => x.PON_DATAHORA).Value.TimeOfDay.ToString();
                    saida = batidasDia.Max(x => x.PON_DATAHORA).Value.TimeOfDay.ToString();


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
                            dif = (TimeSpan.Parse(saida) - TimeSpan.Parse(entrada)).ToString(); // TimeSpan.Compare(TimeSpan.Parse(entrada), TimeSpan.Parse(saida)).ToString();
                        }

                        if (TimeSpan.Parse(saida).Subtract(TimeSpan.Parse(entrada)).TotalMinutes < 10)
                        {
                            saida = "";
                            dif = "";
                            falta = "X";
                        }

                    }

                    //vERIFICA O HORARIO ATIVO PARA O SERVIDOR PELA DATA DO REGISTRO DE PONTO
                    var horario = db.horarioxvinculo.Where(x => x.HORVNC_STATUS == "A" && x.HORVNC_DATAINICIO <= data && (x.HORVNC_DATAFIM >= data || x.HORVNC_DATAFIM == null));
                    if (horario.Count() > 0 && dif != "")
                    {
                        var cargaHoraria = (TimeSpan.Parse(horario.FirstOrDefault().horario.HOR_SAIDA) - TimeSpan.Parse(horario.FirstOrDefault().horario.HOR_ENTRADA)).ToString();

                        if (TimeSpan.Compare(TimeSpan.Parse(cargaHoraria), TimeSpan.Parse(dif)) == 1)
                        {
                            atraso = (TimeSpan.Parse(cargaHoraria) - TimeSpan.Parse(dif)).ToString();
                        }

                    }



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




                if (JustificativaList.Count > 0)
                {
                    justificativa = JustificativaList.FirstOrDefault().MTVJUS_NOME;

                    if(falta == "X" && justificativa != "")
                    {
                        falta = "";
                    }
                }






                CartaoPontoViewModel Batida = new CartaoPontoViewModel()
                {
                    DATA = data,
                    DIA = diasemana.ToUpper(),
                    VNC_ID = vinculo
                    ,

                    FUN_ID = funcionario.FUN_ID,
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



                Relatorio.Add(Batida);

            } while (data < Fim.Value.Date);



            return Relatorio;
        }

    }
}