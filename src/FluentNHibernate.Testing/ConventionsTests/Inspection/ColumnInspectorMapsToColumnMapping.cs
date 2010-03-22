using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class ColumnInspectorMapsToColumnMapping
    {
        private ColumnMapping mapping;
        private IColumnInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new ColumnMapping();
            inspector = new ColumnInspector(typeof(Record), mapping);
        }

        [Test]
        public void CheckMapped()
        {
            mapping.Check = "chk";
            inspector.Check.ShouldEqual("chk");
        }

        [Test]
        public void CheckIsSet()
        {
            mapping.Check = "chk";
            inspector.IsSet(Attr.Check)
                .ShouldBeTrue();
        }

        [Test]
        public void CheckIsNotSet()
        {
            inspector.IsSet(Attr.Check)
                .ShouldBeFalse();
        }

        [Test]
        public void DefaultMapped()
        {
            mapping.Default = "value";
            inspector.Default.ShouldEqual("value");
        }

        [Test]
        public void DefaultIsSet()
        {
            mapping.Default = "value";
            inspector.IsSet(Attr.Default)
                .ShouldBeTrue();
        }

        [Test]
        public void DefaultIsNotSet()
        {
            inspector.IsSet(Attr.Default)
                .ShouldBeFalse();
        }

        [Test]
        public void IndexMapped()
        {
            mapping.Index = "ix";
            inspector.Index.ShouldEqual("ix");
        }

        [Test]
        public void IndexIsSet()
        {
            mapping.Index = "ix";
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
        public void LengthMapped()
        {
            mapping.Length = 100;
            inspector.Length.ShouldEqual(100);
        }

        [Test]
        public void LengthIsSet()
        {
            mapping.Length = 100;
            inspector.IsSet(Attr.Length)
                .ShouldBeTrue();
        }

        [Test]
        public void LengthIsNotSet()
        {
            inspector.IsSet(Attr.Length)
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
        public void NotNullMapped()
        {
            mapping.NotNull = true;
            inspector.NotNull.ShouldBeTrue();
        }

        [Test]
        public void NotNullIsSet()
        {
            mapping.NotNull = true;
            inspector.IsSet(Attr.NotNull)
                .ShouldBeTrue();
        }

        [Test]
        public void NotNullIsNotSet()
        {
            inspector.IsSet(Attr.NotNull)
                .ShouldBeFalse();
        }

        [Test]
        public void PrecisionMapped()
        {
            mapping.Precision = 10;
            inspector.Precision.ShouldEqual(10);
        }

        [Test]
        public void PrecisionIsSet()
        {
            mapping.Precision = 10;
            inspector.IsSet(Attr.Precision)
                .ShouldBeTrue();
        }

        [Test]
        public void PrecisionIsNotSet()
        {
            inspector.IsSet(Attr.Precision)
                .ShouldBeFalse();
        }

        [Test]
        public void ScaleMapped()
        {
            mapping.Scale = 10;
            inspector.Scale.ShouldEqual(10);
        }

        [Test]
        public void ScaleIsSet()
        {
            mapping.Scale = 10;
            inspector.IsSet(Attr.Scale)
                .ShouldBeTrue();
        }

        [Test]
        public void ScaleIsNotSet()
        {
            inspector.IsSet(Attr.Scale)
                .ShouldBeFalse();
        }

        [Test]
        public void SqlTypeMapped()
        {
            mapping.SqlType = "type";
            inspector.SqlType.ShouldEqual("type");
        }

        [Test]
        public void SqlTypeIsSet()
        {
            mapping.SqlType = "type";
            inspector.IsSet(Attr.SqlType)
                .ShouldBeTrue();
        }

        [Test]
        public void SqlTypeIsNotSet()
        {
            inspector.IsSet(Attr.SqlType)
                .ShouldBeFalse();
        }

        [Test]
        public void UniqueMapped()
        {
            mapping.Unique = true;
            inspector.Unique.ShouldBeTrue();
        }

        [Test]
        public void UniqueIsSet()
        {
            mapping.Unique = true;
            inspector.IsSet(Attr.Unique)
                .ShouldBeTrue();
        }

        [Test]
        public void UniqueIsNotSet()
        {
            inspector.IsSet(Attr.Unique)
                .ShouldBeFalse();
        }

        [Test]
        public void UniqueKeyMapped()
        {
            mapping.UniqueKey = "key";
            inspector.UniqueKey.ShouldEqual("key");
        }

        [Test]
        public void UniqueKeyIsSet()
        {
            mapping.UniqueKey = "key";
            inspector.IsSet(Attr.UniqueKey)
                .ShouldBeTrue();
        }

        [Test]
        public void UniqueKeyIsNotSet()
        {
            inspector.IsSet(Attr.UniqueKey)
                .ShouldBeFalse();
        }

        private Member Prop(Expression<Func<IColumnInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }
    }
}