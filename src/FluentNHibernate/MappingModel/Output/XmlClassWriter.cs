using System.Xml;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlClassWriter : XmlClassWriterBase, IXmlWriter<ClassMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;

        public XmlClassWriter(IXmlWriterServiceLocator serviceLocator)
            : base(serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(ClassMapping mapping)
        {
            document = null;
            mapping.AcceptVisitor(this);
            return document;
        }

        public override void ProcessClass(ClassMapping classMapping)
        {
            document = new XmlDocument();

            var classElement = document.AddElement("class")
                .WithAtt("xmlns", "urn:nhibernate-mapping-2.2");

            if (classMapping.HasValue(Attr.BatchSize))
                classElement.WithAtt("batch-size", classMapping.BatchSize);

            if (classMapping.HasValue(Attr.DiscriminatorValue))
                classElement.WithAtt("discriminator-value", classMapping.DiscriminatorValue.ToString());

            if (classMapping.HasValue(Attr.DynamicInsert))
                classElement.WithAtt("dynamic-insert", classMapping.DynamicInsert);

            if (classMapping.HasValue(Attr.DynamicUpdate))
                classElement.WithAtt("dynamic-update", classMapping.DynamicUpdate);

            if (classMapping.HasValue(Attr.Lazy))
                classElement.WithAtt("lazy", classMapping.Lazy);

            if (classMapping.HasValue(Attr.Schema))
                classElement.WithAtt("schema", classMapping.Schema);

            if (classMapping.HasValue(Attr.Mutable))
                classElement.WithAtt("mutable", classMapping.Mutable);

            if (classMapping.HasValue(Attr.Polymorphism))
                classElement.WithAtt("polymorphism", classMapping.Polymorphism);

            if (classMapping.HasValue(Attr.Persister))
                classElement.WithAtt("persister", classMapping.Persister);

            if (classMapping.HasValue(Attr.Where))
                classElement.WithAtt("where", classMapping.Where);

            if (classMapping.HasValue(Attr.OptimisticLock))
                classElement.WithAtt("optimistic-lock", classMapping.OptimisticLock);

            if (classMapping.HasValue(Attr.Check))
                classElement.WithAtt("check", classMapping.Check);

            if (classMapping.HasValue(Attr.Name))
                classElement.WithAtt("name", classMapping.Name);

            if (classMapping.HasValue(Attr.Table))
                classElement.WithAtt("table", classMapping.TableName);

            if (classMapping.HasValue(Attr.Proxy))
                classElement.WithAtt("proxy", classMapping.Proxy);

            if (classMapping.HasValue(Attr.SelectBeforeUpdate))
                classElement.WithAtt("select-before-update", classMapping.SelectBeforeUpdate);

            if (classMapping.HasValue(Attr.Abstract))
                classElement.WithAtt("abstract", classMapping.Abstract);

            if (classMapping.HasValue(Attr.Subselect))
                classElement.WithAtt("subselect", classMapping.Subselect);

            if (classMapping.HasValue(Attr.SchemaAction))
                classElement.WithAtt("schema-action", classMapping.SchemaAction);

            if (classMapping.HasValue(Attr.EntityName))
                classElement.WithAtt("entity-name", classMapping.EntityName);
        }

        public override void Visit(DiscriminatorMapping discriminatorMapping)
        {
            var writer = serviceLocator.GetWriter<DiscriminatorMapping>();
            var discriminatorXml = writer.Write(discriminatorMapping);

            document.ImportAndAppendChild(discriminatorXml);
        }

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
            var joinXml = writer.Write(joinMapping);

            document.ImportAndAppendChild(joinXml);
        }

        public override void Visit(IIdentityMapping mapping)
        {
            var writer = serviceLocator.GetWriter<IIdentityMapping>();
            var idXml = writer.Write(mapping);

            document.ImportAndAppendChild(idXml);
        }

        public override void Visit(NaturalIdMapping naturalIdMapping)
        {
            var writer = serviceLocator.GetWriter<NaturalIdMapping>();
            var naturalIdXml = writer.Write(naturalIdMapping);

            document.ImportAndAppendChild(naturalIdXml);
        }

        public override void Visit(CacheMapping mapping)
        {
            var writer = serviceLocator.GetWriter<CacheMapping>();
            var cacheXml = writer.Write(mapping);

            document.ImportAndAppendChild(cacheXml);
        }

        public override void Visit(ManyToOneMapping manyToOneMapping)
        {
            var writer = serviceLocator.GetWriter<ManyToOneMapping>();
            var manyToOneXml = writer.Write(manyToOneMapping);

            document.ImportAndAppendChild(manyToOneXml);
        }

        public override void Visit(FilterMapping filterMapping)
        {
            var writer = serviceLocator.GetWriter<FilterMapping>();
            var filterXml = writer.Write(filterMapping);

            document.ImportAndAppendChild(filterXml);
        }

        public override void Visit(TuplizerMapping mapping)
        {
            var writer = serviceLocator.GetWriter<TuplizerMapping>();
            var filterXml = writer.Write(mapping);

            document.ImportAndAppendChild(filterXml);
        }
    }
}
