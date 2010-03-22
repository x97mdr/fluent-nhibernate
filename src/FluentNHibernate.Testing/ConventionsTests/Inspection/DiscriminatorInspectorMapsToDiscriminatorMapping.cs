using System.Linq;
using System.Reflection;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class DiscriminatorInspectorMapsToDiscriminatorMapping
    {
        private DiscriminatorMapping mapping;
        private IDiscriminatorInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new DiscriminatorMapping();
            inspector = new DiscriminatorInspector(mapping);
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
        public void ForceMapped()
        {
            mapping.Force = true;
            inspector.Force.ShouldBeTrue();
        }

        [Test]
        public void ForceIsSet()
        {
            mapping.Force = true;
            inspector.IsSet(Attr.Force)
                .ShouldBeTrue();
        }

        [Test]
        public void ForceIsNotSet()
        {
            inspector.IsSet(Attr.Force)
                .ShouldBeFalse();
        }

        [Test]
        public void FormulaMapped()
        {
            mapping.Formula = "e=mc^2";
            inspector.Formula.ShouldEqual("e=mc^2");
        }

        [Test]
        public void FormulaIsSet()
        {
            mapping.Formula = "e=mc^2";
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
        public void TypeMapped()
        {
            mapping.Type = new TypeReference(typeof(string));
            inspector.Type.ShouldEqual(new TypeReference(typeof(string)));
        }

        [Test]
        public void TypeIsSet()
        {
            mapping.Type = new TypeReference(typeof(string));
            inspector.IsSet(Attr.Type)
                .ShouldBeTrue();
        }

        [Test]
        public void TypeIsNotSet()
        {
            inspector.IsSet(Attr.Type)
                .ShouldBeFalse();
        }

        [Test]
        public void LengthMapped()
        {
            mapping.AddColumn(new ColumnMapping { Length = 100 });
            inspector.Length.ShouldEqual(100);
        }

        [Test]
        public void LengthIsSet()
        {
            mapping.AddColumn(new ColumnMapping { Length = 100 });
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
        public void PrecisionIsSet()
        {
            mapping.AddColumn(new ColumnMapping { Precision = 10 });
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
            mapping.AddColumn(new ColumnMapping { Scale = 10 });
            inspector.Scale.ShouldEqual(10);
        }

        [Test]
        public void ScaleIsSet()
        {
            mapping.AddColumn(new ColumnMapping { Scale = 10 });
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
            mapping.AddColumn(new ColumnMapping { NotNull = false });
            inspector.Nullable.ShouldEqual(true);
        }

        [Test]
        public void NullableIsSet()
        {
            mapping.AddColumn(new ColumnMapping { NotNull = false });
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
        public void UniqueMapped()
        {
            mapping.AddColumn(new ColumnMapping { Unique = true });
            inspector.Unique.ShouldEqual(true);
        }

        [Test]
        public void UniqueIsSet()
        {
            mapping.AddColumn(new ColumnMapping { Unique = true });
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
            mapping.AddColumn(new ColumnMapping { UniqueKey = "key" });
            inspector.UniqueKey.ShouldEqual("key");
        }

        [Test]
        public void UniqueKeyIsSet()
        {
            mapping.AddColumn(new ColumnMapping { UniqueKey = "key" });
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
        public void SqlTypeMapped()
        {
            mapping.AddColumn(new ColumnMapping { SqlType = "sql" });
            inspector.SqlType.ShouldEqual("sql");
        }

        [Test]
        public void SqlTypeIsSet()
        {
            mapping.AddColumn(new ColumnMapping { SqlType = "sql" });
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
        public void CheckMapped()
        {
            mapping.AddColumn(new ColumnMapping { Check = "key" });
            inspector.Check.ShouldEqual("key");
        }

        [Test]
        public void CheckIsSet()
        {
            mapping.AddColumn(new ColumnMapping { Check = "key" });
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
            mapping.AddColumn(new ColumnMapping { Default = "key" });
            inspector.Default.ShouldEqual("key");
        }

        [Test]
        public void DefaultIsSet()
        {
            mapping.AddColumn(new ColumnMapping { Default = "key" });
            inspector.IsSet(Attr.Default)
                .ShouldBeTrue();
        }

        [Test]
        public void DefaultIsNotSet()
        {
            inspector.IsSet(Attr.Default)
                .ShouldBeFalse();
        }
    }
}