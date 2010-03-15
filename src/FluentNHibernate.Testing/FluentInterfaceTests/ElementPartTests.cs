using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;
using NUnit.Framework;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    [TestFixture]
    public class ElementPartTests
    {
        IMappingStructure<ElementMapping> structure;
        ElementPart part;

        [SetUp]
        public void CreatePart()
        {
            structure = new FreeStructure<ElementMapping>();
            part = new ElementPart(structure);
        }

        [Test]
        public void CanSetLength()
        {
            part.Length(50);
            structure.ShouldHaveValue(Attr.Length, 50);
        }

        [Test]
        public void CanSetFormula()
        {
            part.Formula("formula");
            
            structure.ShouldHaveValue(Attr.Formula, "formula");
        }
    }
}
