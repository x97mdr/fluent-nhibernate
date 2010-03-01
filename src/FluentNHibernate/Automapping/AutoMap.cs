using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using FluentNHibernate.Sources;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutomappingBuilder
    {
        bool nextBool = true;
        bool validationEnabled;
        bool mergeMappings;
        readonly IConventionFinder conventions = new DefaultConventionFinder();
        readonly List<ITypeSource> typeSources = new List<ITypeSource>();
        readonly List<IMappingSource> mappingSources = new List<IMappingSource>();
        IAutomappingStrategy strategy = new DefaultAutomappingStrategy();

        public AutomappingBuilder(ITypeSource source)
        {
            typeSources.Add(source);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public AutomappingBuilder Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public AutomappingBuilder ValidationEnabled()
        {
            validationEnabled = nextBool;
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Alter some of the configuration options that control how the automapper works
        /// </summary>
        public AutomappingBuilder Setup(Action<DiscoveryRules> expressionsAction)
        {
            //expressionsAction(model.Rules as DiscoveryRules);
            return this;
        }

        /// <summary>
        /// Ignore a base type. This removes it from any mapped inheritance hierarchies, good for non-abstract layer
        /// supertypes.
        /// </summary>
        /// <typeparam name="T">Type to ignore</typeparam>
        public AutomappingBuilder IgnoreBase<T>()
        {
            return IgnoreBase(typeof(T));
        }

        /// <summary>
        /// Ignore a base type. This removes it from any mapped inheritance hierarchies, good for non-abstract layer
        /// supertypes.
        /// </summary>
        /// <param name="baseType">Type to ignore</param>
        public AutomappingBuilder IgnoreBase(Type baseType)
        {
            //ignoredTypes.Add(baseType);
            return this;
        }

        /// <summary>
        /// Explicitly includes a type to be used as part of a mapped inheritance hierarchy.
        /// </summary>
        /// <remarks>
        /// Abstract classes are probably what you'll be using this method with. Fluent NHibernate considers abstract
        /// classes to be layer supertypes, so doesn't automatically map them as part of an inheritance hierarchy. You
        /// can use this method to override that behavior for a specific type; otherwise you should consider using the
        /// <see cref="AutoMappingExpressions.AbstractClassIsLayerSupertype"/> setting.
        /// </remarks>
        /// <typeparam name="T">Type to include</typeparam>
        public AutomappingBuilder IncludeBase<T>()
        {
            return IncludeBase(typeof(T));
        }

        /// <summary>
        /// Explicitly includes a type to be used as part of a mapped inheritance hierarchy.
        /// </summary>
        /// <remarks>
        /// Abstract classes are probably what you'll be using this method with. Fluent NHibernate considers abstract
        /// classes to be layer supertypes, so doesn't automatically map them as part of an inheritance hierarchy. You
        /// can use this method to override that behavior for a specific type; otherwise you should consider using the
        /// <see cref="AutoMappingExpressions.AbstractClassIsLayerSupertype"/> setting.
        /// </remarks>
        /// <param name="baseType">Type to include</param>
        public AutomappingBuilder IncludeBase(Type baseType)
        {
            //includedTypes.Add(baseType);
            return this;
        }

        public AutomappingBuilder Where(Func<Type, bool> where)
        {
            //this.shouldIncludeType = where;
            return this;
        }

        /// <summary>
        /// Specify alterations to be used with this AutoPersisteceModel
        /// </summary>
        /// <param name="alterationDelegate">Lambda to declare alterations</param>
        /// <returns>AutoPersistenceModel</returns>
        public AutomappingBuilder Alterations(Action<AutoMappingAlterationCollection> alterationDelegate)
        {
            //alterationDelegate(alterations);
            return this;
        }

        /// <summary>
        /// Use auto mapping overrides defined in the assembly of T.
        /// </summary>
        /// <typeparam name="T">Type to get assembly from</typeparam>
        /// <returns>AutoPersistenceModel</returns>
        public AutomappingBuilder UseOverridesFromAssemblyOf<T>()
        {
            //alterations.Add(new AutoMappingOverrideAlteration(typeof(T).Assembly));
            return this;
        }

        /// <summary>
        /// Override the mapping of a specific entity.
        /// </summary>
        /// <remarks>This may affect subclasses, depending on the alterations you do.</remarks>
        /// <typeparam name="T">Entity who's mapping to override</typeparam>
        /// <param name="populateMap">Lambda performing alterations</param>
        public AutomappingBuilder Override<T>(Action<AutoMapping<T>> populateMap)
        {
            //inlineOverrides.Add(new InlineOverride(typeof(T), x =>
            //{
            //    if (x is AutoMapping<T>)
            //        populateMap((AutoMapping<T>)x);
            //}));

            return this;
        }

        /// <summary>
        /// Override all mappings.
        /// </summary>
        /// <remarks>Currently only supports ignoring properties on all entities.</remarks>
        /// <param name="alteration">Lambda performing alterations</param>
        public AutomappingBuilder OverrideAll(Action<IPropertyIgnorer> alteration)
        {
            //inlineOverrides.Add(new InlineOverride(typeof(object), x =>
            //{
            //    if (x is IPropertyIgnorer)
            //        alteration((IPropertyIgnorer)x);
            //}));

            return this;
        }

        public AutomappingBuilder MergeMappings()
        {
            mergeMappings = nextBool;
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Alter convention discovery
        /// </summary>
        public SetupConventionFinder<AutomappingBuilder> Conventions
        {
            get { return new SetupConventionFinder<AutomappingBuilder>(this, conventions); }
        }

        public PersistenceModel CreateModel()
        {
            var model = new PersistenceModel(strategy, conventions)
            {
                ValidationEnabled = validationEnabled,
                MergeMappings = mergeMappings
            };

            typeSources.Each(model.AddMappingsSource);
            mappingSources.Each(model.AddMappingsSource);

            return model;
        }

        public AutomappingBuilder Strategy(IAutomappingStrategy automappingStrategy)
        {
            strategy = automappingStrategy;
            return this;
        }
    }
    /// <summary>
    /// Starting point for automapping your entities.
    /// </summary>
    public static class AutoMap
    {
        /// <summary>
        /// Automatically map classes in the assembly that contains <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Class in the assembly you want to map</typeparam>
        public static AutomappingBuilder AssemblyOf<T>()
        {
            return Assembly(typeof(T).Assembly);
        }

        /// <summary>
        /// Automatically map classes in the assembly that contains <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Class in the assembly you want to map</typeparam>
        /// <param name="where">Criteria for selecting a subset of the types in the assembly for mapping</param>
        public static AutomappingBuilder AssemblyOf<T>(Func<Type, bool> where)
        {
            return Assembly(typeof(T).Assembly, where);
        }

        /// <summary>
        /// Automatically map the classes in <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly containing the classes to map</param>
        public static AutomappingBuilder Assembly(Assembly assembly)
        {
            return Assembly(assembly, null);
        }

        /// <summary>
        /// Automatically map the classes in <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly containing the classes to map</param>
        /// <param name="where">Criteria for selecting a subset of the types in the assembly for mapping</param>
        public static AutomappingBuilder Assembly(Assembly assembly, Func<Type, bool> where)
        {
            return Source(new AssemblyTypeSource(assembly), where);
        }

        /// <summary>
        /// Automatically map the classes in each assembly supplied.
        /// </summary>
        /// <param name="assemblies">Assemblies containing classes to map</param>
        public static AutomappingBuilder Assemblies(params Assembly[] assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies));
        }

        /// <summary>
        /// Automatically map the classes in each assembly supplied.
        /// </summary>
        /// <param name="assemblies">Assemblies containing classes to map</param>
        public static AutomappingBuilder Assemblies(IEnumerable<Assembly> assemblies)
        {
            return Source(new CombinedAssemblyTypeSource(assemblies));
        }

        /// <summary>
        /// Automatically map the classes exposed through the supplied <see cref="ITypeSource"/>.
        /// </summary>
        /// <param name="source"><see cref="ITypeSource"/> containing classes to map</param>
        public static AutomappingBuilder Source(ITypeSource source)
        {
            return Source(source, null);
        }

        /// <summary>
        /// Automatically map the classes exposed through the supplied <see cref="ITypeSource"/>.
        /// </summary>
        /// <param name="source"><see cref="ITypeSource"/> containing classes to map</param>
        /// <param name="where">Criteria for selecting a subset of the types in the assembly for mapping</param>
        public static AutomappingBuilder Source(ITypeSource source, Func<Type, bool> where)
        {
            return new AutomappingBuilder(new RestrictedTypeSource(source, where));
        }
    }
}