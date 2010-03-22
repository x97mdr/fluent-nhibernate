using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class CollectionInspectorMapsToBagMapping
    {
        private CollectionMapping mapping;
        private ICollectionInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new CollectionMapping();
            inspector = new CollectionInspector(mapping);
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
        public void BatchSizeMapped()
        {
            mapping.BatchSize = 10;
            inspector.BatchSize.ShouldEqual(10);
        }

        [Test]
        public void BatchSizeIsSet()
        {
            mapping.BatchSize = 10;
            inspector.IsSet(Attr.BatchSize)
                .ShouldBeTrue();
        }

        [Test]
        public void BatchSizeIsNotSet()
        {
            inspector.IsSet(Attr.BatchSize)
                .ShouldBeFalse();
        }

        [Test]
        public void CacheMapped()
        {
            mapping.Cache = new CacheMapping();
            mapping.Cache.Usage = "value";
            inspector.Cache.Usage.ShouldEqual("value");
        }

        [Test]
        public void CacheIsSet()
        {
            mapping.Cache = new CacheMapping();
            mapping.Cache.Usage = "value";
            inspector.IsSet(Attr.Cache)
                .ShouldBeTrue();
        }

        [Test]
        public void CacheIsNotSet()
        {
            inspector.IsSet(Attr.Cache)
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
        public void CheckMapped()
        {
            mapping.Check = "value";
            inspector.Check.ShouldEqual("value");
        }

        [Test]
        public void CheckIsSet()
        {
            mapping.Check = "value";
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
        public void ChildTypeMapped()
        {
            mapping.ChildType = typeof(ExampleClass);
            inspector.ChildType.ShouldEqual(typeof(ExampleClass));
        }

        [Test]
        public void ChildTypeIsSet()
        {
            mapping.ChildType = typeof(ExampleClass);
            inspector.IsSet(Attr.ChildType)
                .ShouldBeTrue();
        }

        [Test]
        public void ChildTypeIsNotSet()
        {
            inspector.IsSet(Attr.ChildType)
                .ShouldBeFalse();
        }

        [Test]
        public void CollectionTypeMapped()
        {
            mapping.CollectionType = new TypeReference(typeof(IList<ExampleClass>));
            inspector.CollectionType.ShouldEqual(new TypeReference(typeof(IList<ExampleClass>)));
        }

        [Test]
        public void CollectionTypeIsSet()
        {
            mapping.CollectionType = new TypeReference(typeof(IList<ExampleClass>));
            inspector.IsSet(Attr.CollectionType)
                .ShouldBeTrue();
        }

        [Test]
        public void CollectionTypeIsNotSet()
        {
            inspector.IsSet(Attr.CollectionType)
                .ShouldBeFalse();
        }

        [Test]
        public void CompositeElementMapped()
        {
            mapping.CompositeElement = new CompositeElementMapping();
            mapping.CompositeElement.Initialise(typeof(ExampleClass));
            inspector.CompositeElement.Class.ShouldEqual(new TypeReference(typeof(ExampleClass)));
        }

        [Test]
        public void CompositeElementIsSet()
        {
            mapping.CompositeElement = new CompositeElementMapping();
            inspector.IsSet(Attr.CompositeElement)
                .ShouldBeTrue();
        }

        [Test]
        public void CompositeElementIsNotSet()
        {
            inspector.IsSet(Attr.CompositeElement)
                .ShouldBeFalse();
        }

        [Test]
        public void ElementMapped()
        {
            mapping.Element = new ElementMapping();
            mapping.Element.Type = new TypeReference(typeof(ExampleClass));
            inspector.Element.Type.ShouldEqual(new TypeReference(typeof(ExampleClass)));
        }

        [Test]
        public void ElementIsSet()
        {
            mapping.Element = new ElementMapping();
            mapping.Element.Type = new TypeReference(typeof(ExampleClass));
            inspector.IsSet(Attr.Element)
                .ShouldBeTrue();
        }

        [Test]
        public void ElementIsNotSet()
        {
            inspector.IsSet(Attr.Element)
                .ShouldBeFalse();
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
        public void GenericMapped()
        {
            mapping.Generic = true;
            inspector.Generic.ShouldEqual(true);
        }

        [Test]
        public void GenericIsSet()
        {
            mapping.Generic = true;
            inspector.IsSet(Attr.Generic)
                .ShouldBeTrue();
        }

        [Test]
        public void GenericIsNotSet()
        {
            inspector.IsSet(Attr.Generic)
                .ShouldBeFalse();
        }

        [Test]
        public void InverseMapped()
        {
            mapping.Inverse = true;
            inspector.Inverse.ShouldEqual(true);
        }

        [Test]
        public void InverseIsSet()
        {
            mapping.Inverse = true;
            inspector.IsSet(Attr.Inverse)
                .ShouldBeTrue();
        }

        [Test]
        public void InverseIsNotSet()
        {
            inspector.IsSet(Attr.Inverse)
                .ShouldBeFalse();
        }

        [Test]
        public void KeyMapped()
        {
            mapping.Key = new KeyMapping();
            mapping.Key.ForeignKey = "key";
            inspector.Key.ForeignKey.ShouldEqual("key");
        }

        [Test]
        public void KeyIsSet()
        {
            mapping.Key = new KeyMapping();
            mapping.Key.ForeignKey = "key";
            inspector.IsSet(Attr.Key)
                .ShouldBeTrue();
        }

        [Test]
        public void KeyIsNotSet()
        {
            inspector.IsSet(Attr.Key)
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
            mapping.OptimisticLock = "all";
            inspector.OptimisticLock.ShouldEqual(OptimisticLock.All);
        }

        [Test]
        public void OptimisticLockIsSet()
        {
            mapping.OptimisticLock = "all";
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
        public void PersisterMapped()
        {
            mapping.Persister = new TypeReference("persister");
            inspector.Persister.ShouldEqual(new TypeReference("persister"));
        }

        [Test]
        public void PersisterIsSet()
        {
            mapping.Persister = new TypeReference("persister");
            inspector.IsSet(Attr.Persister)
                .ShouldBeTrue();
        }

        [Test]
        public void PersisterIsNotSet()
        {
            inspector.IsSet(Attr.Persister)
                .ShouldBeFalse();
        }

        [Test]
        public void RelationshipMapped()
        {
            mapping.Relationship = new ManyToManyMapping();
            mapping.Relationship.Class = new TypeReference(typeof(ExampleClass));
            inspector.Relationship.Class.ShouldEqual(new TypeReference(typeof(ExampleClass)));
        }

        [Test]
        public void RelationshipIsSet()
        {
            mapping.Relationship = new ManyToManyMapping();
            mapping.Relationship.Class = new TypeReference(typeof(ExampleClass));
            inspector.IsSet(Attr.Relationship)
                .ShouldBeTrue();
        }

        [Test]
        public void RelationshipIsNotSet()
        {
            inspector.IsSet(Attr.Relationship)
                .ShouldBeFalse();
        }

        [Test]
        public void SchemaMapped()
        {
            mapping.Schema = "dbo";
            inspector.Schema.ShouldEqual("dbo");
        }

        [Test]
        public void SchemaIsSet()
        {
            mapping.Schema = "dbo";
            inspector.IsSet(Attr.Schema)
                .ShouldBeTrue();
        }

        [Test]
        public void SchemaIsNotSet()
        {
            inspector.IsSet(Attr.Schema)
                .ShouldBeFalse();
        }

        [Test]
        public void TableNameMapped()
        {
            mapping.TableName = "table";
            inspector.TableName.ShouldEqual("table");
        }

        [Test]
        public void TableNameIsSet()
        {
            mapping.TableName = "table";
            inspector.IsSet(Attr.Table)
                .ShouldBeTrue();
        }

        [Test]
        public void TableNameIsNotSet()
        {
            inspector.IsSet(Attr.Table)
                .ShouldBeFalse();
        }

        [Test]
        public void WhereMapped()
        {
            mapping.Where = "x = 1";
            inspector.Where.ShouldEqual("x = 1");
        }

        [Test]
        public void WhereIsSet()
        {
            mapping.Where = "x = 1";
            inspector.IsSet(Attr.Where)
                .ShouldBeTrue();
        }

        [Test]
        public void WhereIsNotSet()
        {
            inspector.IsSet(Attr.Where)
                .ShouldBeFalse();
        }
    }
}