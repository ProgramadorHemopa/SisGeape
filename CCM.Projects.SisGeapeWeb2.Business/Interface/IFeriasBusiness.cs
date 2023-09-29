using CCM.Projects.SisGeape2.Domain;
using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IFeriasBusiness : IBusiness
    {
        int TotalRegistro();
        int TotalServidoresFerias();
        int TotalRegistroByFuncionario(int id);
        List<FeriasDomainModel> BuscaServidoresEmFerias(DateTime inicio, DateTime fim);

        List<FeriasDomainModel> GetFeriasByFuncionario(int func);
        List<FeriasDomainModel> GetFeriasByVinculo(int vnc);
        List<ComunicadoFeriasDomainModel> GetDadosComunicado(int id);
        FeriasDomainModel GetFeriasById(int id);
        bool AddUpdateFerias(FeriasDomainModel _domainModel);
        bool DeleteFerias(FeriasDomainModel _domainModel);
        bool Salvar();
    }
}
