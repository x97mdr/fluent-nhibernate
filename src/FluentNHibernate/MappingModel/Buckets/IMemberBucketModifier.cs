using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.MappingModel.Buckets
{
    public interface IMemberBucketModifier
    {
        void AddProperty(PropertyMapping property);
        void AddCollection(ICollectionMapping collection);
        void AddReference(ManyToOneMapping manyToOne);
        void AddComponent(IComponentMapping component);
        void AddOneToOne(OneToOneMapping mapping);
        void AddAny(AnyMapping mapping);
        void AddFilter(FilterMapping mapping);
        void AddJoin(JoinMapping mapping);
        void AddStoredProcedure(StoredProcedureMapping mapping);
        void SetId(IIdentityMapping mapping);
        void SetVersion(VersionMapping mapping);
    }
}