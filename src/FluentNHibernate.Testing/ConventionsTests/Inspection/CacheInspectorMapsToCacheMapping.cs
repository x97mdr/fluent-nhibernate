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
    public class CacheInspectorMapsToCacheMapping
    {
        private CacheMapping mapping;
        private ICacheInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new CacheMapping();
            inspector = new CacheInspector(mapping);
        }

        [Test]
        public void IncludeMapped()
        {
            mapping.Include = "all";
            inspector.Include.ShouldEqual(Include.All);
        }

        [Test]
        public void IncludeIsSet()
        {
            mapping.Include = "all";
            inspector.IsSet(Attr.Include)
                .ShouldBeTrue();
        }

        [Test]
        public void IncludeIsNotSet()
        {
            inspector.IsSet(Attr.Include)
                .ShouldBeFalse();
        }

        [Test]
        public void RegionMapped()
        {
            mapping.Region = "region";
            inspector.Region.ShouldEqual("region");
        }

        [Test]
        public void RegionIsSet()
        {
            mapping.Region = "region";
            inspector.IsSet(Attr.Region)
                .ShouldBeTrue();
        }

        [Test]
        public void RegionIsNotSet()
        {
            inspector.IsSet(Attr.Region)
                .ShouldBeFalse();
        }

        [Test]
        public void UsageMapped()
        {
            mapping.Usage = "usage";
            inspector.Usage.ShouldEqual("usage");
        }

        [Test]
        public void UsageIsSet()
        {
            mapping.Usage = "usage";
            inspector.IsSet(Attr.Usage)
                .ShouldBeTrue();
        }

        [Test]
        public void UsageIsNotSet()
        {
            inspector.IsSet(Attr.Usage)
                .ShouldBeFalse();
        }

        #region Helpers

        private Member Prop(Expression<Func<ICacheInspector, object>> propertyExpression)
        {
            return ReflectionHelper.GetMember(propertyExpression);
        }

        #endregion
    }
}