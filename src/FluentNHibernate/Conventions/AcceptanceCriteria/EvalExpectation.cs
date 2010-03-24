using System;
using System.Linq.Expressions;
using FluentNHibernate.Conventions.Inspections;

namespace FluentNHibernate.Conventions.AcceptanceCriteria
{
    public class EvalExpectation<TInspector> : IExpectation
        where TInspector : IInspector
    {
        private readonly Func<TInspector, bool> func;

        public EvalExpectation(Func<TInspector, bool> func)
        {
            this.func = func;
        }

        public bool Matches(TInspector inspector)
        {
            return func(inspector);
        }

        bool IExpectation.Matches(IInspector inspector)
        {
            if (inspector is TInspector)
                return Matches((TInspector)inspector);

            return false;
        }
    }
}