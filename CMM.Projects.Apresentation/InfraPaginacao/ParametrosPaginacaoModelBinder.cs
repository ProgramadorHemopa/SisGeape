using System.Web;
using System.Web.Mvc;

namespace SisGeape2.Apresentation.InfraPaginacao
{
    public class ParametrosPaginacaoModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            ParametrosPaginacao paramPaginacao = new ParametrosPaginacao(request.Form);

            paramPaginacao.SortDirection = request.Form["order[0][dir]"];
            paramPaginacao.SortColumnName = request.Form["columns[" + request.Form["order[0][column]"] + "][data]"];
            paramPaginacao.RowCount = int.Parse(request.Form["draw"]);
            paramPaginacao.Start = int.Parse(request.Form["start"]);
            paramPaginacao.Length = int.Parse(request.Form["length"]);
            paramPaginacao.SearchValue = request.Form["search[value]"];
            paramPaginacao.CampoOrdenacao = paramPaginacao.SortColumnName + " " + paramPaginacao.SortDirection;

            return paramPaginacao;
        }
    }

}