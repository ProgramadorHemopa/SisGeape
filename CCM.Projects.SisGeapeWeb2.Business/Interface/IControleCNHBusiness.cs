using CCM.Projects.SisGeape2.Domain;
using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IControleCNHBusiness : IBusiness
    {
        int TotalRegistro();

        List<ControleCNHDomainModel> GetAllControleCNH();
        List<ControleCNHPesquisaDomainModel> GetControleCNHPesquisa(string Matricula, string Nome, int? Situacao, int? Unidade);
        List<ControleCNHDomainModel> GetControleCNHValidade(DateTime? validade);
        ControleCNHVinculoDomainModel GetControleCNHByVinculo(int vnc);

        ControleCNHDomainModel GetControleCNHById(int id);

        ControleCNHAuxiliarDomainModel GetControleCNHAById(int id);

        bool AddUpdateControleCNH(ControleCNHDomainModel _domainModelPai);
        bool AddUpdateControleCNHFilho(ControleCNHAuxiliarDomainModel domainModelFilho);

        bool DeleteControleCNH(ControleCNHDomainModel _domainModelPai);
        bool DeleteControleCNHFilho(ControleCNHAuxiliarDomainModel _domainModelFilho);

        bool Salvar();
    }
}
