using CMM.Projects.Apresentation.Models;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMM.Projects.Apresentation.RelatorioExcel
{
    public class FuncoesGeracaoExcel
    {
        public void CreateCell(ref ICell cell, string item, bool center, ICellStyle styleCenter = null)
        {
            if (center)
            {
                cell.SetCellValue(item);
                cell.CellStyle = styleCenter;
            }
            else
            {
                cell.SetCellValue(item);
            }
        }

        public ICellStyle HeaderStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightCornflowerBlue.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.Alignment = HorizontalAlignment.Center;
            return style;
        }

        //public ICellStyle TextBold(IWorkbook workbook, IFont font)
        //{

        //    font.Boldweight = (short)FontBoldWeight.Bold;
        //    //font.FontHeightInPoints = 11;
        //    //font.FontName = "Calibri";
        //    ICellStyle style = workbook.CreateCellStyle();
        //    //style.WrapText = true;
        //    style.SetFont(font);
        //    return style;
        //}

        public ICellStyle WrapText(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.WrapText = true;
            return style;
        }

        public ICellStyle CenterStyle(IWorkbook workbook)
        {
            ICellStyle styleCenter = workbook.CreateCellStyle();
            styleCenter.Alignment = HorizontalAlignment.Center;
            styleCenter.WrapText = true;
            return styleCenter;
        }

        public void Mesclar(ref ISheet sheet, CellRangeAddress range)
        {
            sheet.AddMergedRegion(range);
        }

        public void CarregarColunas(ref ISheet sheet, List<int> TamanhoColuna)
        {
            if (TamanhoColuna.Count > 0)
            {
                foreach (var item in TamanhoColuna.Select((it, value) => new { tc = it, index = value }))
                {
                    sheet.SetColumnWidth(item.index, item.tc * 256);
                }
            }
            else
            {
                throw new ArgumentException("A listagem de Tamanho de Coluna não pode ser menor que 0", "TamanhoColuna");
            }
        }

        public void adicionarCabecalhoPesquisa(ref IRow row, ICell cell, ISheet sheet, ref int numeroRowInicial, IWorkbook workbook, CamposFuncionarioBuscaRelatorioModelView Campos, int quantColuna)
        {
            numeroRowInicial++;


            row = sheet.CreateRow(numeroRowInicial);
            cell = row.CreateCell(0);
            CreateCell(ref cell, "Servidores Por:", false);
            Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 0, quantColuna));

            numeroRowInicial++;

            if (Campos.MATRICULA != "")
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Matricula:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.MATRICULA, false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.NOME != "")
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Nome:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.NOME.ToUpper() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.TEXTO_CARGO != null)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Cargo:", false);
                string Cargos = "";
                foreach (var item in Campos.TEXTO_CARGO.Select((it, value) => new { content = it, index = value }))
                {
                    if (item.index < (Campos.TEXTO_CARGO.ToList().Count - 1))
                    {
                        Cargos += item.content + ", ";
                    }
                    else
                    {
                        Cargos += item.content + ".";
                    }
                }
                cell = row.CreateCell(1);
                CreateCell(ref cell, Cargos.ToUpper(), true, WrapText(workbook));
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.TEXTO_SITUACAO_VINCULO != null)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Situação:", false);
                string Situacao = "";
                foreach (var item in Campos.TEXTO_SITUACAO_VINCULO.Select((it, value) => new { content = it, index = value }))
                {
                    if (item.index < (Campos.TEXTO_SITUACAO_VINCULO.ToList().Count - 1))
                    {
                        Situacao += item.content + ", ";
                    }
                    else
                    {
                        Situacao += item.content + ".";
                    }
                }
                cell = row.CreateCell(1);
                CreateCell(ref cell, Situacao.ToUpper(), true, WrapText(workbook));
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.TEXTO_FUNCAO != null)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Função:", false);
                string Funcao = "";
                foreach (var item in Campos.TEXTO_FUNCAO.Select((it, value) => new { content = it, index = value }))
                {
                    if (item.index < (Campos.TEXTO_FUNCAO.ToList().Count - 1))
                    {
                        Funcao += item.content + ", ";
                    }
                    else
                    {
                        Funcao += item.content + ".";
                    }
                }
                cell = row.CreateCell(1);
                CreateCell(ref cell, Funcao.ToUpper(), true, WrapText(workbook));
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.TEXTO_UNIDADE != null)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Unidade:", false);
                string Unidade = "";
                foreach (var item in Campos.TEXTO_UNIDADE.Select((it, value) => new { content = it, index = value }))
                {
                    if (item.index < (Campos.TEXTO_UNIDADE.ToList().Count - 1))
                    {
                        Unidade += item.content + ", ";
                    }
                    else
                    {
                        Unidade += item.content + ".";
                    }
                }
                cell = row.CreateCell(1);
                CreateCell(ref cell, Unidade.ToUpper(), true, WrapText(workbook));
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.TEXTO_TIPO_VINCULO != null)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Tipo de Vínculo:", false);
                string Tipo = "";
                foreach (var item in Campos.TEXTO_TIPO_VINCULO.Select((it, value) => new { content = it, index = value }))
                {
                    if (item.index < (Campos.TEXTO_TIPO_VINCULO.ToList().Count - 1))
                    {
                        Tipo += item.content + ", ";
                    }
                    else
                    {
                        Tipo += item.content + ".";
                    }
                }
                cell = row.CreateCell(1);
                CreateCell(ref cell, Tipo.ToUpper(), true, WrapText(workbook));
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.TEXTO_BENEFICIO != null)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Benefício:", false);
                string Beneficio = "";
                foreach (var item in Campos.TEXTO_BENEFICIO.Select((it, value) => new { content = it, index = value }))
                {
                    if (item.index < (Campos.TEXTO_BENEFICIO.ToList().Count - 1))
                    {
                        Beneficio += item.content + ", ";
                    }
                    else
                    {
                        Beneficio += item.content + ".";
                    }
                }
                cell = row.CreateCell(1);
                CreateCell(ref cell, Beneficio.ToUpper(), true, WrapText(workbook));
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.SEXO != "")
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Sexo:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.SEXO.ToUpper() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.IDADE.HasValue)
            {
                string Idd = "";
                switch (Campos.IDADE)
                {
                    case 1: Idd = "Até 25 Anos"; break;
                    case 2: Idd = "De 25 à 40 Anos"; break;
                    case 3: Idd = "De 40 à 55 Anos"; break;
                    case 4: Idd = "De 55 à 70 Anos"; break;
                    case 5: Idd = "Acima de 70 Anos"; break;
                }
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Idade:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Idd + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.INICIO_PERIODO_ADMISSAO.HasValue)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Inicio Período Admissão:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.INICIO_PERIODO_ADMISSAO.Value.ToShortDateString() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.FIM_PERIODO_ADMISSAO.HasValue)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Fim Período Admissão:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.FIM_PERIODO_ADMISSAO.Value.ToShortDateString() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.INICIO_PERIODO_DEMISSAO.HasValue)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Inicio Período Demissão:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.INICIO_PERIODO_DEMISSAO.Value.ToShortDateString() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.FIM_PERIODO_DEMISSAO.HasValue)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Fim Período Demissão:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.FIM_PERIODO_DEMISSAO.Value.ToShortDateString() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.TEXTO_TIPO_APOSENTADORIA != null)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Tipo Entrada Aposentadoria:", false);
                string Entrada = "";
                foreach (var item in Campos.TEXTO_TIPO_APOSENTADORIA.Select((it, value) => new { content = it, index = value }))
                {
                    if (item.index < (Campos.TEXTO_TIPO_APOSENTADORIA.ToList().Count - 1))
                    {
                        Entrada += item.content + ", ";
                    }
                    else
                    {
                        Entrada += item.content + ".";
                    }
                }
                cell = row.CreateCell(1);
                CreateCell(ref cell, Entrada.ToUpper(), true, WrapText(workbook));
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.ENTRADA_APOSENTADORIA_DATA_INICIO.HasValue)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Inicio Período Entrada Aposentadoria:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.ENTRADA_APOSENTADORIA_DATA_INICIO.Value.ToShortDateString() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
            if (Campos.ENTRADA_APOSENTADORIA_DATA_FIM.HasValue)
            {
                row = sheet.CreateRow(numeroRowInicial);
                cell = row.CreateCell(0);
                CreateCell(ref cell, "Inicio Período Entrada Aposentadoria:", false);
                cell = row.CreateCell(1);
                CreateCell(ref cell, Campos.ENTRADA_APOSENTADORIA_DATA_FIM.Value.ToShortDateString() + ".", false);
                Mesclar(ref sheet, new CellRangeAddress(numeroRowInicial, numeroRowInicial, 1, quantColuna));
                numeroRowInicial++;
            }
        }

    }
}