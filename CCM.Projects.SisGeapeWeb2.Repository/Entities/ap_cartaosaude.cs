
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
    
public partial class ap_cartaosaude
{

    public int CSD_ID { get; set; }

    public Nullable<int> FUN_ID { get; set; }

    public Nullable<System.DateTime> CSD_DATAEMISSAO { get; set; }

    public Nullable<int> CSD_FUMA { get; set; }

    public Nullable<int> CSD_ETILISMO { get; set; }

    public Nullable<int> CSD_SEDENTARISMO { get; set; }

    public Nullable<int> CSD_HIPERTENSO { get; set; }

    public Nullable<int> CSD_DIABETICO { get; set; }

    public Nullable<int> CSD_CONCLUSAO { get; set; }

    public Nullable<double> CSD_PESO { get; set; }

    public Nullable<double> CSD_ALTURA { get; set; }

    public Nullable<double> CSD_IMC { get; set; }

    public Nullable<double> CSD_PRESSAOARTERIALMAX { get; set; }

    public Nullable<double> CSD_PRESSAOARTERIALMIN { get; set; }

    public Nullable<double> CSD_GLICEMIA { get; set; }

    public Nullable<double> CSD_COLESTEROLTOTAL { get; set; }

    public Nullable<double> CSD_TRIGLICERIDEOS { get; set; }

    public Nullable<double> CSD_HDL { get; set; }

    public Nullable<double> CSD_LDL { get; set; }

    public Nullable<double> CSD_ACIDOURICO { get; set; }

    public Nullable<int> CSD_CIRCUNFERENCIAABDOMINAL { get; set; }

    public Nullable<int> CSD_TIPO { get; set; }

    public Nullable<System.DateTime> CSD_REGDATE { get; set; }

    public Nullable<int> CSD_REGUSER { get; set; }

    public string CSD_STATUS { get; set; }



    public virtual ap_funcionario ap_funcionario { get; set; }

}

}
