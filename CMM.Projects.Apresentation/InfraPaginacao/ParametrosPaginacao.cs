using System.Collections.Specialized;

namespace SisGeape2.Apresentation.InfraPaginacao
{
    public class ParametrosPaginacao
    {
        public ParametrosPaginacao(NameValueCollection dados)
        {
            string campoChave = SortColumnName;
            string ordem = SortDirection;

            CampoOrdenacao = string.Format("{0} {1}", campoChave, ordem);
        }

        public int Start { get; set; }
        public int Length { get; set; }
        public int RowCount { get; set; }
        public string SearchValue { get; set; }
        public string SortColumnName { get; set; }
        public string SortDirection { get; set; }
        public int Id { get; set; }
        public string CampoOrdenacao { get; set; }
    }

}
