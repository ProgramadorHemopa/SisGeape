using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface ILicencaBusiness
    {
        int TotalLicenca();
        int TotalLicencaByFuncionario(int funcionario);
        List<LicencaDomainModel> GetAll();
        List<LicencaDomainModel> GetAllLicencaPage(int pageStart, int pageSize);
        List<LicencaDomainModel> GetLicencaByFuncionario(int funcionario);
        List<LicencaDomainModel> GetLicencaByFuncionarioPage(int funcionario, int pageStart, int pageSize);
        LicencaDomainModel GetLicencaById(int Id);
        LicencaDomainModel AddUpdateLicenca(LicencaDomainModel modelDomain);
        bool DeleteLicenca(LicencaDomainModel modelDomain);
        bool Salvar();
    }
}
