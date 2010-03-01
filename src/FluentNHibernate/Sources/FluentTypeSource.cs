using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Sources
{
    public class FluentTypeSource : ITypeSource
    {
        readonly ITypeSource originalSource;

        public FluentTypeSource(ITypeSource originalSource)
        {
            this.originalSource = originalSource;
        }

        public IEnumerable<Type> GetTypes()
        {
            return originalSource.GetTypes().Where(x => x.Closes(typeof(ClassMap<>)));
        }
    }
}