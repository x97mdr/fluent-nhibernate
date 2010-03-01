using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class EntityAutomapper
    {
        private readonly IConventionFinder conventionFinder;
        readonly IAutomappingStrategy strategy;

        public AutoMapper AutoMapper { get; set; }

        public EntityAutomapper(IConventionFinder conventionFinder, IAutomappingStrategy strategy)
        {
            this.strategy = strategy;
            this.conventionFinder = conventionFinder;
        }

        public virtual IMappingResult Map(Type entityType, IList<string> mappedProperties)
        {
            var results = new List<IMappingResult>();
            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                 .Where(x => !x.Name.In(mappedProperties));

            foreach (var property in properties)
            {
                var metaData = new MappingMetaData(entityType, property.ToMember());
                var result = TryToMapProperty(metaData, mappedProperties);

                results.Add(result);
            }

            return new AutomappingResult(entityType, strategy, results);
        }

        protected IMappingResult TryToMapProperty(MappingMetaData metaData, IList<string> mappedProperties)
        {
            if (metaData.Member.Name.In(mappedProperties)) return new EmptyResult();
            if (metaData.Member.HasIndexParameters) return new EmptyResult();

            var applicableStep = strategy.GetSteps().GetSteps(AutoMapper, conventionFinder)
                .FirstOrDefault(x => x.IsMappable(metaData.Member));

            if (applicableStep != null)
            {
                var result = applicableStep.Map(metaData);

                mappedProperties.Add(metaData.Member.Name);

                return result;
            }

            return new EmptyResult();
        }
    }
}
