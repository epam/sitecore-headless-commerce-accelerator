namespace Wooli.Foundation.Base.Mappers
{
    using Sitecore.Diagnostics;

    public abstract class Mapper : IMapper
    {
        protected AutoMapper.IMapper InnerMapper { get; set; }

        public TResult Map<TSource, TResult>(TSource source)
        {
            Assert.IsNotNull(this.InnerMapper, "InnerMapper must be initialized");
            Assert.ArgumentNotNull(source, nameof(source));

            return this.InnerMapper.Map<TSource, TResult>(source);
        }
    }
}