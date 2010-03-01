using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentNHibernate.Sources
{
    public class RestrictedTypeSource : ITypeSource
    {
        readonly ITypeSource originalSource;
        readonly Func<Type, bool> criteria;

        public RestrictedTypeSource(ITypeSource originalSource, Func<Type, bool> criteria)
        {
            this.originalSource = originalSource;
            this.criteria = criteria;
        }

        public IEnumerable<Type> GetTypes()
        {
            return originalSource.GetTypes().Where(criteria);
        }
    }
}