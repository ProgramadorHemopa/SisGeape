
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace CCM.Projects.SisGeapeWeb2.Repository.Entities
{

using System;
    using System.Collections.Generic;
    
public partial class ap_licenca
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ap_licenca()
    {

        this.ap_licencapericia = new HashSet<ap_licencapericia>();

    }


    public int LIC_ID { get; set; }

    public Nullable<int> LICTP_ID { get; set; }

    public Nullable<System.DateTime> LIC_DATAENTRADA { get; set; }

    public Nullable<System.DateTime> LIC_DATAINICIO { get; set; }

    public Nullable<System.DateTime> LIC_DATAFIM { get; set; }

    public Nullable<System.DateTime> LIC_DATARETORNO { get; set; }

    public Nullable<int> VNC_ID { get; set; }

    public string LIC_NOMEARQUIVO { get; set; }

    public byte[] LIC_ARQUIVO { get; set; }

    public Nullable<System.DateTime> LIC_REGDATE { get; set; }

    public Nullable<int> LIC_REGUSER { get; set; }

    public string LIC_STATUS { get; set; }

    public string LIC_NUMPORTARIA { get; set; }

    public Nullable<System.DateTime> LIC_DATAPORTARIA { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ap_licencapericia> ap_licencapericia { get; set; }

    public virtual ap_licencatipo ap_licencatipo { get; set; }

    public virtual ap_vinculo ap_vinculo { get; set; }

}

}
