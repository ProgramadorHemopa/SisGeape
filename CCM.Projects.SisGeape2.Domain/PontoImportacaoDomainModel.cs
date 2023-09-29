using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeape2.Domain
{
    public class PontoImportacaoDomainModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PontoImportacaoDomainModel()
        {
            this.pontos = new HashSet<PontoDomainModel>();
        }
        public int PONIMP_ID { get; set; }
        public string PONIMP_OBSERVACAO { get; set; }
        public Nullable<System.DateTime> PONIMP_DATAINICIO { get; set; }
        public Nullable<System.DateTime> PONIMP_DATAFIM { get; set; }
        public Nullable<int> PONIMP_TOTAL { get; set; }
        public Nullable<int> PONEQP_ID { get; set; }
        public Nullable<int> PONIMP_REGUSER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PontoDomainModel> pontos { get; set; }
    }


    public class PontoDomainModel
    {
        public int PON_ID { get; set; }
        //PIS
        public string IDENTIFICADOR_FUNCIONARIO { get; set; }
        public string MATRICULA_FUNCIONARIO { get; set; }
        public Nullable<int> VNC_ID { get; set; }
        public Nullable<int> FUN_ID { get; set; }
        public string PON_VALIDATE { get; set; }
        public Nullable<int> PONIMP_ID { get; set; }
        public Nullable<System.DateTime> PON_DATA { get; set; }
        public string PON_ENTRADA1 { get; set; }
        public string PON_SAIDA1 { get; set; }
        public string PON_ENTRADA2 { get; set; }
        public string PON_SAIDA2 { get; set; }
        public virtual PontoImportacaoDomainModel pontoimportacao { get; set; }

    }
}
