using System;
using System.Collections;
using System.Collections.Generic;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;

namespace FluentNHibernate.Cfg
{
    /// <summary>
    /// Container for automatic mappings
    /// </summary>
    public class AutoMappingsContainer : IEnumerable<PersistenceModel>
    {
        private readonly IList<PersistenceModel> mappings = new List<PersistenceModel>();
        private string exportPath;

        internal AutoMappingsContainer()
        {}

        /// <summary>
        /// Add automatic mappings
        /// </summary>
        /// <param name="model">Lambda returning an auto mapping setup</param>
        /// <returns>Auto mappings configuration</returns>
        public AutoMappingsContainer Add(Func<AutomappingBuilder> model)
        {
            return Add(model());
        }

        /// <summary>
        /// Add automatic mappings
        /// </summary>
        /// <param name="model">Auto mapping setup</param>
        /// <returns>Auto mappings configuration</returns>
        public AutoMappingsContainer Add(AutomappingBuilder model)
        {
            mappings.Add(model.CreateModel());
            WasUsed = true;
            return this;
        }

        /// <summary>
        /// Sets the export location for generated mappings
        /// </summary>
        /// <param name="path">Path to folder for mappings</param>
        /// <returns>Auto mappings configuration</returns>
        public AutoMappingsContainer ExportTo(string path)
        {
            exportPath = path;
            return this;
        }

        /// <summary>
        /// Gets whether any mappings were added
        /// </summary>
        internal bool WasUsed { get; set; }

        /// <summary>
        /// Applies any added mappings to the NHibernate Configuration
        /// </summary>
        /// <param name="cfg">NHibernate Configuration instance</param>
        internal void Apply(Configuration cfg)
        {
            foreach (var mapping in mappings)
            {
                if (!string.IsNullOrEmpty(exportPath))
                    mapping.WriteMappingsTo(exportPath);

                mapping.Configure(cfg);
            }
        }

        public IEnumerator<PersistenceModel> GetEnumerator()
        {
            return mappings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}