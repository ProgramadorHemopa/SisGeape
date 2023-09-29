using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Areas.SASS
{
    public class SASSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SASS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            context.Routes.MapMvcAttributeRoutes();


            context.MapRoute(
                "SASS_default",
                "SASS/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                    namespaces: new[] { "CMM.Projects.Apresentation.Areas.SASS.Controllers" }
            );
        }
    }
}