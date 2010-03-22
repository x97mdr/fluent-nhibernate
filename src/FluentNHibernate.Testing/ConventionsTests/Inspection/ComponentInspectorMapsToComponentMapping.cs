using System.Linq;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.Inspection
{
    [TestFixture, Category("Inspection DSL")]
    public class ComponentInspectorMapsToComponentMapping
    {
        private ComponentMapping mapping;
        private IComponentInspector inspector;

        [SetUp]
        public void CreateDsl()
        {
            mapping = new ComponentMapping(ComponentType.Component);
            inspector = new ComponentInspector(mapping);
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
        public void ClassMapped()
        {
            mapping.Class = new TypeReference(typeof(ExampleClass));
            inspector.Class.ShouldEqual(new TypeReference(typeof(ExampleClass)));
        }

        [Test]
        public void ClassIsSet()
        {
            mapping.Class = new TypeReference(typeof(ExampleClass));
            inspector.IsSet(Attr.Class)
                .ShouldBeTrue();
        }

        [Test]
        public void ClassIsNotSet()
        {
            inspector.IsSet(Attr.Class)
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
            inspector.Collections.First().ShouldBeOfType<ICollectionInspector>();
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
            inspector.Components.First().ShouldBeOfType<IComponentBaseInspector>();
        }

        [Test]
        public void ComponentsCollectionIsEmpty()
        {
            inspector.Components.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void InsertMapped()
        {
            mapping.Insert = true;
            inspector.Insert.ShouldEqual(true);
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
        public void OptimisticLockMapped()
        {
            mapping.OptimisticLock = true;
            inspector.OptimisticLock.ShouldEqual(true);
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
        public void OneToOnesCollectionHasSameCountAsMapping()
        {
            mapping.AddOneToOne(new OneToOneMapping());
            inspector.OneToOnes.Count().ShouldEqual(1);
        }

        [Test]
        public void OneToOnesCollectionOfInspectors()
        {
            mapping.AddOneToOne(new OneToOneMapping());
            inspector.OneToOnes.First().ShouldBeOfType<IOneToOneInspector>();
        }

        [Test]
        public void OneToOnesCollectionIsEmpty()
        {
            inspector.OneToOnes.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void ParentMapped()
        {
            mapping.Parent = new ParentMapping();
            mapping.Parent.Name = "name";
            inspector.Parent.Name.ShouldEqual("name");
        }

        [Test]
        public void ParentIsSet()
        {
            mapping.Parent = new ParentMapping();
            mapping.Parent.Name = "name";
            inspector.IsSet(Attr.Parent)
                .ShouldBeTrue();
        }

        [Test]
        public void ParentIsNotSet()
        {
            inspector.IsSet(Attr.Parent)
                .ShouldBeFalse();
        }

        [Test]
        public void UniqueMapped()
        {
            mapping.Unique = true;
            inspector.Unique.ShouldEqual(true);
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
        public void PropertiesCollectionHasSameCountAsMapping()
        {
            mapping.AddProperty(new PropertyMapping());
            inspector.Properties.Count().ShouldEqual(1);
        }

        [Test]
        public void PropertiesCollectionOfInspectors()
        {
            mapping.AddProperty(new PropertyMapping());
            inspector.Properties.First().ShouldBeOfType<IPropertyInspector>();
        }

        [Test]
        public void PropertiesCollectionIsEmpty()
        {
            inspector.Properties.IsEmpty().ShouldBeTrue();
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
            inspector.References.First().ShouldBeOfType<IManyToOneInspector>();
        }

        [Test]
        public void ReferencesCollectionIsEmpty()
        {
            inspector.References.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void TypeMapped()
        {
            mapping.Type = typeof(ExampleClass);
            inspector.Type.ShouldEqual(typeof(ExampleClass));
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
            inspector.IsSet(Attr.Type)
                .ShouldBeFalse();
        }

        [Test]
        public void UpdateMapped()
        {
            mapping.Update = true;
            inspector.Update.ShouldEqual(true);
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
    }
}