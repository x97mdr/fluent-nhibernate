using System;
using System.Collections.Generic;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class SimpleTypeCollectionStep : IAutomappingStep
    {
        readonly IAutomappingStrategy strategy;
        readonly AutoKeyMapper keys;
        readonly AutoCollectionCreator collections;

        public SimpleTypeCollectionStep(IAutomappingStrategy strategy)
        {
            this.strategy = strategy;
            keys = new AutoKeyMapper(strategy.GetRules());
            collections = new AutoCollectionCreator();
        }

        public bool IsMappable(Member property)
        {
            if (!property.PropertyType.IsGenericType)
                return false;

            var childType = property.PropertyType.GetGenericArguments()[0];

            return property.CanWrite &&
                property.PropertyType.ClosesInterface(typeof(IEnumerable<>)) &&
                    (childType.IsPrimitive || childType.In(typeof(string), typeof(DateTime)));
        }

        public IMappingResult Map(MappingMetaData metaData)
        {
            if (metaData.Member.DeclaringType != metaData.EntityType)
                return new EmptyResult();

            var mapping = collections.CreateCollectionMapping(metaData.Member.PropertyType);

            mapping.ContainingEntityType = metaData.EntityType;
            mapping.Member = metaData.Member;
            mapping.SetDefaultValue(x => x.Name, metaData.Member.Name);

            keys.SetKey(metaData, mapping);
            SetElement(metaData, mapping);

            var members = new MemberBucket();
            
            members.AddCollection(mapping);

            return new AutomappingResult(metaData.EntityType, strategy, members);
        }

        private void SetElement(MappingMetaData metaData, ICollectionMapping mapping)
        {
            var element = new ElementMapping
            {
                ContainingEntityType = metaData.EntityType,
                Type = new TypeReference(metaData.Member.PropertyType.GetGenericArguments()[0])
            };
            var rules = strategy.GetRules();

            element.AddDefaultColumn(new ColumnMapping { Name = rules.SimpleTypeCollectionValueColumnRule(metaData.Member) });
            mapping.SetDefaultValue(x => x.Element, element);
        }
    }
}