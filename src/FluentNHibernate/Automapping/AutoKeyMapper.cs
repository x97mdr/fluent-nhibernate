using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Automapping
{
    public class AutoKeyMapper
    {
        readonly IAutomappingDiscoveryRules rules;

        public AutoKeyMapper(IAutomappingDiscoveryRules rules)
        {
            this.rules = rules;
        }

        public void SetKey(MappingMetaData metaData, ICollectionMapping mapping)
        {
            var columnName = metaData.Member.DeclaringType.Name + "_id";

            //if (container is ComponentMapping)
            //    columnName = rules.ComponentColumnPrefixRule(((ComponentMapping)container).Member) + columnName;

            var key = new KeyMapping();

            key.ContainingEntityType = metaData.EntityType;
            key.AddDefaultColumn(new ColumnMapping { Name = columnName });

            mapping.SetDefaultValue(x => x.Key, key);
        }
    }
}