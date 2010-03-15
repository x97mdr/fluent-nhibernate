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
    public class BagInspectorMapsToBagMapping
    {
        private CollectionMapping mapping;
        private IBagInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new CollectionMapping();
            inspector = new BagInspector(mapping);
        }

        [Test]
        public void OrderByIsSet()
        {
            mapping.OrderBy = "AField";
            inspector.IsSet(Prop(x => x.OrderBy))
                .ShouldBeTrue();
        }

        [Test]
        public void OrderByIsNotSet()
        {
            inspector.IsSet(Prop(x => x.OrderBy))
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<IBagInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}