using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;

namespace FluentNHibernate.Conventions.Instances
{
    public class KeyManyToOneInstance : KeyManyToOneInspector, IKeyManyToOneInstance
    {
        private readonly KeyManyToOneMapping mapping;
        private bool nextBool = true;

        public KeyManyToOneInstance(KeyManyToOneMapping mapping)
            : base(mapping)
        {
            this.mapping = mapping;
        }

        public new IAccessInstance Access
        {
            get
            {
                return new AccessInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.Access))
                        mapping.Access = value;
                });
            }
        }

        public new void ForeignKey(string name)
        {
            if (!mapping.HasUserDefinedValue(Attr.ForeignKey))
                mapping.ForeignKey = name;
        }

        public void Lazy()
        {
            if (!mapping.HasUserDefinedValue(Attr.Lazy))
                mapping.Lazy = nextBool;
            nextBool = true;
        }

        public new INotFoundInstance NotFound
        {
            get
            {
                return new NotFoundInstance(value =>
                {
                    if (!mapping.HasUserDefinedValue(Attr.NotFound))
                        mapping.NotFound = value;
                });
            }
        }

        public IKeyManyToOneInstance Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }
    }
}
