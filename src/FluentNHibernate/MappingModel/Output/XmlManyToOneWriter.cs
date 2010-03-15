using System.Xml;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlManyToOneWriter : NullMappingModelVisitor, IXmlWriter<ManyToOneMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;
        private XmlDocument document;

        public XmlManyToOneWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(ManyToOneMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessManyToOne(ManyToOneMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement("many-to-one");

            if (mapping.HasValue(Attr.Access))
                element.WithAtt("access", mapping.Access);

            if (mapping.HasValue(Attr.Cascade))
                element.WithAtt("cascade", mapping.Cascade);

            if (mapping.HasValue(Attr.Class))
                element.WithAtt("class", mapping.Class);

            if (mapping.HasValue(Attr.Fetch))
                element.WithAtt("fetch", mapping.Fetch);

            if (mapping.HasValue(Attr.ForeignKey))
                element.WithAtt("foreign-key", mapping.ForeignKey);

            if (mapping.HasValue(Attr.Insert))
                element.WithAtt("insert", mapping.Insert);

            if (mapping.HasValue(Attr.Lazy))
                element.WithAtt("lazy", mapping.Lazy ? "proxy" : "false");

            if (mapping.HasValue(Attr.Name))
                element.WithAtt("name", mapping.Name);

            if (mapping.HasValue(Attr.NotFound))
                element.WithAtt("not-found", mapping.NotFound);

            if (mapping.HasValue(Attr.PropertyRef))
                element.WithAtt("property-ref", mapping.PropertyRef);

            if (mapping.HasValue(Attr.Update))
                element.WithAtt("update", mapping.Update);

            if (mapping.HasValue(Attr.EntityName))
                element.WithAtt("entity-name", mapping.EntityName);
        }

        public override void Visit(ColumnMapping columnMapping)
        {
            var writer = serviceLocator.GetWriter<ColumnMapping>();
            var xml = writer.Write(columnMapping);

            document.ImportAndAppendChild(xml);
        }
    }
}