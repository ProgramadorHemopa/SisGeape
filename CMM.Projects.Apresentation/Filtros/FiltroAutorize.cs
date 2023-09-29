using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CMM.Projects.Apresentation.Filtros
{
    public class FiltroAutorize : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                HttpCookie authoCookies = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authoCookies != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authoCookies.Value);
                    if (ticket.Expired)
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Usuario", action = "Login", area = "" }));
                    else
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        SystemUserModelView user = js.Deserialize<SystemUserModelView>(ticket.UserData);
                        if (user != null)
                        {
                            MyIdentity myIdentity = new MyIdentity(user);
                            MyPrincipal myPrincipal = new MyPrincipal(myIdentity);
                            HttpContext.Current.User = myPrincipal;
                        }
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Usuario", action = "Login", area = "" }));
                }

            }
            catch (Exception ex)
            {
                //filterContext.Controller.TempData["msgError"] = ex.Message;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Usuario", action = "Login", area = "" }));
            }

        }


    }

    public static class FilterExtension
    {
        public static void Autorizar(this ActionExecutingContext filterContext, Perfil perfil, Sistema sistema, bool permitirAdministrador = false)
        {
            try
            {
                //var objUsuario = (Usuario)context.HttpContext.Session["Usuario"];

                //if (objUsuario == null)
                //    throw new Exception("Sua sessão expirou, faça login para continuar");

                //if (!permitirAdministrador || objUsuario.Perfil != UsuarioPerfil.Administrador)
                //    if (objUsuario.Perfil != perfil)
                //        throw new Exception("Você não tem permissão para acessar essa área");

                HttpCookie authoCookies = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authoCookies != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authoCookies.Value);
                    if (ticket.Expired)
                        filterContext.Result = new RedirectToRouteResult(Redirecionar(perfil, sistema));
                    else
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        SystemUserModelView user = js.Deserialize<SystemUserModelView>(ticket.UserData);
                        if (ticket.Name != user.SUSR_LOGIN)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Usuario", action = "Login", area = "" }));
                        }
                        else if (user != null)
                        {
                            MyIdentity myIdentity = new MyIdentity(user);
                            MyPrincipal myPrincipal = new MyPrincipal(myIdentity);
                            HttpContext.Current.User = myPrincipal;
                        }

                        if (!permitirAdministrador || user.SUSR_PERFIL != Perfil.Admin)
                            if (user.SUSR_PERFIL != perfil || user.SIS_ID != sistema)
                            {

                                filterContext.Controller.TempData["msgError"] = "Você não tem permissão para acessar essa area!";
                                filterContext.Result = new RedirectToRouteResult(Redirecionar((Perfil)user.SUSR_PERFIL, (Sistema)user.SIS_ID));
                            }
                    }


                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Usuario", action = "Login", area = "" }));
                }
            }
            catch (Exception ex)
            {
                filterContext.Controller.TempData["msgError"] = ex.Message;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Usuario", action = "Login", area = "" }));
            }
        }

        private static RouteValueDictionary Redirecionar(Perfil perfil, Sistema sistema)
        {
            RouteValueDictionary routeReturn = new RouteValueDictionary();
            if (sistema == Sistema.SisGeape)
                routeReturn = new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" });
            else if (sistema == Sistema.SASS)
                routeReturn = new RouteValueDictionary(new { controller = "Home", action = "Index", area = "SASS" });


            return routeReturn;
        }
    }

    public class AutorizarOperadorGEAPE : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.Autorizar(Perfil.GEAPE, Sistema.SisGeape, true);
        }
    }

    public class AutorizarOperadorSASS : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.Autorizar(Perfil.Operador, Sistema.SASS, true);
        }
    }

}