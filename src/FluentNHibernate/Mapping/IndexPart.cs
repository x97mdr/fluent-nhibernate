using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class IndexPart
    {
        readonly IMappingStructure<IndexMapping> structure;

        public IndexPart(IMappingStructure<IndexMapping> structure)
        {
            this.structure = structure;
        }

        public IndexPart Column(string indexColumnName)
        {
            var column = new ColumnStructure(structure);

            new ColumnPart(column)
                .Name(indexColumnName);

            structure.AddChild(column);

            return this;
        }

        public IndexPart Type<TIndex>()
        {
            return Type(typeof(TIndex));
        }

        public IndexPart Type(Type type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));
            return this;
        }
    }
}
