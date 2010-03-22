using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class ParamStructureBuilder : ParamBuilder
    {
        readonly IMappingStructure structure;

        public ParamStructureBuilder(IMappingStructure structure)
        {
            this.structure = structure;
        }

        public override ParamBuilder AddParam(string name, string value)
        {
            var paramStructure = new FreeStructure<ParamMapping>();

            paramStructure.SetValue(Attr.Name, name);
            paramStructure.SetValue(Attr.Value, value);

            structure.AddChild(paramStructure);

            return this;
        }
    }

    public class ParamMappingBuilder : ParamBuilder
    {
        readonly GeneratorMapping mapping;

        public ParamMappingBuilder(GeneratorMapping mapping)
        {
            this.mapping = mapping;
        }

        public override ParamBuilder AddParam(string name, string value)
        {
            var param = new ParamMapping();

            param.Name = name;
            param.Value = value;

            mapping.AddParam(param);

            return this;
        }
    }

    public abstract class ParamBuilder
    {
        public abstract ParamBuilder AddParam(string name, string value);
    }
}