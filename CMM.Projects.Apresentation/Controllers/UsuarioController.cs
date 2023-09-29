using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.Filtros;
using CMM.Projects.Apresentation.InfraAuthentication;
using CMM.Projects.Apresentation.Models;
using SisGeape2.Apresentation.InfraPaginacao;
using SisGeape2.Apresentation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CMM.Projects.Apresentation.Controllers
{
    public class UsuarioController : Controller
    {

        private infraMessage msg = new infraMessage();


        ISystemUser userBusiness;

        public UsuarioController(ISystemUser _userBusiness)
        {
            userBusiness = _userBusiness;
        }

        // GET: Usuario
        [FiltroAutorize]
        public ActionResult Index()
        {
            List<SystemUserDomainModel> domainModel = userBusiness.GetAllUserPermission();
            List<SystemUserModelView> users = new List<SystemUserModelView>();
            Mapper.Map(domainModel, users);
            return View(users);
        }

        [FiltroAutorize]
        public JsonResult ListarUsuario(ParametrosPaginacao paginacao)
        {
            var _user = userBusiness.GetAllUserPermission();


            int tot = _user.Count();



            if (!String.IsNullOrWhiteSpace(paginacao.SearchValue))

            {
                _user = _user.Where("SUSR_NAME.ToUpper().Contains(@0) OR SUSR_LOGIN.ToUpper().Contains(@0) ", paginacao.SearchValue.ToUpper()).ToList();
            }
            int totFiltrado = _user.Count();

            var dadosPaginados = _user.OrderBy(paginacao.CampoOrdenacao).Skip(paginacao.Start).Take(paginacao.Length).ToList();

            return Json(new
            {
                data = dadosPaginados,
                draw = paginacao.RowCount,
                recordsTotal = tot,
                recordsFiltered = totFiltrado
            }, JsonRequestBehavior.AllowGet);
        }


        [FiltroAutorize]
        public ActionResult Register(int? id)
        {
            SystemRoleModelView _user = new SystemRoleModelView();

            if (id != null)
            {
                SystemUserRolesDomainView _domain = userBusiness.GetUserbyId(id.Value);
                Mapper.Map(_domain, _user);
            }
            var listServidor = userBusiness.GetAllUser();
            ViewBag.SUSR_ID = new SelectList(listServidor, "SUSR_ID", "SUSR_NAME", _user.SUSR_ID, listServidor.Where(x => x.SUSR_PERFIL != 0 || x.SIS_ID != 0).Select(x => x.SUSR_ID));

            return View(_user);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [FiltroAutorize]
        public ActionResult Register(SystemRoleModelView _user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemUserRolesDomainView userDomainModel = new SystemUserRolesDomainView();
                    Mapper.Map(_user, userDomainModel);
                    userDomainModel.SUROL_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;
                    if (_user.SUROL_ID == 0)
                        userBusiness.AddUser(userDomainModel);
                    else
                        userBusiness.EditUser(userDomainModel);


                    if (userBusiness.Salvar())
                        TempData["msgSuccess"] = msg.MensagemSucesso();

                    return RedirectToAction("Index");
                    //return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
                }
                else
                    throw new Exception();

            }
            catch (Exception ex)
            {

                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                string mensg = "";
                foreach (var err in erros)
                {
                    mensg += err.ErrorMessage + " <br/>";
                }

                if (mensg.Length == 0)
                {
                    mensg = ex.Message;
                }
                TempData["msgInfo"] = mensg;
                return View(_user);
                //return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

            }

        }

        // GET: Usuario/Edit/5
        //[FiltroAutorize]
        //public ActionResult EditUser(int id)
        //{
        //    SystemUserEditModelView _userEdit = new SystemUserEditModelView();



        //    if (id != 0)
        //    {
        //        _userEdit.SUSR_ID = id;
        //        ViewBag.Title = "Alterar Senha";
        //    }
        //    else
        //        RedirectToAction("Index");



        //    return PartialView(_userEdit);
        //}

        //// POST: Usuario/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[FiltroAutorize]
        //public JsonResult EditUser(SystemUserEditModelView _user)
        //{
        //    try
        //    {
        //        //if (ModelState.IsValid)
        //        //{
        //        //    SystemUserEditDomainModel userEditDomainModel = new SystemUserEditDomainModel();
        //        //    _user.SUSR_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

        //        //    Mapper.Map(_user, userEditDomainModel);

        //        //    if (userBusiness.VerificaSenha(userEditDomainModel))
        //        //    {
        //        //        userBusiness.EditUser(userEditDomainModel);

        //        //        if (userBusiness.Salvar())
        //        //            return Json(new { resultado = true, tipomsg = "", msg = msg.MensagemSucesso() }, JsonRequestBehavior.AllowGet);
        //        //        else
        //        //            return Json(new { resultado = false, tipomsg = "", msg = msg.MensagemErro() }, JsonRequestBehavior.AllowGet);

        //        //    }
        //        //    else
        //        //        return Json(new { resultado = false, tipomsg = "", msg = "Senha Anterior Incorreta!" }, JsonRequestBehavior.AllowGet);

        //        //}
        //        //else 
        //        throw new Exception();
        //    }
        //    catch
        //    {
        //        IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
        //        string mensg = "";
        //        foreach (var err in erros)
        //        {
        //            mensg += err.ErrorMessage + " <br/>";
        //        }
        //        return Json(new { resultado = false, tipomsg = "danger", msg = mensg }, JsonRequestBehavior.AllowGet);

        //    }
        //}


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //Login POST
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "")
        {

            if (ModelState.IsValid)
            {

                var lUser = userBusiness.GetLoginUser(login.Usuario.ToUpper(), login.Password.ToUpper());

                if (lUser != null)
                {

                    //SystemUserDomainModel userDomainModel = userBusiness.GetUserByName(login.Usuario.ToUpper()).FirstOrDefault();
                    SystemUserModelView user = new SystemUserModelView();
                    Mapper.Map(lUser, user);
                    if (user != null)
                    {

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string data = js.Serialize(user);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.SUSR_LOGIN, DateTime.Now, DateTime.Now.AddMinutes(30), false, data);
                        string encToken = FormsAuthentication.Encrypt(ticket);
                        HttpCookie authoCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        authoCookies.HttpOnly = true;
                        Response.Cookies.Add(authoCookies);


                        if (user.SIS_ID == Sistema.SASS)
                        {
                            return RedirectToAction("Index", "Home", new { area = "SASS" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }

                }
                else
                {
                    ViewBag.Senha = "Usuário não encontrado!";

                }


            }

            ModelState.Remove("Password");
            return View();
        }

        //Logout
        [FiltroAutorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        // GET: Usuario/Delete/5
        [FiltroAutorize]
        public ActionResult Delete(int? id)
        {
            SystemRoleModelView _user = new SystemRoleModelView();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            else
            {
                SystemUserRolesDomainView _domain = userBusiness.GetUserbyId(id.Value);
                Mapper.Map(_domain, _user);
                var listServidor = userBusiness.GetAllUser().Find(x => x.SUSR_ID == _user.SUSR_ID);
                ViewBag.SUSR_ID = listServidor.SUSR_NAME; //new SelectList(listServidor, "SUSR_ID", "SUSR_NAME", _user.SUSR_ID, listServidor.Where(x => x.SUSR_PERFIL != 0 || x.SIS_ID != 0).Select(x => x.SUSR_ID));

            }
            return View(_user);
        }

        // POST: Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [FiltroAutorize]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SystemUserRolesDomainView domainModel = userBusiness.GetUserbyId(id);
                if (domainModel != null)
                {
                    domainModel.SUROL_REGUSER = ((HttpContext.User as MyPrincipal).Identity as MyIdentity).User.SUSR_ID;

                    userBusiness.DeleteUser(domainModel);


                    if (userBusiness.Salvar())
                    {
                        TempData["msgSuccess"] = msg.MensagemSucesso().ToString();
                        return RedirectToAction("Index");
                    }
                    else
                        throw new Exception();
                }
                else
                    throw new InvalidOperationException();


            }
            catch (Exception e)
            {
                TempData["msgError"] = "Ocorreu um erro na sua operação. </br> " + e.Message;


                return RedirectToAction("Index");
            }

        }
    }
}
