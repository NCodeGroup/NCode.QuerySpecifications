using System.ComponentModel;
using NCode.QuerySpecifications.Configuration;

namespace NCode.QuerySpecifications.Configurators
{
    public interface ITransformConfigurator<TIn, TOut> : IQueryConfigurator<TOut>
        where TIn : class
        where TOut : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        ITransformConfiguration<TIn, TOut> TransformConfiguration { get; }
    }
}