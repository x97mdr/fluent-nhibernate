using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Mapping
{
    public class ManualResult : IMappingResult
    {
        public ManualResult(Type typeBeingMapped, IMemberBucket bucket)
        {
            Members = bucket;
            TypeBeingMapped = typeBeingMapped;
        }

        public Type TypeBeingMapped { get; private set; }
        public bool RequiresAutomapping
        {
            get { return false; }
        }
        
        public IAutomappingStrategy AutomappingStrategy
        {
            get { return new NullStrategy(); }
        }

        public IMemberBucket Members { get; private set; }
        
        public void ApplyTo(IMergableWithBucket bucket)
        {
            bucket.MergeWithBucket(Members);
        }

        public IEnumerable<PropertyMapping> Properties
        {
            get { return Members.Properties; }
        }
        public IEnumerable<ICollectionMapping> Collections
        {
            get { return Members.Collections; }
        }
        public IEnumerable<ManyToOneMapping> References
        {
            get { return Members.References; }
        }
        public IEnumerable<IComponentMapping> Components
        {
            get { return Members.Components; }
        }
        public IEnumerable<OneToOneMapping> OneToOnes
        {
            get { return Members.OneToOnes; }
        }
        public IEnumerable<AnyMapping> Anys
        {
            get { return Members.Anys; }
        }
        public IEnumerable<FilterMapping> Filters
        {
            get { return Members.Filters; }
        }
        public IEnumerable<JoinMapping> Joins
        {
            get { return Members.Joins; }
        }
        public IEnumerable<StoredProcedureMapping> StoredProcedures
        {
            get { return Members.StoredProcedures; }
        }
        public IIdentityMapping Id
        {
            get { return Members.Id; }
        }
        public VersionMapping Version
        {
            get { return Members.Version; }
        }
        public AttributeStore Attributes
        {
            get { return Members.Attributes; }
        }
    }
}