using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class OneToManyPart<TChild> : ToManyBase<OneToManyPart<TChild>, TChild, OneToManyMapping>
    {
        readonly Type entity;
        readonly IMappingStructure<CollectionMapping> structure;
        readonly IMappingStructure<KeyMapping> keyStructure;
        readonly IMappingStructure relationshipStructure;
        private readonly ColumnMappingCollection<OneToManyPart<TChild>> keyColumns;
        private readonly CollectionCascadeExpression<OneToManyPart<TChild>> cascade;
        private readonly NotFoundExpression<OneToManyPart<TChild>> notFound;

        public OneToManyPart(Type entity, IMappingStructure<CollectionMapping> structure, IMappingStructure<KeyMapping> keyStructure, IMappingStructure relationshipStructure)
            : base(structure, keyStructure, relationshipStructure)
        {
            this.entity = entity;
            this.structure = structure;
            this.keyStructure = keyStructure;
            this.relationshipStructure = relationshipStructure;

            keyColumns = new ColumnMappingCollection<OneToManyPart<TChild>>(this, keyStructure);
            cascade = new CollectionCascadeExpression<OneToManyPart<TChild>>(this, value => structure.SetValue(Attr.Cascade, value));
            notFound = new NotFoundExpression<OneToManyPart<TChild>>(this, value => relationshipStructure.SetValue(Attr.NotFound, value));
        }

        public NotFoundExpression<OneToManyPart<TChild>> NotFound
        {
            get { return notFound; }
        }

        public new CollectionCascadeExpression<OneToManyPart<TChild>> Cascade
        {
            get { return cascade; }
        }

        private void EnsureGenericDictionary()
        {
            var childType = typeof(TChild);
            if (!(childType.IsGenericType && childType.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
                throw new ArgumentException(" must be of type IDictionary<> to be used in a ternary assocation. Type was: " + childType);
        }

        public OneToManyPart<TChild> AsTernaryAssociation()
        {
            return AsTernaryAssociation(null);
        }

        public OneToManyPart<TChild> AsTernaryAssociation(string indexColumnName)
        {
            //EnsureGenericDictionary();

            var indexStructure = new TypeStructure<IndexManyToManyMapping>(entity);
            var part = new IndexManyToManyPart(indexStructure);

            if (!string.IsNullOrEmpty(indexColumnName))
                part.Column(indexColumnName);

            structure.AddChild(indexStructure);

            return this;
        }

        public OneToManyPart<TChild> AsEntityMap()
        {
            // The argument to AsMap will be ignored as the ternary association will overwrite the index mapping for the map.
            // Therefore just pass null.
            return AsMap(null).AsTernaryAssociation();
        }

        public OneToManyPart<TChild> AsEntityMap(string indexColumnName)
        {
            return AsMap(null).AsTernaryAssociation(indexColumnName);
        }

        public OneToManyPart<TChild> KeyColumn(string columnName)
        {
            KeyColumns.Clear();
            KeyColumns.Add(columnName);
            return this;
        }

        public ColumnMappingCollection<OneToManyPart<TChild>> KeyColumns
        {
            get { return keyColumns; }
        }

        public OneToManyPart<TChild> ForeignKeyConstraintName(string foreignKeyName)
        {
            keyStructure.SetValue(Attr.ForeignKey, foreignKeyName);
            return this;
        }

        /// <summary>
        /// Sets the order-by clause for this one-to-many relationship.
        /// </summary>
        public OneToManyPart<TChild> OrderBy(string orderBy)
        {
            structure.SetValue(Attr.OrderBy, orderBy);
            return this;
        }

        public OneToManyPart<TChild> ReadOnly()
        {
            structure.SetValue(Attr.Mutable, !nextBool);
            nextBool = true;
            return this;
        }

        public OneToManyPart<TChild> Subselect(string subselect)
        {
            structure.SetValue(Attr.Subselect, subselect);
            return this;
        }

        public OneToManyPart<TChild> KeyUpdate()
        {
            keyStructure.SetValue(Attr.Update, nextBool);
            nextBool = true;
            return this;
        }

        public OneToManyPart<TChild> KeyNullable()
        {
            keyStructure.SetValue(Attr.NotNull, !nextBool);
            nextBool = true;
            return this;
        }
    }
}
