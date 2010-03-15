using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Automapping
{
    public class AutoKeyMapper
    {
        readonly AutoMappingExpressions expressions;

        public AutoKeyMapper(AutoMappingExpressions expressions)
        {
            this.expressions = expressions;
        }

        public void SetKey(Member property, ClassMappingBase classMap, CollectionMapping mapping)
        {
            var columnName = property.DeclaringType.Name + "_id";

            if (classMap is ComponentMapping)
                columnName = expressions.GetComponentColumnPrefix(((ComponentMapping)classMap).Member) + columnName;

            var key = new KeyMapping();

            key.ContainingEntityType = classMap.Type;
            key.AddDefaultColumn(new ColumnMapping() { Name = columnName });

            mapping.Key = key;
        }
    }
}