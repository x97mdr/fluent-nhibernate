using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoMapper
    {
        private List<AutoMapType> mappingTypes;
        readonly IAutomappingStrategy strategy;
        private readonly IEnumerable<InlineOverride> inlineOverrides;
        private readonly EntityAutomapper entityAutomapper;

        public AutoMapper(IConventionFinder conventionFinder, IEnumerable<InlineOverride> inlineOverrides, IAutomappingStrategy strategy)
            : this(strategy, conventionFinder, inlineOverrides, new EntityAutomapper(conventionFinder, strategy))
        {}

        protected AutoMapper(IAutomappingStrategy strategy, IConventionFinder conventionFinder, IEnumerable<InlineOverride> inlineOverrides, EntityAutomapper entityAutomapper)
        {
            this.strategy = strategy;
            this.inlineOverrides = inlineOverrides;
            this.entityAutomapper = entityAutomapper;
            this.entityAutomapper.AutoMapper = this; // TODO: Remove this dependency
        }

        private void ApplyOverrides(Type classType, IList<string> mappedProperties, ClassMappingBase mapping)
        {
            var autoMapType = typeof(AutoMapping<>).MakeGenericType(classType);
            var autoMap = Activator.CreateInstance(autoMapType, mappedProperties);

            inlineOverrides
                .Where(x => x.CanOverride(classType))
                .Each(x => x.Apply(autoMap));

            ((IAutoClasslike)autoMap).AlterModel(mapping);
        }

        public ClassMappingBase MergeMap(Type classType, ClassMappingBase mapping, IList<string> mappedProperties)
        {
            // map class first, then subclasses - this way subclasses can inspect the class model
            // to see which properties have already been mapped
            ApplyOverrides(classType, mappedProperties, mapping);

            var result = entityAutomapper.Map(classType, mappedProperties);

            result.ApplyTo(mapping);

            if (mappingTypes != null)
                MapInheritanceTree(classType, mapping, mappedProperties);

            return mapping;
        }

        private void MapInheritanceTree(Type classType, ClassMappingBase mapping, IList<string> mappedProperties)
        {
            var rules = strategy.GetRules();
            var discriminatorSet = false;
            var isDiscriminated = rules.FindDiscriminatedEntityRule(classType);

            foreach (var inheritedClass in mappingTypes.Where(q =>
                q.Type.BaseType == classType &&
                    !rules.FindConcreteBaseTypeRule(q.Type.BaseType)))
            {
                if (isDiscriminated && !discriminatorSet && mapping is ClassMapping)
                {
                    var discriminatorColumn = rules.DiscriminatorColumnRule(classType);
                    var discriminator = new DiscriminatorMapping
                    {
                        ContainingEntityType = classType,
                        Type = new TypeReference(typeof(string))
                    };
                    discriminator.AddDefaultColumn(new ColumnMapping { Name = discriminatorColumn });

                    ((ClassMapping)mapping).Discriminator = discriminator;
                    discriminatorSet = true;
                }

                SubclassMapping subclassMapping;
                var subclassStrategy = rules.SubclassStrategyRule(classType);

                if (subclassStrategy == SubclassStrategy.JoinedSubclass)
                {
                    subclassMapping = new SubclassMapping(SubclassType.JoinedSubclass);
                    subclassMapping.Key = new KeyMapping();
                    subclassMapping.Key.AddDefaultColumn(new ColumnMapping { Name = mapping.Type.Name + "_id" });
                }
                else
                    subclassMapping = new SubclassMapping(SubclassType.Subclass);

				// track separate set of properties for each sub-tree within inheritance hierarchy
            	var subClassProperties = new List<string>(mappedProperties);
				MapSubclass(subClassProperties, subclassMapping, inheritedClass);

                mapping.AddSubclass(subclassMapping);

				MergeMap(inheritedClass.Type, (ClassMappingBase)subclassMapping, subClassProperties);
            }
        }

        private void MapSubclass(IList<string> mappedProperties, SubclassMapping subclass, AutoMapType inheritedClass)
        {
            subclass.Name = inheritedClass.Type.AssemblyQualifiedName;
            subclass.Type = inheritedClass.Type;
            
	    ApplyOverrides(inheritedClass.Type, mappedProperties, subclass);
            //entityAutomapper.Map((ClassMappingBase)subclass, inheritedClass.Type, mappedProperties);
            inheritedClass.IsMapped = true;
        }

        public ClassMapping Map(Type classType, List<AutoMapType> types)
        {
            var classMap = new ClassMapping { Type = classType };

            classMap.SetDefaultValue(x => x.Name, classType.AssemblyQualifiedName);
            classMap.SetDefaultValue(x => x.TableName, GetDefaultTableName(classType));

            mappingTypes = types;
            return (ClassMapping)MergeMap(classType, classMap, new List<string>());
        }

        private string GetDefaultTableName(Type type)
        {
            var tableName = type.Name;

            if (type.IsGenericType)
            {
                // special case for generics: GenericType_GenericParameterType
                tableName = type.Name.Substring(0, type.Name.IndexOf('`'));

                foreach (var argument in type.GetGenericArguments())
                {
                    tableName += "_";
                    tableName += argument.Name;
                }
            }

            return "`" + tableName + "`";
        }

        /// <summary>
        /// Flags a type as already mapped, stop it from being auto-mapped.
        /// </summary>
        public void FlagAsMapped(Type type)
        {
            mappingTypes
                .Where(x => x.Type == type)
                .Each(x => x.IsMapped = true);
        }
    }
}
