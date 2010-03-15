using System;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate
{
    public interface IMappingProvider
    {
        IUserDefinedMapping GetUserDefinedMappings();
        // HACK: In place just to keep compatibility until verdict is made
        HibernateMapping GetHibernateMapping();
        //IEnumerable<string> GetIgnoredProperties();
    }

    public interface IUserDefinedMapping
    {
        IMappingStructure Structure { get; }
        Type Type { get; }
        IMapping CreateEmptyModel();
        void ApplyCustomisations();
    }

    public class FluentMapUserDefinedMappings : IUserDefinedMapping
    {
        public FluentMapUserDefinedMappings(Type entityType, IMappingStructure structure)
        {
            Structure = structure;
            Type = entityType;
        }

        public IMappingStructure Structure { get; private set; }
        public Type Type { get; private set; }
        
        public IMapping CreateEmptyModel()
        {
            return Structure.CreateMappingNode();
        }

        public void ApplyCustomisations()
        {
            Structure.ApplyCustomisations();
        }

        public object CreateModelShape()
        {
            return Structure;
        }
    }
}