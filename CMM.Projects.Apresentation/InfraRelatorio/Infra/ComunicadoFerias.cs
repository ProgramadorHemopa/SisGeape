using Microsoft.Reporting.WebForms;
using SisGeape2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SisGeape2.Models.vinculo;

namespace SisGeape2.InfraRelatorio.Infra
{
    public class dsComunicadoFerias
    {
        public string NOME { get; set; }
        public string MATRICULA { get; set; }
        public string CARGO { get; set; }
        public string UNIDADE { get; set; }
        public string INICIO_AQUISITIVO { get; set; }
        public string FIM_AQUISITIVO { get; set; }
        public string INICIO_GOZO { get; set; }
        public string RETORNO { get; set; }
        public string UNIDADE_RESPONSAVEL { get; set; }


    }

    public class ComunicadoFerias
    {
        private readonly ModeloDados db = new ModeloDados();
        private List<dsComunicadoFerias> listComunicado;

        public List<dsComunicadoFerias> GetDadosFuncionario(int? FRS_ID)
        {
            var frs = db.ferias.Where(x=>x.FRS_STATUS=="A").Where(z => z.FRS_ID == FRS_ID).ToList().FirstOrDefault();

            var comunicadoFuncionario = new dsComunicadoFerias
            {

                NOME = frs.vinculo.funcionario.FUN_NOME,
                CARGO = frs.vinculo.cargo.CRG_NOME,
                UNIDADE = frs.vinculo.vinculoxunidade.Where(x=>x.VNCU_DATAINICIO <= frs.FRS_DATA_INICIOAQUISITIVO).Where(x => x.VNCU_DATAFIM >= frs.FRS_DATA_FIMAQUISITIVO || x.VNCU_DATAFIM == null).FirstOrDefault().unidade.UND_NOME,
                MATRICULA = frs.vinculo.funcionario.FUN_MATRICULA,
                INICIO_AQUISITIVO = frs.FRS_DATA_INICIOAQUISITIVO.Value.ToShortDateString(),
                FIM_AQUISITIVO = frs.FRS_DATA_FIMAQUISITIVO.Value.ToShortDateString(),
                INICIO_GOZO = frs.FRS_DATA_INICIOGOZO.Value.ToShortDateString(),
                RETORNO = frs.FRS_DATA_RETORNO.Value.ToShortDateString()
            };
            
            listComunicado = new List<dsComunicadoFerias>();
            listComunicado.Add(comunicadoFuncionario);

            return listComunicado;
        }    
    }
}