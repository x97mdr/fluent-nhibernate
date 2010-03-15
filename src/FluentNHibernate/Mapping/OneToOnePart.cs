using System;
using System.Diagnostics;
using System.Linq.Expressions;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class OneToOnePart<TOther>
    {
        readonly IMappingStructure<OneToOneMapping> structure;
        readonly AccessStrategyBuilder<OneToOnePart<TOther>> access;
        readonly FetchTypeExpression<OneToOnePart<TOther>> fetch;
        readonly CascadeExpression<OneToOnePart<TOther>> cascade;
        bool nextBool = true;

        public OneToOnePart(IMappingStructure<OneToOneMapping> structure)
        {
            this.structure = structure;

            access = new AccessStrategyBuilder<OneToOnePart<TOther>>(this, value => structure.SetValue(Attr.Access, value));
            fetch = new FetchTypeExpression<OneToOnePart<TOther>>(this, value => structure.SetValue(Attr.Fetch, value));
            cascade = new CascadeExpression<OneToOnePart<TOther>>(this, value => structure.SetValue(Attr.Cascade, value));
        }

        public OneToOnePart<TOther> Class<T>()
        {
            return Class(typeof(T));
        }

        public OneToOnePart<TOther> Class(Type type)
        {
            structure.SetValue(Attr.Class, new TypeReference(type));
            return this;
        }

        public FetchTypeExpression<OneToOnePart<TOther>> Fetch
        {
            get { return fetch; }
        }

        public OneToOnePart<TOther> ForeignKey(string foreignKeyName)
        {
            structure.SetValue(Attr.ForeignKey, foreignKeyName);
            return this;
        }

        public OneToOnePart<TOther> PropertyRef(Expression<Func<TOther, object>> expression)
        {
            var member = expression.ToMember();

            return PropertyRef(member.Name);
        }

        public OneToOnePart<TOther> PropertyRef(string propertyName)
        {
            structure.SetValue(Attr.PropertyRef, propertyName);

            return this;
        }

        public OneToOnePart<TOther> Constrained()
        {
            structure.SetValue(Attr.Constrained, nextBool);
            nextBool = true;

            return this;
        }

        public CascadeExpression<OneToOnePart<TOther>> Cascade
        {
            get { return cascade; }
        }

        public AccessStrategyBuilder<OneToOnePart<TOther>> Access
        {
            get { return access; }
        }

        public OneToOnePart<TOther> LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public OneToOnePart<TOther> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }
    }
}
