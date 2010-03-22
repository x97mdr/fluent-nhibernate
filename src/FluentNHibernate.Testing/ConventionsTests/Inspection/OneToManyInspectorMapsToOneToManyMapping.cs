using System.Reflection;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class OneToManyInspectorMapsToOneToManyMapping
    {
        private OneToManyMapping mapping;
        private IOneToManyInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new OneToManyMapping();
            inspector = new OneToManyInspector(mapping);
        }

        [Test]
        public void ChildTypeMapped()
        {
            mapping.ChildType = typeof(ExampleClass);
            inspector.ChildType.ShouldEqual(typeof(ExampleClass));
        }

        [Test]
        public void ChildTypeIsSet()
        {
            mapping.ChildType = typeof(ExampleClass);
            inspector.IsSet(Attr.ChildType)
                .ShouldBeTrue();
        }

        [Test]
        public void ChildTypeIsNotSet()
        {
            inspector.IsSet(Attr.ChildType)
                .ShouldBeFalse();
        }

        [Test]
        public void ClassMapped()
        {
            mapping.Class = new TypeReference(typeof(ExampleClass));
            inspector.Class.ShouldEqual(new TypeReference(typeof(ExampleClass)));
        }

        [Test]
        public void ClassIsSet()
        {
            mapping.Class = new TypeReference(typeof(ExampleClass));
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
        public void NotFoundMapped()
        {
            mapping.NotFound = "exception";
            inspector.NotFound.ShouldEqual(NotFound.Exception);
        }

        [Test]
        public void NotFoundIsSet()
        {
            mapping.NotFound = "exception";
            inspector.IsSet(Attr.NotFound)
                .ShouldBeTrue();
        }

        [Test]
        public void NotFoundIsNotSet()
        {
            inspector.IsSet(Attr.NotFound)
                .ShouldBeFalse();
        }
    }
}