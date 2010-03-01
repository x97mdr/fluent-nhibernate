using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Automapping.Steps
{
    public class OneToManyStep : IAutomappingStep
    {
        readonly SimpleTypeCollectionStep simpleTypeCollectionStep;
        readonly AutoEntityCollection entityCollectionStep;

        public OneToManyStep(IAutomappingStrategy strategy)
        {
            simpleTypeCollectionStep = new SimpleTypeCollectionStep(strategy);
            entityCollectionStep = new AutoEntityCollection(strategy);
        }

        public bool IsMappable(Member property)
        {
            return simpleTypeCollectionStep.IsMappable(property) ||
                   entityCollectionStep.IsMappable(property);
        }

        public IMappingResult Map(MappingMetaData metaData)
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