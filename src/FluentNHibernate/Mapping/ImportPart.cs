using System;
using System.Xml;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class ImportPart
    {
        readonly IMappingStructure<ImportMapping> structure;

        public ImportPart(IMappingStructure<ImportMapping> structure)
        {
            this.structure = structure;
        }

        public ImportPart As(string alternativeName)
        {
            structure.SetValue(Attr.Rename, alternativeName);
            return this;
        }
    }
}