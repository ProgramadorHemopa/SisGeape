using Microsoft.Reporting.WebForms;
using SisGeape2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SisGeape2.Models.vinculo;


namespace SisGeape2.InfraRelatorio.Infra
{
    public class FunCracha
    {
        public string FUN_NOME { get; set; }
        public string CRG_NOME { get; set; }
        public string FUN_TIPOSANGUINEO { get; set; }
        public string FUN_MATRICULA { get; set; }
        public byte[] FUNFT_ARQUIVO { get; set; }
        public byte[] MATRICULA { get; set; }


    }


    public class EmitirCracha
    {

        private readonly ModeloDados db = new ModeloDados();
        private List<FunCracha> servidor;


        public List<FunCracha> GetDadosFuncionario(int fun)
        {

            var x = db.vinculo.Where(z => z.FUN_ID == fun).Where(z => z.VNCST_ID != (int)Situacao.Desligado).ToList().FirstOrDefault();

            var funcracha = new FunCracha
            {
                FUN_NOME = x.funcionario.FUN_NOMECRACHA,
                CRG_NOME = x.cargo.CRG_NOME,
                FUN_TIPOSANGUINEO = GetTipagem(x.funcionario.FUN_TIPOSANGUINEO),
                FUN_MATRICULA = x.funcionario.FUN_MATRICULA,
                FUNFT_ARQUIVO = x.funcionario.funcionarioxfoto.FirstOrDefault().FUNFT_ARQUIVO,
                MATRICULA = GerarBarCode(x.funcionario.FUN_MATRICULA)

            };
            servidor = new List<FunCracha>();
            servidor.Add(funcracha);

            return servidor;
        }

        public byte[] GerarBarCode(string pMATRICULA)
        {
            var bc = new BarcodeLib.Barcode();
            System.Drawing.Image BcImage = bc.Encode(BarcodeLib.TYPE.CODE128, pMATRICULA);
            using (var ms = new System.IO.MemoryStream())
            {
                BcImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public string GetTipagem(int? FUN_TIPO)
        {
            int? esc = FUN_TIPO;
            if (esc == 1)
                return "A-";
            else if (esc == 2)
                return "A+";
            else if (esc == 3)
                return "B-";
            else if (esc == 4)
                return "B+";
            else if (esc == 5)
                return "AB-";
            else if (esc == 6)
                return "AB+";
            else if (esc == 7)
                return "O-";
            else if (esc == 8)
                return "O+";
            else
                return "";
        }

        /*
        public FileResult LoadCracha(int FUN_ID, ReportViewer viewer)
        {
            try
            {
                var func = GetDadosFuncionario(FUN_ID);



            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }
        */

    }
}