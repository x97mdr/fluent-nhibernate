using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.MappingModel.Structure;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Mapping
{
    public class SubclassMap<T> : ClasslikeMapBase<T>, IIndeterminateSubclassMappingProvider
    {
        readonly IMappingStructure<SubclassMapping> structure;

        private bool nextBool = true;
        private IList<JoinMapping> joins = new List<JoinMapping>();
        IMappingStructure<KeyMapping> keyStructure;

        public SubclassMap()
            : this(new SubclassStructure(SubclassType.Unknown, typeof(T)))
        {}

        SubclassMap(IMappingStructure<SubclassMapping> structure)
            : base(structure)
        {
            this.structure = structure;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public SubclassMap<T> Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        IUserDefinedMapping IMappingProvider.GetUserDefinedMappings()
        {
            return new FluentMapUserDefinedMappings(typeof(T), structure);
        }

        public HibernateMapping GetHibernateMapping()
        {
            throw new NotImplementedException();
        }

        public void Abstract()
        {
            structure.SetValue(Attr.Abstract, nextBool);
            nextBool = true;
        }

        public void DynamicInsert()
        {
            structure.SetValue(Attr.DynamicInsert, nextBool);
            nextBool = true;
        }

        public void DynamicUpdate()
        {
            structure.SetValue(Attr.DynamicUpdate, nextBool);
            nextBool = true;
        }

        public void LazyLoad()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
        }

        public void Proxy<TProxy>()
        {
            Proxy(typeof(TProxy));
        }

        public void Proxy(Type proxyType)
        {
            structure.SetValue(Attr.Proxy, proxyType.AssemblyQualifiedName);
        }

        public void SelectBeforeUpdate()
        {
            structure.SetValue(Attr.SelectBeforeUpdate, nextBool);
            nextBool = true;
        }

        public void DiscriminatorValue(object discriminatorValue)
        {
            structure.SetValue(Attr.DiscriminatorValue, discriminatorValue);
        }

        public void Table(string table)
        {
            structure.SetValue(Attr.Table, table);
        }

        public void Schema(string schema)
        {
            structure.SetValue(Attr.Schema, schema);
        }

        public void Check(string constraint)
        {
            structure.SetValue(Attr.Check, constraint);
        }

        public void KeyColumn(string columnName)
        {
            if (keyStructure == null)
            {
                keyStructure = new FreeStructure<KeyMapping>();
                structure.AddChild(keyStructure);
            }

            var column = new ColumnStructure(keyStructure);

            new ColumnPart(column)
                .Name(columnName);

            keyStructure.AddChild(column);
        }

        public void Subselect(string subselect)
        {
            structure.SetValue(Attr.Subselect, subselect);
        }

        public void Persister<TPersister>()
        {
            structure.SetValue(Attr.Persister, new TypeReference(typeof(TPersister)));
        }

        public void Persister(Type type)
        {
            structure.SetValue(Attr.Persister, new TypeReference(type));
        }

        public void Persister(string type)
        {
            structure.SetValue(Attr.Persister, new TypeReference(type));
        }

        public void BatchSize(int batchSize)
        {
            structure.SetValue(Attr.BatchSize, batchSize);
        }

        public void EntityName(string entityname)
        {
            structure.SetValue(Attr.EntityName, entityname);
        }

        /// <summary>
        /// Sets additional tables for the class via the NH 2.0 Join element, this only works if
        /// the hierarchy you're mapping has a discriminator.
        /// </summary>
        /// <param name="tableName">Joined table name</param>
        /// <param name="action">Joined table mapping</param>
        public void Join(string tableName, Action<JoinPart<T>> action)
        {
            var joinStructure = new TypeStructure<JoinMapping>(typeof(T));
            var join = new JoinPart<T>(joinStructure);

            if (!string.IsNullOrEmpty(tableName))
                join.Table(tableName);

            action(join);

            structure.AddChild(joinStructure);
        }
    }
}