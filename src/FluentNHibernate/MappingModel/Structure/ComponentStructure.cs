using System;
using System.Linq;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public class ComponentStructure : BaseMappingStructure<ComponentMapping>
    {
        readonly ComponentType componentType;
        readonly Member member;
        readonly Type type;

        public ComponentStructure(ComponentType componentType, Member member, Type type)
        {
            this.componentType = componentType;
            this.member = member;
            this.type = type;
        }

        public override ComponentMapping CreateMappingNode()
        {
            var mapping = new ComponentMapping(componentType);

            mapping.Initialise(type);
            mapping.Initialise(member);

            Children
                .Select(x => x.CreateMappingNode())
                .Each(mapping.AddChild);

            return mapping;
        }
    }
}