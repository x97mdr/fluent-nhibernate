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
                //mapping.TableName = mapping.ChildType.Name + "To" + mapping.ContainingEntityType.Name;
            }
            else
            {
                // bi-directional
                if (mapping.IsSpecified("TableName") && mapping.OtherSide.IsSpecified("TableName"))
                {
                    // TODO: We could check if they're the same here and warn the user if they're not
                    return;
                }

                if (mapping.IsSpecified("TableName") && !mapping.OtherSide.IsSpecified("TableName"))
                    mapping.OtherSide.TableName = mapping.TableName;
                else if (!mapping.IsSpecified("TableName") && mapping.OtherSide.IsSpecified("TableName"))
                    mapping.TableName = mapping.OtherSide.TableName;
                else
                {
                    var tableName = mapping.Member.Name + "To" + mapping.OtherSide.Member.Name;

                    mapping.TableName = tableName;
                    mapping.OtherSide.TableName = tableName;
                }
            }
        }
    }
}