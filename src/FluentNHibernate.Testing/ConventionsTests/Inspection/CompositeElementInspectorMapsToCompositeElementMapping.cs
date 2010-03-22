using System.Linq;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class CompositeElementInspectorMapsToCompositeElementMapping
    {
        private CompositeElementMapping mapping;
        private ICompositeElementInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new CompositeElementMapping();
            mapping.Initialise(typeof(ExampleClass));
            inspector = new CompositeElementInspector(mapping);
        }

        [Test]
        public void ClassMapped()
        {
            inspector.Class.ShouldEqual(new TypeReference(typeof(ExampleClass)));
        }

        [Test]
        public void ClassIsSet()
        {
            mapping.Class = new TypeReference(typeof(ExampleInheritedClass));
            inspector.IsSet(Attr.Class)
                .ShouldBeTrue();
        }

        [Test]
        public void ClassIsNotSet()
        {
            inspector.IsSet(Attr.Class)
                .ShouldBeFalse();
        }

        [Test]
        public void ParentMapped()
        {
            mapping.Parent = new ParentMapping();
            mapping.Parent.Name = "test";
            inspector.Parent.Name.ShouldEqual("test");
        }

        [Test]
        public void ParentIsSet()
        {
            mapping.Parent = new ParentMapping();
            mapping.Parent.Name = "test";
            inspector.IsSet(Attr.Parent)
                .ShouldBeTrue();
        }

        [Test]
        public void ParentIsNotSet()
        {
            inspector.IsSet(Attr.Parent)
                .ShouldBeFalse();
        }

        [Test]
        public void PropertiesCollectionHasSameCountAsMapping()
        {
            mapping.AddProperty(new PropertyMapping());
            inspector.Properties.Count().ShouldEqual(1);
        }

        [Test]
        public void PropertiesCollectionOfInspectors()
        {
            mapping.AddProperty(new PropertyMapping());
            inspector.Properties.First().ShouldBeOfType<IPropertyInspector>();
        }

        [Test]
        public void PropertiesCollectionIsEmpty()
        {
            inspector.Properties.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void ReferencesCollectionHasSameCountAsMapping()
        {
            mapping.AddReference(new ManyToOneMapping());
            inspector.References.Count().ShouldEqual(1);
        }

        [Test]
        public void ReferencesCollectionOfInspectors()
        {
            mapping.AddReference(new ManyToOneMapping());
            inspector.References.First().ShouldBeOfType<IManyToOneInspector>();
        }

        [Test]
        public void ReferencesCollectionIsEmpty()
        {
            inspector.References.IsEmpty().ShouldBeTrue();
        }
    }
}