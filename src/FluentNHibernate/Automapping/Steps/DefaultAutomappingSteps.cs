using System.Collections.Generic;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Automapping.Steps
{
    public class DefaultAutomappingSteps : IAutomappingStepSet
    {
        readonly IAutomappingStrategy strategy;

        public DefaultAutomappingSteps(IAutomappingStrategy strategy)
        {
            this.strategy = strategy;
        }

        public IEnumerable<IAutomappingStep> GetSteps(AutoMapper automapper, IConventionFinder conventionFinder)
        {
            return new IAutomappingStep[]
            {
                new IdentityStep(strategy), 
                new VersionStep(strategy), 
                new ComponentStep(strategy, automapper),
                new PropertyStep(strategy, conventionFinder),
                new ManyToManyStep(strategy),
                new ManyToOneStep(strategy),
                new OneToManyStep(strategy),
            };
        }
    }
}