using System.Linq;
using System.Xml;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Output;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.Testing;
using NUnit.Framework;
using Rhino.Mocks;

namespace FluentNHibernate.Testing.MappingModel.Output
{
    [TestFixture]
    public class XmlCompositeElementWriterTester
    {
        private IXmlWriter<CompositeElementMapping> writer;

        [SetUp]
        public void GetWriterFromContainer()
        {
            var container = new XmlWriterContainer();
            writer = container.Resolve<IXmlWriter<CompositeElementMapping>>();
        }

        [Test]
        public void ShouldWriteClassAttribute()
        {
            var testHelper = new XmlWriterTestHelper<CompositeElementMapping>();

            testHelper.Check(x => x.Class, new TypeReference("t")).MapsToAttribute("class");
            testHelper.VerifyAll(writer);
        }

        [Test]
        public void ShouldWriteProperties()
        {
            var mapping = new CompositeElementMapping(typeof(ExampleClass));
            mapping.AddProperty(new PropertyMapping());

            writer.VerifyXml(mapping)
                .Element("property").Exists();
        }

        [Test]
        public void ShouldWriteManyToOnes()
        {
            var mapping = new CompositeElementMapping(typeof(ExampleClass));
            mapping.AddReference(new ManyToOneMapping());

            writer.VerifyXml(mapping)
                .Element("many-to-one").Exists();
        }

        [Test, Ignore]
        public void ShouldWriteNestedCompositeElement()
        {
            Assert.Fail();
        }

        [Test]
        public void ShouldWriteParent()
        {
            var mapping = new CompositeElementMapping(typeof(ExampleClass));
            mapping.Parent = new ParentMapping(null);

            writer.VerifyXml(mapping)
                .Element("parent").Exists();
        }

        [Test]
        public void ShouldWriteParentAsFirstElement()
        {
            var mapping = new CompositeElementMapping(typeof(ExampleClass));
            mapping.Parent = new ParentMapping(null);
            mapping.AddProperty(new PropertyMapping());

            writer.VerifyXml(mapping)
                .Element("parent").IsFirst()
                .Element("property").Exists();
        }
    }
}
