using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class CompositeIdentityPart<T>
	{
        readonly IMappingStructure<CompositeIdMapping> structure;
        readonly AccessStrategyBuilder<CompositeIdentityPart<T>> access;
        bool nextBool = true;

        public CompositeIdentityPart(IMappingStructure<CompositeIdMapping> structure)
        {
            this.structure = structure;
            access = new AccessStrategyBuilder<CompositeIdentityPart<T>>(this, value => structure.SetValue(Attr.Access, value));
        }

        /// <summary>
		/// Defines a property to be used as a key for this composite-id.
		/// </summary>
		/// <param name="expression">A member access lambda expression for the property</param>
		/// <returns>The composite identity part fluent interface</returns>
		public CompositeIdentityPart<T> KeyProperty(Expression<Func<T, object>> expression)
		{
            var member = expression.ToMember();

            return KeyProperty(member, member.Name, null);
		}

		/// <summary>
		/// Defines a property to be used as a key for this composite-id with an explicit column name.
		/// </summary>
		/// <param name="expression">A member access lambda expression for the property</param>
		/// <param name="columnName">The column name in the database to use for this key, or null to use the property name</param>
		/// <returns>The composite identity part fluent interface</returns>
		public CompositeIdentityPart<T> KeyProperty(Expression<Func<T, object>> expression, string columnName)
		{
            var member = expression.ToMember();

		    return KeyProperty(member, columnName, null);
		}

        /// <summary>
        /// Defines a property to be used as a key for this composite-id with an explicit column name.
        /// </summary>
        /// <param name="expression">A member access lambda expression for the property</param>        
        /// <param name="keyPropertyAction">Additional settings for the key property</param>
        /// <returns>The composite identity part fluent interface</returns>
        public CompositeIdentityPart<T> KeyProperty(Expression<Func<T, object>> expression, Action<KeyPropertyPart> keyPropertyAction)
        {
            var member = expression.ToMember();
            return KeyProperty(member, string.Empty, keyPropertyAction);
        }

        protected virtual CompositeIdentityPart<T> KeyProperty(Member property, string columnName, Action<KeyPropertyPart> customMapping)
        {
            var keyStructure = new MemberStructure<KeyPropertyMapping>(property);
            var part = new KeyPropertyPart(keyStructure);

            if (customMapping != null)
                customMapping(part);

            if (!string.IsNullOrEmpty(columnName))
                part.ColumnName(columnName);

            structure.AddChild(keyStructure);

            return this;
        }

		/// <summary>
		/// Defines a reference to be used as a many-to-one key for this composite-id with an explicit column name.
		/// </summary>
		/// <param name="expression">A member access lambda expression for the property</param>
		/// <returns>The composite identity part fluent interface</returns>
		public CompositeIdentityPart<T> KeyReference(Expression<Func<T, object>> expression)
		{
            var member = expression.ToMember();

		    return KeyReference(member, member.Name, null);
		}

		/// <summary>
		/// Defines a reference to be used as a many-to-one key for this composite-id with an explicit column name.
		/// </summary>
		/// <param name="expression">A member access lambda expression for the property</param>
		/// <param name="columnName">The column name in the database to use for this key, or null to use the property name</param>
		/// <returns>The composite identity part fluent interface</returns>
		public CompositeIdentityPart<T> KeyReference(Expression<Func<T, object>> expression, string columnName)
		{
            var member = expression.ToMember();

            return KeyReference(member, columnName, null);
		}


        /// <summary>
        /// Defines a reference to be used as a many-to-one key for this composite-id with an explicit column name.
        /// </summary>
        /// <param name="expression">A member access lambda expression for the property</param>
        /// <param name="columnName">The column name in the database to use for this key, or null to use the property name</param>
        /// <param name="customMapping">A lambda expression specifying additional settings for the key reference</param>
        /// <returns>The composite identity part fluent interface</returns>
        public CompositeIdentityPart<T> KeyReference(Expression<Func<T, object>> expression, string columnName, Action<KeyManyToOnePart> customMapping)
        {
            var member = expression.ToMember();

            return KeyReference(member, columnName, customMapping);
        }

        protected virtual CompositeIdentityPart<T> KeyReference(Member property, string columnName, Action<KeyManyToOnePart> customMapping)
        {
            var keyStructure = new MemberStructure<KeyManyToOneMapping>(property);
            var part = new KeyManyToOnePart(keyStructure);

            if (!string.IsNullOrEmpty(columnName))
                part.Column(columnName);

            if (customMapping != null)
                customMapping(part);

            structure.AddChild(keyStructure);      

            return this;
        }

		/// <summary>
		/// Set the access and naming strategy for this identity.
		/// </summary>
		public AccessStrategyBuilder<CompositeIdentityPart<T>> Access
		{
			get { return access; }
		}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public CompositeIdentityPart<T> Not
        {
            get
            {
                nextBool = false;
                return this;
            }
        }

        public CompositeIdentityPart<T> Mapped()
        {
            structure.SetValue(Attr.Mapped, nextBool);
            nextBool = true;
            return this;
        }

        public CompositeIdentityPart<T> UnsavedValue(string value)
        {
            structure.SetValue(Attr.UnsavedValue, value);
            return this;
        }
	}
}
