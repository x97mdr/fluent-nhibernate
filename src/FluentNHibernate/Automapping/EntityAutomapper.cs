using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class EntityAutomapper
    {
        private readonly IAutomappingStepSet steps;
        private readonly IConventionFinder conventionFinder;

        public AutoMapper AutoMapper { get; set; }

        public EntityAutomapper(IAutomappingStepSet steps, IConventionFinder conventionFinder)
        {
            this.steps = steps;
            this.conventionFinder = conventionFinder;
        }

        public virtual IAutomappingResult Map(Type entityType, IList<string> mappedProperties)
        {
            var results = new List<IAutomappingResult>();
            var properties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                 .Where(x => !x.Name.In(mappedProperties));

            foreach (var property in properties)
            {
                var metaData = new MappingMetaData()
                {
                    EntityType = entityType,
                    Member = property.ToMember()
                };

                var result = TryToMapProperty(metaData, mappedProperties);

                results.Add(result);
            }

            return new AutomappingResult(results);
        }

        protected IAutomappingResult TryToMapProperty(MappingMetaData metaData, IList<string> mappedProperties)
        {
            if (metaData.Member.Name.In(mappedProperties)) return new EmptyResult();
            if (metaData.Member.HasIndexParameters) return new EmptyResult();

            var applicableStep = steps.GetSteps(AutoMapper, conventionFinder)
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
