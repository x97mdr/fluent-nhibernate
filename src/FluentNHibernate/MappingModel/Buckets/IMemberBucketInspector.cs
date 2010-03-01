using System.Collections.Generic;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.MappingModel.Buckets
{
    public interface IMemberBucketInspector
    {
        IEnumerable<PropertyMapping> Properties { get; }
        IEnumerable<ICollectionMapping> Collections { get; }
        IEnumerable<ManyToOneMapping> References { get; }
        IEnumerable<IComponentMapping> Components { get; }
        IEnumerable<OneToOneMapping> OneToOnes { get; }
        IEnumerable<AnyMapping> Anys { get; }
        IEnumerable<FilterMapping> Filters { get; }
        IEnumerable<JoinMapping> Joins { get; }
        IEnumerable<StoredProcedureMapping> StoredProcedures { get; }
        IIdentityMapping Id { get; }
        VersionMapping Version { get; }
        AttributeStore Attributes { get; }
    }
}