using System.Xml;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlKeyWriter : NullMappingModelVisitor, IXmlWriter<KeyMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;
        private XmlDocument document;

        public XmlKeyWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(KeyMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessKey(KeyMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement("key");

            if (mapping.HasValue(Attr.ForeignKey))
                element.WithAtt("foreign-key", mapping.ForeignKey);

            if (mapping.HasValue(Attr.OnDelete))
                element.WithAtt("on-delete", mapping.OnDelete);

            if (mapping.HasValue(Attr.PropertyRef))
                element.WithAtt("property-ref", mapping.PropertyRef);

            if (mapping.HasValue(Attr.NotNull))
                element.WithAtt("not-null", mapping.NotNull);

            if (mapping.HasValue(Attr.Update))
                element.WithAtt("update", mapping.Update);

            if (mapping.HasValue(Attr.Unique))
                element.WithAtt("unique", mapping.Unique);

        }

        public override void Visit(ColumnMapping mapping)
        {
            var writer = serviceLocator.GetWriter<ColumnMapping>();
            var columnXml = writer.Write(mapping);

            document.ImportAndAppendChild(columnXml);
        }
    }
}