using System.Linq;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Structure
{
    public class MemberStructure<T> : BaseMappingStructure<T>
        where T : IMemberMapping, new()
    {
        readonly Member member;

        public MemberStructure(Member member)
        {
            this.member = member;
        }

        public override T CreateMappingNode()
        {
            var mapping = new T();

            mapping.Initialise(member);

            Children
                .Select(x => x.CreateMappingNode())
                .Each(mapping.AddChild);

            return mapping;
        }
    }
}