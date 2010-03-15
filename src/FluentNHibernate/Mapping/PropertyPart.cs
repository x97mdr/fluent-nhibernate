using System;
using System.Diagnostics;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using NHibernate.UserTypes;

namespace FluentNHibernate.Mapping
{
    public class PropertyPart
    {
        readonly IMappingStructure<PropertyMapping> structure;
        private readonly AccessStrategyBuilder<PropertyPart> access;
        private readonly PropertyGeneratedBuilder generated;
        private readonly ColumnMappingCollection<PropertyPart> columns;
        
        private bool nextBool = true;

        public PropertyPart(IMappingStructure<PropertyMapping> structure)
        {
            this.structure = structure;

            columns = new ColumnMappingCollection<PropertyPart>(this, structure);
            access = new AccessStrategyBuilder<PropertyPart>(this, value => structure.SetValue(Attr.Access, value));
            generated = new PropertyGeneratedBuilder(this, value => structure.SetValue(Attr.Generated, value));
        }

        public PropertyGeneratedBuilder Generated
        {
            get { return generated; }
        }

        public PropertyPart Column(string columnName)
        {
            Columns.Clear();
            Columns.Add(columnName);
            return this;
        }

        public ColumnMappingCollection<PropertyPart> Columns
        {
            get { return columns; }
        }

        /// <summary>
        /// Set the access and naming strategy for this property.
        /// </summary>
        public AccessStrategyBuilder<PropertyPart> Access
        {
            get { return access; }
        }

        public PropertyPart Insert()
        {
            structure.SetValue(Attr.Insert, nextBool);
            nextBool = true;

            return this;
        }

        public PropertyPart Update()
        {
            structure.SetValue(Attr.Update, nextBool);
            nextBool = true;

            return this;
        }

        public PropertyPart Length(int length)
        {
            structure.SetValue(Attr.Length, length);
            return this;
        }

        public PropertyPart Nullable()
        {
            structure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public PropertyPart ReadOnly()
        {
            structure.SetValue(Attr.Insert, !nextBool);
            structure.SetValue(Attr.Update, !nextBool);
            nextBool = true;
            return this;
        }

        public PropertyPart Formula(string formula) 
        {
            structure.SetValue(Attr.Formula, formula);
            return this;
        }

        public PropertyPart LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public PropertyPart Index(string index)
        {
            structure.SetValue(Attr.Index, index);
            return this;
        }

        /// <summary>
        /// Specifies that a custom type (an implementation of <see cref="IUserType"/>) should be used for this property for mapping it to/from one or more database columns whose format or type doesn't match this .NET property.
        /// </summary>
        /// <typeparam name="TCustomtype">A type which implements <see cref="IUserType"/>.</typeparam>
        /// <returns>This property mapping to continue the method chain</returns>
        public PropertyPart CustomType<TCustomtype>()
        {
            return CustomType(typeof(TCustomtype));
        }

        /// <summary>
        /// Specifies that a custom type (an implementation of <see cref="IUserType"/>) should be used for this property for mapping it to/from one or more database columns whose format or type doesn't match this .NET property.
        /// </summary>
        /// <param name="type">A type which implements <see cref="IUserType"/>.</param>
        /// <returns>This property mapping to continue the method chain</returns>
        public PropertyPart CustomType(Type type)
        {
            if (typeof(ICompositeUserType).IsAssignableFrom(type))
                AddColumnsFromCompositeUserType(type);

            return CustomType(TypeMapping.GetTypeString(type));
        }

        /// <summary>
        /// Specifies that a custom type (an implementation of <see cref="IUserType"/>) should be used for this property for mapping it to/from one or more database columns whose format or type doesn't match this .NET property.
        /// </summary>
        /// <param name="type">A type which implements <see cref="IUserType"/>.</param>
        /// <returns>This property mapping to continue the method chain</returns>
        public PropertyPart CustomType(string type)
        {
            structure.SetValue(Attr.Type, new TypeReference(type));

            return this;
        }

        private void AddColumnsFromCompositeUserType(Type compositeUserType)
        {
            var inst = (ICompositeUserType)Activator.CreateInstance(compositeUserType);

            foreach (var name in inst.PropertyNames)
            {
                Columns.Add(name);
            }
        }

        public PropertyPart CustomSqlType(string sqlType)
        {
            structure.SetValue(Attr.SqlType, sqlType);
            return this;
        }

        public PropertyPart Unique()
        {
            structure.SetValue(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        public PropertyPart Precision(int precision)
        {
            structure.SetValue(Attr.Precision, precision);
            return this;
        }

        public PropertyPart Scale(int scale)
        {
            structure.SetValue(Attr.Scale, scale);
            return this;
        }

        public PropertyPart Default(string value)
        {
            structure.SetValue(Attr.Default, value);
            return this;
        }

        /// <summary>
        /// Specifies the name of a multi-column unique constraint.
        /// </summary>
        /// <param name="keyName">Name of constraint</param>
        public PropertyPart UniqueKey(string keyName)
        {
            structure.SetValue(Attr.UniqueKey, keyName);
            return this;
        }

        public PropertyPart OptimisticLock()
        {
            structure.SetValue(Attr.OptimisticLock, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PropertyPart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public PropertyPart Check(string constraint)
        {
            structure.SetValue(Attr.Check, constraint);
            return this;
        }
    }
}
