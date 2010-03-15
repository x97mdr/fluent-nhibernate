using System;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class KeyPropertyPart
    {
        readonly IMappingStructure<KeyPropertyMapping> structure;

        public KeyPropertyPart(IMappingStructure<KeyPropertyMapping> structure)
        {
            this.structure = structure;
            Access = new AccessStrategyBuilder<KeyPropertyPart>(this, value => structure.SetValue(Attr.Access, value));
        }

        public KeyPropertyPart ColumnName(string columnName)
        {
            var column = new ColumnStructure(structure);
            
            new ColumnPart(column)
                .Name(columnName);
            
            structure.AddChild(column);
            return this;
        }

        public KeyPropertyPart Type(Type type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }

        public AccessStrategyBuilder<KeyPropertyPart> Access { get; private set; }
    }
}