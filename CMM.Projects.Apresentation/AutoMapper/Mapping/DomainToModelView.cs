using AutoMapper;
using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeape2.Domain.SASS;
using CMM.Projects.Apresentation.Areas.SASS.Models;
using CMM.Projects.Apresentation.Models;

namespace SisGeape2.Apresentation.AutoMapper.Mapping
{
    public class DomainToModelView : Profile
    {
        public DomainToModelView()
        {
            CreateMap<BancoDomainModel, bancoModelView>();
            CreateMap<FuncionarioDomainModel, FuncionarioModelView>();
            CreateMap<BeneficioDomainModel, BeneficioModelView>();
            CreateMap<BeneficioVinculoDomainModel, BeneficioVinculoModelView>();
            CreateMap<CargoDomainModel, CargoModelView>();
            CreateMap<FuncaoDomainModel, FuncaoModelView>();
            CreateMap<FuncionarioDependenteDomainModel, FuncionarioDependenteModelView>();
            CreateMap<ReferenciaFuncaoDomainModel, ReferenciaFuncaoModelView>();
            CreateMap<UnidadeDomainModel, UnidadeModelView>();
            CreateMap<VinculoDomainModel, VinculoModelView>();
            CreateMap<VinculoSituacaoDomainModel, VinculoSituacaoModelView>();
            CreateMap<VinculoTipoDomainModel, VinculoTipoModelView>();
            CreateMap<VinculoUnidadeDomainModel, VinculoUnidadeModelView>();
            CreateMap<SystemUserDomainModel, SystemUserModelView>();
            CreateMap<UnidadeDomainModel, UnidadeModelView>();
            CreateMap<FuncaoVinculoDomainModel, FuncaoVinculoModelView>();
            CreateMap<JustificativaPontoDomainModel, JustificativaPontoModelView>();
            CreateMap<MotivoJustificativaDomainModel, MotivoJustificativaModelView>();
            CreateMap<CidDomainModel, CidModelView>();
            CreateMap<FeriasDomainModel, FeriasModelView>();
            CreateMap<FuncionarioSolicitarCrachaDomainModel, FuncionarioSolicitarCrachaModelView>();
            CreateMap<ConselhoDomainModel, ConselhoModelView>();
            CreateMap<ConselhoClasseDomainModel, ConselhoClasseModelView>();
            CreateMap<ConselhoClasseVinculoDomainModel, ConselhoClasseVinculoModelView>();
            CreateMap<PesquisaConselhoClasseDomainModel, PesquisaConselhoClasseModelView>();
            CreateMap<PesquisaConselhoClasseListDomainModel, PesquisaConselhoClasseListModelView>();
            CreateMap<FuncionarioPesquisaDomainModel, FuncionarioPesquisaModelView>();
            CreateMap<FuncionarioCartaoPontoDomainModel, FuncionarioCartaoPontoModelView>();

            CreateMap<FuncionarioNomeCrachaDomainModel, FuncionarioNomeCrachaModelView>();

            CreateMap<CartaoPontoDomainModel, CartaoPontoModelView>();
            CreateMap<PontoImportacaoDomainModel, PontoImportacaoModelView>();
            CreateMap<PesquisaCartaoPontoDomainModel, PesquisaCartaoPontoModelView>();
            CreateMap<UnidadeCartaoPontoDomainModel, UnidadeCartaoPontoModelView>();
            //Adicionado por Angelo Matos em 27022020
            CreateMap<UnidadeCartaoPontoFaltasDomainModel, UnidadeCartaoPontoFaltasModelView>();


            CreateMap<AvaliacaoClinicaDomainModel, AvaliacaoClinicaModelView>();
            CreateMap<FuncionarioCartaoSaudeDomainModel, FuncionarioCartaoSaudeModelView>();
            CreateMap<ConsultaDomainModel, ConsultaModelView>();
            CreateMap<SystemUserRolesDomainView, SystemRoleModelView>();
            CreateMap<FeriadoDomainModel, FeriadoModelView>();



            CreateMap<ControleCNHVinculoDomainModel, ControleCNHVinculoModelView>();
            CreateMap<ControleCNHDomainModel, ControleCNHModelView>();
            CreateMap<ControleCNHAuxiliarDomainModel, ControleCNHAuxiliarModelView>();
            CreateMap<ControleCNHAuxiliarDomainModel, ControleCNHModelView>();
            CreateMap<ControleCNHPesquisaDomainModel, ControleCNHPesquisaModelView>();

            //Adicionado por Angelo Matos em 26092019
            CreateMap<FuncionarioRelatorioFuncaoDomainModel, FuncionarioRelatorioFuncaoModelView>();
            CreateMap<FuncionarioRelatorioAposentadoriaDomainModel, FuncionarioRelatorioAposentadoriaModelView>();
            CreateMap<FuncionarioRelatorioBeneficioDomainModel, FuncionarioRelatorioBeneficioModelView>();
            CreateMap<FuncionarioRelatorioCessaoDomainModel, FuncionarioRelatorioCessaoModelView>();


        }

    }
}