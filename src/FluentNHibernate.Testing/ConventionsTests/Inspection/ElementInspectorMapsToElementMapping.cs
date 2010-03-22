using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class ElementInspectorMapsToElementMapping
    {
        private ElementMapping mapping;
        private IElementInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new ElementMapping();
            inspector = new ElementInspector(mapping);
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
        public void TypeMapped()
        {
            mapping.Type = new TypeReference(typeof(ExampleClass));
            inspector.Type.ShouldEqual(new TypeReference(typeof(ExampleClass)));
        }

        [Test]
        public void TypeIsSet()
        {
            mapping.Type = new TypeReference(typeof(ExampleClass));
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
        public void FormulaMapped()
        {
            mapping.Formula = "formula";
            inspector.Formula.ShouldEqual("formula");
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
        public void LengthMapped()
        {
            mapping.Length = 50;
            inspector.Length.ShouldEqual(50);
        }

        [Test]
        public void LengthIsSet()
        {
            mapping.Length = 50;
            inspector.IsSet(Attr.Length)
                .ShouldBeTrue();
        }

        [Test]
        public void LengthIsNotSet()
        {
            inspector.IsSet(Attr.Length)
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IElementInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}