using System.Xml;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlCacheWriter : NullMappingModelVisitor, IXmlWriter<CacheMapping>
    {
        private XmlDocument document;

        public XmlDocument Write(CacheMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessCache(CacheMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement("cache");

            if (mapping.HasValue(Attr.Region))
                element.WithAtt("region", mapping.Region);

            if (mapping.HasValue(Attr.Usage))
                element.WithAtt("usage", mapping.Usage);
        }
    }
}