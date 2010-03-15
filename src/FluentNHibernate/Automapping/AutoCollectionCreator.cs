using System;
using System.Collections.Generic;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Automapping
{
    public class AutoCollectionCreator
    {
        public CollectionMapping CreateCollectionMapping(Type type, Member member)
        {
            var collection = new CollectionMapping();

            collection.Initialise(type, member);

            return collection;
        }
    }
}