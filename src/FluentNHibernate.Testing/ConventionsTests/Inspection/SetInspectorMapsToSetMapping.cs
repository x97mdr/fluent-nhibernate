using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class SetInspectorMapsToSetMapping
    {
        private CollectionMapping mapping;
        private ISetInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new CollectionMapping();
            inspector = new SetInspector(mapping);
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

        #region Helpers

        private Member Prop(Expression<Func<ISetInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}