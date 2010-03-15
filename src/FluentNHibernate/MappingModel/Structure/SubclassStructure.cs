using System;
using System.Linq;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public class SubclassStructure : BaseMappingStructure<SubclassMapping>
    {
        readonly SubclassType subclassType;
        readonly Type type;

        public SubclassStructure(SubclassType subclassType, Type type)
        {
            this.subclassType = subclassType;
            this.type = type;
        }

        public override SubclassMapping CreateMappingNode()
        {
            var mapping = new SubclassMapping(subclassType);

            mapping.Initialise(type);

            Children
                .Select(x => x.CreateMappingNode())
                .Each(mapping.AddChild);

            return mapping;
        }
    }
}