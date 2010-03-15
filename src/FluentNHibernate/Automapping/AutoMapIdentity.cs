using System;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Automapping
{
    public class AutoMapIdentity : IAutoMapper
    {
        private readonly AutoMappingExpressions expressions;

        public AutoMapIdentity(AutoMappingExpressions conventions)
        {
            this.expressions = conventions;
        }

        public bool MapsProperty(Member property)
        {
            return expressions.FindIdentity(property);
        }

        public void Map(ClassMappingBase classMap, Member property)
        {
            if (!(classMap is ClassMapping)) return;

            var idMapping = new IdMapping(property) { ContainingEntityType = classMap.Type };
            ((ClassMapping)classMap).Id = idMapping;        
        }
    }
}