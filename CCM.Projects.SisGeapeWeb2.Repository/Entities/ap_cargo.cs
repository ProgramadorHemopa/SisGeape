
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
    
public partial class ap_cargo
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ap_cargo()
    {

        this.ap_vinculo = new HashSet<ap_vinculo>();

        this.ap_conselho = new HashSet<ap_conselho>();

    }


    public int CRG_ID { get; set; }

    public string CRG_CODIGO { get; set; }

    public string CRG_NOME { get; set; }

    public string CRG_DESCRICAO { get; set; }

    public string CRG_REFSALARIAL { get; set; }

    public string CRG_REFCARGO { get; set; }

    public string CRG_RISCOS { get; set; }

    public Nullable<int> CRG_ESCOLARIDADE { get; set; }

    public Nullable<decimal> CRG_BASESALARIAL { get; set; }

    public Nullable<System.DateTime> CRG_REGDATE { get; set; }

    public Nullable<int> CRG_REGUSER { get; set; }

    public string CRG_STATUS { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ap_vinculo> ap_vinculo { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ap_conselho> ap_conselho { get; set; }

}

}
