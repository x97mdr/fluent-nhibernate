using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class ElementPart
    {
        readonly IMappingStructure<ElementMapping> structure;
        readonly ColumnMappingCollection<ElementPart> columns;

        public ElementPart(IMappingStructure<ElementMapping> structure)
        {
            this.structure = structure;
            columns = new ColumnMappingCollection<ElementPart>(this, structure);            
        }

        public ElementPart Column(string elementColumnName)
        {
            columns.Add(elementColumnName);
            return this;
        }

        public ColumnMappingCollection<ElementPart> Columns
        {
            get { return columns; }
        }

        public ElementPart Type<TElement>()
        {
            structure.SetValue(Attr.Type, new TypeReference(typeof(TElement)));
            return this;
        }

        public ElementPart Length(int length)
        {
            structure.SetValue(Attr.Length, length);
            return this;
        }

        public ElementPart Formula(string formula)
        {
            structure.SetValue(Attr.Formula, formula);
            return this;
        }
    }
}