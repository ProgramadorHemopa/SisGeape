
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
    
public partial class ap_controlecnh_aux
{

    public int CNHA_ID { get; set; }

    public Nullable<int> CNH_ID { get; set; }

    public Nullable<System.DateTime> CNHA_VALIDADE { get; set; }

    public Nullable<System.DateTime> CNHA_RECEBIMENTO { get; set; }

    public Nullable<System.DateTime> CNHA_EMISSAO { get; set; }

    public string CNHA_CATEGORIA { get; set; }

    public Nullable<System.DateTime> CNHA_REGDATE { get; set; }

    public Nullable<int> CNHA_REGUSER { get; set; }

    public string CNHA_STATUS { get; set; }



    public virtual ap_controlecnh ap_controlecnh { get; set; }

}

}