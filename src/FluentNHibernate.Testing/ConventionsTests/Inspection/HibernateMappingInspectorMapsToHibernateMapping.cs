using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class HibernateMappingInspectorMapsToHibernateMapping
    {
        private HibernateMapping mapping;
        private IHibernateMappingInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new HibernateMapping();
            inspector = new HibernateMappingInspector(mapping);
        }

        [Test]
        public void CatalogMapped()
        {
            mapping.Catalog = "cat";
            inspector.Catalog.ShouldEqual("cat");
        }

        [Test]
        public void CatalogIsSet()
        {
            mapping.Catalog = "cat";
            inspector.IsSet(Attr.Catalog)
                .ShouldBeTrue();
        }

        [Test]
        public void CatalogIsNotSet()
        {
            inspector.IsSet(Attr.Catalog)
                .ShouldBeFalse();
        }

        [Test]
        public void DefaultAccessMapped()
        {
            mapping.DefaultAccess = "field";
            inspector.DefaultAccess.ShouldEqual(Access.Field);
        }

        [Test]
        public void DefaultAccessIsSet()
        {
            mapping.DefaultAccess = "field";
            inspector.IsSet(Attr.DefaultAccess)
                .ShouldBeTrue();
        }

        [Test]
        public void DefaultAccessIsNotSet()
        {
            inspector.IsSet(Attr.DefaultAccess)
                .ShouldBeFalse();
        }

        [Test]
        public void DefaultCascadeMapped()
        {
            mapping.DefaultCascade = "all";
            inspector.DefaultCascade.ShouldEqual(Cascade.All);
        }

        [Test]
        public void DefaultCascadeIsSet()
        {
            mapping.DefaultCascade = "all";
            inspector.IsSet(Attr.DefaultCascade)
                .ShouldBeTrue();
        }

        [Test]
        public void DefaultCascadeIsNotSet()
        {
            inspector.IsSet(Attr.DefaultCascade)
                .ShouldBeFalse();
        }

        [Test]
        public void DefaultLazyMapped()
        {
            mapping.DefaultLazy = true;
            inspector.DefaultLazy.ShouldEqual(true);
        }

        [Test]
        public void DefaultLazyIsSet()
        {
            mapping.DefaultLazy = true;
            inspector.IsSet(Attr.DefaultLazy)
                .ShouldBeTrue();
        }

        [Test]
        public void DefaultLazyIsNotSet()
        {
            inspector.IsSet(Attr.DefaultLazy)
                .ShouldBeFalse();
        }

        [Test]
        public void SchemaMapped()
        {
            mapping.Schema = "dbo";
            inspector.Schema.ShouldEqual("dbo");
        }

        [Test]
        public void SchemaIsSet()
        {
            mapping.Schema = "dbo";
            inspector.IsSet(Attr.Schema)
                .ShouldBeTrue();
        }

        [Test]
        public void SchemaIsNotSet()
        {
            inspector.IsSet(Attr.Schema)
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IHibernateMappingInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}