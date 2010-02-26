using System;
using System.Collections.Generic;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Buckets;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping.Steps
{
    public class VersionStep : IAutomappingStep
    {
        private static readonly IList<string> ValidNames = new List<string> { "version", "timestamp" };
        private static readonly IList<Type> ValidTypes = new List<Type> { typeof(int), typeof(long), typeof(TimeSpan), typeof(byte[]) };

        public bool IsMappable(Member property)
        {
            return ValidNames.Contains(property.Name.ToLowerInvariant()) && ValidTypes.Contains(property.PropertyType);
        }

        public IAutomappingResult Map(MappingMetaData metaData)
        {
            var version = new VersionMapping
            {
                Name = metaData.Member.Name,
            };

            version.SetDefaultValue("Type", GetDefaultType(metaData.Member));
            version.AddDefaultColumn(new ColumnMapping { Name = metaData.Member.Name });

            if (IsSqlTimestamp(metaData.Member))
            {
                version.Columns.Each(x =>
                {
                    x.SqlType = "timestamp";
                    x.NotNull = true;
                });
                version.UnsavedValue = null;
            }

            var members = new MemberBucket();
            members.SetVersion(version);
            
            return new AutomappingResult(members);
        }

        private bool IsSqlTimestamp(Member property)
        {
            return property.PropertyType == typeof(byte[]);
        }

        private TypeReference GetDefaultType(Member property)
        {
            if (IsSqlTimestamp(property))
                return new TypeReference("BinaryBlob");

            return new TypeReference(property.PropertyType);
        }
    }
}