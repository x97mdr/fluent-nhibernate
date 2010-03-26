using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using System;

namespace FluentNHibernate.Automapping
{
    public class AutoKeyMapper
    {
        readonly AutoMappingExpressions expressions;

        public AutoKeyMapper(AutoMappingExpressions expressions)
        {
            this.expressions = expressions;
        }

        public void SetKey(Member property, ClassMappingBase classMap, ICollectionMapping mapping)
        {
            var columnName = String.Format("{0}_{1}_id", property.DeclaringType.Name, property.Name);

            if (classMap is ComponentMapping)
                columnName = expressions.GetComponentColumnPrefix(((ComponentMapping)classMap).Member) + columnName;

            var key = new KeyMapping();

            key.ContainingEntityType = classMap.Type;
            key.AddDefaultColumn(new ColumnMapping { Name = columnName });

            mapping.SetDefaultValue(x => x.Key, key);
        }
    }
}