using System;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Automapping.Steps
{
    public interface IAutomappingStep
    {
        bool IsMappable(Member member);
        IAutomappingResult Map(MappingMetaData metaData);
    }

    public class MappingMetaData
    {
        public Member Member { get; set; }
        public Type EntityType { get; set; }
    }
}