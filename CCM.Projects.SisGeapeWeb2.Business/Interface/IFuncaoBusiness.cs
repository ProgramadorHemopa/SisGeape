using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IFuncaoBusiness
    {
        int TotalRegistros();
        List<FuncaoDomainModel> GetFuncao();
        List<FuncaoDomainModel> GetFuncaoRecords(int pageStart, int pageSize);
        List<FuncaoDomainModel> GetFuncaoByName(string name);
        FuncaoDomainModel GetFuncaoById(int Id);
        void AddUpdateFuncao(FuncaoDomainModel modelDomain);
        bool DeleteFuncao(FuncaoDomainModel modelDomain);
        bool Salvar();
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IsValid(FuncaoDomainModel cargo);




        ReferenciaFuncaoDomainModel GetReferenciaFuncaoById(int Id);
        List<ReferenciaFuncaoDomainModel> GetAllReferenciaFuncao();
        bool AddUpdateReferenciaFuncao(ReferenciaFuncaoDomainModel modelDomain);
        bool DeleteReferenciaFuncao(ReferenciaFuncaoDomainModel modelDomain);
        int ReferenciaFuncaoTotalRegistros();


    }
}
