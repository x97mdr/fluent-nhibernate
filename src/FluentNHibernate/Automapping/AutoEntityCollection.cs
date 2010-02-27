using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoEntityCollection : IAutomappingStep
    {
        readonly IAutomappingDiscoveryRules rules;
        readonly AutoKeyMapper keys;
        AutoCollectionCreator collections;

        public AutoEntityCollection(IAutomappingDiscoveryRules rules)
        {
            this.rules = rules;
            keys = new AutoKeyMapper(rules);
            collections = new AutoCollectionCreator();
        }

        public bool IsMappable(Member property)
        {
            return property.CanWrite &&
                property.PropertyType.Namespace.In("System.Collections.Generic", "Iesi.Collections.Generic");
        }

        public IAutomappingResult Map(MappingMetaData metaData)
        {
            if (metaData.Member.DeclaringType != metaData.EntityType)
                return new EmptyResult();

            var mapping = collections.CreateCollectionMapping(metaData.Member.PropertyType);

            mapping.ContainingEntityType = metaData.EntityType;
            mapping.Member = metaData.Member;
            mapping.SetDefaultValue(x => x.Name, metaData.Member.Name);

            SetRelationship(metaData, mapping);
            keys.SetKey(metaData, mapping);

            var members = new MemberBucket();
            members.AddCollection(mapping);  
            
            return new AutomappingResult(members);
        }

        private void SetRelationship(MappingMetaData metaData, ICollectionMapping mapping)
        {
            var relationship = new OneToManyMapping
            {
                Class = new TypeReference(metaData.Member.PropertyType.GetGenericArguments()[0]),
                ContainingEntityType = metaData.EntityType
            };

            mapping.SetDefaultValue(x => x.Relationship, relationship);
        }
    }
}