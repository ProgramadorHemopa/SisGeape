
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
    
public partial class pon_horario
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public pon_horario()
    {

        this.pon_horarioxvinculo = new HashSet<pon_horarioxvinculo>();

    }


    public int HOR_ID { get; set; }

    public string HOR_DESCRICAO { get; set; }

    public Nullable<System.TimeSpan> HOR_TOLERANCIAENTRADA { get; set; }

    public Nullable<System.TimeSpan> HOR_ENTRADA { get; set; }

    public Nullable<System.TimeSpan> HOR_TOLERANCIASAIDA { get; set; }

    public Nullable<System.TimeSpan> HOR_SAIDA { get; set; }

    public Nullable<int> HOR_REGUSER { get; set; }

    public Nullable<System.DateTime> HOR_REGDATE { get; set; }

    public string HOR_STATUS { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<pon_horarioxvinculo> pon_horarioxvinculo { get; set; }

}

}
