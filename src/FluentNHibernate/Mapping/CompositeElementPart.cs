using System;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    /// <summary>
    /// Component-element for component HasMany's.
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    public class CompositeElementPart<T>
    {
        readonly IMappingStructure<CompositeElementMapping> structure;

        public CompositeElementPart(IMappingStructure<CompositeElementMapping> structure)
        {
            this.structure = structure;
        }

        public PropertyPart Map(Expression<Func<T, object>> expression)
        {
            return Map(expression, null);
        }

        public PropertyPart Map(Expression<Func<T, object>> expression, string columnName)
        {
            return Map(expression.ToMember(), columnName);
        }

        protected PropertyPart Map(Member property, string columnName)
        {
            var propertyStructure = new MemberStructure<PropertyMapping>(property);
            var propertyMap = new PropertyPart(propertyStructure);

            if (!string.IsNullOrEmpty(columnName))
                propertyMap.Column(columnName);

            structure.AddChild(propertyStructure);

            return propertyMap;
        }

        public ManyToOnePart<TOther> References<TOther>(Expression<Func<T, TOther>> expression)
        {
            return References(expression, null);
        }

        public ManyToOnePart<TOther> References<TOther>(Expression<Func<T, TOther>> expression, string columnName)
        {
            return References<TOther>(expression.ToMember(), columnName);
        }

        protected virtual ManyToOnePart<TOther> References<TOther>(Member property, string columnName)
        {
            var referencesStructure = new MemberStructure<ManyToOneMapping>(property);
            var part = new ManyToOnePart<TOther>(referencesStructure);

            if (columnName != null)
                part.Column(columnName);

            structure.AddChild(referencesStructure);

            return part;
        }

        /// <summary>
        /// Maps a property of the component class as a reference back to the containing entity
        /// </summary>
        /// <param name="expression">Parent reference property</param>
        /// <returns>Component being mapped</returns>
        public CompositeElementPart<T> ParentReference(Expression<Func<T, object>> expression)
        {
            var parentStructure = new MemberStructure<ParentMapping>(expression.ToMember());
            structure.AddChild(parentStructure);
            return this;
        }
    }
}