using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Conventions.AcceptanceCriteria
{
    public class SetCriterion : IAcceptanceCriterion
    {
        private readonly bool inverse;

        public SetCriterion(bool inverse)
        {
            this.inverse = inverse;
        }

        public bool IsSatisfiedBy<T>(Expression<Func<T, object>> expression, T inspector) where T : IInspector
        {
            var member = expression.ToMember();
            var attr = ParseEnum(member); // TODO: harden
            var result = inspector.IsSet(attr);

            return inverse ? !result : result;
        }

        Attr ParseEnum(Member member)
        {
            if (member.Name == "TableName")
                return Attr.Table;

            return (Attr)Enum.Parse(typeof(Attr), member.Name);
        }
    }
}