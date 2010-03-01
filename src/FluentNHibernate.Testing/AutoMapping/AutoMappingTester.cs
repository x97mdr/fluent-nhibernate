using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Mapping;
using FluentNHibernate.Testing.DomainModel;
using FluentNHibernate.Testing.DomainModel.Mapping;
using NHibernate.Cfg;

namespace FluentNHibernate.Testing.Automapping
{
    public class AutoMappingTester<T> : MappingTester<T>
    {
        public AutoMappingTester(AutomappingBuilder mapper)
            : base(mapper.CreateModel())
        {
            ForMapping((ClassMap<T>)null);
        }
    }
}