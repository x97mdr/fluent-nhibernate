using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate.MappingModel.ClassBased;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel
{
    public class HibernateMapping : MappingBase, IMapping
    {
        readonly IList<ClassMapping> classes;
        readonly IList<FilterDefinitionMapping> filters;
        readonly IList<ImportMapping> imports;
        readonly ValueStore values = new ValueStore();

        public HibernateMapping()
        {
            classes = new List<ClassMapping>();
            filters = new List<FilterDefinitionMapping>();
            imports = new List<ImportMapping>();
        }

        public override void AcceptVisitor(IMappingModelVisitor visitor)
        {
            visitor.ProcessHibernateMapping(this);

            foreach (var import in Imports)
                visitor.Visit(import);

            foreach (var classMapping in Classes)
                visitor.Visit(classMapping);

            foreach (var filterMapping in Filters)
                visitor.Visit(filterMapping);
        }

        public IEnumerable<ClassMapping> Classes
        {
            get { return classes; }
        }

        public IEnumerable<FilterDefinitionMapping> Filters
        {
            get { return filters; }
        }

        public IEnumerable<ImportMapping> Imports
        {
            get { return imports; }
        }

        public void AddClass(ClassMapping classMapping)
        {
            classes.Add(classMapping);            
        }

        public void AddFilter(FilterDefinitionMapping filterMapping)
        {
            filters.Add(filterMapping);
        }

        public void AddImport(ImportMapping importMapping)
        {
            imports.Add(importMapping);
        }

        public string Catalog
        {
            get { return values.Get(Attr.Catalog); }
            set { values.Set(Attr.Catalog, value); }
        }

        public string DefaultAccess
        {
            get { return values.Get(Attr.DefaultAccess); }
            set { values.Set(Attr.DefaultAccess, value); }
        }

        public string DefaultCascade
        {
            get { return values.Get(Attr.DefaultCascade); }
            set { values.Set(Attr.DefaultCascade, value); }
        }

        public bool AutoImport
        {
            get { return values.Get<bool>(Attr.AutoImport); }
            set { values.Set(Attr.AutoImport, value); }
        }

        public string Schema
        {
            get { return values.Get(Attr.Schema); }
            set { values.Set(Attr.Schema, value); }
        }

        public bool DefaultLazy
        {
            get { return values.Get<bool>(Attr.DefaultLazy); }
            set { values.Set(Attr.DefaultLazy, value); }
        }

        public string Namespace
        {
            get { return values.Get(Attr.Namespace); }
            set { values.Set(Attr.Namespace, value); }
        }

        public string Assembly
        {
            get { return values.Get(Attr.Assembly); }
            set { values.Set(Attr.Assembly, value); }
        }

        public override bool HasUserDefinedValue(Attr property)
        {
            return HasValue(property);
        }

        public bool HasValue(Attr attr)
        {
            return values.HasValue(attr);
        }

        public bool Equals(HibernateMapping other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.classes.ContentEquals(classes) &&
                other.filters.ContentEquals(filters) &&
                other.imports.ContentEquals(imports) &&
                Equals(other.values, values);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(HibernateMapping)) return false;
            return Equals((HibernateMapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (classes != null ? classes.GetHashCode() : 0);
                result = (result * 397) ^ (filters != null ? filters.GetHashCode() : 0);
                result = (result * 397) ^ (imports != null ? imports.GetHashCode() : 0);
                result = (result * 397) ^ (values != null ? values.GetHashCode() : 0);
                return result;
            }
        }

        public void AddChild(IMapping child)
        {
            if (child is FilterDefinitionMapping)
                AddFilter((FilterDefinitionMapping)child);
            if (child is ImportMapping)
                AddImport((ImportMapping)child);
        }

        public void UpdateValues(ValueStore otherValues)
        {
            values.Merge(otherValues);
        }
    }
}