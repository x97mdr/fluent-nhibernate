using System;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// The fluent-interface part for a external component reference. These are
    /// components which have their bulk/body declared external to a class mapping
    /// and are reusable.
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    public class ReferenceComponentPart<T> : IReferenceComponentMappingProvider
    {
        readonly IMappingStructure<ReferenceComponentMapping> structure;
        private string columnPrefix;

        public ReferenceComponentPart(IMappingStructure<ReferenceComponentMapping> structure)
        {
            this.structure = structure;
        }

        /// <summary>
        /// Sets the prefix for any columns defined within the component. To refer to the property
        /// that exposes this component use {property}.
        /// </summary>
        /// <example>
        /// // Entity using Address component
        /// public class Person
        /// {
        ///   public Address PostalAddress { get; set; }
        /// }
        /// 
        /// ColumnPrefix("{property}_") will result in any columns of Person.Address being prefixed with "PostalAddress_".
        /// </example>
        /// <param name="prefix">Prefix for column names</param>
        public ReferenceComponentPart<T> ColumnPrefix(string prefix)
        {
            columnPrefix = prefix;
            return this;
        }

        Type IReferenceComponentMappingProvider.Type
        {
            get { return typeof(T); }
        }
    }
}