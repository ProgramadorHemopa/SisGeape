using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface ICargoBusiness
    {

        int TotalRegistros();
        List<CargoDomainModel> GetCargo();
        List<CargoDomainModel> DdlCargo();

        List<CargoDomainModel> GetCargoRecords(string name, int pageStart, int pageSize);
        List<CargoDomainModel> GetCargoByName(string name);
        CargoDomainModel GetCargoById(int Id);
        void AddUpdateCargo(CargoDomainModel cargo);
        bool DeleteBanco(CargoDomainModel cargo);
        bool Salvar();
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IsValid(CargoDomainModel cargo);
    }
}
