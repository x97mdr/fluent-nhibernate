using System.Linq;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class ClassInspectorMapsToClassMapping
    {
        private ClassMapping mapping;
        private IClassInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new ClassMapping(typeof(ExampleClass));
            inspector = new ClassInspector(mapping);
        }

        [Test]
        public void AbstractMapped()
        {
            mapping.Abstract = true;
            inspector.Abstract.ShouldBeTrue();
        }

        [Test]
        public void AbstractIsSet()
        {
            mapping.Abstract = true;
            inspector.IsSet(Attr.Abstract)
                .ShouldBeTrue();
        }

        [Test]
        public void AbstractIsNotSet()
        {
            inspector.IsSet(Attr.Abstract)
                .ShouldBeFalse();
        }

        [Test]
        public void AnysCollectionHasSameCountAsMapping()
        {
            mapping.AddAny(new AnyMapping());
            inspector.Anys.Count().ShouldEqual(1);
        }

        [Test]
        public void AnysCollectionOfInspectors()
        {
            mapping.AddAny(new AnyMapping());
            inspector.Anys.First().ShouldBeOfType<IAnyInspector>();
        }

        [Test]
        public void AnysCollectionIsEmpty()
        {
            inspector.Anys.IsEmpty().ShouldBeTrue();
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
            mapping.Cache.Usage = "test";
            inspector.Cache.Usage.ShouldEqual("test");
        }

        [Test]
        public void CacheIsSet()
        {
            mapping.Cache = new CacheMapping();
            mapping.Cache.Usage = "test";
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
        public void CheckMapped()
        {
            mapping.Check = "test";
            inspector.Check.ShouldEqual("test");
        }

        [Test]
        public void CheckIsSet()
        {
            mapping.Check = "test";
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
        public void CollectionsCollectionHasSameCountAsMapping()
        {
            mapping.AddCollection(new CollectionMapping());
            inspector.Collections.Count().ShouldEqual(1);
        }

        [Test]
        public void CollectionsCollectionOfInspectors()
        {
            mapping.AddCollection(new CollectionMapping());
            inspector.Collections.First().ShouldImplementType<ICollectionInspector>();
        }

        [Test]
        public void CollectionsCollectionIsEmpty()
        {
            inspector.Collections.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void ComponentsCollectionHasSameCountAsMapping()
        {
            mapping.AddComponent(new ComponentMapping(ComponentType.Component));
            inspector.Components.Count().ShouldEqual(1);
        }

        [Test]
        public void ComponentsCollectionOfInspectors()
        {
            mapping.AddComponent(new ComponentMapping(ComponentType.Component));
            inspector.Components.First().ShouldImplementType<IComponentBaseInspector>();
        }

        [Test]
        public void ComponentsCollectionIsEmpty()
        {
            inspector.Components.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void DiscriminatorMapped()
        {
            mapping.Discriminator = new DiscriminatorMapping();
            mapping.Discriminator.Insert = true;
            inspector.Discriminator.Insert.ShouldBeTrue();
        }

        [Test]
        public void DiscriminatorIsSet()
        {
            mapping.Discriminator = new DiscriminatorMapping();
            mapping.Discriminator.Insert = true;
            inspector.IsSet(Attr.Discriminator)
                .ShouldBeTrue();
        }

        [Test]
        public void DiscriminatorIsNotSet()
        {
            inspector.IsSet(Attr.Discriminator)
                .ShouldBeFalse();
        }

        [Test]
        public void DiscriminatorValueMapped()
        {
            mapping.DiscriminatorValue = "test";
            inspector.DiscriminatorValue.ShouldEqual("test");
        }

        [Test]
        public void DiscriminatorValueIsSet()
        {
            mapping.DiscriminatorValue = "test";
            inspector.IsSet(Attr.DiscriminatorValue)
                .ShouldBeTrue();
        }

        [Test]
        public void DiscriminatorValueIsNotSet()
        {
            inspector.IsSet(Attr.DiscriminatorValue)
                .ShouldBeFalse();
        }

        [Test]
        public void DynamicInsertMapped()
        {
            mapping.DynamicInsert = true;
            inspector.DynamicInsert.ShouldBeTrue();
        }

        [Test]
        public void DynamicInsertIsSet()
        {
            mapping.DynamicInsert = true;
            inspector.IsSet(Attr.DynamicInsert)
                .ShouldBeTrue();
        }

        [Test]
        public void DynamicInsertIsNotSet()
        {
            inspector.IsSet(Attr.DynamicInsert)
                .ShouldBeFalse();
        }

        [Test]
        public void DynamicUpdateMapped()
        {
            mapping.DynamicUpdate = true;
            inspector.DynamicUpdate.ShouldBeTrue();
        }

        [Test]
        public void DynamicUpdateIsSet()
        {
            mapping.DynamicUpdate = true;
            inspector.IsSet(Attr.DynamicUpdate)
                .ShouldBeTrue();
        }

        [Test]
        public void DynamicUpdateIsNotSet()
        {
            inspector.IsSet(Attr.DynamicUpdate)
                .ShouldBeFalse();
        }

        [Test]
        public void IdMapped()
        {
            mapping.Id = new IdMapping { Name = "test" };
            inspector.Id.Name.ShouldEqual("test");
        }

        [Test]
        public void IdIsSet()
        {
            mapping.Id = new IdMapping { Name = "test" };
            inspector.IsSet(Attr.Id)
                .ShouldBeTrue();
        }

        [Test]
        public void IdIsNotSet()
        {
            inspector.IsSet(Attr.Id)
                .ShouldBeFalse();
        }

        [Test]
        public void JoinsCollectionHasSameCountAsMapping()
        {
            mapping.AddJoin(new JoinMapping());
            inspector.Joins.Count().ShouldEqual(1);
        }

        [Test]
        public void JoinsCollectionOfInspectors()
        {
            mapping.AddJoin(new JoinMapping());
            inspector.Joins.First().ShouldImplementType<IJoinInspector>();
        }

        [Test]
        public void JoinsCollectionIsEmpty()
        {
            inspector.Joins.IsEmpty().ShouldBeTrue();
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
        public void ReadOnlyMapped()
        {
            mapping.Mutable = true;
            inspector.ReadOnly.ShouldBeFalse();
        }

        [Test]
        public void ReadOnlyIsSet()
        {
            mapping.Mutable = true;
            inspector.IsSet(Attr.Mutable)
                .ShouldBeTrue();
        }

        [Test]
        public void ReadOnlyIsNotSet()
        {
            inspector.IsSet(Attr.Mutable)
                .ShouldBeFalse();
        }

        [Test]
        public void NameMapped()
        {
            mapping.Name = "test";
            inspector.Name.ShouldEqual("test");
        }

        [Test]
        public void NameIsSet()
        {
            mapping.Name = "test";
            inspector.IsSet(Attr.Name)
                .ShouldBeTrue();
        }

        [Test, Ignore("Name is set by default")]
        public void NameIsNotSet()
        {
            inspector.IsSet(Attr.Name)
                .ShouldBeFalse();
        }

        [Test]
        public void OneToOnesCollectionHasSameCountAsMapping()
        {
            mapping.AddOneToOne(new OneToOneMapping());
            inspector.OneToOnes.Count().ShouldEqual(1);
        }

        [Test]
        public void OneToOnesCollectionOfInspectors()
        {
            mapping.AddOneToOne(new OneToOneMapping());
            inspector.OneToOnes.First().ShouldImplementType<IOneToOneInspector>();
        }

        [Test]
        public void OneToOnesCollectionIsEmpty()
        {
            inspector.OneToOnes.IsEmpty().ShouldBeTrue();
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
            mapping.Persister = "string";
            inspector.Persister.ShouldEqual("string");
        }

        [Test]
        public void PersisterIsSet()
        {
            mapping.Persister = "string";
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
        public void PolymorphismMapped()
        {
            mapping.Polymorphism = "implicit";
            inspector.Polymorphism.ShouldEqual(Polymorphism.Implicit);
        }

        [Test]
        public void PolymorphismIsSet()
        {
            mapping.Polymorphism = "implicit";
            inspector.IsSet(Attr.Polymorphism)
                .ShouldBeTrue();
        }

        [Test]
        public void PolymorphismIsNotSet()
        {
            inspector.IsSet(Attr.Polymorphism)
                .ShouldBeFalse();
        }

        [Test]
        public void PropertiesCollectionHasSameCountAsMapping()
        {
            mapping.AddProperty(new PropertyMapping());
            inspector.Properties.Count().ShouldEqual(1);
        }

        [Test]
        public void PropertiesCollectionOfInspectors()
        {
            mapping.AddProperty(new PropertyMapping());
            inspector.Properties.First().ShouldImplementType<IPropertyInspector>();
        }

        [Test]
        public void PropertiesCollectionIsEmpty()
        {
            inspector.Properties.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void ProxyMapped()
        {
            mapping.Proxy = "proxy";
            inspector.Proxy.ShouldEqual("proxy");
        }

        [Test]
        public void ProxyIsSet()
        {
            mapping.Proxy = "proxy";
            inspector.IsSet(Attr.Proxy)
                .ShouldBeTrue();
        }

        [Test]
        public void ProxyIsNotSet()
        {
            inspector.IsSet(Attr.Proxy)
                .ShouldBeFalse();
        }

        [Test]
        public void WhereMapped()
        {
            mapping.Where = "where";
            inspector.Where.ShouldEqual("where");
        }

        [Test]
        public void WhereIsSet()
        {
            mapping.Where = "where";
            inspector.IsSet(Attr.Where)
                .ShouldBeTrue();
        }

        [Test]
        public void WhereIsNotSet()
        {
            inspector.IsSet(Attr.Where)
                .ShouldBeFalse();
        }

        [Test]
        public void SubselectMapped()
        {
            mapping.Subselect = "sql";
            inspector.Subselect.ShouldEqual("sql");
        }

        [Test]
        public void SubselectIsSet()
        {
            mapping.Subselect = "sql";
            inspector.IsSet(Attr.Subselect)
                .ShouldBeTrue();
        }

        [Test]
        public void SubselectIsNotSet()
        {
            inspector.IsSet(Attr.Subselect)
                .ShouldBeFalse();
        }

        [Test]
        public void ReferencesCollectionHasSameCountAsMapping()
        {
            mapping.AddReference(new ManyToOneMapping());
            inspector.References.Count().ShouldEqual(1);
        }

        [Test]
        public void ReferencesCollectionOfInspectors()
        {
            mapping.AddReference(new ManyToOneMapping());
            inspector.References.First().ShouldImplementType<IManyToOneInspector>();
        }

        [Test]
        public void ReferencesCollectionIsEmpty()
        {
            inspector.References.IsEmpty().ShouldBeTrue();
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
        public void SelectBeforeUpdateMapped()
        {
            mapping.SelectBeforeUpdate = true;
            inspector.SelectBeforeUpdate.ShouldBeTrue();
        }

        [Test]
        public void SelectBeforeUpdateIsSet()
        {
            mapping.SelectBeforeUpdate = true;
            inspector.IsSet(Attr.SelectBeforeUpdate)
                .ShouldBeTrue();
        }

        [Test]
        public void SelectBeforeUpdateIsNotSet()
        {
            inspector.IsSet(Attr.SelectBeforeUpdate)
                .ShouldBeFalse();
        }

        [Test]
        public void SubclassesCollectionHasSameCountAsMapping()
        {
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));
            inspector.Subclasses.Count().ShouldEqual(1);
        }

        [Test]
        public void SubclassesCollectionOfInspectors()
        {
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));
            inspector.Subclasses.First().ShouldImplementType<ISubclassInspector>();
        }

        [Test]
        public void SubclassesCollectionIsEmpty()
        {
            inspector.Subclasses.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void TableNameMapped()
        {
            mapping.TableName = "tbl";
            inspector.TableName.ShouldEqual("tbl");
        }

        [Test]
        public void TableNameIsSet()
        {
            mapping.TableName = "tbl";
            inspector.IsSet(Attr.Table)
                .ShouldBeTrue();
        }

        [Test, Ignore("Table names are specified by default")]
        public void TableNameIsNotSet()
        {
            inspector.IsSet(Attr.Table)
                .ShouldBeFalse();
        }

        [Test]
        public void TypeMapped()
        {
            mapping.Type = typeof(ExampleClass);
            inspector.EntityType.ShouldEqual(typeof(ExampleClass));
        }

        [Test]
        public void TypeIsSet()
        {
            mapping.Type = typeof(ExampleClass);
            inspector.IsSet(Attr.Type)
                .ShouldBeTrue();
        }

        [Test]
        public void TypeIsNotSet()
        {
            inspector.IsSet(Attr.EntityType)
                .ShouldBeFalse();
        }

        [Test]
        public void VersionMapped()
        {
            mapping.Version = new VersionMapping();
            mapping.Version.Name = "test";
            inspector.Version.Name.ShouldEqual("test");
        }

        [Test]
        public void VersionIsSet()
        {
            mapping.Version = new VersionMapping();
            mapping.Version.Name = "test";
            inspector.IsSet(Attr.Version)
                .ShouldBeTrue();
        }

        [Test]
        public void VersionIsNotSet()
        {
            inspector.IsSet(Attr.Version)
                .ShouldBeFalse();
        }

        [Test]
        public void SchemaActionMapped()
        {
            mapping.SchemaAction = "none";
            inspector.SchemaAction.ShouldEqual(SchemaAction.None);
        }

        [Test]
        public void SchemaActionIsSet()
        {
            mapping.SchemaAction = "none";
            inspector.IsSet(Attr.SchemaAction)
                .ShouldBeTrue();
        }

        [Test]
        public void SchemaActionIsNotSet()
        {
            inspector.IsSet(Attr.SchemaAction)
                .ShouldBeFalse();
        }
    }
}