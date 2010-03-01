using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Automapping.Steps
{
    public interface IAutomappingStep
    {
        bool IsMappable(Member member);
        IMappingResult Map(MappingMetaData metaData);
    }
}