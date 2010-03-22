using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class ArrayInspectorMapsToArrayMapping
    {
        private CollectionMapping mapping;
        private IArrayInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new CollectionMapping();
            inspector = new ArrayInspector(mapping);
        }

        [Test]
        public void MapsIndexToInspector()
        {
            mapping.Index = new IndexMapping();
            inspector.Index.ShouldBeOfType<IIndexInspector>();
        }

        [Test]
        public void IndexIsSet()
        {
            mapping.Index = new IndexMapping();
            inspector.IsSet(Attr.Index)
                .ShouldBeTrue();
        }

        [Test]
        public void IndexIsNotSet()
        {
            inspector.IsSet(Attr.Index)
                .ShouldBeFalse();
        }

        [Test]
        public void MapsIndexManyToManyToInspector()
        {
            mapping.Index = new IndexManyToManyMapping();
            inspector.Index.ShouldBeOfType<IIndexManyToManyInspector>();
        }

        [Test]
        public void IndexManyToManyIsSet()
        {
            mapping.Index = new IndexManyToManyMapping();
            inspector.IsSet(Attr.Index)
                .ShouldBeTrue();
        }
    }
}