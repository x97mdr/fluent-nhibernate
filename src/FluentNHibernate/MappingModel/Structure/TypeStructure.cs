using System;
using System.Linq;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public class TypeStructure<T> : BaseMappingStructure<T>
        where T : ITypeMapping, new()
    {
        readonly Type type;

        public TypeStructure(Type type)
        {
            this.type = type;
        }

        public override T CreateMappingNode()
        {
            var mapping = new T();

            mapping.Initialise(type);

            Children
                .Select(x => x.CreateMappingNode())
                .Each(mapping.AddChild);

            return mapping;
        }
    }
}