using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// Maps to the Filter element in NH 2.0
    /// </summary>
    public class FilterPart
    {
        readonly IMappingStructure<FilterMapping> structure;

        public FilterPart(IMappingStructure<FilterMapping> structure)
        {
            this.structure = structure;
        }

        public FilterPart Name(string name)
        {
            structure.SetValue(Attr.Name, name);
            return this;
        }

        public FilterPart Condition(string condition)
        {
            structure.SetValue(Attr.Condition, condition);
            return this;
        }
    }
}
