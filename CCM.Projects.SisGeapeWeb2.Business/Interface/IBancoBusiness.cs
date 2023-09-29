using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IBancoBusiness
    {

        //CRUD operation 

        int TotalRegistros();
        List<BancoDomainModel> GetBanco();
        List<BancoDomainModel> GetBancoRecords(int pageStart, int pageSize);
        List<BancoDomainModel> GetBancoByName(string name);
        BancoDomainModel GetBancoById(int Id);
        BancoDomainModel AddUpdateBanco(BancoDomainModel banco);
        bool DeleteBanco(BancoDomainModel banco);
        bool Salvar();
        IEnumerable<ValidationResult> IsValid(BancoDomainModel banco);
    }
}
