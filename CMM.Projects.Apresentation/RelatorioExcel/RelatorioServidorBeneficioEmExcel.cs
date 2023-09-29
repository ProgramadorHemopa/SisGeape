using CMM.Projects.Apresentation.Models;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CMM.Projects.Apresentation.RelatorioExcel
{
    public class RelatorioServidorBeneficioEmExcel : FuncoesGeracaoExcel
    {
        List<string> Cabecalhos;
        List<FuncionarioRelatorioBeneficioModelView> Dados;
        CamposFuncionarioBuscaRelatorioModelView Campos;
        public RelatorioServidorBeneficioEmExcel(List<string> cabecalhos, List<FuncionarioRelatorioBeneficioModelView> dados, CamposFuncionarioBuscaRelatorioModelView campos)
        {
            Cabecalhos = cabecalhos;
            Dados = dados;
            Campos = campos;
        }


        public byte[] CriarRelatorio()
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Relatório Servidores por Benefício");
            int rowNumer = 0;

            //---- HEADER
            IRow row = sheet.CreateRow(rowNumer);
            ICell cell;

            cell = row.CreateCell(0);

            CreateCell(ref cell, "RELATÓRIO DE SERVIDORES POR BENEFÍCIO", false);

            Mesclar(ref sheet, new CellRangeAddress(rowNumer, rowNumer, 0, 6));

            rowNumer++;
            row = sheet.CreateRow(rowNumer);

            adicionarCabecalhoPesquisa(ref row, cell, sheet, ref rowNumer, workbook, Campos, Cabecalhos.Count - 1);

            rowNumer++;
            row = sheet.CreateRow(rowNumer);


            rowNumer++;
            row = sheet.CreateRow(rowNumer);

            foreach (var item in Cabecalhos.Select((it, value) => new { Conteudo = it, Index = value }))
            {
                cell = row.CreateCell(item.Index);
                cell.SetCellValue(item.Conteudo);
                cell.CellStyle = HeaderStyle(workbook);
            }

            foreach (var item in Dados.Select((it, value) => new { Conteudo = it, Index = value }))
            {
                rowNumer++;
                row = sheet.CreateRow(rowNumer);

                cell = row.CreateCell(0);
                CreateCell(ref cell, item.Conteudo.FUN_MATRICULA, true, CenterStyle(workbook));

                cell = row.CreateCell(1);
                CreateCell(ref cell, item.Conteudo.FUN_NOME, true, WrapText(workbook));

                cell = row.CreateCell(2);
                CreateCell(ref cell, item.Conteudo.FUN_UNIDADE, true, CenterStyle(workbook));

                cell = row.CreateCell(3);
                CreateCell(ref cell, item.Conteudo.CARGO, true, WrapText(workbook));

                cell = row.CreateCell(4);
                CreateCell(ref cell, item.Conteudo.BENEFICIO_NOME, true, WrapText(workbook));

                cell = row.CreateCell(5);
                CreateCell(ref cell, item.Conteudo.BENEFICIO_DATA_INICIO.HasValue ? item.Conteudo.BENEFICIO_DATA_INICIO.Value.ToShortDateString() : " - ", true, CenterStyle(workbook));

                cell = row.CreateCell(6);
                CreateCell(ref cell, item.Conteudo.BENEFICIO_DATA_FIM.HasValue ? item.Conteudo.BENEFICIO_DATA_FIM.Value.ToShortDateString() : " - ", true, CenterStyle(workbook));

            }

            //Resolve problema: O Excel encontrou conteúdo ilegível / Invalid or corrupt file (unreadable content)
            while (rowNumer < 20)
            {
                rowNumer++;
                row = sheet.CreateRow(rowNumer);
                row.CreateCell(0).SetCellValue(" ");
                row.CreateCell(1).SetCellValue(" ");
            }

            //Tamanho das colunas
            int[] TamanhoColuna = new int[] { 15, 40, 30, 35, 30, 15, 15 };
            CarregarColunas(ref sheet, TamanhoColuna.ToList());


            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);

            return stream.ToArray();

        }
    }
}