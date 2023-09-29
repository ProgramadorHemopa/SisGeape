using SisGeape2.Apresentation.AutoMapper;
using SisGeape2.Apresentation.InfraPaginacao;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMM.Projects.Apresentation
{
    public partial class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.EnsureInitialized();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);




            AutoMapperConfig.RegisterMappings();
            UnityConfig.RegisterComponents();


            //System.Web.Optimization.BundleTable.EnableOptimizations = true;
            //GlobalFilters.Filters.Add(new Filtros.FiltroAutorize());

            //Entrada dos Parametros de Ordenacao dos Grids
            ModelBinders.Binders.Add(typeof(ParametrosPaginacao), new ParametrosPaginacaoModelBinder());
        }


    }
}
