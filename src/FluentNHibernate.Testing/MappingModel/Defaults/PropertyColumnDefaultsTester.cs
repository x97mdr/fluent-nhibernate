using System;
using System.Linq;
using System.Linq.Expressions;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Testing.DomainModel.Mapping;
using FluentNHibernate.Utils.Reflection;
using NUnit.Framework;

namespace FluentNHibernate.Testing.MappingModel.Defaults
{
    [TestFixture]
    public class PropertyColumnDefaultsTester
    {
        IMappingStructure<PropertyMapping> structure;
        PropertyPart part;

        [SetUp]
        public void CreatePart()
        {
            structure = new MemberStructure<PropertyMapping>(Prop(x => x.Name));
            part = new PropertyPart(structure);
        }

        [Test]
        public void ShouldHaveNoDefaultsIfUserSpecifiedColumn()
        {
            part.Column("explicit");

            var columns = structure.Children
                .Where(x => x is IMappingStructure<ColumnMapping>)
                .Select(x => (IMappingStructure<ColumnMapping>)x);

            columns.ShouldHaveCount(1);
            columns.Single().ShouldHaveValue(Attr.Name, "explicit");
        }

        private Member Prop(Expression<Func<PropertyTarget, object>> propertyAccessor)
        {
            return ReflectionHelper.GetMember(propertyAccessor);
        }
    }
}
