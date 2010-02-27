using System;

namespace FluentNHibernate.Automapping.Steps
{
    public class MappingMetaData
    {
        public Member Member { get; private set; }
        public Type EntityType { get; private set; }

        public MappingMetaData(Type entityType, Member member)
        {
            EntityType = entityType;
            Member = member;
        }
    }
}