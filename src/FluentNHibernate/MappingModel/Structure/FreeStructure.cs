using System.Linq;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    /// <summary>
    /// Structure that has no real category (key, discriminator...)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FreeStructure<T> : BaseMappingStructure<T>
        where T : IMapping, new()
    {
        public override T CreateMappingNode()
        {
            var mapping = new T();

            Children
                .Select(x => x.CreateMappingNode())
                .Each(mapping.AddChild);

            return mapping;
        }
    }
}