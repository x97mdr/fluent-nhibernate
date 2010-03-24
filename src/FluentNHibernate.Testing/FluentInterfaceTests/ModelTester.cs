using System;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    public class ModelTester<TFluentClass, TModel>
    {
        private readonly Func<TFluentClass> instantiatePart;
        private readonly Func<IMapping> getModel;
        private TFluentClass fluentClass;

        public ModelTester(Func<TFluentClass> instantiatePart, Func<IMapping> getModel)
        {
            this.instantiatePart = instantiatePart;
            this.getModel = getModel;
        }

        public ModelTester<TFluentClass, TModel> Mapping(Action<TFluentClass> action)
        {
            fluentClass = instantiatePart();
            action(fluentClass);
            return this;
        }

        public void ModelShouldMatch(Action<TModel> action)
        {
            var model = getModel();
            action((TModel)model);
        }
    }
}