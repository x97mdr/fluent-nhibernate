using System;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Automapping.Steps
{
    public class IdentityStep : IAutomappingStep
    {
        readonly IAutomappingStrategy strategy;

        public IdentityStep(IAutomappingStrategy strategy)
        {
            this.strategy = strategy;
        }

        public bool IsMappable(Member property)
        {
            return strategy.GetRules().FindIdentityRule(property);
        }

        public IMappingResult Map(MappingMetaData metaData)
        {
            var idMapping = new IdMapping { ContainingEntityType = metaData.EntityType };
            idMapping.AddDefaultColumn(new ColumnMapping() { Name = metaData.Member.Name });
            idMapping.Name = metaData.Member.Name;
            idMapping.Type = new TypeReference(metaData.Member.PropertyType);
            idMapping.Member = metaData.Member;
            idMapping.SetDefaultValue("Generator", GetDefaultGenerator(metaData.Member));

            var members = new MemberBucket();
            members.SetId(idMapping);
            
            return new AutomappingResult(metaData.EntityType, strategy, members);
        }

        private GeneratorMapping GetDefaultGenerator(Member property)
        {
            var generatorMapping = new GeneratorMapping();
            var defaultGenerator = new GeneratorBuilder(generatorMapping, property.PropertyType);

            if (property.PropertyType == typeof(Guid))
                defaultGenerator.GuidComb();
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(long))
                defaultGenerator.Identity();
            else
                defaultGenerator.Assigned();

            return generatorMapping;
        }
    }
}