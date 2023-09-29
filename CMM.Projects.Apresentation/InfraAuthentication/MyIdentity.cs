using CMM.Projects.Apresentation.Models;
using System.Security.Principal;

namespace CMM.Projects.Apresentation.InfraAuthentication
{
    public class MyIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public SystemUserModelView User { get; set; }

        public MyIdentity(SystemUserModelView user)
        {
            if (user != null)
                Identity = new GenericIdentity(user.SUSR_LOGIN);

            User = user;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}
