using FluentNHibernate.Automapping.Results;

namespace FluentNHibernate.Automapping.Steps
{
    public interface IAutomappingStep
    {
        bool IsMappable(Member member);
        IAutomappingResult Map(MappingMetaData metaData);
    }
}