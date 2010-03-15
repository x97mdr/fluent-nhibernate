using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class ColumnMappingCollection<TParent> : IEnumerable<IMappingStructure<ColumnMapping>>
    {
        readonly TParent parent;
        readonly IMappingStructure parentStructure;

        public ColumnMappingCollection(TParent parent, IMappingStructure structure)
        {
            this.parent = parent;
            this.parentStructure = structure;
        }

        public TParent Add(string name)
        {
            var column = new ColumnStructure(parentStructure);

            new ColumnPart(column)
                .Name(name);

            parentStructure.AddChild(column);

            return parent;
        }

        public TParent Add(params string[] names)
        {
            foreach (var name in names)
            {
                Add(name);
            }
            return parent;
        }

        public TParent Add(string columnName, Action<ColumnPart> customColumnMapping)
        {
            var column = new ColumnStructure(parentStructure);
            var part = new ColumnPart(column);

            part.Name(columnName);

            customColumnMapping(part);
            
            parentStructure.AddChild(column);

            return parent;
        }

        public TParent Clear()
        {
            parentStructure.RemoveChildrenMatching(x => x is IMappingStructure<ColumnMapping>);
            return parent;
        }

        public IEnumerator<IMappingStructure<ColumnMapping>> GetEnumerator()
        {
            return parentStructure.Children
                .Where(x => x is IMappingStructure<ColumnMapping>)
                .Select(x => x as IMappingStructure<ColumnMapping>)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}