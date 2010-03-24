using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Output;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class XmlCollectionWriterTester
    {
        private IXmlWriter<CollectionMapping> writer;

        [SetUp]
        public void GetWriterFromContainer()
        {
            var container = new XmlWriterContainer();
            writer = container.Resolve<IXmlWriter<CollectionMapping>>();
        }

        [Test]
        public void ShouldWriteBagForBagMapping()
        {
            var mapping = new CollectionMapping { Type = Collection.Bag };

            writer.VerifyXml(mapping)
                .RootElement.HasName("bag");
        }

        [Test]
        public void ShouldWriteListForListMapping()
        {
            var mapping = new CollectionMapping { Type = Collection.List };

            writer.VerifyXml(mapping)
                .RootElement.HasName("list");
        }

        [Test]
        public void ShouldWriteSetForSetMapping()
        {
            var mapping = new CollectionMapping { Type = Collection.Set };

            writer.VerifyXml(mapping)
                .RootElement.HasName("set");
        }

        [Test]
        public void ShouldWriteMapForMapMapping()
        {
            var mapping = new CollectionMapping { Type = Collection.Map };

            writer.VerifyXml(mapping)
                .RootElement.HasName("map");
        }

        [Test]
        public void ShouldWriteArrayForArrayMapping()
        {
            var mapping = new CollectionMapping { Type = Collection.Array };

            writer.VerifyXml(mapping)
                .RootElement.HasName("array");
        }
    }
}