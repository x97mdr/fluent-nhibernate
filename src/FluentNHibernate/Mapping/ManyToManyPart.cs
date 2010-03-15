using System;
using System.Collections;
using System.Collections.Generic;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class ManyToManyPart<TChild> : ToManyBase<ManyToManyPart<TChild>, TChild, ManyToManyMapping>
    {
        readonly Type entity;
        readonly IMappingStructure<CollectionMapping> structure;
        readonly IMappingStructure<KeyMapping> keyStructure;
        readonly IMappingStructure relationshipStructure;
        readonly FetchTypeExpression<ManyToManyPart<TChild>> fetch;
        readonly NotFoundExpression<ManyToManyPart<TChild>> notFound;

        public ManyToManyPart(Type entity, IMappingStructure<CollectionMapping> structure, IMappingStructure<KeyMapping> keyStructure, IMappingStructure relationshipStructure)
            : base(structure, keyStructure, relationshipStructure)
        {
            this.entity = entity;
            this.structure = structure;
            this.keyStructure = keyStructure;
            this.relationshipStructure = relationshipStructure;

            fetch = new FetchTypeExpression<ManyToManyPart<TChild>>(this, value => structure.SetValue(Attr.Fetch, value));
            notFound = new NotFoundExpression<ManyToManyPart<TChild>>(this, value => relationshipStructure.SetValue(Attr.NotFound, value));
        }

        public ManyToManyPart<TChild> ChildKeyColumn(string childKeyColumn)
        {
            relationshipStructure.RemoveChildrenMatching(x => x is IMappingStructure<ColumnMapping>);

            var column = new ColumnStructure(relationshipStructure);

            new ColumnPart(column)
                .Name(childKeyColumn);

            relationshipStructure.AddChild(column);

            return this;
        }

        public ManyToManyPart<TChild> ParentKeyColumn(string parentKeyColumn)
        {
            keyStructure.RemoveChildrenMatching(x => x is IMappingStructure<ColumnMapping>);

            var column = new ColumnStructure(keyStructure);

            new ColumnPart(column)
                .Name(parentKeyColumn);

            keyStructure.AddChild(column);

            return this;
        }

        public ManyToManyPart<TChild> ForeignKeyConstraintNames(string parentForeignKeyName, string childForeignKeyName)
        {
            keyStructure.SetValue(Attr.ForeignKey, parentForeignKeyName);
            relationshipStructure.SetValue(Attr.ForeignKey, childForeignKeyName);
            return this;
        }

        public FetchTypeExpression<ManyToManyPart<TChild>> FetchType
        {
            get { return fetch; }
        }

        private void EnsureDictionary()
        {
            var childType = typeof(TChild);
            if (!typeof(IDictionary).IsAssignableFrom(childType))
                throw new ArgumentException(" must be of type IDictionary to be used in a non-generic ternary association. Type was: " + childType);
        }

        private void EnsureGenericDictionary()
        {
            //var childType = typeof(TChild);
            //if (!(childType.IsGenericType && childType.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            //    throw new ArgumentException(" must be of type IDictionary<> to be used in a ternary assocation. Type was: " + childType);
        }

        public ManyToManyPart<TChild> AsTernaryAssociation()
        {
            EnsureGenericDictionary();

            return AsTernaryAssociation(string.Empty, null);
        }

        public ManyToManyPart<TChild> AsTernaryAssociation(string indexColumn, string valueColumn)
        {
            EnsureGenericDictionary();

            var indexStructure = new TypeStructure<IndexManyToManyMapping>(entity);
            var part = new IndexManyToManyPart(indexStructure);

            if (!string.IsNullOrEmpty(indexColumn))
                part.Column(indexColumn);

            if (!string.IsNullOrEmpty(valueColumn))
                ChildKeyColumn(valueColumn);

            structure.AddChild(indexStructure);

            return this;
        }

        public ManyToManyPart<TChild> AsTernaryAssociation(Type indexType, Type valueType)
        {
            return AsTernaryAssociation(indexType, indexType.Name + "_id", valueType, valueType.Name + "_id");
        }

        public ManyToManyPart<TChild> AsTernaryAssociation(Type indexType, string indexColumn, Type valueType, string valueColumn)
        {
            EnsureDictionary();

            var indexStructure = new TypeStructure<IndexManyToManyMapping>(entity);
            var part = new IndexManyToManyPart(indexStructure);
            part.Column(indexColumn);
            part.Type(indexType);

            ChildKeyColumn(valueColumn);

            structure.AddChild(indexStructure);

            return this;
        }

        public ManyToManyPart<TChild> AsSimpleAssociation()
        {
            EnsureGenericDictionary();

            var indexType = typeof(TChild).GetGenericArguments()[0];
            var valueType = typeof(TChild).GetGenericArguments()[1];

            return AsSimpleAssociation(indexType.Name + "_id", valueType.Name + "_id");
        }

        public ManyToManyPart<TChild> AsSimpleAssociation(string indexColumn, string valueColumn)
        {
            EnsureGenericDictionary();

            //var indexType = typeof(TChild).GetGenericArguments()[0];
            //var valueType = typeof(TChild).GetGenericArguments()[1];

            var indexStructure = new FreeStructure<IndexMapping>();
            var part = new IndexPart(indexStructure);
            part.Column(indexColumn);
            part.Type(entity);

            ChildKeyColumn(valueColumn);

            structure.AddChild(indexStructure);

            return this;
        }

        public ManyToManyPart<TChild> AsEntityMap()
        {
            // The argument to AsMap will be ignored as the ternary association will overwrite the index mapping for the map.
            // Therefore just pass null.
            return AsMap(null).AsTernaryAssociation();
        }

        public ManyToManyPart<TChild> AsEntityMap(string indexColumn, string valueColumn)
        {
            return AsMap(null).AsTernaryAssociation(indexColumn, valueColumn);
        }

        public Type ChildType
        {
            get { return typeof(TChild); }
        }

        public NotFoundExpression<ManyToManyPart<TChild>> NotFound
        {
            get { return notFound; }
        }

        /// <summary>
        /// Sets the order-by clause for this one-to-many relationship.
        /// </summary>
        public ManyToManyPart<TChild> OrderBy(string orderBy)
        {
            structure.SetValue(Attr.OrderBy, orderBy);
            return this;
        }

        public ManyToManyPart<TChild> ReadOnly()
        {
            structure.SetValue(Attr.Mutable, !nextBool);
            nextBool = true;
            return this;
        }

        public ManyToManyPart<TChild> Subselect(string subselect)
        {
            structure.SetValue(Attr.Subselect, subselect);
            return this;
        }
    }
}
