using CCM.Projects.SisGeape2.Domain;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface
{
    public interface IConselhoClasseBusiness
    {
        bool AddUpdateConselho(ConselhoDomainModel domainModel);
        bool AddUpdateConselhoClasse(ConselhoClasseDomainModel domainModel);
        bool DeleteConselhoClasse(ConselhoClasseDomainModel domainModel);
        bool DeleteConselho(ConselhoDomainModel domainModel);

        ConselhoClasseDomainModel GetConselhoClasseById(int id);
        ConselhoDomainModel GetConselhoById(int id);

        List<ConselhoDomainModel> ddlConselhoByCargo(int cargo);

        List<ConselhoDomainModel> GetConselho();


        List<ConselhoClasseDomainModel> GetConselhoClasse();
        List<ConselhoClasseDomainModel> GetConselhoClasseByVinculo(int vnc);

        List<PesquisaConselhoClasseListDomainModel> GetPesquisaConselho(PesquisaConselhoClasseDomainModel domainModel);

        ConselhoClasseVinculoDomainModel GetFuncionarioConselhoByVinculo(int vnc);

        int TotalRegistroConselho();
        int TotalRegistroConselhoClasse();

        bool Salvar();
    }
}
