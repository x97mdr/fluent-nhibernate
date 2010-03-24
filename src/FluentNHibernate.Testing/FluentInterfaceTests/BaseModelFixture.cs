using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.DomainModel.Mapping;
using FluentNHibernate.Utils;
using FluentNHibernate.Utils.Reflection;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    public abstract class BaseModelFixture
    {
        protected static ModelTester<ClassMap<T>, ClassMapping> ClassMap<T>()
        {
            var map = new ClassMap<T>();
            return new ModelTester<ClassMap<T>, ClassMapping>(
                () => map,
                () => create_mapping(((IMappingProvider)map).GetUserDefinedMappings().Structure));
        }

        protected static ModelTester<DiscriminatorPart, DiscriminatorMapping> DiscriminatorMap<T>()
        {
            var structure = new FreeStructure<DiscriminatorMapping>();
            var parentStructure = new FreeStructure<ClassMapping>();
            return new ModelTester<DiscriminatorPart, DiscriminatorMapping>(
                () => new DiscriminatorPart(structure, parentStructure),
                () => create_mapping(structure));
        }

        protected static ModelTester<SubclassPart<T>, SubclassMapping> Subclass<T>()
        {
            var structure = new SubclassStructure(SubclassType.JoinedSubclass, typeof(T));
            return new ModelTester<SubclassPart<T>, SubclassMapping>(
                () => new SubclassPart<T>(null, structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<SubclassMap<T>, SubclassMapping> SubclassMapForSubclass<T>()
        {
            var map = new SubclassMap<T>();
            return new ModelTester<SubclassMap<T>, SubclassMapping>(() => map, () =>
            {
                var userMappings = ((IIndeterminateSubclassMappingProvider)map).GetUserDefinedMappings();
                var mapping = (SubclassMapping)userMappings.Structure.CreateMappingNode();
                userMappings.Structure.ApplyCustomisations();
                mapping.SubclassType = SubclassType.Subclass;
                return mapping;
            });
        }

        protected static ModelTester<JoinedSubClassPart<T>, SubclassMapping> JoinedSubclass<T>()
        {
            var structure = new SubclassStructure(SubclassType.JoinedSubclass, typeof(T));
            return new ModelTester<JoinedSubClassPart<T>, SubclassMapping>(
                () => new JoinedSubClassPart<T>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<SubclassMap<T>, SubclassMapping> SubclassMapForJoinedSubclass<T>()
        {
            var map = new SubclassMap<T>();
            return new ModelTester<SubclassMap<T>, SubclassMapping>(() => map, () =>
            {
                var userMappings = ((IIndeterminateSubclassMappingProvider)map).GetUserDefinedMappings();
                var mapping = (SubclassMapping)userMappings.Structure.CreateMappingNode();
                userMappings.Structure.ApplyCustomisations();
                mapping.SubclassType = SubclassType.JoinedSubclass;
                return mapping;
            });
        }

        protected static ModelTester<ComponentPart<T>, ComponentMapping> Component<T>()
        {
            var structure = new ComponentStructure(ComponentType.Component, ReflectionHelper.GetMember<ExampleClass>(x => x.Parent), typeof(ExampleClass));
            return new ModelTester<ComponentPart<T>, ComponentMapping>(
                () => new ComponentPart<T>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<DynamicComponentPart<T>, ComponentMapping> DynamicComponent<T>()
        {
            var structure = new ComponentStructure(ComponentType.DynamicComponent, ReflectionHelper.GetMember<ExampleClass>(x => x.Dictionary), typeof(ExampleClass));
            return new ModelTester<DynamicComponentPart<T>, ComponentMapping>(
                () => new DynamicComponentPart<T>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<VersionPart, VersionMapping> Version()
        {
            var structure = new MemberStructure<VersionMapping>(ReflectionHelper.GetMember<VersionTarget>(x => x.VersionNumber));
            return new ModelTester<VersionPart, VersionMapping>(
                () => new VersionPart(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<CachePart, CacheMapping> Cache()
        {
            var structure = new FreeStructure<CacheMapping>();
            return new ModelTester<CachePart, CacheMapping>(
                () => new CachePart(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<IdentityPart<int>, IdMapping> Id()
        {
            var structure = new MemberStructure<IdMapping>(ReflectionHelper.GetMember<IdentityTarget>(x => x.IntId));
            return new ModelTester<IdentityPart<int>, IdMapping>(
                () => new IdentityPart<int>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<CompositeIdentityPart<T>, CompositeIdMapping> CompositeId<T>()
        {
            var structure = new FreeStructure<CompositeIdMapping>();
            return new ModelTester<CompositeIdentityPart<T>, CompositeIdMapping>(
                () => new CompositeIdentityPart<T>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping> OneToOne()
        {
            var structure = new MemberStructure<OneToOneMapping>(ReflectionHelper.GetMember<PropertyTarget>(x => x.Reference));
            return new ModelTester<OneToOnePart<PropertyReferenceTarget>, OneToOneMapping>(
                () => new OneToOnePart<PropertyReferenceTarget>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<PropertyPart, PropertyMapping> Property()
        {
            var structure = new MemberStructure<PropertyMapping>(ReflectionHelper.GetMember<PropertyTarget>(x => x.Name));
            return new ModelTester<PropertyPart, PropertyMapping>(
                () => new PropertyPart(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<PropertyPart, PropertyMapping> Property<T>(Expression<Func<T, object>> property)
        {
            var structure = new MemberStructure<PropertyMapping>(property.ToMember());
            return new ModelTester<PropertyPart, PropertyMapping>(
                () => new PropertyPart(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<OneToManyPart<T>, CollectionMapping> OneToMany<T>(Expression<Func<OneToManyTarget, IEnumerable<T>>> property)
        {
            var structure = new TypeAndMemberStructure<CollectionMapping>(typeof(OneToManyTarget), property.ToMember());
            var key = new TypeStructure<KeyMapping>(typeof(OneToManyTarget));
            var relationship = new TypeStructure<OneToManyMapping>(typeof(T));
            return new ModelTester<OneToManyPart<T>, CollectionMapping>(
                () => new OneToManyPart<T>(typeof(T), structure, key, relationship),
                () => create_mapping(structure));
        }

        protected static ModelTester<ManyToManyPart<T>, CollectionMapping> ManyToMany<T>(Expression<Func<ManyToManyTarget, IList<T>>> property)
        {
            var structure = new TypeAndMemberStructure<CollectionMapping>(typeof(ManyToManyTarget), property.ToMember());
            var key = new TypeStructure<KeyMapping>(typeof(OneToManyTarget));
            var relationship = new TypeStructure<ManyToManyMapping>(typeof(T));
            return new ModelTester<ManyToManyPart<T>, CollectionMapping>(
                () => new ManyToManyPart<T>(typeof(T), structure, key, relationship),
                () => create_mapping(structure));
        }

        protected static ModelTester<ManyToManyPart<IDictionary>, CollectionMapping> ManyToMany(Expression<Func<ManyToManyTarget, IDictionary>> property)
        {
            var structure = new TypeAndMemberStructure<CollectionMapping>(typeof(ManyToManyTarget), property.ToMember());
            var key = new TypeStructure<KeyMapping>(typeof(OneToManyTarget));
            var relationship = new TypeStructure<ManyToManyMapping>(typeof(ManyToManyTarget));
            return new ModelTester<ManyToManyPart<IDictionary>, CollectionMapping>(
                () => new ManyToManyPart<IDictionary>(typeof(ManyToManyTarget), structure, key, relationship),
                () => create_mapping(structure));
        }

        protected static ModelTester<ManyToManyPart<IDictionary<TIndex, TValue>>, CollectionMapping> ManyToMany<TIndex, TValue>(Expression<Func<ManyToManyTarget, IDictionary<TIndex, TValue>>> property)
        {
            var structure = new TypeAndMemberStructure<CollectionMapping>(typeof(ManyToManyTarget), property.ToMember());
            var key = new TypeStructure<KeyMapping>(typeof(OneToManyTarget));
            var relationship = new TypeStructure<ManyToManyMapping>(typeof(ManyToManyTarget));
            return new ModelTester<ManyToManyPart<IDictionary<TIndex, TValue>>, CollectionMapping>(
               () => new ManyToManyPart<IDictionary<TIndex, TValue>>(typeof(ManyToManyTarget), structure, key, relationship),
               () => create_mapping(structure));
        }

        protected static ModelTester<ManyToOnePart<PropertyReferenceTarget>, ManyToOneMapping> ManyToOne()
        {
            var structure = new MemberStructure<ManyToOneMapping>(ReflectionHelper.GetMember<PropertyTarget>(x => x.Reference));
            return new ModelTester<ManyToOnePart<PropertyReferenceTarget>, ManyToOneMapping>(
                () => new ManyToOnePart<PropertyReferenceTarget>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<AnyPart<T>, AnyMapping> Any<T>()
        {
            var structure = new MemberStructure<AnyMapping>(ReflectionHelper.GetMember<MappedObject>(x => x.Parent));
            return new ModelTester<AnyPart<T>, AnyMapping>(
                () => new AnyPart<T>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<JoinPart<T>, JoinMapping> Join<T>()
        {
            var structure = new FreeStructure<JoinMapping>();
            return new ModelTester<JoinPart<T>, JoinMapping>(
                () => new JoinPart<T>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<HibernateMappingPart, HibernateMapping> HibernateMapping()
        {
            var structure = new FreeStructure<HibernateMapping>();
            var map = new HibernateMappingPart(structure);
            return new ModelTester<HibernateMappingPart, HibernateMapping>(
                () => map,
                () => ((IHibernateMappingProvider)map).GetHibernateMapping());
        }

        protected static ModelTester<CompositeElementPart<T>, CompositeElementMapping> CompositeElement<T>()
        {
            var structure = new TypeStructure<CompositeElementMapping>(typeof(T));
            return new ModelTester<CompositeElementPart<T>, CompositeElementMapping>(
                () => new CompositeElementPart<T>(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<StoredProcedurePart, StoredProcedureMapping> StoredProcedure()
        {
            var structure = new FreeStructure<StoredProcedureMapping>();
            return new ModelTester<StoredProcedurePart, StoredProcedureMapping>(
                () => new StoredProcedurePart(structure),
                () => create_mapping(structure));
        }

        protected static ModelTester<NaturalIdPart<T>, NaturalIdMapping> NaturalId<T>()
        {
            var structure = new FreeStructure<NaturalIdMapping>();
            return new ModelTester<NaturalIdPart<T>, NaturalIdMapping>(
                () => new NaturalIdPart<T>(structure),
                () => create_mapping(structure));
        }

        static IMapping create_mapping(IMappingStructure structure)
        {
            var mapping = structure.CreateMappingNode();
            
            structure.ApplyCustomisations();

            return mapping;
        }
    }
}