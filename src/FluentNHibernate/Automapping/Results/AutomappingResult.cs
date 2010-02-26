using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping.Results
{
    public class AutomappingResult : IAutomappingResult
    {
        readonly IMemberBucket container = new MemberBucket();

        public AutomappingResult()
            : this(new MemberBucket())
        {}

        public AutomappingResult(IMemberBucket container)
        {
            this.container = container;
        }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return container.Properties; }
        }
        public IEnumerable<ICollectionMapping> Collections
        {
            get { return container.Collections; }
        }
        public IEnumerable<ManyToOneMapping> References
        {
            get { return container.References; }
        }
        public IEnumerable<IComponentMapping> Components
        {
            get { return container.Components; }
        }
        public IEnumerable<OneToOneMapping> OneToOnes
        {
            get { return container.OneToOnes; }
        }
        public IEnumerable<AnyMapping> Anys
        {
            get { return container.Anys; }
        }
        public IEnumerable<FilterMapping> Filters
        {
            get { return container.Filters; }
        }
        public IEnumerable<JoinMapping> Joins
        {
            get { return container.Joins; }
        }
        public IEnumerable<StoredProcedureMapping> StoredProcedures
        {
            get { return container.StoredProcedures; }
        }
        public IIdentityMapping Id
        {
            get { return container.Id; }
        }
        public VersionMapping Version
        {
            get { return container.Version; }
        }

        public AutomappingResult(IEnumerable<IAutomappingResult> results)
        {
            results.Each(IncludeResult);
        }

        public void ApplyTo(IMergableWithBucket bucket)
        {
            bucket.MergeWithBucket(this);
        }

        void IncludeResult(IAutomappingResult partialResult)
        {
            partialResult.ApplyTo(container);
        }
    }
}