using System.Collections.Generic;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.MappingModel
{
    public interface ICommonMembersInspector
    {
        IEnumerable<PropertyMapping> Properties { get; }
        IEnumerable<ICollectionMapping> Collections { get; }
        IEnumerable<ManyToOneMapping> References { get; }
        IEnumerable<IComponentMapping> Components { get; }
        IEnumerable<OneToOneMapping> OneToOnes { get; }
        IEnumerable<AnyMapping> Anys { get; }
        IEnumerable<FilterMapping> Filters { get; }
    }

    public interface ICommonMembersModifier
    {
        void AddProperty(PropertyMapping property);
        void AddCollection(ICollectionMapping collection);
        void AddReference(ManyToOneMapping manyToOne);
        void AddComponent(IComponentMapping component);
        void AddOneToOne(OneToOneMapping mapping);
        void AddAny(AnyMapping mapping);
        void AddFilter(FilterMapping mapping);
    }

    public interface ICommonMappingMembers : ICommonMembersInspector, ICommonMembersModifier
    {}
}