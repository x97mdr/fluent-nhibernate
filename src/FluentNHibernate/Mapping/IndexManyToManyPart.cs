using System;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class IndexManyToManyPart
    {
        readonly IMappingStructure<IndexManyToManyMapping> structure;

        public IndexManyToManyPart(IMappingStructure<IndexManyToManyMapping> structure)
        {
            this.structure = structure;
        }

        public IndexManyToManyPart Column(string indexColumnName)
        {
            var column = new ColumnStructure(structure);

            new ColumnPart(column)
                .Name(indexColumnName);
            
            structure.AddChild(column);

            return this;
        }

        public IndexManyToManyPart Type<TIndex>()
        {
            structure.SetValue(Attr.Class, new TypeReference(typeof(TIndex)));
            return this;
        }

        public IndexManyToManyPart Type(Type indexType)
        {
            structure.SetValue(Attr.Class, new TypeReference(indexType));
            return this;
        }
    }
}