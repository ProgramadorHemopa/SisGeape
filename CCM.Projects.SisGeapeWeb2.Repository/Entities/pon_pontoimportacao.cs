
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
    
public partial class pon_pontoimportacao
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public pon_pontoimportacao()
    {

        this.pon_ponto = new HashSet<pon_ponto>();

    }


    public int PONIMP_ID { get; set; }

    public string PONIMP_OBSERVACAO { get; set; }

    public Nullable<System.DateTime> PONIMP_DATAINICIO { get; set; }

    public Nullable<System.DateTime> PONIMP_DATAFIM { get; set; }

    public Nullable<int> PONIMP_TOTAL { get; set; }

    public Nullable<int> PONEQP_ID { get; set; }

    public Nullable<System.DateTime> PONIMP_REGDATE { get; set; }

    public Nullable<int> PONIMP_REGUSER { get; set; }

    public string PONIMP_STATUS { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<pon_ponto> pon_ponto { get; set; }

    public virtual pon_pontoequipamento pon_pontoequipamento { get; set; }

}

}
