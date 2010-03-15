using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    [TestFixture]
    public class KeyManyToOnePartTests
    {
        KeyManyToOnePart part;
        IMappingStructure<KeyManyToOneMapping> structure;

        [SetUp]
        public void SetUp()
        {
            structure = new FreeStructure<KeyManyToOneMapping>();
            part = new KeyManyToOnePart(structure);
        }

        [Test]
        public void ShouldSetForeignKey()
        {
            part.ForeignKey("fk1");
            structure.ShouldHaveValue(Attr.ForeignKey, "fk1");
        }

        [Test]
        public void ShouldSetAccessStrategy()
        {
            part.Access.Field();
            structure.ShouldHaveValue(Attr.Access, "field");
        }

        [Test]
        public void ShouldSetLazy()
        {
            part.Lazy();
            structure.ShouldHaveValue(Attr.Lazy, true);
        }

        [Test]
        public void ShouldSetNotLazy()
        {
            part.Not.Lazy();
            structure.ShouldHaveValue(Attr.Lazy, false);
        }

        [Test]
        public void ShouldSetName()
        {
            part.Name("keypart1");
            structure.ShouldHaveValue(Attr.Name, "keypart1");
        }

        [Test]
        public void ShouldSetNotFound()
        {
            part.NotFound.Ignore();
            structure.ShouldHaveValue(Attr.NotFound, "ignore");
        }
    }
}