using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Conventions.Inspections
{
    //public class InspectorModelMapper<TInspector, TMapping>
    //{
    //    private readonly IDictionary<Attr, Attr> mappings = new Dictionary<Attr, Attr>();

    //    public void Map(Expression<Func<TInspector, object>> inspectorProperty, Attr mappingProperty)
    //    {
    //        mappings[inspectorProperty.ToMember().Name] = mappingProperty;
    //    }

    //    public Attr Get(Attr property)
    //    {
    //        if (mappings.ContainsKey(property))
    //            return mappings[property];

    //        return property;
    //    }
    //}
}