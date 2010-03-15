using FluentNHibernate.Automapping.TestFixtures;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Identity;
using NHibernate;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel.Equality
{
    [TestFixture]
    public class WhenComparingTwoIdenticalAnyMappings : MappingEqualitySpec<AnyMapping>
    {
        protected override AnyMapping CreateMapping()
        {
            var mapping = new AnyMapping
            {
                Access = "access",
                Cascade = "cascade",
                ContainingEntityType = typeof(Target),
                IdType = "id-type",
                Insert = true,
                Lazy = true,
                MetaType = new TypeReference(typeof(Target)),
                Name = "name",
                OptimisticLock = true,
                Update = true
            };

            mapping.AddIdentifierDefaultColumn(new ColumnMapping { Name = "default-id-col" });
            mapping.AddIdentifierColumn(new ColumnMapping { Name = "id-col" });
            mapping.AddMetaValue(new MetaValueMapping(null) { Value = "value" });
            mapping.AddTypeDefaultColumn(new ColumnMapping { Name = "default-type-col" });
            mapping.AddTypeColumn(new ColumnMapping { Name = "type-col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalArrayMappings : MappingEqualitySpec<CollectionMapping>
    {
        protected override CollectionMapping CreateMapping()
        {
            var mapping = new CollectionMapping
            {
                Access = "access", Cascade = "cascade", ContainingEntityType = typeof(Target),
                Lazy = true, Name = "name", OptimisticLock = "lock",
                BatchSize = 1, Cache = new CacheMapping(), Check = "check",
                ChildType = typeof(Target), CollectionType = new TypeReference(typeof(Target)), CompositeElement = new CompositeElementMapping(typeof(ExampleClass)),
                Element = new ElementMapping(), Fetch = "fetch", Generic = true,
                Index = new IndexMapping(), Inverse = true, Key = new KeyMapping(),
                Mutable = true, OrderBy = "order-by",
                OtherSide = new CollectionMapping(), Persister = new TypeReference(typeof(Target)), Relationship = new ManyToManyMapping(null),
                Schema = "schema", Subselect = "subselect", TableName = "table", Where = "where"
            };

            mapping.Filters.Add(new FilterMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalBagMappings : MappingEqualitySpec<CollectionMapping>
    {
        protected override CollectionMapping CreateMapping()
        {
            var mapping = new CollectionMapping
            {
                Access = "access", Cascade = "cascade", ContainingEntityType = typeof(Target),
                Lazy = true, Name = "name", OptimisticLock = "lock",
                BatchSize = 1, Cache = new CacheMapping(), Check = "check",
                ChildType = typeof(Target),
                CollectionType = new TypeReference(typeof(Target)),
                CompositeElement = new CompositeElementMapping(typeof(ExampleClass)),
                Element = new ElementMapping(), Fetch = "fetch", Generic = true,
                Inverse = true, Key = new KeyMapping(),
                Mutable = true, OrderBy = "order-by",
                OtherSide = new CollectionMapping(), Persister = new TypeReference(typeof(Target)), Relationship = new ManyToManyMapping(null),
                Schema = "schema", Subselect = "subselect", TableName = "table", Where = "where"
            };

            mapping.Filters.Add(new FilterMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalCacheMappings : MappingEqualitySpec<CacheMapping>
    {
        protected override CacheMapping CreateMapping()
        {
            return new CacheMapping
            {
                ContainedEntityType = typeof(Target),
                Include = "include",
                Region = "region",
                Usage = "usage"
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalClassMappings : MappingEqualitySpec<ClassMapping>
    {
        protected override ClassMapping CreateMapping()
        {
            var mapping = new ClassMapping(typeof(Target))
            {
                Abstract = true, BatchSize = 10, Cache = new CacheMapping(),
                Check = "check", Discriminator = new DiscriminatorMapping(), DiscriminatorValue = "value",
                DynamicInsert = true, DynamicUpdate = true, EntityName = "entity-name",
                Id = new IdMapping(null), Lazy = true, Mutable = true,
                Name = "name", OptimisticLock = "lock", Persister = "persister",
                Polymorphism = "poly", Proxy = "proxy", Schema = "schema",
                SchemaAction = "action", SelectBeforeUpdate = true, Subselect = "subselect",
                TableName = "table", Tuplizer = new TuplizerMapping(), Type = typeof(Target),
                Version = new VersionMapping(null), Where = "where"
            };

            mapping.AddAny(new AnyMapping());
            mapping.AddCollection(new CollectionMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddFilter(new FilterMapping());
            mapping.AddJoin(new JoinMapping());
            mapping.AddOneToOne(new OneToOneMapping(null));
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());
            mapping.AddStoredProcedure(new StoredProcedureMapping());
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalCompositeElementMappings : MappingEqualitySpec<CompositeElementMapping>
    {
        protected override CompositeElementMapping CreateMapping()
        {
            var mapping = new CompositeElementMapping(typeof(ExampleClass))
            {
                Class = new TypeReference(typeof(Target)),
                ContainingEntityType = typeof(Target),
                Parent = new ParentMapping(null),
            };

            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalCompositeIdMappings : MappingEqualitySpec<CompositeIdMapping>
    {
        protected override CompositeIdMapping CreateMapping()
        {
            var mapping = new CompositeIdMapping(null)
            {
                Class = new TypeReference(typeof(Target)),
                ContainingEntityType = typeof(Target),
                Access = "access",
                Mapped = true,
                Name = "name",
                UnsavedValue = "unsaved"
            };

            mapping.AddKeyManyToOne(new KeyManyToOneMapping(null));
            mapping.AddKeyProperty(new KeyPropertyMapping(null));

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalComponentMappings : MappingEqualitySpec<ComponentMapping>
    {
        protected override ComponentMapping CreateMapping()
        {
            var mapping = new ComponentMapping(ComponentType.Component)
            {
                Access = "access",
                Class = new TypeReference(typeof(Target)),
                ContainingEntityType = typeof(Target),
                Insert = true,
                Lazy = true,
                Member = new DummyPropertyInfo("name", typeof(Target)).ToMember(),
                Name = "name",
                OptimisticLock = true,
                Parent = new ParentMapping(null),
                Type = typeof(Target),
                Unique = true,
                Update = true
            };

            mapping.AddAny(new AnyMapping());
            mapping.AddCollection(new CollectionMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddFilter(new FilterMapping());
            mapping.AddJoin(new JoinMapping());
            mapping.AddOneToOne(new OneToOneMapping(null));
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());
            mapping.AddStoredProcedure(new StoredProcedureMapping());
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalColumnMappings : MappingEqualitySpec<ColumnMapping>
    {
        protected override ColumnMapping CreateMapping()
        {
            return new ColumnMapping
            {
                Check = "check",
                Default = "default",
                Index = "index",
                Length = 1,
                Member = new DummyPropertyInfo("prop", typeof(Target)).ToMember(),
                Name = "name",
                NotNull = true,
                Precision = 1,
                Scale = 1,
                SqlType = "sql-type",
                Unique = true,
                UniqueKey = "unique-key"
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalDiscriminatorMappings : MappingEqualitySpec<DiscriminatorMapping>
    {
        protected override DiscriminatorMapping CreateMapping()
        {
            var mapping = new DiscriminatorMapping
            {
                ContainingEntityType = typeof(Target),
                Force = true,
                Formula = "formula",
                Insert = true,
                Type = new TypeReference(typeof(Target))
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });
            
            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalDynamicComponentMappings : MappingEqualitySpec<ComponentMapping>
    {
        protected override ComponentMapping CreateMapping()
        {
            var mapping = new ComponentMapping(ComponentType.Component)
            {
                Access = "access",
                ContainingEntityType = typeof(Target),
                Insert = true,
                Member = new DummyPropertyInfo("name", typeof(Target)).ToMember(),
                Name = "name",
                OptimisticLock = true,
                Parent = new ParentMapping(null),
                Type = typeof(Target),
                Unique = true,
                Update = true
            };

            mapping.AddAny(new AnyMapping());
            mapping.AddCollection(new CollectionMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddFilter(new FilterMapping());
            mapping.AddJoin(new JoinMapping());
            mapping.AddOneToOne(new OneToOneMapping(null));
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());
            mapping.AddStoredProcedure(new StoredProcedureMapping());
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalElementMappings : MappingEqualitySpec<ElementMapping>
    {
        protected override ElementMapping CreateMapping()
        {
            var mapping = new ElementMapping
            {
                ContainingEntityType = typeof(Target),
                Formula = "formula",
                Length = 1,
                Type = new TypeReference(typeof(Target))
            };

            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalExternalComponentMappings : MappingEqualitySpec<ExternalComponentMapping>
    {
        protected override ExternalComponentMapping CreateMapping()
        {
            var mapping = new ExternalComponentMapping(typeof(Target))
            {
                Access = "access",
                ContainingEntityType = typeof(Target),
                Insert = true,
                Member = new DummyPropertyInfo("name", typeof(Target)).ToMember(),
                Name = "name",
                OptimisticLock = true,
                Parent = new ParentMapping(null),
                Type = typeof(Target),
                Unique = true,
                Update = true
            };

            mapping.AddAny(new AnyMapping());
            mapping.AddCollection(new CollectionMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddFilter(new FilterMapping());
            mapping.AddJoin(new JoinMapping());
            mapping.AddOneToOne(new OneToOneMapping(null));
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());
            mapping.AddStoredProcedure(new StoredProcedureMapping());
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalFilterDefinitionMappings : MappingEqualitySpec<FilterDefinitionMapping>
    {
        protected override FilterDefinitionMapping CreateMapping()
        {
            var mapping = new FilterDefinitionMapping
            {
                Condition = "condition",
                Name = "name"
            };

            mapping.Parameters.Add("name", NHibernateUtil.Int32);

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalFilterMappings : MappingEqualitySpec<FilterMapping>
    {
        protected override FilterMapping CreateMapping()
        {
            var mapping = new FilterMapping
            {
                Condition = "condition",
                Name = "name"
            };

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalGeneratorMappings : MappingEqualitySpec<GeneratorMapping>
    {
        protected override GeneratorMapping CreateMapping()
        {
            var mapping = new GeneratorMapping
            {
                Class = "class",
                ContainingEntityType = typeof(Target),
            };

            mapping.AddParam(new ParamMapping { Name = "left", Value = "right" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }


    [TestFixture]
    public class WhenComparingTwoIdenticalHibernateMappings : MappingEqualitySpec<HibernateMapping>
    {
        protected override HibernateMapping CreateMapping()
        {
            var mapping = new HibernateMapping
            {
                Assembly = "assembly",
                AutoImport = true,
                Catalog = "catalog",
                DefaultAccess = "access",
                DefaultCascade = "cascade",
                DefaultLazy = true,
                Namespace = "namespace",
                Schema = "schema"
            };

            mapping.AddClass(new ClassMapping(typeof(Target)));
            mapping.AddFilter(new FilterDefinitionMapping());
            mapping.AddImport(new ImportMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalIdMappings : MappingEqualitySpec<IdMapping>
    {
        protected override IdMapping CreateMapping()
        {
            var mapping = new IdMapping(new DummyPropertyInfo("prop", typeof(Target)).ToMember())
            {
                Name = "name",
                Access = "access",
                ContainingEntityType = typeof(Target),
                Generator = new GeneratorMapping(),
                Type = new TypeReference(typeof(Target)),
                UnsavedValue = "unsaved"
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalImportMappings : MappingEqualitySpec<ImportMapping>
    {
        protected override ImportMapping CreateMapping()
        {
            return new ImportMapping
            {
                Class = new TypeReference("class"),
                Rename = "rename"
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalIndexManyToManyMappings : MappingEqualitySpec<IndexManyToManyMapping>
    {
        protected override IndexManyToManyMapping CreateMapping()
        {
            var mapping = new IndexManyToManyMapping(typeof(Target))
            {
                ContainingEntityType = typeof(Target),
                ForeignKey = "fk"
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalIndexMappings : MappingEqualitySpec<IndexMapping>
    {
        protected override IndexMapping CreateMapping()
        {
            var mapping = new IndexMapping
            {
                ContainingEntityType = typeof(Target),
                Type = new TypeReference(typeof(Target)),
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalJoinedSubclassMappings : MappingEqualitySpec<SubclassMapping>
    {
        protected override SubclassMapping CreateMapping()
        {
            var mapping = new SubclassMapping(SubclassType.JoinedSubclass)
            {
                Abstract = true,
                BatchSize = 10,
                Check = "check",
                DynamicInsert = true,
                DynamicUpdate = true,
                EntityName = "entity-name",
                Lazy = true,
                Name = "name",
                Persister = new TypeReference(typeof(Target)),
                Proxy = "proxy",
                Schema = "schema",
                SelectBeforeUpdate = true,
                Subselect = "subselect",
                TableName = "table",
                Type = typeof(Target),
                Extends = "extends",
                Key = new KeyMapping()
            };

            mapping.AddAny(new AnyMapping());
            mapping.AddCollection(new CollectionMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddFilter(new FilterMapping());
            mapping.AddJoin(new JoinMapping());
            mapping.AddOneToOne(new OneToOneMapping(null));
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());
            mapping.AddStoredProcedure(new StoredProcedureMapping());
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalJoinMappings : MappingEqualitySpec<JoinMapping>
    {
        protected override JoinMapping CreateMapping()
        {
            var mapping = new JoinMapping
            {
                Catalog = "catalog",
                ContainingEntityType = typeof(Target),
                Fetch = "fetch",
                Inverse = true,
                Key = new KeyMapping(),
                Optional = true,
                Schema = "schema",
                Subselect = "subselect",
                TableName = "table"
            };

            mapping.AddAny(new AnyMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalKeyManyToOneMappings : MappingEqualitySpec<KeyManyToOneMapping>
    {
        protected override KeyManyToOneMapping CreateMapping()
        {
            var mapping = new KeyManyToOneMapping(null)
            {
                ContainingEntityType = typeof(Target),
                ForeignKey = "fk",
                Access = "access",
                Class = new TypeReference(typeof(Target)),
                Lazy = true,
                Name = "name",
                NotFound = "not-found"
            };

            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalKeyPropertyMappings : MappingEqualitySpec<KeyPropertyMapping>
    {
        protected override KeyPropertyMapping CreateMapping()
        {
            var mapping = new KeyPropertyMapping(null)
            {
                ContainingEntityType = typeof(Target),
                Access = "access",
                Name = "name",
                Type = new TypeReference(typeof(Target))
            };

            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalKeyMappings : MappingEqualitySpec<KeyMapping>
    {
        protected override KeyMapping CreateMapping()
        {
            var mapping = new KeyMapping
            {
                ContainingEntityType = typeof(Target),
                ForeignKey = "fk",
                NotNull = true,
                OnDelete = "del",
                PropertyRef = "prop",
                Unique = true,
                Update = true
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalListMappings : MappingEqualitySpec<CollectionMapping>
    {
        protected override CollectionMapping CreateMapping()
        {
            var mapping = new CollectionMapping
            {
                Access = "access", Cascade = "cascade", ContainingEntityType = typeof(Target),
                Lazy = true, Name = "name", OptimisticLock = "lock",
                BatchSize = 1, Cache = new CacheMapping(), Check = "check",
                ChildType = typeof(Target),
                CollectionType = new TypeReference(typeof(Target)),
                CompositeElement = new CompositeElementMapping(typeof(ExampleClass)),
                Element = new ElementMapping(), Fetch = "fetch", Generic = true,
                Index = new IndexMapping(), Inverse = true, Key = new KeyMapping(),
                Mutable = true, OrderBy = "order-by",
                OtherSide = new CollectionMapping(), Persister = new TypeReference(typeof(Target)), Relationship = new ManyToManyMapping(null),
                Schema = "schema", Subselect = "subselect", TableName = "table", Where = "where"
            };

            mapping.Filters.Add(new FilterMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalManyToOneMappings : MappingEqualitySpec<ManyToOneMapping>
    {
        protected override ManyToOneMapping CreateMapping()
        {
            var mapping = new ManyToOneMapping
            {
                Access = "access",
                Cascade = "cascade",
                Class = new TypeReference(typeof(Target)),
                ContainingEntityType = typeof(Target),
                Fetch = "fetch",
                ForeignKey = "fk",
                Insert = true,
                Lazy = true,
                PropertyRef = "prop",
                Update = true,
                Name = "name",
                NotFound = "not found"
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalManyToManyMappings : MappingEqualitySpec<ManyToManyMapping>
    {
        protected override ManyToManyMapping CreateMapping()
        {
            var mapping = new ManyToManyMapping(typeof(Target))
            {
                ContainingEntityType = typeof(Target),
                Class = new TypeReference(typeof(Target)),
                ForeignKey = "fk",
                ChildType = typeof(Target),
                Fetch = "fetch",
                Lazy = true,
                NotFound = "not-found",
                ParentType = typeof(Target),
                Where = "where"
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalMapMappings : MappingEqualitySpec<CollectionMapping>
    {
        protected override CollectionMapping CreateMapping()
        {
            var mapping = new CollectionMapping
            {
                Access = "access", Cascade = "cascade", ContainingEntityType = typeof(Target),
                Lazy = true, Name = "name", OptimisticLock = "lock",
                BatchSize = 1, Cache = new CacheMapping(), Check = "check",
                ChildType = typeof(Target),
                CollectionType = new TypeReference(typeof(Target)),
                CompositeElement = new CompositeElementMapping(typeof(ExampleClass)),
                Element = new ElementMapping(), Fetch = "fetch", Generic = true,
                Index = new IndexMapping(), Inverse = true, Key = new KeyMapping(),
                Mutable = true, OrderBy = "order-by",
                OtherSide = new CollectionMapping(), Persister = new TypeReference(typeof(Target)), Relationship = new ManyToManyMapping(null),
                Schema = "schema", Subselect = "subselect", TableName = "table", Where = "where",
                Sort = "sort"
            };

            mapping.Filters.Add(new FilterMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalMetaValueMappings : MappingEqualitySpec<MetaValueMapping>
    {
        protected override MetaValueMapping CreateMapping()
        {
            return new MetaValueMapping(typeof(Target))
            {
                ContainingEntityType = typeof(Target),
                Value = "value"
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalOneToManyMappings : MappingEqualitySpec<OneToManyMapping>
    {
        protected override OneToManyMapping CreateMapping()
        {
            return new OneToManyMapping(typeof(Target))
            {
                ContainingEntityType = typeof(Target),
                ChildType = typeof(Target),
                NotFound = "not-found"
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalOneToOneMappings : MappingEqualitySpec<OneToOneMapping>
    {
        protected override OneToOneMapping CreateMapping()
        {
            return new OneToOneMapping(null)
            {
                Access = "access",
                Cascade = "cascade",
                Class = new TypeReference(typeof(Target)),
                ContainingEntityType = typeof(Target),
                Fetch = "fetch",
                ForeignKey = "fk",
                Lazy = true,
                PropertyRef = "prop",
                Name = "name",
                Constrained = true
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalParentMappings : MappingEqualitySpec<ParentMapping>
    {
        protected override ParentMapping CreateMapping()
        {
            return new ParentMapping(null)
            {
                ContainingEntityType = typeof(Target),
                Name = "name"
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalPropertyMappings : MappingEqualitySpec<PropertyMapping>
    {
        protected override PropertyMapping CreateMapping()
        {
            var mapping = new PropertyMapping
            {
                Access = "access",
                ContainingEntityType = typeof(Target),
                Formula = "formula",
                Generated = "generated",
                Insert = true,
                Lazy = true,
                Name = "name",
                OptimisticLock = true,
                Type = new TypeReference(typeof(Target)),
                Update = true
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalReferenceComponentMappings : MappingEqualitySpec<ReferenceComponentMapping>
    {
        protected override ReferenceComponentMapping CreateMapping()
        {
            var mapping = new ReferenceComponentMapping(ComponentType.Component, new DummyPropertyInfo("name", typeof(Target)).ToMember(), typeof(Target), typeof(Target), null);
            mapping.AssociateExternalMapping(new ExternalComponentMapping(typeof(Target)));
            mapping.Access = "access";
            mapping.ContainingEntityType = typeof(Target);
            mapping.Insert = true;
            mapping.Name = "name";
            mapping.OptimisticLock = true;
            mapping.Parent = new ParentMapping(null);
            mapping.Unique = true;
            mapping.Update = true;
            mapping.AddAny(new AnyMapping());
            mapping.AddCollection(new CollectionMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddOneToOne(new OneToOneMapping(null));
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalSetMappings : MappingEqualitySpec<CollectionMapping>
    {
        protected override CollectionMapping CreateMapping()
        {
            var mapping = new CollectionMapping
            {
                Access = "access", Cascade = "cascade", ContainingEntityType = typeof(Target),
                Lazy = true, Name = "name", OptimisticLock = "lock",
                BatchSize = 1, Cache = new CacheMapping(), Check = "check",
                ChildType = typeof(Target),
                CollectionType = new TypeReference(typeof(Target)),
                CompositeElement = new CompositeElementMapping(typeof(ExampleClass)),
                Element = new ElementMapping(), Fetch = "fetch", Generic = true,
                Inverse = true, Key = new KeyMapping(),
                Mutable = true, OrderBy = "order-by",
                OtherSide = new CollectionMapping(), Persister = new TypeReference(typeof(Target)), Relationship = new ManyToManyMapping(null),
                Schema = "schema", Subselect = "subselect", TableName = "table", Where = "where",
                Sort = "sort"
            };

            mapping.Filters.Add(new FilterMapping());

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }


    [TestFixture]
    public class WhenComparingTwoIdenticalStoredProcedureMappings : MappingEqualitySpec<StoredProcedureMapping>
    {
        protected override StoredProcedureMapping CreateMapping()
        {
            return new StoredProcedureMapping
            {
                Check = "check",
                Name = "name",
                Query = "query",
                SPType = "type",
                Type = typeof(Target)
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalSubclassMappings : MappingEqualitySpec<SubclassMapping>
    {
        protected override SubclassMapping CreateMapping()
        {
            var mapping = new SubclassMapping(SubclassType.JoinedSubclass)
            {
                Abstract = true,
                DynamicInsert = true,
                DynamicUpdate = true,
                EntityName = "entity-name",
                Lazy = true,
                Name = "name",
                Proxy = "proxy",
                SelectBeforeUpdate = true,
                Type = typeof(Target),
                Extends = "extends",
                DiscriminatorValue = "value"
            };

            mapping.AddAny(new AnyMapping());
            mapping.AddCollection(new CollectionMapping());
            mapping.AddComponent(CreateComponent());
            mapping.AddFilter(new FilterMapping());
            mapping.AddJoin(new JoinMapping());
            mapping.AddOneToOne(new OneToOneMapping(null));
            mapping.AddProperty(CreateProperty());
            mapping.AddReference(new ManyToOneMapping());
            mapping.AddStoredProcedure(new StoredProcedureMapping());
            mapping.AddSubclass(new SubclassMapping(SubclassType.JoinedSubclass));

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalTuplizerMappings : MappingEqualitySpec<TuplizerMapping>
    {
        protected override TuplizerMapping CreateMapping()
        {
            return new TuplizerMapping
            {
                Mode = TuplizerMode.DynamicMap,
                Type = new TypeReference(typeof(Target))
            };
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class WhenComparingTwoIdenticalVersionMappings : MappingEqualitySpec<VersionMapping>
    {
        protected override VersionMapping CreateMapping()
        {
            var mapping = new VersionMapping(null)
            {
                Access = "access",
                ContainingEntityType = typeof(Target),
                Generated = "generated",
                Name = "name",
                Type = new TypeReference(typeof(Target)),
                UnsavedValue = "value"
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = "default" });
            mapping.AddColumn(new ColumnMapping { Name = "col" });

            return mapping;
        }

        [Test]
        public void ShouldBeEqual()
        {
            areEqual.ShouldBeTrue();
        }
    }

    public abstract class MappingEqualitySpec<T> : Specification
    {
        protected abstract T CreateMapping();

        protected ComponentMapping CreateComponent()
        {
            return new ComponentMapping(ComponentType.Component);
        }

        protected ComponentMapping CreateDynamicComponent()
        {
            return new ComponentMapping(ComponentType.Component);
        }

        protected PropertyMapping CreateProperty()
        {
            return new PropertyMapping();
        }

        public override void establish_context()
        {
            firstMapping = CreateMapping();
            secondMapping = CreateMapping();
        }

        public override void because()
        {
            areEqual = firstMapping.Equals(secondMapping);
        }

        T firstMapping;
        T secondMapping;
        protected bool areEqual;

        protected class Target {}
    }
}