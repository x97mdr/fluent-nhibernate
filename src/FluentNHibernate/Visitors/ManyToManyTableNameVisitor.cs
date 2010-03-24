using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Visitors
{
    public class ManyToManyTableNameVisitor : DefaultMappingModelVisitor
    {
        public override void ProcessCollection(CollectionMapping mapping)
        {
            if (!(mapping.Relationship is ManyToManyMapping))
                return;

            if (mapping.OtherSide == null)
            {
                // uni-directional
                mapping.SetDefaultTableName(mapping.ChildType.Name + "To" + mapping.ContainingEntityType.Name);
            }
            else
            {
                // bi-directional
                if (mapping.HasUserDefinedValue(Attr.Table) && mapping.OtherSide.HasUserDefinedValue(Attr.Table))
                {
                    // TODO: We could check if they're the same here and warn the user if they're not
                    return;
                }

                if (mapping.HasUserDefinedValue(Attr.Table) && !mapping.OtherSide.HasUserDefinedValue(Attr.Table))
                    mapping.OtherSide.SetDefaultTableName(mapping.TableName);
                else if (!mapping.HasUserDefinedValue(Attr.Table) && mapping.OtherSide.HasUserDefinedValue(Attr.Table))
                    mapping.SetDefaultTableName(mapping.OtherSide.TableName);
                else
                {
                    var tableName = mapping.Member.Name + "To" + mapping.OtherSide.Member.Name;

                    mapping.SetDefaultTableName(tableName);
                    mapping.OtherSide.SetDefaultTableName(tableName);
                }
            }
        }
    }
}