using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using BS = G2.ML.BusinessServices;

namespace G2.ML.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
		#region Unity Container
		private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
		{
			var container = new UnityContainer();
			RegisterTypes(container);
			return container;
		});

		public static IUnityContainer GetConfiguredContainer()
		{
			return container.Value;
		}
		#endregion

		public static void RegisterTypes(IUnityContainer container)
		{
			RegisterControllerDependencies(container);
			RegisterBusinessServices(container);
		}

		public static void RegisterBusinessServices(IUnityContainer container)
		{
			container.RegisterType<BS.Contracts.IAccountService, BS.Factories.AccountService>();
			container.RegisterType<BS.Contracts.IBuyerService, BS.Factories.BuyerService>();
			container.RegisterType<BS.Contracts.IReportService, BS.Factories.ReportService>();
			container.RegisterType<BS.Contracts.ISaleService, BS.Factories.SaleService>();
			container.RegisterType<BS.Contracts.IDashboardService, BS.Factories.DashboardService>();
		}

		public static void RegisterControllerDependencies(IUnityContainer container)
		{
			container.RegisterType<Web.Controllers.ReportController>(new InjectionConstructor(new ResolvedParameter<BS.Contracts.IReportService>(),
																				 new ResolvedParameter<BS.Contracts.ISaleService>()));
			container.RegisterType<Web.Controllers.BuyerController>(new InjectionConstructor(new ResolvedParameter<BS.Contracts.IBuyerService>()));
			container.RegisterType<Web.Controllers.SaleController>(new InjectionConstructor(new ResolvedParameter<BS.Contracts.ISaleService>()));
			container.RegisterType<Web.Controllers.DashboardController>(new InjectionConstructor(new ResolvedParameter<BS.Contracts.IDashboardService>()));
		}
	}
}
