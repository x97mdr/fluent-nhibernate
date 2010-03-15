using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterfaceTests
{
    [TestFixture]
    public class ColumnPartTests
    {
        ColumnPart columnPart;
        ColumnStructure structure;

        [SetUp]
        public void SetUp()
        {
            structure = new ColumnStructure(new FreeStructure<ClassMapping>());
            columnPart = new ColumnPart(structure);
        }

        [Test]
        public void ShouldSetName()
        {
            columnPart.Name("col1");
            structure.ShouldHaveValue(Attr.Name, "col1");
        }

        [Test]
        public void ShouldSetLength()
        {
            columnPart.Length(50);
            structure.ShouldHaveValue(Attr.Length, 50);
        }

        [Test]
        public void ShouldSetAsNullable()
        {
            columnPart.Nullable();
            structure.ShouldHaveValue(Attr.NotNull, false);
        }

        [Test]
        public void ShouldSetAsNotNullable()
        {
            columnPart.Not.Nullable();
            structure.ShouldHaveValue(Attr.NotNull, true);
        }

        [Test]
        public void ShouldSetUnique()
        {
            columnPart.Unique();
            structure.ShouldHaveValue(Attr.Unique, true);
        }

        [Test]
        public void ShouldSetNotUnique()
        {
            columnPart.Not.Unique();
            structure.ShouldHaveValue(Attr.Unique, false);
        }

        [Test]
        public void ShouldSetUniqueKey()
        {
            columnPart.UniqueKey("key1");
            structure.ShouldHaveValue(Attr.UniqueKey, "key1");
        }

        [Test]
        public void ShouldSetSqlType()
        {
            columnPart.SqlType("ntext");
            structure.ShouldHaveValue(Attr.SqlType, "ntext");
        }

        [Test]
        public void ShouldSetIndex()
        {
            columnPart.Index("index1");
            structure.ShouldHaveValue(Attr.Index, "index1");
        }
        
    }
}
