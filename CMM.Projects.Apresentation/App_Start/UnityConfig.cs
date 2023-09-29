using CCM.Projects.SisGeapeWeb2.Business;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CCM.Projects.SisGeapeWeb2.Business.Interface.SASS;
using CCM.Projects.SisGeapeWeb2.Business.SASS;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CMM.Projects.Apresentation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<CCM.Projects.SisGeapeWeb2.Repository.Infra.Interface.IUnitOfWork, CCM.Projects.SisGeapeWeb2.Repository.Infra.UnitOfWork>();
            container.RegisterType<IBancoBusiness, BancoBusiness>();
            container.RegisterType<IBeneficioBusiness, BeneficioBusiness>();
            container.RegisterType<ICargoBusiness, CargoBusiness>();
            container.RegisterType<ILicencaBusiness, LicencaBusiness>();
            container.RegisterType<ISystemUser, SystemUserBusiness>();
            container.RegisterType<IUnidadeBusiness, UnidadeBusiness>();
            container.RegisterType<IFuncaoBusiness, FuncaoBusiness>();
            container.RegisterType<IVinculoBusiness, VinculoBusiness>();
            container.RegisterType<IVinculoUnidadeBusiness, VinculoUnidadeBusiness>();
            container.RegisterType<IFuncionarioBusiness, FuncionarioBusiness>();
            container.RegisterType<IFuncionarioDependenteBusiness, FuncionarioDependenteBusiness>();
            container.RegisterType<IFuncaoVinculoBusiness, FuncaoVinculoBusiness>();
            container.RegisterType<IBeneficioVinculoBusiness, BeneficioVinculoBusiness>();
            container.RegisterType<IJustificativaPontoBusiness, JustificativaPontoBusiness>();
            container.RegisterType<ICidBusiness, CidBusiness>();
            container.RegisterType<IFeriasBusiness, FeriasBusiness>();
            container.RegisterType<IConselhoClasseBusiness, ConselhoClasseBusiness>();
            container.RegisterType<IPontoImportacaoBusiness, PontoImportacaoBusiness>();
            container.RegisterType<ICartaoPontoBusiness, CartaoPontoBusiness>();
            container.RegisterType<IAvaliacaoClinicaBusiness, AvaliacaoClinicaBusiness>();
            container.RegisterType<ICartaoSaudeBusiness, CartaoSaudeBusiness>();


            container.RegisterType<IFeriadoBusiness, FeriadoBusiness>();
            container.RegisterType<IControleCNHBusiness, ControleCNHBusiness>();




            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(container);
        }
    }
}