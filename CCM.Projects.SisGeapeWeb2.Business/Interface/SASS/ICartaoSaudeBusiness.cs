using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeape2.Domain.SASS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface.SASS
{
    public interface ICartaoSaudeBusiness
    {
        List<PesquisaCartaoPontoDomainModel> getPesquisa(PesquisaCartaoPontoDomainModel pesquisa);
        IEnumerable<ConsultaDomainModel> ListarTodos();
        ConsultaDomainModel buscarPorId(int id);
        Task<FuncionarioCartaoSaudeDomainModel> buscarConsultaPorFuncionario(int id);
        Task<List<VinculoDomainModel>> buscarVinculoPendenteDeAtendimentoPorData(DateTime? inicio, DateTime? fim);

        bool AddUpdateConsulta(ConsultaDomainModel domainModel);
        bool DeleteConsulta(ConsultaDomainModel domainModel);

        Task<bool> Salvar();
        int TotalRegistro();

    }
}
