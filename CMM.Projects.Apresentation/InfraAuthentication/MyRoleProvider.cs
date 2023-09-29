using CMM.Projects.Apresentation.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace CMM.Projects.Apresentation.InfraAuthentication
{
    public class MyRoleProvider : RoleProvider
    {
        private int _cacheTimeoutInMinute = 30;

        //ISystemUser userBusiness;
        //protected MyRoleProvider(ISystemUser _userBusiness)
        //{
        //    userBusiness = _userBusiness;
        //}

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            //check cache
            var cacheKey = string.Format("{0}_role", username);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (string[])HttpRuntime.Cache[cacheKey];
            }
            string[] roles = new string[] { };
            //roles =  userBusiness.GetRoleByUserName(username.ToUpper()).Select(x=>x.ROL_NOME).ToArray<string>();

                //roles = (from a in dc.roles
                //            join b in dc.systemuserxroles on a.ROL_ID equals b.ROL_ID
                //            join c in dc.systemuser on b.SUSR_ID equals c.SUSR_ID
                //            where c.SUSR_LOGIN.Equals(username)
                //            select a.ROL_NOME).ToArray<string>();
                if (roles.Count() > 0)
                {
                    HttpRuntime.Cache.Insert(cacheKey, roles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinute), Cache.NoSlidingExpiration);

                }

            return roles;
        }

        //public override string[] GetRolesForUser(string username)
        //{

            //
            // Prox Linhas Comentadas para rodar LOCAL em 31/01/2022 DPN
            //


          //  if (!HttpContext.Current.User.Identity.IsAuthenticated)
          //  {
          //      return null;
          //  }

            //check cache
          //  var cacheKey = string.Format("{0}_role", username);
          //  if (HttpRuntime.Cache[cacheKey] != null)
          //  {
          //      return (string[])HttpRuntime.Cache[cacheKey];
          //  }
          //  string[] roles = new string[] { };
          //  using (UserContext dc = new UserContext())
          //  {
          //      roles = (from a in dc.roles
          //               join b in dc.systemuserxroles on a.ROL_ID equals b.ROL_ID
          //               join c in dc.systemuser on b.SUSR_ID equals c.SUSR_ID
          //               where c.SUSR_LOGIN.Equals(username)
          //               select a.ROL_NOME).ToArray<string>();
          //      if (roles.Count() > 0)
          //      {
          //          HttpRuntime.Cache.Insert(cacheKey, roles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinute), Cache.NoSlidingExpiration);
          //      }
          //  }
          //  return roles;

            //
            // Inserido abaixo: 
            //

            //throw new NotImplementedException();
        //}

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)

        {

            //throw new NotImplementedException();

            // Inserido acima: "=> throw new NotImplementedException();" e as
            // DUAS prox Linhas Comentadas para rodar LOCAL em 31/01/2022 DPN
            //

            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();

        }
    }
}
