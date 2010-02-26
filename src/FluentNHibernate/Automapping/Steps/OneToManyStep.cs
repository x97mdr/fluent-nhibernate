using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;

namespace FluentNHibernate.Automapping.Steps
{
    public class OneToManyStep : IAutomappingStep
    {
        readonly SimpleTypeCollectionStep simpleTypeCollectionStep;
        readonly AutoEntityCollection entityCollectionStep;

        public OneToManyStep(IAutomappingDiscoveryRules rules)
        {
            simpleTypeCollectionStep = new SimpleTypeCollectionStep(rules);
            entityCollectionStep = new AutoEntityCollection(rules);
        }

        public bool IsMappable(Member property)
        {
            return simpleTypeCollectionStep.IsMappable(property) ||
                   entityCollectionStep.IsMappable(property);
        }

        public IAutomappingResult Map(MappingMetaData metaData)
        {
            if (metaData.Member.DeclaringType != metaData.EntityType)
                return new EmptyResult();

            if (simpleTypeCollectionStep.IsMappable(metaData.Member))
                return simpleTypeCollectionStep.Map(metaData);
            else if (entityCollectionStep.IsMappable(metaData.Member))
                return entityCollectionStep.Map(metaData);

            return new EmptyResult();
        }
    }
}