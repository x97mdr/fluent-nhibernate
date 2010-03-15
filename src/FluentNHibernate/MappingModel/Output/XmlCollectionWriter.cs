using System;
using System.Xml;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlCollectionWriter : NullMappingModelVisitor, IXmlWriter<CollectionMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;
        private XmlDocument document;
        bool hasComponent;

        public XmlCollectionWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(CollectionMapping mappingModel)
        {
            hasComponent = mappingModel.CompositeElement != null;

            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void Visit(KeyMapping mapping)
        {
            var writer = serviceLocator.GetWriter<KeyMapping>();
            var keyXml = writer.Write(mapping);

            document.ImportAndAppendChild(keyXml);
        }

        public override void Visit(IIndexMapping mapping)
        {
            var writer = serviceLocator.GetWriter<IIndexMapping>();
            var keyXml = writer.Write(mapping);

            document.ImportAndAppendChild(keyXml);
        }

        public override void Visit(CacheMapping mapping)
        {
            var writer = serviceLocator.GetWriter<CacheMapping>();
            var cacheXml = writer.Write(mapping);

            document.ImportAndAppendChild(cacheXml);
        }

        public override void Visit(ICollectionRelationshipMapping mapping)
        {
            // TODO: Make this more elegant
            if (hasComponent) return;

            var writer = serviceLocator.GetWriter<ICollectionRelationshipMapping>();
            var relationshipXml = writer.Write(mapping);

            document.ImportAndAppendChild(relationshipXml);
        }

        public override void Visit(CompositeElementMapping mapping)
        {
            var writer = serviceLocator.GetWriter<CompositeElementMapping>();
            var xml = writer.Write(mapping);

            document.ImportAndAppendChild(xml);
        }

        public override void Visit(ElementMapping mapping)
        {
            var writer = serviceLocator.GetWriter<ElementMapping>();
            var xml = writer.Write(mapping);

            document.ImportAndAppendChild(xml);
        }

        public override void ProcessCollection(CollectionMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement(mapping.Type.GetElementName());

            if (mapping.HasValue(Attr.Access))
                element.WithAtt("access", mapping.Access);

            if (mapping.HasValue(Attr.BatchSize))
                element.WithAtt("batch-size", mapping.BatchSize);

            if (mapping.HasValue(Attr.Cascade))
                element.WithAtt("cascade", mapping.Cascade);

            if (mapping.HasValue(Attr.Check))
                element.WithAtt("check", mapping.Check);

            if (mapping.HasValue(Attr.CollectionType) && mapping.CollectionType != TypeReference.Empty)
                element.WithAtt("collection-type", mapping.CollectionType);

            if (mapping.HasValue(Attr.Fetch))
                element.WithAtt("fetch", mapping.Fetch);

            if (mapping.HasValue(Attr.Generic))
                element.WithAtt("generic", mapping.Generic);

            if (mapping.HasValue(Attr.Inverse))
                element.WithAtt("inverse", mapping.Inverse);

            if (mapping.HasValue(Attr.Lazy))
                element.WithAtt("lazy", mapping.Lazy);

            if (mapping.HasValue(Attr.Name))
                element.WithAtt("name", mapping.Name);

            if (mapping.HasValue(Attr.OptimisticLock))
                element.WithAtt("optimistic-lock", mapping.OptimisticLock);

            if (mapping.HasValue(Attr.Persister))
                element.WithAtt("persister", mapping.Persister);

            if (mapping.HasValue(Attr.Schema))
                element.WithAtt("schema", mapping.Schema);

            if (mapping.HasValue(Attr.Table))
                element.WithAtt("table", mapping.TableName);

            if (mapping.HasValue(Attr.Where))
                element.WithAtt("where", mapping.Where);

            if (mapping.HasValue(Attr.Subselect))
                element.WithAtt("subselect", mapping.Subselect);

            if (mapping.HasValue(Attr.Mutable))
                element.WithAtt("mutable", mapping.Mutable);

            if (mapping.HasValue(Attr.Sort))
                element.WithAtt("sort", mapping.Sort);

            if (mapping.HasValue(Attr.OrderBy))
                element.WithAtt("order-by", mapping.OrderBy);
        }

        public override void Visit(FilterMapping filterMapping)
        {
            var writer = serviceLocator.GetWriter<FilterMapping>();
            var xml = writer.Write(filterMapping);
            document.ImportAndAppendChild(xml);
        }
    }
}