using System;
using System.Linq;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public class TypeAndMemberStructure<T> : BaseMappingStructure<T>
        where T : ITypeAndMemberMapping, new()
    {
        readonly Type type;
        readonly Member member;

        public TypeAndMemberStructure(Type type, Member member)
        {
            this.type = type;
            this.member = member;
        }

        public override T CreateMappingNode()
        {
            var mapping = new T();

            mapping.Initialise(type, member);

            
            Children
                .Select(x => x.CreateMappingNode())
                .Each(mapping.AddChild);

            return mapping;
        }
    }
}