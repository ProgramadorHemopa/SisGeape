using CCM.Projects.SisGeapeWeb2.Business;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Lifetime;


namespace CMM.Projects.Apresentation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var container = new UnityContainer();
            container.RegisterType<ICargoBusiness, CargoBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IFuncionarioBusiness, FuncionarioBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnidadeBusiness, UnidadeBusiness>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
