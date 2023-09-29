using CCM.Projects.SisGeape2.Domain;
using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IJustificativaPontoBusiness
    {
        int TotalRegistro();
        int TotalRegistroByFuncionario(int id);
        List<JustificativaPontoDomainModel> GetJustificativaByFuncionario(int func);
        List<JustificativaPontoDomainModel> GetJustificativaByVinculo(int vnc);
        JustificativaPontoDomainModel GetJustificativaById(int id);
        bool AddUpdateJustificativa(JustificativaPontoDomainModel _domainModel);
        bool DeleteJustificativa(JustificativaPontoDomainModel _domainModel);
        bool Salvar();
        List<JustificativaPontoDomainModel> BuscarJustificativaPorParametros(int? vinculo, int? cargo, int? unidade, int? motivo, string matricula, string nome, DateTime? inicio, DateTime? fim);



        //Motivo Justificativa
        List<MotivoJustificativaDomainModel> GetMotivoJustificativa();
        MotivoJustificativaDomainModel GetMotivoJustificativaById(int id);
        bool AddUpdateMotivoJustificativa(MotivoJustificativaDomainModel _domainModel);
        bool DeleteMotivoJustificativa(MotivoJustificativaDomainModel _domainModel);


    }
}
