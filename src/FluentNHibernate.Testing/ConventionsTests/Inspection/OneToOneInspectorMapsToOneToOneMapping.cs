using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class OneToOneInspectorMapsToOneToOneMapping
    {
        private OneToOneMapping mapping;
        private IOneToOneInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new OneToOneMapping();
            inspector = new OneToOneInspector(mapping);
        }

        [Test]
        public void AccessMapped()
        {
            mapping.Access = "field";
            inspector.Access.ShouldEqual(Access.Field);
        }

        [Test]
        public void AccessIsSet()
        {
            mapping.Access = "field";
            inspector.IsSet(Attr.Access)
                .ShouldBeTrue();
        }

        [Test]
        public void AccessIsNotSet()
        {
            inspector.IsSet(Attr.Access)
                .ShouldBeFalse();
        }

        [Test]
        public void CascadeMapped()
        {
            mapping.Cascade = "all";
            inspector.Cascade.ShouldEqual(Cascade.All);
        }

        [Test]
        public void CascadeIsSet()
        {
            mapping.Cascade = "all";
            inspector.IsSet(Attr.Cascade)
                .ShouldBeTrue();
        }

        [Test]
        public void CascadeIsNotSet()
        {
            inspector.IsSet(Attr.Cascade)
                .ShouldBeFalse();
        }

        [Test]
        public void ConstrainedMapped()
        {
            mapping.Constrained = true;
            inspector.Constrained.ShouldBeTrue();
        }

        [Test]
        public void ConstrainedIsSet()
        {
            mapping.Constrained = true;
            inspector.IsSet(Attr.Constrained)
                .ShouldBeTrue();
        }

        [Test]
        public void ConstrainedIsNotSet()
        {
            inspector.IsSet(Attr.Constrained)
                .ShouldBeFalse();
        }

        [Test]
        public void FetchMapped()
        {
            mapping.Fetch = "join";
            inspector.Fetch.ShouldEqual(Fetch.Join);
        }

        [Test]
        public void FetchIsSet()
        {
            mapping.Fetch = "join";
            inspector.IsSet(Attr.Fetch)
                .ShouldBeTrue();
        }

        [Test]
        public void FetchIsNotSet()
        {
            inspector.IsSet(Attr.Fetch)
                .ShouldBeFalse();
        }

        [Test]
        public void ForeignKeyMapped()
        {
            mapping.ForeignKey = "key";
            inspector.ForeignKey.ShouldEqual("key");
        }

        [Test]
        public void ForeignKeyIsSet()
        {
            mapping.ForeignKey = "key";
            inspector.IsSet(Attr.ForeignKey)
                .ShouldBeTrue();
        }

        [Test]
        public void ForeignKeyIsNotSet()
        {
            inspector.IsSet(Attr.ForeignKey)
                .ShouldBeFalse();
        }

        [Test]
        public void LazyMapped()
        {
            mapping.Lazy = true;
            inspector.LazyLoad.ShouldEqual(true);
        }

        [Test]
        public void LazyIsSet()
        {
            mapping.Lazy = true;
            inspector.IsSet(Attr.Lazy)
                .ShouldBeTrue();
        }

        [Test]
        public void LazyIsNotSet()
        {
            inspector.IsSet(Attr.Lazy)
                .ShouldBeFalse();
        }

        [Test]
        public void NameMapped()
        {
            mapping.Name = "name";
            inspector.Name.ShouldEqual("name");
        }

        [Test]
        public void NameIsSet()
        {
            mapping.Name = "name";
            inspector.IsSet(Attr.Name)
                .ShouldBeTrue();
        }

        [Test]
        public void NameIsNotSet()
        {
            inspector.IsSet(Attr.Name)
                .ShouldBeFalse();
        }

        [Test]
        public void PropertyRefMapped()
        {
            mapping.PropertyRef = "ref";
            inspector.PropertyRef.ShouldEqual("ref");
        }

        [Test]
        public void PropertyRefIsSet()
        {
            mapping.PropertyRef = "ref";
            inspector.IsSet(Attr.PropertyRef)
                .ShouldBeTrue();
        }

        [Test]
        public void PropertyRefIsNotSet()
        {
            inspector.IsSet(Attr.PropertyRef)
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IOneToOneInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}