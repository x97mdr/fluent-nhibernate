using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Automapping
{
    public class PrivateAutoMapper : AutoMapper
    {
        private readonly IAutomappingDiscoveryRules rules;

        internal PrivateAutoMapper(IAutomappingStepSet steps, IAutomappingDiscoveryRules rules, IConventionFinder conventionFinder, IEnumerable<InlineOverride> inlineOverrides)
            : base(steps, rules, conventionFinder, inlineOverrides, new PrivateEntityAutomapper(rules, steps, conventionFinder))
        {
            this.rules = rules;
        }
    }

    public class PrivateEntityAutomapper : EntityAutomapper
    {
        readonly IAutomappingDiscoveryRules rules;

        public PrivateEntityAutomapper(IAutomappingDiscoveryRules rules, IAutomappingStepSet steps, IConventionFinder conventionFinder)
            : base(steps, conventionFinder)
        {
            this.rules = rules;
        }

        public override IAutomappingResult Map(Type entityType, IList<string> mappedProperties)
        {
            // This will ONLY map private properties. Do not call base.

            var rule = rules.FindMappablePrivatePropertiesRule;
            if (rule == null)
                throw new InvalidOperationException("The FindMappablePrivateProperties convention must be supplied to use the PrivateAutoMapper. ");

            var results = new List<IAutomappingResult>();

            foreach (var property in entityType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).Select(x => x.ToMember()))
            {
                if (!rule(property)) continue;

                var result = TryToMapProperty(new MappingMetaData(entityType, property), mappedProperties);

                results.Add(result);
            }
            
            return new AutomappingResult(results);
        } 
    }
}
