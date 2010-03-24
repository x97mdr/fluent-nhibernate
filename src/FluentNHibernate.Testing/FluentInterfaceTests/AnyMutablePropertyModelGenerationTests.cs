using System;
using System.Linq;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.DomainModel.Mapping;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    [TestFixture]
    public class AnyMutablePropertyModelGenerationTests : BaseModelFixture
    {
        [Test]
        public void AccessSetsModelAccessPropertyToValue()
        {
            any(m => m.Access.Field())
                .ModelShouldMatch(x => x.Access.ShouldEqual("field"));
        }

        [Test]
        public void CascadeSetsModelCascadePropertyToValue()
        {
            any(m => m.Cascade.All())
                .ModelShouldMatch(x => x.Cascade.ShouldEqual("all"));
        }

        [Test]
        public void IdentityTypeSetsModelIdTypePropertyToTypeName()
        {
            any(m => {})
                .ModelShouldMatch(x => x.IdType.ShouldEqual(typeof(int).AssemblyQualifiedName));
        }

        [Test]
        public void InsertSetsModelInsertPropertyToTrue()
        {
            any(m => m.Insert())
                .ModelShouldMatch(x => x.Insert.ShouldBeTrue());
        }

        [Test]
        public void NotInsertSetsModelInsertPropertyToFalse()
        {
            any(m => m.Not.Insert())
                .ModelShouldMatch(x => x.Insert.ShouldBeFalse());
        }

        [Test]
        public void UpdateSetsModelUpdatePropertyToTrue()
        {
            any(m => m.Update())
                .ModelShouldMatch(x => x.Update.ShouldBeTrue());
        }

        [Test]
        public void NotUpdateSetsModelUpdatePropertyToFalse()
        {
            any(m => m.Not.Update())
                .ModelShouldMatch(x => x.Update.ShouldBeFalse());
        }

        [Test]
        public void ReadOnlySetsModelInsertPropertyToFalse()
        {
            any(m => m.ReadOnly())
                .ModelShouldMatch(x => x.Insert.ShouldBeFalse());
        }

        [Test]
        public void NotReadOnlySetsModelInsertPropertyToTrue()
        {
            any(m => m.Not.ReadOnly())
                .ModelShouldMatch(x => x.Insert.ShouldBeTrue());
        }

        [Test]
        public void ReadOnlySetsModelUpdatePropertyToFalse()
        {
            any(m => m.ReadOnly())
                .ModelShouldMatch(x => x.Update.ShouldBeFalse());
        }

        [Test]
        public void NotReadOnlySetsModelUpdatePropertyToTrue()
        {
            any(m => m.Not.ReadOnly())
                .ModelShouldMatch(x => x.Update.ShouldBeTrue());
        }

        [Test]
        public void LazyLoadSetsModelLazyPropertyToTrue()
        {
            any(m => m.LazyLoad())
                .ModelShouldMatch(x => x.Lazy.ShouldBeTrue());
        }

        [Test]
        public void NotLazyLoadSetsModelLazyPropertyToFalse()
        {
            any(m => m.Not.LazyLoad())
                .ModelShouldMatch(x => x.Lazy.ShouldBeFalse());
        }

        [Test]
        public void OptimisticLockSetsModelOptimisticLockPropertyToTrue()
        {
            Any<SecondMappedObject>()
                .Mapping(m => m
                    .IdentityType<int>()
                    .EntityIdentifierColumn("col")
                    .EntityTypeColumn("col2")
                    .OptimisticLock())
                .ModelShouldMatch(x => x.OptimisticLock.ShouldBeTrue());
        }

        [Test]
        public void NotOptimisticLockSetsModelOptimisticLockPropertyToFalse()
        {
            any(m => m.Not.OptimisticLock())
                .ModelShouldMatch(x => x.OptimisticLock.ShouldBeFalse());
        }

        [Test]
        public void MetaTypePropertyShouldBeSetToPropertyTypeIfNoMetaValuesSet()
        {
            any(m => {})
                .ModelShouldMatch(x => x.MetaType.ShouldEqual(new TypeReference(typeof(SecondMappedObject))));
        }

        [Test]
        public void MetaTypePropertyShouldBeSetToStringIfMetaValuesSet()
        {
            any(m => m.AddMetaValue<Record>("Rec"))
                .ModelShouldMatch(x => x.MetaType.ShouldEqual(new TypeReference(typeof(string))));
        }

        [Test]
        public void NamePropertyShouldBeSetToPropertyName()
        {
            any(m => {})
                .ModelShouldMatch(x => x.Name.ShouldEqual("Parent"));
        }

        [Test]
        public void EntityIdentifierColumnShouldAddToModelColumnsCollection()
        {
            any(m => {})
                .ModelShouldMatch(x => x.IdentifierColumns.Count().ShouldEqual(1));
        }

        [Test]
        public void EntityTypeColumnShouldAddToModelColumnsCollection()
        {
            any(m => {})
                .ModelShouldMatch(x => x.TypeColumns.Count().ShouldEqual(1));
        }

        [Test]
        public void AddMetaValueShouldAddToModelMetaValuesCollection()
        {
            any(m => m.AddMetaValue<Record>("Rec"))
                .ModelShouldMatch(x => x.MetaValues.Count().ShouldEqual(1));
        }

        static ModelTester<AnyPart<SecondMappedObject>, AnyMapping> any(Action<AnyPart<SecondMappedObject>> alter)
        {
            return Any<SecondMappedObject>()
                .Mapping(m =>
                {
                    m.IdentityType<int>();
                    m.EntityIdentifierColumn("col");
                    m.EntityTypeColumn("col2");
                    alter(m);
                });
        }
    }
}