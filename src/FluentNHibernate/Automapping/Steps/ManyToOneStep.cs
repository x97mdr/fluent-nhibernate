using System;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Automapping.Steps
{
    public class ManyToOneStep : IAutomappingStep
    {
        private readonly Func<Member, bool> findPropertyconvention = p => (
            p.PropertyType.Namespace != "System" && // ignore clr types (won't be entities)
                p.PropertyType.Namespace != "System.Collections.Generic" &&
                    p.PropertyType.Namespace != "Iesi.Collections.Generic" &&
                        !p.PropertyType.IsEnum);

        public bool IsMappable(Member property)
        {
            if (property.CanWrite)
                return findPropertyconvention(property);

            return false;
        }

        public IAutomappingResult Map(MappingMetaData metaData)
        {
            var manyToOne = CreateMapping(metaData.Member);
            var members = new MemberBucket();

            members.AddReference(manyToOne);

            return new AutomappingResult(members);
        }

        private ManyToOneMapping CreateMapping(Member property)
        {
            var mapping = new ManyToOneMapping { Member = property };

            mapping.SetDefaultValue(x => x.Name, property.Name);
            mapping.SetDefaultValue(x => x.Class, new TypeReference(property.PropertyType));
            mapping.AddDefaultColumn(new ColumnMapping { Name = property.Name + "_id" });

            return mapping;
        }
    }
}