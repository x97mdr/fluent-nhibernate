using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class MapInspectorMapsToMapMapping
    {
        private CollectionMapping mapping;
        private IMapInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new CollectionMapping();
            inspector = new MapInspector(mapping);
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

        [Test]
        public void OrderByIsSet()
        {
            mapping.OrderBy = "AField";
            inspector.IsSet(Attr.OrderBy)
                .ShouldBeTrue();
        }

        [Test]
        public void OrderByIsNotSet()
        {
            inspector.IsSet(Attr.OrderBy)
                .ShouldBeFalse();
        }

        [Test]
        public void SortByIsSet()
        {
            mapping.Sort = "AField";
            inspector.IsSet(Attr.Sort)
                .ShouldBeTrue();
        }

        [Test]
        public void SortByIsNotSet()
        {
            inspector.IsSet(Attr.Sort)
                .ShouldBeFalse();
        }
    }
}