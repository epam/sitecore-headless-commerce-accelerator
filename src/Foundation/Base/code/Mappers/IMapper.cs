namespace Wooli.Foundation.Base.Mappers
{
    using AutoMapper;

    /// <summary>
    /// Performs mapping operations
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Mapper configuration
        /// </summary>
        MapperConfiguration Configuration { get; }

        /// <summary>
        /// Maps source object to object of TResult type
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TResult">Type of result object</typeparam>
        /// <param name="source">The source object to map</param>
        /// <returns>Mapping result object</returns>
        TResult Map<TSource, TResult>(TSource source);
    }
}
