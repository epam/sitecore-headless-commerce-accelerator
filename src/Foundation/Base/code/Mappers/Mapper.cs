namespace Wooli.Foundation.Base.Mappers
{
    using AutoMapper;

    using Sitecore.Diagnostics;

    public abstract class Mapper : IMapper
    {
        protected AutoMapper.IMapper InnerMapper { get; set; }

        public abstract MapperConfiguration Configuration { get; }

        public TResult Map<TSource, TResult>(TSource source)
        {
            Assert.IsNotNull(this.InnerMapper, "InnerMapper must be initialized");
            Assert.ArgumentNotNull(source, nameof(source));

            return this.InnerMapper.Map<TSource, TResult>(source);
        }
    }
}