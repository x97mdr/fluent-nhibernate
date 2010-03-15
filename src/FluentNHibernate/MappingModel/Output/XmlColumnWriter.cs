using System.Xml;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlColumnWriter : NullMappingModelVisitor, IXmlWriter<ColumnMapping>
    {
        private XmlDocument document;

        public XmlDocument Write(ColumnMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessColumn(ColumnMapping columnMapping)
        {
            document = new XmlDocument();

            var element = document.CreateElement("column");

            if (columnMapping.HasValue(Attr.Name))
                element.WithAtt("name", columnMapping.Name);

            if (columnMapping.HasValue(Attr.Check))
                element.WithAtt("check", columnMapping.Check);

            if (columnMapping.HasValue(Attr.Length))
                element.WithAtt("length", columnMapping.Length);

            if (columnMapping.HasValue(Attr.Index))
                element.WithAtt("index", columnMapping.Index);

            if (columnMapping.HasValue(Attr.NotNull))
                element.WithAtt("not-null", columnMapping.NotNull);

            if (columnMapping.HasValue(Attr.SqlType))
                element.WithAtt("sql-type", columnMapping.SqlType);

            if (columnMapping.HasValue(Attr.Unique))
                element.WithAtt("unique", columnMapping.Unique);

            if (columnMapping.HasValue(Attr.UniqueKey))
                element.WithAtt("unique-key", columnMapping.UniqueKey);

            if (columnMapping.HasValue(Attr.Precision))
                element.WithAtt("precision", columnMapping.Precision);

            if (columnMapping.HasValue(Attr.Scale))
                element.WithAtt("scale", columnMapping.Scale);

            if (columnMapping.HasValue(Attr.Default))
                element.WithAtt("default", columnMapping.Default);

            document.AppendChild(element);
        }
    }
}
