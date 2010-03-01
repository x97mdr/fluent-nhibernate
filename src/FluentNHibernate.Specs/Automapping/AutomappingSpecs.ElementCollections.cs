using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Results;
using FluentNHibernate.Automapping.Rules;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Specs.Automapping.Fixtures;
using Machine.Specifications;

namespace FluentNHibernate.Specs.Automapping
{
    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_strings : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfStrings);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_ints : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfInts);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_doubles : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfDoubles);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_shorts : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfShorts);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_longs : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfLongs);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_floats : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfFloats);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_bools : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfBools);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_DateTimes : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.IsMappable(FakeMembers.IListOfDateTimes);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_told_to_map_a_list_of_simple_types_with_a_custom_column_defined : AutoMapOneToManySpec
    {
        Establish context = () =>
            rules.SimpleTypeCollectionValueColumn("custom_column");

        Because of = () =>
            result = step.Map(new MappingMetaData(FakeMembers.Type, FakeMembers.IListOfStrings));

        It should_create_use_the_element_column_name_defined_in_the_expressions = () =>
            result.Collections.Single().Element.Columns.Single().Name.ShouldEqual("custom_column");
    }

    public class when_the_automapper_is_told_to_map_a_list_of_simple_types : AutoMapOneToManySpec
    {
        Because of = () =>
            result = step.Map(new MappingMetaData(FakeMembers.Type, FakeMembers.IListOfStrings));

        It should_create_a_collection = () =>
            result.Collections.Count().ShouldEqual(1);

        It should_create_a_collection_that_s_a_bag = () =>
            result.Collections.Single().ShouldBeOfType<BagMapping>();

        It should_create_an_element_for_the_collection = () =>
            result.Collections.Single().Element.ShouldNotBeNull();

        It should_use_the_default_element_column = () =>
            result.Collections.Single().Element.Columns.Single().Name.ShouldEqual("Value");

        It should_set_the_element_type_to_the_first_generic_argument_of_the_collection_type = () =>
            result.Collections.Single().Element.Type.ShouldEqual(new TypeReference(typeof(string)));

        It should_create_a_key = () =>
            result.Collections.Single().Key.ShouldNotBeNull();

        It should_set_the_key_s_containing_entity_to_the_type_owning_the_property = () =>
            result.Collections.Single().Key.ContainingEntityType.ShouldEqual(FakeMembers.Type);

        It should_create_a_column_for_the_key_with_the_default_id_naming = () =>
            result.Collections.Single().Key.Columns.Single().Name.ShouldEqual("Target_id");

        It should_set_the_collection_s_containing_entity_type_to_the_type_owning_the_property = () =>
            result.Collections.Single().ContainingEntityType.ShouldEqual(FakeMembers.Type);

        It should_set_the_collection_s_member_to_the_property = () =>
            result.Collections.Single().Member.ShouldEqual(FakeMembers.IListOfStrings);

        It should_set_the_collection_s_name_to_the_property_name = () =>
            result.Collections.Single().Name.ShouldEqual(FakeMembers.IListOfStrings.Name);

        It should_not_create_a_relationship_for_the_collection = () =>
            result.Collections.Single().Relationship.ShouldBeNull();

        It should_not_create_a_component_for_the_collection = () =>
            result.Collections.Single().CompositeElement.ShouldBeNull();
    }

    public abstract class AutoMapOneToManySpec
    {
        Establish context = () =>
        {
            rules = new DefaultDiscoveryRules();
            step = new OneToManyStep(new TestStrategy(rules));
        };

        protected static OneToManyStep step;
        protected static IMappingResult result;
        protected static DefaultDiscoveryRules rules;
    }

    public abstract class AutoMapOneToManyVisitSpec
    {
        Establish context = () =>
        {
            rules = new DefaultDiscoveryRules();
            step = new SimpleTypeCollectionStep(new TestStrategy(rules));
        };

        protected static SimpleTypeCollectionStep step;
        protected static DefaultDiscoveryRules rules;
    }

    public class TestStrategy : DefaultAutomappingStrategy
    {
        readonly IAutomappingDiscoveryRules rules;

        public TestStrategy(IAutomappingDiscoveryRules rules)
        {
            this.rules = rules;
        }

        public override IAutomappingDiscoveryRules GetRules()
        {
            return rules;
        }
    }
}