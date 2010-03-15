using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class ManyToOnePart<TOther>
    {
        readonly IMappingStructure<ManyToOneMapping> structure;
        readonly AccessStrategyBuilder<ManyToOnePart<TOther>> access;
        readonly FetchTypeExpression<ManyToOnePart<TOther>> fetch;
        readonly NotFoundExpression<ManyToOnePart<TOther>> notFound;
        readonly CascadeExpression<ManyToOnePart<TOther>> cascade;
        bool nextBool = true;

        public ManyToOnePart(IMappingStructure<ManyToOneMapping> structure)
        {
            this.structure = structure;

            access = new AccessStrategyBuilder<ManyToOnePart<TOther>>(this, value => structure.SetValue(Attr.Access, value));
            fetch = new FetchTypeExpression<ManyToOnePart<TOther>>(this, value => structure.SetValue(Attr.Fetch, value));
            cascade = new CascadeExpression<ManyToOnePart<TOther>>(this, value => structure.SetValue(Attr.Cascade, value));
            notFound = new NotFoundExpression<ManyToOnePart<TOther>>(this, value => structure.SetValue(Attr.NotFound, value));
        }

        private void AddColumn(string column)
        {
            var columnStructure = new ColumnStructure(structure);
            var part = new ColumnPart(columnStructure);
            part.Name(column);
            structure.AddChild(columnStructure);
        }

        public FetchTypeExpression<ManyToOnePart<TOther>> Fetch
		{
			get { return fetch; }
		}

        public NotFoundExpression<ManyToOnePart<TOther>> NotFound
        {
            get { return notFound; }
        }

        public ManyToOnePart<TOther> Unique()
        {
            structure.SetValue(Attr.Unique, nextBool);
            nextBool = true;
            return this;
        }

        /// <summary>
        /// Specifies the name of a multi-column unique constraint.
        /// </summary>
        /// <param name="keyName">Name of constraint</param>
        public ManyToOnePart<TOther> UniqueKey(string keyName)
        {
            structure.SetValue(Attr.UniqueKey, keyName);
            return this;
        }

        public ManyToOnePart<TOther> Index(string indexName)
        {
            structure.SetValue(Attr.Index, indexName);
            return this;
        }

        public ManyToOnePart<TOther> Class<T>()
        {
	        return Class(typeof(T));
        }

        public ManyToOnePart<TOther> Class(Type type)
        {
            structure.SetValue(Attr.Class, new TypeReference(type));
            return this;
        }

        public ManyToOnePart<TOther> ReadOnly()
        {
            structure.SetValue(Attr.Insert, !nextBool);
            structure.SetValue(Attr.Update, !nextBool);
            nextBool = true;
            return this;
        }

        public ManyToOnePart<TOther> LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }
		
		public ManyToOnePart<TOther> ForeignKey(string foreignKeyName)
		{
		    structure.SetValue(Attr.ForeignKey, foreignKeyName);
			return this;
		}

        public ManyToOnePart<TOther> Insert()
        {
            structure.SetValue(Attr.Insert, nextBool);
            nextBool = true;
            return this;
        }

        public ManyToOnePart<TOther> Update()
        {
            structure.SetValue(Attr.Update, nextBool);
            nextBool = true;
            return this;
        }

        public CascadeExpression<ManyToOnePart<TOther>> Cascade
		{
			get { return cascade; }
		}

        public ManyToOnePart<TOther> Columns(params string[] columns)
        {
            foreach (var column in columns)
                AddColumn(column);

            return this;
        }

        public ManyToOnePart<TOther> Columns(params Expression<Func<TOther, object>>[] columns)
        {
            return Columns(columns.Select(x => x.ToMember().Name).ToArray());
        }

        public ManyToOnePart<TOther> Column(string name)
        {
            structure.RemoveChildrenMatching(x => x is IMappingStructure<ColumnMapping>);
            AddColumn(name);

            return this;
        }

        public ManyToOnePart<TOther> PropertyRef(Expression<Func<TOther, object>> expression)
        {
            var member = expression.ToMember();

            return PropertyRef(member.Name);
        }

        public ManyToOnePart<TOther> PropertyRef(string property)
        {
            structure.SetValue(Attr.PropertyRef, property);
            return this;
        }

        public ManyToOnePart<TOther> Nullable()
        {
            structure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }

        public AccessStrategyBuilder<ManyToOnePart<TOther>> Access
        {
            get { return access; }
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ManyToOnePart<TOther> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }
    }
}
