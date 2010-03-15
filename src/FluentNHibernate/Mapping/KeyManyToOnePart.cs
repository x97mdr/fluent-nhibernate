using System;
using System.Diagnostics;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.Identity;
using FluentNHibernate.MappingModel.Structure;

namespace FluentNHibernate.Mapping
{
    public class KeyManyToOnePart
    {
        readonly IMappingStructure<KeyManyToOneMapping> structure;
        bool nextBool = true;

        public KeyManyToOnePart(IMappingStructure<KeyManyToOneMapping> structure)
        {
            this.structure = structure;
            Access = new AccessStrategyBuilder<KeyManyToOnePart>(this, value => structure.SetValue(Attr.Access, value));
            NotFound = new NotFoundExpression<KeyManyToOnePart>(this, value => structure.SetValue(Attr.NotFound, value));
        }

        /// <summary>
        /// Inverts the next boolean
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public KeyManyToOnePart Not
        {
            get
            {
                nextBool = !nextBool;
                return this;
            }
        }

        public KeyManyToOnePart ForeignKey(string foreignKey)
        {
            structure.SetValue(Attr.ForeignKey, foreignKey);
            return this;
        }

        /// <summary>
        /// Defines how NHibernate will access the object for persisting/hydrating (Defaults to Property)
        /// </summary>
        public AccessStrategyBuilder<KeyManyToOnePart> Access { get; private set; }

        public NotFoundExpression<KeyManyToOnePart> NotFound { get; private set; }

        public KeyManyToOnePart Lazy()
        {
            structure.SetValue(Attr.Lazy, nextBool);
            nextBool = true;
            return this;
        }

        public KeyManyToOnePart Name(string name)
        {
            structure.SetValue(Attr.Name, name);
            return this;
        }

        public KeyManyToOnePart Column(string columnName)
        {
            var column = new ColumnStructure(structure);

            new ColumnPart(column)
                .Name(columnName);

            structure.AddChild(column);
            return this;
        }
    }
}