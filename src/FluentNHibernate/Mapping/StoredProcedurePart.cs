using System;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class StoredProcedurePart
    {
        readonly IMappingStructure<StoredProcedureMapping> structure;

        public StoredProcedurePart(IMappingStructure<StoredProcedureMapping> structure)
        {
            this.structure = structure;
        }

        public CheckTypeExpression<StoredProcedurePart> Check
        {
            get { return new CheckTypeExpression<StoredProcedurePart>(this, value => structure.SetValue(Attr.Check, value)); }
        }

        public StoredProcedurePart StoredProcedureType(string type)
        {
            structure.SetValue(Attr.SPType, type);
            return this;
        }

        public StoredProcedurePart Query(string query)
        {
            structure.SetValue(Attr.Query, query);
            return this;
        }
    }
}
