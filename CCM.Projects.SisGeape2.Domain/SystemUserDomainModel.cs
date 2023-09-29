namespace CCM.Projects.SisGeape2.Domain
{
    public class SystemUserDomainModel
    {

        public int SUSR_ID { get; set; }
        public string SUSR_NAME { get; set; }
        public string SUSR_PASSWORD { get; set; }
        public string SUSR_CONFIRM_PASSWORD { get; set; }
        public int SUSR_PERFIL { get; set; }
        public int SIS_ID { get; set; }
        public string SUSR_LOGIN { get; set; }
        public string SUSR_NOMEPERFIL { get; set; }
        public int SUSR_REGUSER { get; set; }
    }


    public class LoginDomainModel
    {

        public string Usuario { get; set; }
        public string Password { get; set; }

    }

    public class RoleDomainModel
    {

        public int ROL_ID { get; set; }
        public string ROL_NOME { get; set; }
        public int? ROL_REGUSER { get; set; }

    }

    public class SystemUserRolesDomainView
    {

        public int SUROL_ID { get; set; }
        public int SUSR_ID { get; set; }
        public int SUSR_PERFIL { get; set; }
        public int SIS_ID { get; set; }
        public int? SUROL_REGUSER { get; set; }

    }



    public class SystemUserEditDomainModel
    {

        public int SUSR_ID { get; set; }
        public string SUSR_PASSWORD { get; set; }
        public string SUSR_CONFIRM_PASSWORD { get; set; }
        public int SUSR_REGUSER { get; set; }
    }
}
