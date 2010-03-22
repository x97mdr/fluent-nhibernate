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
    public class PropertyInspectorMapsToPropertyMapping
    {
        private PropertyMapping mapping;
        private IPropertyInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new PropertyMapping();
            inspector = new PropertyInspector(mapping);
        }

        [Test]
        public void AccessMapped()
        {
            mapping.Access = "field";
            inspector.Access.ShouldEqual(Access.FromString(mapping.Access));
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
        public void CustomTypeMappedToType()
        {
            mapping.Type = new TypeReference(typeof(int));
            inspector.Type.ShouldEqual(mapping.Type);
        }

        [Test]
        public void CustomTypeIsSet()
        {
            mapping.Type = new TypeReference(typeof(int));
            inspector.IsSet(Attr.Type)
                .ShouldBeTrue();
        }

        [Test]
        public void CustomTypeIsNotSet()
        {
            inspector.IsSet(Attr.Type)
                .ShouldBeFalse();
        }

        [Test]
        public void EntityTypeMappedToClrType()
        {
            inspector.EntityType.ShouldEqual(mapping.ContainingEntityType);
        }

        [Test]
        public void FormulaMapped()
        {
            mapping.Formula = "formula";
            inspector.Formula.ShouldEqual(mapping.Formula);
        }

        [Test]
        public void FormulaIsSet()
        {
            mapping.Formula = "formula";
            inspector.IsSet(Attr.Formula)
                .ShouldBeTrue();
        }

        [Test]
        public void FormulaIsNotSet()
        {
            inspector.IsSet(Attr.Formula)
                .ShouldBeFalse();
        }

        [Test]
        public void InsertMapped()
        {
            mapping.Insert = true;
            inspector.Insert.ShouldEqual(mapping.Insert);
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
        public void UpdateMapped()
        {
            mapping.Update = true;
            inspector.Update.ShouldEqual(mapping.Update);
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

        [Test]
        public void NameMapped()
        {
            mapping.Name = "name";
            inspector.Name.ShouldEqual(mapping.Name);
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
            inspector.OptimisticLock.ShouldEqual(mapping.OptimisticLock);
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
        public void GeneratedMapped()
        {
            mapping.Generated = "never";
            inspector.Generated.ShouldEqual(Generated.Never);
        }

        [Test]
        public void GeneratedIsSet()
        {
            mapping.Generated = "never";
            inspector.IsSet(Attr.Generated)
                .ShouldBeTrue();
        }

        [Test]
        public void GeneratedIsNotSet()
        {
            inspector.IsSet(Attr.Generated)
                .ShouldBeFalse();
        }

        [Test]
        public void PropertyMapped()
        {
            mapping.Member = Prop(x => x.Access);
            inspector.Property.ShouldEqual(mapping.Member);
        }

        [Test]
        public void ColumnCollectionHasSameCountAsMapping()
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
        public void IndexMapped()
        {
            mapping.AddColumn(new ColumnMapping { Index = "index" });
            inspector.Index.ShouldEqual("index");
        }

        [Test]
        public void IndexIsSet()
        {
            mapping.AddColumn(new ColumnMapping { Index = "index" });
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
        public void LengthMapped()
        {
            mapping.AddColumn(new ColumnMapping() { Length = 10 });
            inspector.Length.ShouldEqual(10);
        }

        [Test]
        public void LengthIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { Length = 10 });
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
        public void PrecisionMapped()
        {
            mapping.AddColumn(new ColumnMapping() { Precision = 10 });
            inspector.Precision.ShouldEqual(10);
        }

        [Test]
        public void PrecisionIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { Precision = 10 });
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
            mapping.AddColumn(new ColumnMapping() { Scale = 10 });
            inspector.Scale.ShouldEqual(10);
        }

        [Test]
        public void ScaleIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { Scale = 10 });
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
        public void NullableMapped()
        {
            mapping.AddColumn(new ColumnMapping() { NotNull = false });
            inspector.Nullable.ShouldEqual(true);
        }

        [Test]
        public void NullableIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { NotNull = false });
            inspector.IsSet(Attr.NotNull)
                .ShouldBeTrue();
        }

        [Test]
        public void NullableIsNotSet()
        {
            inspector.IsSet(Attr.NotNull)
                .ShouldBeFalse();
        }

        [Test]
        public void SqlTypeMapped()
        {
            mapping.AddColumn(new ColumnMapping() { SqlType = "sql" });
            inspector.SqlType.ShouldEqual("sql");
        }

        [Test]
        public void SqlTypeIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { SqlType = "sql" });
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
            mapping.AddColumn(new ColumnMapping() { Unique = true });
            inspector.Unique.ShouldEqual(true);
        }

        [Test]
        public void UniqueIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { Unique = true });
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
            mapping.AddColumn(new ColumnMapping() { UniqueKey = "key" });
            inspector.UniqueKey.ShouldEqual("key");
        }

        [Test]
        public void UniqueKeyIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { UniqueKey = "key" });
            inspector.IsSet(Attr.UniqueKey)
                .ShouldBeTrue();
        }

        [Test]
        public void UniqueKeyIsNotSet()
        {
            inspector.IsSet(Attr.UniqueKey)
                .ShouldBeFalse();
        }

        [Test]
        public void CheckMapped()
        {
            mapping.AddColumn(new ColumnMapping() { Check = "key" });
            inspector.Check.ShouldEqual("key");
        }

        [Test]
        public void CheckIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { Check = "key" });
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
            mapping.AddColumn(new ColumnMapping() { Default = "key" });
            inspector.Default.ShouldEqual("key");
        }

        [Test]
        public void DefaultIsSet()
        {
            mapping.AddColumn(new ColumnMapping() { Default = "key" });
            inspector.IsSet(Attr.Default)
                .ShouldBeTrue();
        }

        [Test]
        public void DefaultIsNotSet()
        {
            inspector.IsSet(Attr.Default)
                .ShouldBeFalse();
        }

        private Member Prop(Expression<Func<IPropertyInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }
    }
}
