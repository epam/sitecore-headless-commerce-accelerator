using AutoMapper;

namespace HCA.Foundation.SitecoreCommerce.Mappers.Provider
{
    public class ProviderMapper: Base.Mappers.Mapper
    {
        public ProviderMapper() => InnerMapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ProviderProfile>()));
    }
}