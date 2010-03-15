using System.Xml;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlOneToOneWriter : NullMappingModelVisitor, IXmlWriter<OneToOneMapping>
    {
        private XmlDocument document;

        public XmlDocument Write(OneToOneMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessOneToOne(OneToOneMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement("one-to-one");

            if (mapping.HasValue(Attr.Access))
                element.WithAtt("access", mapping.Access);

            if (mapping.HasValue(Attr.Cascade))
                element.WithAtt("cascade", mapping.Cascade);

            if (mapping.HasValue(Attr.Class))
                element.WithAtt("class", mapping.Class);

            if (mapping.HasValue(Attr.Constrained))
                element.WithAtt("constrained", mapping.Constrained);

            if (mapping.HasValue(Attr.Fetch))
                element.WithAtt("fetch", mapping.Fetch);

            if (mapping.HasValue(Attr.ForeignKey))
                element.WithAtt("foreign-key", mapping.ForeignKey);

            if (mapping.HasValue(Attr.Lazy))
                element.WithAtt("lazy", mapping.Lazy ? "proxy" : "false");

            if (mapping.HasValue(Attr.Name))
                element.WithAtt("name", mapping.Name);

            if (mapping.HasValue(Attr.PropertyRef))
                element.WithAtt("property-ref", mapping.PropertyRef);

            if (mapping.HasValue(Attr.EntityName))
                element.WithAtt("entity-name", mapping.EntityName);
        }
    }
}