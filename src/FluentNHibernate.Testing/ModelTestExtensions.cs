using System;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Testing
{
    public static class ModelTestExtensions
    {
        public static bool IsSpecified<T>(this T model, Attr attr)
            where T : IMappingBase
        {
            return model.HasUserDefinedValue(attr);
        }
    }
}