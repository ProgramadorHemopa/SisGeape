﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class geapedbContextEntities : DbContext
{
    public geapedbContextEntities()
        : base("name=geapedbContextEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<ap_banco> ap_banco { get; set; }

    public virtual DbSet<ap_beneficio> ap_beneficio { get; set; }

    public virtual DbSet<ap_beneficioxvinculo> ap_beneficioxvinculo { get; set; }

    public virtual DbSet<ap_cargo> ap_cargo { get; set; }

    public virtual DbSet<ap_cartaosaude> ap_cartaosaude { get; set; }

    public virtual DbSet<ap_cessao> ap_cessao { get; set; }

    public virtual DbSet<ap_conselhoclasse> ap_conselhoclasse { get; set; }

    public virtual DbSet<ap_curso> ap_curso { get; set; }

    public virtual DbSet<ap_ferias> ap_ferias { get; set; }

    public virtual DbSet<ap_funcao> ap_funcao { get; set; }

    public virtual DbSet<ap_funcaoxvinculo> ap_funcaoxvinculo { get; set; }

    public virtual DbSet<ap_funcionario> ap_funcionario { get; set; }

    public virtual DbSet<ap_funcionarioxcurso> ap_funcionarioxcurso { get; set; }

    public virtual DbSet<ap_funcionarioxdependente> ap_funcionarioxdependente { get; set; }

    public virtual DbSet<ap_funcionarioxfoto> ap_funcionarioxfoto { get; set; }

    public virtual DbSet<ap_licenca> ap_licenca { get; set; }

    public virtual DbSet<ap_licencapericia> ap_licencapericia { get; set; }

    public virtual DbSet<ap_licencasituacaopericia> ap_licencasituacaopericia { get; set; }

    public virtual DbSet<ap_licencatipo> ap_licencatipo { get; set; }

    public virtual DbSet<ap_referenciafuncao> ap_referenciafuncao { get; set; }

    public virtual DbSet<ap_unidade> ap_unidade { get; set; }

    public virtual DbSet<ap_vinculo> ap_vinculo { get; set; }

    public virtual DbSet<ap_vinculosituacao> ap_vinculosituacao { get; set; }

    public virtual DbSet<ap_vinculotipo> ap_vinculotipo { get; set; }

    public virtual DbSet<ap_vinculoxunidade> ap_vinculoxunidade { get; set; }

    public virtual DbSet<pon_horario> pon_horario { get; set; }

    public virtual DbSet<pon_horarioxvinculo> pon_horarioxvinculo { get; set; }

    public virtual DbSet<pon_justificativaponto> pon_justificativaponto { get; set; }

    public virtual DbSet<pon_motivojustificativa> pon_motivojustificativa { get; set; }

    public virtual DbSet<pon_ponto> pon_ponto { get; set; }

    public virtual DbSet<pon_pontoequipamento> pon_pontoequipamento { get; set; }

    public virtual DbSet<pon_pontoimportacao> pon_pontoimportacao { get; set; }

    public virtual DbSet<roles> roles { get; set; }

    public virtual DbSet<systemuser> systemuser { get; set; }

    public virtual DbSet<systemuserxroles> systemuserxroles { get; set; }

    public virtual DbSet<ap_horario> ap_horario { get; set; }

    public virtual DbSet<ap_horarioxdiasemana> ap_horarioxdiasemana { get; set; }

    public virtual DbSet<ap_licencapericiasituacao> ap_licencapericiasituacao { get; set; }

    public virtual DbSet<pon_situacaopericia> pon_situacaopericia { get; set; }

    public virtual DbSet<ap_cid10> ap_cid10 { get; set; }

    public virtual DbSet<ap_funcionariocracha> ap_funcionariocracha { get; set; }

    public virtual DbSet<ap_conselho> ap_conselho { get; set; }

    public virtual DbSet<sass_avaliacaoclinica> sass_avaliacaoclinica { get; set; }

    public virtual DbSet<sass_consulta> sass_consulta { get; set; }

    public virtual DbSet<sass_prontuario> sass_prontuario { get; set; }

    public virtual DbSet<sass_prontuarioxavaliacao> sass_prontuarioxavaliacao { get; set; }

    public virtual DbSet<ap_feriado> ap_feriado { get; set; }

    public virtual DbSet<ap_controlecnh> ap_controlecnh { get; set; }

    public virtual DbSet<ap_controlecnh_aux> ap_controlecnh_aux { get; set; }

    public virtual DbSet<systemuser_old> systemuser_old { get; set; }

}

}

