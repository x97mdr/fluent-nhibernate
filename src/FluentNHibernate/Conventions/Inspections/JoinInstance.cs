using System;
using System.Diagnostics;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel;

namespace FluentNHibernate.Conventions.Inspections
{
    public class JoinInstance : JoinInspector, IJoinInstance
    {
        private readonly JoinMapping mapping;
        private bool nextBool = true;

        public JoinInstance(JoinMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IJoinInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public new IFetchInstance Fetch
        {
            get
            {
                return new FetchInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Fetch))
                        mapping.Fetch = value;
                });
            }
        }

        public new void Inverse()
        {
            if (!mapping.HasUserDefinedValue(Attr.Inverse))
                mapping.Inverse = nextBool;
            nextBool = true;
        }

        public new IKeyInstance Key
        {
            get { return new KeyInstance(mapping.Key); }
        }

        public new void Optional()
        {
            if (!mapping.HasUserDefinedValue(Attr.Optional))
                mapping.Optional = nextBool;
            nextBool = true;
        }

        public new void Schema(string schema)
        {
            if (!mapping.HasUserDefinedValue(Attr.Schema))
                mapping.Schema = schema;
        }

        public void Table(string table)
        {
            if (!mapping.HasUserDefinedValue(Attr.Table))
                mapping.TableName = table;
        }

        public new void Catalog(string catalog)
        {
            if (!mapping.HasUserDefinedValue(Attr.Catalog))
                mapping.Catalog = catalog;
        }

        public new void Subselect(string subselect)
        {
            if (!mapping.HasUserDefinedValue(Attr.Subselect))
                mapping.Subselect = subselect;
        }
    }
}