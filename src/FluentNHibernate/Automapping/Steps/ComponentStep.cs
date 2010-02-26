using System.Collections.Generic;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Automapping.Steps
{
    public class ComponentStep : IAutomappingStep
    {
        private readonly IAutomappingDiscoveryRules rules;
        private readonly AutoMapper mapper;

        public ComponentStep(IAutomappingDiscoveryRules rules, AutoMapper mapper)
        {
            this.rules = rules;
            this.mapper = mapper;
        }

        public bool IsMappable(Member property)
        {
            return rules.FindComponentRule(property.PropertyType);
        }

        public IAutomappingResult Map(MappingMetaData metaData)
        {
            var mapping = new ComponentMapping
            {
                Name = metaData.Member.Name,
                Member = metaData.Member,
                ContainingEntityType = metaData.EntityType,
                Type = metaData.Member.PropertyType
            };

            mapper.FlagAsMapped(metaData.Member.PropertyType);
            mapper.MergeMap(metaData.Member.PropertyType, mapping, new List<string>());

            var members = new MemberBucket();
            members.AddComponent(mapping);

            return new AutomappingResult(members);
        }
    }
}