﻿using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Specs.Automapping.Fixtures;
using Machine.Specifications;

namespace FluentNHibernate.Specs.Automapping
{
    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_strings : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfStrings);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_ints : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfInts);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_doubles : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfDoubles);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_shorts : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfShorts);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_longs : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfLongs);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_floats : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfFloats);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_bools : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfBools);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_asked_if_it_can_map_a_list_of_DateTimes : AutoMapOneToManyVisitSpec
    {
        Because of = () =>
            maps_property = step.MapsProperty(FakeMembers.IListOfDateTimes);

        It should_accept_the_property = () =>
            maps_property.ShouldBeTrue();

        static bool maps_property;
    }

    public class when_the_automapper_is_told_to_map_a_list_of_simple_types_with_a_custom_column_defined : AutoMapOneToManySpec
    {
        Establish context = () =>
            expressions.SimpleTypeCollectionValueColumn = t => "custom_column";

        Because of = () =>
            step.Map(container, FakeMembers.IListOfStrings);

        It should_create_use_the_element_column_name_defined_in_the_expressions = () =>
            container.Collections.Single().Element.Columns.Single().Name.ShouldEqual("custom_column");
    }

    public class when_the_automapper_is_told_to_map_a_list_of_simple_types : AutoMapOneToManySpec
    {
        Because of = () =>
            step.Map(container, FakeMembers.IListOfStrings);

        It should_create_a_collection = () =>
            container.Collections.Count().ShouldEqual(1);

        It should_create_a_collection_that_s_a_bag = () =>
            container.Collections.Single().ShouldBeOfType<BagMapping>();

        It should_create_an_element_for_the_collection = () =>
            container.Collections.Single().Element.ShouldNotBeNull();

        It should_use_the_default_element_column = () =>
            container.Collections.Single().Element.Columns.Single().Name.ShouldEqual("Value");

        It should_set_the_element_type_to_the_first_generic_argument_of_the_collection_type = () =>
            container.Collections.Single().Element.Type.ShouldEqual(new TypeReference(typeof(string)));

        It should_create_a_key = () =>
            container.Collections.Single().Key.ShouldNotBeNull();

        It should_set_the_key_s_containing_entity_to_the_type_owning_the_property = () =>
            container.Collections.Single().Key.ContainingEntityType.ShouldEqual(FakeMembers.Type);

        It should_create_a_column_for_the_key_with_the_default_id_naming = () =>
            container.Collections.Single().Key.Columns.Single().Name.ShouldEqual("Target_IListOfStrings_id");

        It should_set_the_collection_s_containing_entity_type_to_the_type_owning_the_property = () =>
            container.Collections.Single().ContainingEntityType.ShouldEqual(FakeMembers.Type);

        It should_set_the_collection_s_member_to_the_property = () =>
            container.Collections.Single().Member.ShouldEqual(FakeMembers.IListOfStrings);

        It should_set_the_collection_s_name_to_the_property_name = () =>
            container.Collections.Single().Name.ShouldEqual(FakeMembers.IListOfStrings.Name);

        It should_not_create_a_relationship_for_the_collection = () =>
            container.Collections.Single().Relationship.ShouldBeNull();

        It should_not_create_a_component_for_the_collection = () =>
            container.Collections.Single().CompositeElement.ShouldBeNull();
    }

    public abstract class AutoMapOneToManySpec
    {
        Establish context = () =>
        {
            expressions = new AutoMappingExpressions();
            step = new AutoMapOneToMany(expressions);
            container = new ClassMapping
            {
                Type = FakeMembers.Type
            };
        };

        protected static AutoMapOneToMany step;
        protected static ClassMapping container;
        protected static AutoMappingExpressions expressions;
    }

    public abstract class AutoMapOneToManyVisitSpec
    {
        Establish context = () =>
        {
            expressions = new AutoMappingExpressions();
            step = new AutoSimpleTypeCollection(expressions);
        };

        protected static AutoSimpleTypeCollection step;
        protected static AutoMappingExpressions expressions;
    }
}