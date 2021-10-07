using System;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;
using JsonSharp;

namespace Oxalate.Server
{
    public interface IPlugin
    {
        public string PluginName { get; }
        public string PluginFullname { get; }
        public string PluginVersion { get; }
        public string PluginDescription { get; }
        public PluginAPI API { get; }
        public Translation Translation { get; }
        public JsonObject Config { get; }

        /// <summary>
        /// Execute when plugin is enabled.
        /// </summary>
        public void EnablePlugin(PluginAPI api);

        /// <summary>
        /// Execute when plugin is manually reloaded.
        /// </summary>
        public void ReloadPlugin();

        /// <summary>
        /// Execute when plugin is disabled.
        /// </summary>
        public void DisablePlugin();

    }
}
