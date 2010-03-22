using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class ManyToOneInspectorMapsToManyToOneMapping
    {
        private ManyToOneMapping mapping;
        private IManyToOneInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new ManyToOneMapping();
            inspector = new ManyToOneInspector(mapping);
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
        public void ClassMapped()
        {
            mapping.Class = new TypeReference(typeof(string));
            inspector.Class.ShouldEqual(new TypeReference(typeof(string)));
        }

        [Test]
        public void ClassIsSet()
        {
            mapping.Class = new TypeReference(typeof(string));
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
        public void ColumnsCollectionHasSameCountAsMapping()
        {
            mapping.AddColumn(new ColumnMapping());
            inspector.Columns.Count().ShouldEqual(1);
        }

        [Test]
        public void ColumnsCollectionOfInspectors()
        {
            mapping.AddColumn(new ColumnMapping());
            inspector.Columns.First().ShouldBeOfType<IColumnInspector>();
        }

        [Test]
        public void ColumnsCollectionIsEmpty()
        {
            inspector.Columns.IsEmpty().ShouldBeTrue();
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
        public void InsertMapped()
        {
            mapping.Insert = true;
            inspector.Insert.ShouldBeTrue();
        }

        [Test]
        public void InsertIsSet()
        {
            mapping.Insert = true;
            inspector.IsSet(Attr.Insert)
                .ShouldBeTrue();
        }

        [Test]
        public void InsertIsNotSet()
        {
            inspector.IsSet(Attr.Insert)
                .ShouldBeFalse();
        }

        [Test]
        public void LazyLoadMapped()
        {
            mapping.Lazy = true;
            inspector.LazyLoad.ShouldEqual(true);
        }

        [Test]
        public void LazyLoadIsSet()
        {
            mapping.Lazy = true;
            inspector.IsSet(Attr.Lazy)
                .ShouldBeTrue();
        }

        [Test]
        public void LazyLoadIsNotSet()
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

        [Test]
        public void UpdateMapped()
        {
            mapping.Update = true;
            inspector.Update.ShouldEqual(true);
        }

        [Test]
        public void UpdateIsSet()
        {
            mapping.Update = true;
            inspector.IsSet(Attr.Update)
                .ShouldBeTrue();
        }

        [Test]
        public void UpdateIsNotSet()
        {
            inspector.IsSet(Attr.Update)
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IManyToOneInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}