using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Automapping.Results
{
    public class EmptyResult : IAutomappingResult
    {
        public void ApplyTo(IMergableWithBucket bucket)
        {}

        public IEnumerable<PropertyMapping> Properties
        {
            get { yield break; }
        }
        public IEnumerable<ICollectionMapping> Collections
        {
            get { yield break; }
        }
        public IEnumerable<ManyToOneMapping> References
        {
            get { yield break; }
        }
        public IEnumerable<IComponentMapping> Components
        {
            get { yield break; }
        }
        public IEnumerable<OneToOneMapping> OneToOnes
        {
            get { yield break; }
        }
        public IEnumerable<AnyMapping> Anys
        {
            get { yield break; }
        }
        public IEnumerable<FilterMapping> Filters
        {
            get { yield break; }
        }

        public IEnumerable<JoinMapping> Joins
        {
            get { yield break; }
        }

        public IEnumerable<StoredProcedureMapping> StoredProcedures
        {
            get { yield break; }
        }
        public IIdentityMapping Id
        {
            get { return null; }
        }
        public VersionMapping Version
        {
            get { return null; }
        }
    }
}