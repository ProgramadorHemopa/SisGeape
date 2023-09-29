using CCM.Projects.SisGeape2.Domain;
using System;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IFeriadoBusiness : IBusiness
    {
        int TotalRegistro();

        List<FeriadoDomainModel> GetFeriadoPorData(DateTime? dataInicio, DateTime? dataFim);

        FeriadoDomainModel GetFeriadoById(int id);

        bool AddUpdateFeriado(FeriadoDomainModel _domainModel);
        bool DeleteFeriado(FeriadoDomainModel _domainModel);
        bool Salvar();
    }
}
