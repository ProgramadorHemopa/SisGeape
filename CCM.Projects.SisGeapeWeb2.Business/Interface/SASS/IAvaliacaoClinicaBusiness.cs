using CCM.Projects.SisGeape2.Domain.SASS;
using System.Collections.Generic;

namespace CCM.Projects.SisGeapeWeb2.Business.Interface.SASS
{
    public interface IAvaliacaoClinicaBusiness
    {
        bool AddUpdateAvalicaoClinica(AvaliacaoClinicaDomainModel domainModel);
        AvaliacaoClinicaDomainModel AvaliacaoClinicaById(int id);
        List<AvaliacaoClinicaDomainModel> ListarAvaliacaoClinica();

        bool DeleteAvaliacaoClinica(AvaliacaoClinicaDomainModel domainModel);
        bool Salvar();
    }
}
