using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.Web.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void Configure(AutoMapper.IMapperConfigurationExpression config)
        {
			config.CreateMap<Models.LoginVM, BO.LoginBO>().ReverseMap();
			config.CreateMap<Models.UserLoginResultVM, BO.UserLoginResultBO>().ReverseMap();

			config.CreateMap<BO.ChangePwdBO, Models.ChangePwdVM>()
				.ForMember(d => d.ConfirmPassword, opt => opt.Ignore())
				.ReverseMap();
			config.CreateMap<Models.RegisterVM, BO.RegisterBO>().ReverseMap();

			config.CreateMap<Models.BuyerVM, BO.BuyerBO>().ReverseMap();
			config.CreateMap<Models.PagerVM, BO.PagerBO>().ReverseMap();
			config.CreateMap<Models.BuyerTypeVM, BO.BuyerTypeBO>().ReverseMap();
			config.CreateMap<Models.BuyerSearchVM, BO.BuyerSearchBO>().ReverseMap();
			config.CreateMap<Models.BuyerSeachResultVM, BO.BuyerSearchResultBO>().ReverseMap();

			config.CreateMap<Models.SaleVM, BO.SaleBO>().ReverseMap();
			config.CreateMap<Models.SaleAddVM, BO.SaleAddBO>().ReverseMap();
			config.CreateMap<Models.SaleSearchVM, BO.SaleSearchBO>().ReverseMap();
			config.CreateMap<Models.SaleSearchResultVM, BO.SaleSearchResultBO>().ReverseMap();

			config.CreateMap<Models.SaleBrokerage, BO.SaleBrokerageBO>().ReverseMap();
			config.CreateMap<Models.SalePayment, BO.SalePaymentBO>().ReverseMap();

			config.CreateMap<BO.SalesReportBO, Models.SalesReportVM>()
				.ForMember(d => d.BuyerList, opt => opt.Ignore())
				.ForMember(d => d.SallerList, opt => opt.Ignore())
				.ForMember(d => d.StatusList, opt => opt.Ignore())
				.ReverseMap();
			config.CreateMap<BO.BrokerageReportBO, Models.BrokerageReportVM>()
				.ForMember(d => d.BuyerList, opt => opt.Ignore())
				.ForMember(d => d.SallerList, opt => opt.Ignore())
				.ForMember(d => d.StatusList, opt => opt.Ignore())
				.ReverseMap();

			//config.CreateMap<Areas.Admin.Models.PressVM, BO.Press>().ReverseMap();
			//config.CreateMap<Areas.Admin.Models.ImageVM, BO.Image>()
			//      .ForMember(d => d.IsDeleted, opt => opt.Ignore())
			//      .ForMember(d => d.ImageType, opt => opt.Ignore())
			//      .ReverseMap();

		}
    }
}