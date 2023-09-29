using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeape2.Domain.SASS;
using CMM.Projects.Apresentation.Areas.SASS.Models;
using CMM.Projects.Apresentation.Models;

namespace SisGeape2.Apresentation.AutoMapper.Mapping
{
    public class ModelViewToDomain : Profile
    {
        public ModelViewToDomain()
        {
            CreateMap<bancoModelView, BancoDomainModel>();
            CreateMap<FuncionarioModelView, FuncionarioDomainModel>();
            CreateMap<BeneficioModelView, BeneficioDomainModel>();
            CreateMap<BeneficioVinculoModelView, BeneficioVinculoDomainModel>();
            CreateMap<CargoModelView, CargoDomainModel>();
            CreateMap<FuncaoModelView, FuncaoDomainModel>();
            CreateMap<FuncionarioDependenteModelView, FuncionarioDependenteDomainModel>();
            CreateMap<ReferenciaFuncaoModelView, ReferenciaFuncaoDomainModel>();
            CreateMap<UnidadeModelView, UnidadeDomainModel>();
            CreateMap<VinculoModelView, VinculoDomainModel>();
            CreateMap<VinculoSituacaoModelView, VinculoSituacaoDomainModel>();
            CreateMap<VinculoTipoModelView, VinculoTipoDomainModel>();
            CreateMap<VinculoUnidadeModelView, VinculoUnidadeDomainModel>();
            CreateMap<SystemUserModelView, SystemUserDomainModel>();
            CreateMap<SystemUserEditDomainModel, SystemUserDomainModel>();
            CreateMap<UnidadeModelView, UnidadeDomainModel>();
            CreateMap<MotivoJustificativaModelView, MotivoJustificativaDomainModel>();
            CreateMap<JustificativaPontoModelView, JustificativaPontoDomainModel>();
            CreateMap<CidModelView, CidDomainModel>();
            CreateMap<FeriasModelView, FeriasDomainModel>();
            CreateMap<FuncionarioSolicitarCrachaModelView, FuncionarioSolicitarCrachaDomainModel>();
            CreateMap<FuncionarioPesquisaModelView, FuncionarioPesquisaDomainModel>();
            CreateMap<FuncionarioCartaoPontoModelView, FuncionarioCartaoPontoDomainModel>();
            CreateMap<FuncionarioNomeCrachaModelView, FuncionarioNomeCrachaDomainModel>();
            CreateMap<CartaoPontoModelView, CartaoPontoDomainModel>();
            CreateMap<ConselhoModelView, ConselhoDomainModel>();
            CreateMap<ConselhoClasseModelView, ConselhoClasseDomainModel>();
            CreateMap<ConselhoClasseVinculoModelView, ConselhoClasseVinculoDomainModel>();
            CreateMap<PesquisaConselhoClasseModelView, PesquisaConselhoClasseDomainModel>();
            CreateMap<PesquisaConselhoClasseListModelView, PesquisaConselhoClasseListDomainModel>();
            CreateMap<PontoImportacaoModelView, PontoImportacaoDomainModel>();
            CreateMap<PesquisaCartaoPontoModelView, PesquisaCartaoPontoDomainModel>();
            CreateMap<UnidadeCartaoPontoModelView, UnidadeCartaoPontoDomainModel>();
            //Adicionado por Angelo Matos em 27022020
            CreateMap<UnidadeCartaoPontoFaltasModelView, UnidadeCartaoPontoFaltasDomainModel>();

            CreateMap<AvaliacaoClinicaModelView, AvaliacaoClinicaDomainModel>();
            CreateMap<FuncionarioCartaoSaudeModelView, FuncionarioCartaoSaudeDomainModel>();
            CreateMap<ConsultaModelView, ConsultaDomainModel>();
            CreateMap<FeriadoModelView, FeriadoDomainModel>();
            CreateMap<SystemUserRolesDomainView, SystemRoleModelView>();



            CreateMap<ControleCNHVinculoModelView, ControleCNHVinculoDomainModel>();
            CreateMap<ControleCNHModelView, ControleCNHDomainModel>();
            CreateMap<ControleCNHModelView, ControleCNHAuxiliarDomainModel>();
            CreateMap<ControleCNHAuxiliarModelView, ControleCNHAuxiliarDomainModel>();
            CreateMap<ControleCNHPesquisaModelView, ControleCNHPesquisaDomainModel>();

            //Adicionado por Angelo Matos em 26092019
            CreateMap<FuncionarioRelatorioFuncaoModelView, FuncionarioRelatorioFuncaoDomainModel>();
            CreateMap<FuncionarioRelatorioAposentadoriaModelView, FuncionarioRelatorioAposentadoriaDomainModel>();
            CreateMap<FuncionarioRelatorioBeneficioModelView, FuncionarioRelatorioBeneficioDomainModel>();
            CreateMap<FuncionarioRelatorioCessaoModelView, FuncionarioRelatorioCessaoDomainModel>();



        }

    }


}