using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;
using Machine.Specifications;

namespace FluentNHibernate.Specs.Model
{
    public class the_model_s_type_mapping_implementations
    {
        Because of = () =>
            types = typeof(ITypeMapping).Assembly
                .GetTypes()
                .Where(x => x.HasInterface(typeof(ITypeMapping)) && !x.IsAbstract);

        It should_have_a_public_constructor_taking_a_type_instance_for_each_concrete_implementation = () =>
            types.Each(x =>
            {
                var ctor = x.GetConstructor(new[] {typeof(Type)});

                if (ctor == null)
                    Console.WriteLine(x.Name + " doesn't have the expected ctor");

                ctor.ShouldNotBeNull();
            });
        
        static IEnumerable<Type> types;
    }
    
    public class the_model_s_member_mapping_implementations
    {
        Because of = () =>
            types = typeof(ITypeMapping).Assembly
                .GetTypes()
                .Where(x => x.HasInterface(typeof(IMemberMapping)) && !x.IsAbstract);

        It should_have_a_public_constructor_taking_a_member_instance_for_each_concrete_implementation = () =>
            types.Each(x =>
            {
                var ctor = x.GetConstructor(new[] { typeof(Member) });

                if (ctor == null)
                    Console.WriteLine(x.Name + " doesn't have the expected ctor");

                ctor.ShouldNotBeNull();
            });

        static IEnumerable<Type> types;
    }
}
