
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
    
public partial class ap_beneficioxvinculo
{

    public int BNFVNC_ID { get; set; }

    public Nullable<int> BNF_ID { get; set; }

    public Nullable<int> VNC_ID { get; set; }

    public Nullable<System.DateTime> BNFVNC_DATAINICIO { get; set; }

    public Nullable<System.DateTime> BNFVNC_DATAFIM { get; set; }

    public Nullable<System.DateTime> BNFVNC_REGDATE { get; set; }

    public Nullable<int> BNFVNC_REGUSER { get; set; }

    public string BNFVNC_STATUS { get; set; }

    public string BNFVNC_NUMPORTARIA { get; set; }

    public Nullable<System.DateTime> BNFVNC_DATAPORTARIA { get; set; }



    public virtual ap_beneficio ap_beneficio { get; set; }

    public virtual ap_vinculo ap_vinculo { get; set; }

}

}
