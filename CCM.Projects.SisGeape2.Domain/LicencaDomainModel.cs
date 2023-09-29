using System;
using System.Web;

namespace CCM.Projects.SisGeape2.Domain
{
    public class LicencaDomainModel
    {
        public int LIC_ID { get; set; }

        public int LICTP_ID { get; set; }

        public DateTime? LIC_DATAENTRADA { get; set; }

        public DateTime? LIC_DATAINICIO { get; set; }
        public DateTime? LIC_DATAFIM { get; set; }

        public DateTime? LIC_DATARETORNO { get; set; }

        public int VNC_ID { get; set; }

        public HttpPostedFileBase ANEXO { get; set; }
        public string LIC_NOMEARQUIVO { get; set; }

        public string LIC_NUMPORTARIA { get; set; }
        public DateTime? LIC_DATAPORTARIA { get; set; }

        public int? LIC_REGUSER { get; set; }


        //public virtual licencatipoModelView licencatipo { get; set; }
        //public virtual vinculo vinculo { get; set; }
    }
}
