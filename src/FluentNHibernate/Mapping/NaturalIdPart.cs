using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class NaturalIdPart<T>
    {
        readonly FreeStructure<NaturalIdMapping> structure;
        bool nextBool = true;

        public NaturalIdPart(FreeStructure<NaturalIdMapping> structure)
        {
            this.structure = structure;
        }

        /// <summary>
        /// Defines a property to be used for this natural-id.
        /// </summary>
        /// <param name="expression">A member access lambda expression for the property</param>
        /// <returns>The natural id part fluent interface</returns>
        public NaturalIdPart<T> Property(Expression<Func<T, object>> expression)
        {
            var member = expression.ToMember();
            return Property(expression, member.Name);
        }

        /// <summary>
        /// Defines a property to be used for this natural-id with an explicit column name.
        /// </summary>
        /// <param name="expression">A member access lambda expression for the property</param>
        /// <param name="columnName">The column name in the database to use for this natural id, or null to use the property name</param>
        /// <returns>The natural id part fluent interface</returns>
        public NaturalIdPart<T> Property(Expression<Func<T, object>> expression, string columnName)
        {
            var member = expression.ToMember();
            return Property(member, columnName);
        }

        NaturalIdPart<T> Property(Member member, string columnName)
        {
            var propertyStructure = new MemberStructure<PropertyMapping>(member);
            var part = new PropertyPart(propertyStructure);
            part.Column(columnName);

            structure.AddChild(propertyStructure);

            return this;
        }

        /// <summary>
        /// Defines a reference to be used as a many-to-one key for this natural-id with an explicit column name.
        /// </summary>
        /// <param name="expression">A member access lambda expression for the property</param>
        /// <returns>The natural ID part fluent interface</returns>
        public NaturalIdPart<T> Reference(Expression<Func<T, object>> expression)
        {
            var member = expression.ToMember();
            return Reference(expression, member.Name);
        }

        /// <summary>
        /// Defines a reference to be used as a many-to-one key for this natural-id with an explicit column name.
        /// </summary>
        /// <param name="expression">A member access lambda expression for the property</param>
        /// <param name="columnName">The column name in the database to use for this key, or null to use the property name</param>
        /// <returns>The natural id part fluent interface</returns>
        public NaturalIdPart<T> Reference(Expression<Func<T, object>> expression, string columnName)
        {
            var member = expression.ToMember();
            return Reference(member, columnName);
        }

        protected virtual NaturalIdPart<T> Reference(Member member, string columnName)
        {
            var referenceStructure = new MemberStructure<ManyToOneMapping>(member);
            var part = new ManyToOnePart<T>(referenceStructure);
            part.Column(columnName);

            structure.AddChild(referenceStructure);

            return this;
        }

        public NaturalIdPart<T> ReadOnly()
        {
            structure.SetValue(Attr.Mutable, !nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public NaturalIdPart<T> Not
        {
            get
            {
                nextBool = false;
                return this;
            }
        }
    }
}

