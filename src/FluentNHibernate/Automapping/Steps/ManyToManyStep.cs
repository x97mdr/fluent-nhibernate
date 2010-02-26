using System;
using System.Linq;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Automapping.Steps
{
    public class ManyToManyStep : IAutomappingStep
    {
        private readonly IAutomappingDiscoveryRules rules;

        public ManyToManyStep(IAutomappingDiscoveryRules rules)
        {
            this.rules = rules;
        }

        public bool IsMappable(Member property)
        {
            var type = property.PropertyType;
            if (type.Namespace != "Iesi.Collections.Generic" &&
                type.Namespace != "System.Collections.Generic")
                return false;

            var hasInverse = GetInverseProperty(property) != null;
            return hasInverse;
        }

        private static Member GetInverseProperty(Member property)
        {
            Type type = property.PropertyType;
            var inverseSide = type.GetGenericTypeDefinition()
                .MakeGenericType(property.DeclaringType);

            var argument = type.GetGenericArguments()[0];
            return argument.GetProperties()
                .Where(x => x.PropertyType == inverseSide)
                .Select(x => x.ToMember())
                .FirstOrDefault();
        }

        private ICollectionMapping GetCollection(Member property)
        {
            if (property.PropertyType.FullName.Contains("ISet"))
                return new SetMapping();

            return new BagMapping();
        }

        private void ConfigureModel(MappingMetaData metaData, ICollectionMapping mapping, MemberBucket classMap, Type parentSide)
        {
            // TODO: Make the child type safer
            mapping.SetDefaultValue(x => x.Name, metaData.Member.Name);
            mapping.Relationship = CreateManyToMany(metaData.Member, metaData.Member.PropertyType.GetGenericArguments()[0], metaData.EntityType);
            mapping.ContainingEntityType = metaData.EntityType;
            mapping.ChildType = metaData.Member.PropertyType.GetGenericArguments()[0];
            mapping.Member = metaData.Member;

            SetKey(metaData, classMap, mapping);

            if (parentSide != metaData.Member.DeclaringType)
                mapping.Inverse = true;
        }

        private ICollectionRelationshipMapping CreateManyToMany(Member property, Type child, Type parent)
        {
            var mapping = new ManyToManyMapping
            {
                Class = new TypeReference(property.PropertyType.GetGenericArguments()[0]),
                ContainingEntityType = parent
            };

            mapping.AddDefaultColumn(new ColumnMapping { Name = child.Name + "_id" });

            return mapping;
        }

        private void SetKey(MappingMetaData metaData, MemberBucket container, ICollectionMapping mapping)
        {
            var columnName = metaData.Member.DeclaringType.Name + "_id";

            //if (container is ComponentMapping)
            //    columnName = rules.ComponentColumnPrefixRule(((ComponentMapping)container).Member) + columnName;

            var key = new KeyMapping();

            key.ContainingEntityType = metaData.EntityType;
            key.AddDefaultColumn(new ColumnMapping { Name = columnName });

            mapping.SetDefaultValue(x => x.Key, key);
        }

        public IAutomappingResult Map(MappingMetaData metaData)
        {
            var inverseProperty = GetInverseProperty(metaData.Member);
            var parentSide = rules.FindParentSideForManyToManyRule(metaData.Member.DeclaringType, inverseProperty.DeclaringType);
            var mapping = GetCollection(metaData.Member);

            var members = new MemberBucket();

            ConfigureModel(metaData, mapping, members, parentSide);

            members.AddCollection(mapping);

            return new AutomappingResult(members);
        }
    }
}