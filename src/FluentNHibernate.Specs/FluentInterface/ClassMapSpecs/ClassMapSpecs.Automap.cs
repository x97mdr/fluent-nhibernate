using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Identity;
using Machine.Specifications;

namespace FluentNHibernate.Specs.FluentInterface.ClassMapSpecs
{
    public abstract class ClassMapAutomappingSpec
    {
        Establish context = () =>
            map = new ClassMap<Target>();

        protected static ClassMap<Target> map;
        protected static ClassMapping mapping;

        protected class Target
        {
            public int Id { get; set; }
            public string PropertyOne { get; set; }
            public string PropertyTwo { get; set; }
            public ChildTarget Reference { get; set; }
            public IList<ChildTarget> Children { get; set; }
        }

        protected class ChildTarget
        {
            
        }
    }

    public class when_class_map_is_told_to_automap_with_a_property_manually_mapped : ClassMapAutomappingSpec
    {
        //Because of = () =>
        //{
        //    map.Automap();
        //    map.Map(x => x.PropertyOne, "custom_column");
        //    mapping = map.As<IMappingProvider>().GetClassMapping();
        //};

        //It shouldnt_overwrite_the_manual_mapped_property = () =>
        //    mapping.Properties.First(x => x.Name == "PropertyOne").Columns.ShouldContain(x => x.Name == "custom_column");

        //It should_automap_the_unmapped_property = () =>
        //    mapping.Properties.ShouldContain(x => x.Name == "PropertyTwo");

        //It should_only_contain_property_mappings_for_the_two_properties_on_the_entity = () =>
        //    mapping.Properties.Count().ShouldEqual(2);
    }

    public class when_class_map_is_told_to_automap_with_the_id_property_manually_mapped : ClassMapAutomappingSpec
    {
        //Because of = () =>
        //{
        //    map.Automap();
        //    map.Id(x => x.Id, "col");
        //    mapping = map.As<IMappingProvider>().GetClassMapping();
        //};

        //It shouldnt_overwrite_the_manual_mapped_id = () =>
        //    mapping.Id.As<IdMapping>().Columns.ShouldContain(x => x.Name == "col");
    }

    public class when_class_map_is_told_to_automap_with_a_reference_property_manually_mapped : ClassMapAutomappingSpec
    {
        //Because of = () =>
        //{
        //    map.Automap();
        //    map.References(x => x.Reference, "col");
        //    mapping = map.As<IMappingProvider>().GetClassMapping();
        //};

        //It shouldnt_overwrite_the_manual_mapped_reference = () =>
        //    mapping.References.Single().Columns.ShouldContain(x => x.Name == "col");
    }

    public class when_class_map_is_told_to_automap_with_a_collection_property_manually_mapped : ClassMapAutomappingSpec
    {
        //Because of = () =>
        //{
        //    map.Automap();
        //    map.HasMany(x => x.Children).Access.Field();
        //    mapping = map.As<IMappingProvider>().GetClassMapping();
        //};

        //It shouldnt_overwrite_the_manual_mapped_collection = () =>
        //    mapping.Collections.Single().Access.ShouldEqual("field");
    }

    public class when_class_map_is_told_to_automap : ClassMapAutomappingSpec
    {
        //Because of = () =>
        //{
        //    map.Automap();
        //    mapping = map.As<IMappingProvider>().GetClassMapping();
        //};

        //It should_map_properties = () =>
        //    mapping.Properties.Count().ShouldEqual(2);

        //It should_map_the_id = () =>
        //    mapping.Id.ShouldNotBeNull();

        //It should_map_the_collection = () =>
        //    mapping.Collections.Count().ShouldEqual(1);

        //It should_map_the_reference = () =>
        //    mapping.References.Count().ShouldEqual(1);

        //It should_use_the_property_name_for_the_reference_name = () =>
        //    mapping.References.Single().Name.ShouldEqual("Reference");

        //It should_set_property_name_to_name_of_member = () =>
        //{
        //    mapping.Properties.ShouldContain(x => x.Name == "PropertyOne");
        //    mapping.Properties.ShouldContain(x => x.Name == "PropertyTwo");
        //};

        //It should_set_property_column_name_to_name_of_member = () =>
        //{
        //    mapping.Properties.First(x => x.Name == "PropertyOne").Columns.ShouldContain(x => x.Name == "PropertyOne");
        //    mapping.Properties.First(x => x.Name == "PropertyTwo").Columns.ShouldContain(x => x.Name == "PropertyTwo");
        //};
    }
}