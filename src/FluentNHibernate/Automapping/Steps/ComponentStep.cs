using System.Collections.Generic;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Automapping.Steps
{
    public class ComponentStep : IAutomappingStep
    {
        private readonly AutoMapper mapper;
        readonly IAutomappingStrategy strategy;

        public ComponentStep(IAutomappingStrategy strategy, AutoMapper mapper)
        {
            this.strategy = strategy;
            this.mapper = mapper;
        }

        public bool IsMappable(Member property)
        {
            return strategy.GetRules().FindComponentRule(property.PropertyType);
        }

        public IMappingResult Map(MappingMetaData metaData)
        {
            var mapping = new ComponentMapping(ComponentType.Component)
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

            return new AutomappingResult(metaData.EntityType, strategy, members);
        }
    }
}