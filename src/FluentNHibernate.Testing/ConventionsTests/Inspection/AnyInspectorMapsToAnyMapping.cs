using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class AnyInspectorMapsToAnyMapping
    {
        private AnyMapping mapping;
        private IAnyInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new AnyMapping();
            inspector = new AnyInspector(mapping);
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
        public void IdentifierColumnsCollectionHasSameCountAsMapping()
        {
            mapping.AddIdentifierColumn(new ColumnMapping());
            inspector.IdentifierColumns.Count().ShouldEqual(1);
        }

        [Test]
        public void IdentifierColumnsCollectionOfInspectors()
        {
            mapping.AddIdentifierColumn(new ColumnMapping());
            inspector.IdentifierColumns.First().ShouldBeOfType<IColumnInspector>();
        }

        [Test]
        public void IdentifierColumnsCollectionIsEmpty()
        {
            inspector.IdentifierColumns.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void IdTypeMapped()
        {
            mapping.IdType = "type";
            inspector.IdType.ShouldEqual("type");
        }

        [Test]
        public void IdTypeIsSet()
        {
            mapping.IdType = "type";
            inspector.IsSet(Attr.IdType)
                .ShouldBeTrue();
        }

        [Test]
        public void IdTypeIsNotSet()
        {
            inspector.IsSet(Attr.IdType)
                .ShouldBeFalse();
        }

        [Test]
        public void InsertMapped()
        {
            mapping.Insert = true;
            inspector.Insert.ShouldEqual(true);
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
        public void MetaTypeMapped()
        {
            mapping.MetaType = new TypeReference(typeof(string));
            inspector.MetaType.ShouldEqual(new TypeReference(typeof(string)));
        }

        [Test]
        public void MetaTypeIsSet()
        {
            mapping.MetaType = new TypeReference(typeof(string));
            inspector.IsSet(Attr.MetaType)
                .ShouldBeTrue();
        }

        [Test]
        public void MetaTypeIsNotSet()
        {
            inspector.IsSet(Attr.MetaType)
                .ShouldBeFalse();
        }

        [Test]
        public void MetaValuesCollectionHasSameCountAsMapping()
        {
            mapping.AddMetaValue(new MetaValueMapping());
            inspector.MetaValues.Count().ShouldEqual(1);
        }

        [Test]
        public void MetaValuesCollectionOfInspectors()
        {
            mapping.AddMetaValue(new MetaValueMapping());
            inspector.MetaValues.First().ShouldBeOfType<IMetaValueInspector>();
        }

        [Test]
        public void MetaValuesCollectionIsEmpty()
        {
            inspector.MetaValues.IsEmpty().ShouldBeTrue();
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
        public void OptimisticLockMapped()
        {
            mapping.OptimisticLock = true;
            inspector.OptimisticLock.ShouldEqual(true);
        }

        [Test]
        public void OptimisticLockIsSet()
        {
            mapping.OptimisticLock = true;
            inspector.IsSet(Attr.OptimisticLock)
                .ShouldBeTrue();
        }

        [Test]
        public void OptimisticLockIsNotSet()
        {
            inspector.IsSet(Attr.OptimisticLock)
                .ShouldBeFalse();
        }

        [Test]
        public void TypeColumnsCollectionHasSameCountAsMapping()
        {
            mapping.AddTypeColumn(new ColumnMapping());
            inspector.TypeColumns.Count().ShouldEqual(1);
        }

        [Test]
        public void TypeColumnsCollectionOfInspectors()
        {
            mapping.AddTypeColumn(new ColumnMapping());
            inspector.TypeColumns.First().ShouldBeOfType<IColumnInspector>();
        }

        [Test]
        public void TypeColumnsCollectionIsEmpty()
        {
            inspector.TypeColumns.IsEmpty().ShouldBeTrue();
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
    }
}