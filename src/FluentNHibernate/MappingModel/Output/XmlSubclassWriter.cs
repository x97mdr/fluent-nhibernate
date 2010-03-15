using System.Collections.Generic;
using System.Xml;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlSubclassWriter : XmlClassWriterBase, IXmlWriter<SubclassMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;

        public XmlSubclassWriter(IXmlWriterServiceLocator serviceLocator)
            : base(serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(SubclassMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessSubclass(SubclassMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement(mapping.SubclassType.GetElementName());

            if (mapping.HasValue(Attr.Name))
                element.WithAtt("name", mapping.Name);

            if (mapping.HasValue(Attr.Extends))
                element.WithAtt("extends", mapping.Extends);

            if (mapping.HasValue(Attr.Proxy))
                element.WithAtt("proxy", mapping.Proxy);

            if (mapping.HasValue(Attr.Lazy))
                element.WithAtt("lazy", mapping.Lazy);

            if (mapping.HasValue(Attr.DynamicUpdate))
                element.WithAtt("dynamic-update", mapping.DynamicUpdate);

            if (mapping.HasValue(Attr.DynamicInsert))
                element.WithAtt("dynamic-insert", mapping.DynamicInsert);

            if (mapping.HasValue(Attr.SelectBeforeUpdate))
                element.WithAtt("select-before-update", mapping.SelectBeforeUpdate);

            if (mapping.HasValue(Attr.Abstract))
                element.WithAtt("abstract", mapping.Abstract);

            if (mapping.HasValue(Attr.EntityName))
                element.WithAtt("entity-name", mapping.EntityName);

            if (mapping.SubclassType == SubclassType.Subclass)
            {
                if (mapping.HasValue(Attr.DiscriminatorValue))
                    element.WithAtt("discriminator-value", mapping.DiscriminatorValue.ToString());
            }
            else
            {
                if (mapping.HasValue(Attr.Table))
                    element.WithAtt("table", mapping.TableName);

                if (mapping.HasValue(Attr.Schema))
                    element.WithAtt("schema", mapping.Schema);

                if (mapping.HasValue(Attr.Check))
                    element.WithAtt("check", mapping.Check);

                if (mapping.HasValue(Attr.Subselect))
                    element.WithAtt("subselect", mapping.Subselect);

                if (mapping.HasValue(Attr.Persister))
                    element.WithAtt("persister", mapping.Persister);

                if (mapping.HasValue(Attr.BatchSize))
                    element.WithAtt("batch-size", mapping.BatchSize);
            }
        }

        public override void Visit(KeyMapping keyMapping)        {            var writer = serviceLocator.GetWriter<KeyMapping>();            var keyXml = writer.Write(keyMapping);            document.ImportAndAppendChild(keyXml);        }
        public override void Visit(SubclassMapping subclassMapping)
        {
            var writer = serviceLocator.GetWriter<SubclassMapping>();
            var subclassXml = writer.Write(subclassMapping);

            document.ImportAndAppendChild(subclassXml);
        }

        public override void Visit(IComponentMapping componentMapping)
        {
            var writer = serviceLocator.GetWriter<IComponentMapping>();
            var componentXml = writer.Write(componentMapping);

            document.ImportAndAppendChild(componentXml);
        }

        public override void Visit(JoinMapping joinMapping)
        {
            var writer = serviceLocator.GetWriter<JoinMapping>();
            var xml = writer.Write(joinMapping);

            document.ImportAndAppendChild(xml);
        }
    }
}