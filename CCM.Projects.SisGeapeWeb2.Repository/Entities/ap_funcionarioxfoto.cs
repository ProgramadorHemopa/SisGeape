
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
    
public partial class ap_funcionarioxfoto
{

    public int FUNFT_ID { get; set; }

    public Nullable<int> FUN_ID { get; set; }

    public byte[] FUNFT_ARQUIVO { get; set; }

    public Nullable<System.DateTime> FUNFT_REGDATE { get; set; }

    public Nullable<int> FUNFT_REGUSER { get; set; }

    public string FUNFT_STATUS { get; set; }



    public virtual ap_funcionario ap_funcionario { get; set; }

}

}
