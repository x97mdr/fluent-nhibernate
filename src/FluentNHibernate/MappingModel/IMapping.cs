using System;
using System.Collections.Generic;

namespace FluentNHibernate.MappingModel
{
    public interface IMapping
    {
        void AddChild(IMapping child);
        void UpdateValues(ValueStore otherValues);
    }

    /// <summary>
    /// Denotes a mapping node that is directly related to a member (property, any, bag, etc...)
    /// </summary>
    public interface IMemberMapping : IMapping
    {
        void Initialise(Member member);
    }

    /// <summary>
    /// Denotes a mapping node that represents a type (class, subclass, etc...)
    /// </summary>
    public interface ITypeMapping : IMapping
    {
        void Initialise(Type type);
    }

    public interface ITypeAndMemberMapping : IMapping
    {
        void Initialise(Type type, Member member);
    }
}