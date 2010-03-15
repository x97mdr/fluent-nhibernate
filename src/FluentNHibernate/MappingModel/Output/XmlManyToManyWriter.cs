using System;
using System.Xml;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlManyToManyWriter : NullMappingModelVisitor, IXmlWriter<ManyToManyMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;
        private XmlDocument document;

        public XmlManyToManyWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(ManyToManyMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessManyToMany(ManyToManyMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement("many-to-many");

            if (mapping.HasValue(Attr.Class))
                element.WithAtt("class", mapping.Class);

            if (mapping.HasValue(Attr.Fetch))
                element.WithAtt("fetch", mapping.Fetch);

            if (mapping.HasValue(Attr.ForeignKey))
                element.WithAtt("foreign-key", mapping.ForeignKey);

            if (mapping.HasValue(Attr.Lazy))
                element.WithAtt("lazy", mapping.Lazy);

            if (mapping.HasValue(Attr.NotFound))
                element.WithAtt("not-found", mapping.NotFound);

            if (mapping.HasValue(Attr.Where))
                element.WithAtt("where", mapping.Where);

            if (mapping.HasValue(Attr.EntityName))
                element.WithAtt("entity-name", mapping.EntityName);
        }

        public override void Visit(ColumnMapping mapping)
        {
            var writer = serviceLocator.GetWriter<ColumnMapping>();
            var columnXml = writer.Write(mapping);

            document.ImportAndAppendChild(columnXml);
        }
    }
}