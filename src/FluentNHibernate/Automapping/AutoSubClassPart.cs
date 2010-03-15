using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Automapping
{
    public class AutoSubClassPart<T> : SubClassPart<T>, IAutoClasslike
    {
        private readonly IList<Member> propertiesMapped = new List<Member>();

        public AutoSubClassPart(DiscriminatorPart parent, IMappingStructure<SubclassMapping> structure)
            : base(parent, structure)
        {}

        public IEnumerable<Member> PropertiesMapped
        {
            get { return propertiesMapped; }
        }

        public object GetMapping()
        {
            return ((ISubclassMappingProvider)this).GetSubclassMapping();
        }

        void IAutoClasslike.DiscriminateSubClassesOnColumn(string column)
        {
            
        }

        void IAutoClasslike.AlterModel(ClassMappingBase mapping)
        { }

        protected override OneToManyPart<TChild> HasMany<TChild>(Type type, Member property)
        {
            propertiesMapped.Add(property);
            return base.HasMany<TChild>(type, property);
        }

        protected override PropertyPart Map(Member property, string columnName)
        {
            propertiesMapped.Add(property);
            return base.Map(property, columnName);
        }

        protected override ManyToOnePart<TOther> References<TOther>(Member property, string columnName)
        {
            propertiesMapped.Add(property);
            return base.References<TOther>(property, columnName);
        }

        protected override ManyToManyPart<TChild> HasManyToMany<TChild>(Type childType, Member property)
        {
            propertiesMapped.Add(property);
            return base.HasManyToMany<TChild>(childType, property);
        }

        protected override ComponentPart<TComponent> Component<TComponent>(Member property, Action<ComponentPart<TComponent>> action)
        {
            propertiesMapped.Add(property);

            if (action == null)
                action = c => { };

            return base.Component(property, action);
        }

        protected override OneToOnePart<TOther> HasOne<TOther>(Member property)
        {
            propertiesMapped.Add(property);
            return base.HasOne<TOther>(property);
        }

        public void JoinedSubClass<TSubclass>(string keyColumn, Action<AutoJoinedSubClassPart<TSubclass>> action)
        {
            var genericType = typeof(AutoJoinedSubClassPart<>).MakeGenericType(typeof(TSubclass));
            var joinedclass = (AutoJoinedSubClassPart<TSubclass>)Activator.CreateInstance(genericType, keyColumn);

            action(joinedclass);

            //subclasses[typeof(TSubclass)] = joinedclass;
        }

        public IAutoClasslike JoinedSubClass(Type type, string keyColumn)
        {
            var genericType = typeof(AutoJoinedSubClassPart<>).MakeGenericType(type);
            var joinedclass = (ISubclassMappingProvider)Activator.CreateInstance(genericType, keyColumn);

            //subclasses[type] = joinedclass;

            return (IAutoClasslike)joinedclass;
        }

        public void SubClass<TSubclass>(string discriminatorValue, Action<AutoSubClassPart<TSubclass>> action)
        {
            var genericType = typeof(AutoSubClassPart<>).MakeGenericType(typeof(TSubclass));
            var subclass = (AutoSubClassPart<TSubclass>)Activator.CreateInstance(genericType, discriminatorValue);

            action(subclass);

            //subclasses[typeof(TSubclass)] = subclass;
        }

        public IAutoClasslike SubClass(Type type, string discriminatorValue)
        {
            var genericType = typeof(AutoSubClassPart<>).MakeGenericType(type);
            var subclass = (ISubclassMappingProvider)Activator.CreateInstance(genericType, discriminatorValue);

            //subclasses[type] = subclass;

            return (IAutoClasslike)subclass;
        }

        public IUserDefinedMapping GetUserDefinedMappings()
        {
            throw new NotImplementedException();
        }

        public HibernateMapping GetHibernateMapping()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetIgnoredProperties()
        {
            return propertiesMapped.Select(x => x.Name);
        }
    }
}