using System;
using System.Collections.Generic;

namespace FluentNHibernate.AV2
{
    public class TypeMapper
    {
        public IEnumerable<IMapping> Automap(IEnumerable<Type> types)
        {
            return null;
        }
    }

    public interface IMapping
    {}

    public interface IPreProcessedMapping
    {
        
    }

    public static class PipelineStuff
    {
        public static TOut Compose<TIn, TOut>(this TIn input, Func<TIn, TOut> func)
        {
            return func(input);
        }
    }

    public class Pipeline<TIn, TOut>
    {
        private readonly List<IFilter> filters = new List<IFilter>();

        public void AddFilter<TFilterIn, TFilterOut>(Filter<TFilterIn, TFilterOut> filter)
        {
            filters.Add(filter);
        }

        public TOut Filter(TIn input)
        {
            object result = input;

            foreach (var filter in filters)
                result = filter.Go(result);

            return (TOut)result;
        }
    }

    public interface IFilter
    {
        object Go(object intput);
    }

    public class Filter<TIn, TOut> : IFilter
    {
        readonly Func<TIn, TOut> transform;

        public Filter(Func<TIn, TOut> transform)
        {
            this.transform = transform;
        }

        public object Go(object input)
        {
            if (!(input is TIn))
                throw new InvalidOperationException("Pipe broke.");

            return transform((TIn)input);
        }
    }

    //public class Pipe
    //{
        
    //}

    //public interface IResolvedMapping
    //{}

    //public interface IUnresolvedMapping
    //{}

    //public class UnresolvedClassMapping : IUnresolvedMapping
    //{}

    //public interface IStep<TIn, TOut>
    //{
    //    TOut Process(TIn input);
    //}

    //public class ParseStep : IStep<IEnumerable<Type>, IEnumerable<IUnresolvedMapping>>
    //{
    //    public IEnumerable<IUnresolvedMapping> Process(IEnumerable<Type> input)
    //    {
    //        yield break;
    //    }
    //}

    //public class ResolutionStep : IStep<IEnumerable<IUnresolvedMapping>, IEnumerable<IResolvedMapping>>
    //{
    //    public IEnumerable<IResolvedMapping> Process(IEnumerable<IUnresolvedMapping> input)
    //    {
    //        yield break;
    //    }
    //}

    //public static class FuncComposer
    //{
    //    public static K<T, Answer> ToContinuation<T, Answer>(this T value)
    //    {
    //        return x => x(value);
    //    }
    //}
}
