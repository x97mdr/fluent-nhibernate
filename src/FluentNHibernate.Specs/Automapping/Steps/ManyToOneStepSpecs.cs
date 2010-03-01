using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Mapping;
using FluentNHibernate.Specs.Automapping.Fixtures;
using Machine.Specifications;

namespace FluentNHibernate.Specs.Automapping.Steps
{
    public class when_the_many_to_one_step_is_asked_if_it_can_map_a_set_property : ManyToOneStepSpec
    {
        Because of = () =>
            result = step.IsMappable(FakeMembers.ISetOfTargets);

        It should_respond_with_false = () =>
            result.ShouldBeFalse();
    }

    public class when_the_many_to_one_step_is_asked_if_it_can_map_a_list_property : ManyToOneStepSpec
    {
        Because of = () =>
            result = step.IsMappable(FakeMembers.IListOfTargets);

        It should_respond_with_false = () =>
            result.ShouldBeFalse();
    }

    public class when_the_many_to_one_step_is_asked_if_it_can_map_a_value_type_property : ManyToOneStepSpec
    {
        Because of = () =>
            result = step.IsMappable(FakeMembers.String);

        It should_respond_with_false = () =>
            result.ShouldBeFalse();
    }

    public class when_the_many_to_one_step_is_asked_if_it_can_map_an_entity_property : ManyToOneStepSpec
    {
        Because of = () =>
            result = step.IsMappable(FakeMembers.Entity);

        It should_respond_with_true = () =>
            result.ShouldBeTrue();
    }

    public class ManyToOneStepSpec
    {
        Establish context = () =>
            step = new ManyToOneStep(new DefaultAutomappingStrategy());

        protected static ManyToOneStep step;
        protected static bool result;
    }
}