using System.Data;
using BO = G2.ML.BusinessObjects;

namespace G2.ML.BusinessServices
{
    public static class AutoMapperConfiguration
    {
        public static void Configure(AutoMapper.IMapperConfigurationExpression config)
        {
            /*
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<IDataReader, BO.News>();
            });
            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
            */

            /*
            var _config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IDataReader, BO.News>();
            });
            _config.AssertConfigurationIsValid();
            */

            //AutoMapper.Mapper.Initialize(cfg =>
            //{
            //    //AutoMapper.Mappers.MapperRegistry.Mappers.Add(new AutoMapper.Data.DataReaderMapper { YieldReturnEnabled = true });
            //});
        }
    }
}
