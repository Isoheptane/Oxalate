using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Reflection;
using Oxalate.Standard;

namespace Oxalate.Server
{
    public class LoadedPlugin
    {
        bool enabled;
        string pluginNamespace;
        Assembly assembly;
        IPlugin plugin;

        public bool Enabled
        {
            get { return enabled; }
        }

        public IPlugin Plugin
        {
            get { return plugin; }
        }

        public LoadedPlugin(string pluginNamespace, Assembly assembly, IPlugin plugin)
        {
            enabled = false;
            this.pluginNamespace = pluginNamespace;
            this.assembly = assembly;
            this.plugin = plugin;
        }

        /// <summary>
        /// Enable the Plugin
        /// </summary>
        public void Enable(PluginAPI api)
        {
            if (!Directory.Exists($"plugins/{Plugin.PluginName}/"))
                Directory.CreateDirectory($"plugins/{Plugin.PluginName}");

            string[] embeddedResourceNames = assembly.GetManifestResourceNames();
            foreach (string resourceName in embeddedResourceNames)
            {
                string fileName = resourceName.Remove(0, pluginNamespace.Length + 1);
                if (File.Exists($"plugins/{Plugin.PluginName}/{fileName}"))
                    continue;

                Stream stream = assembly.GetManifestResourceStream(resourceName);
                FileStream fileStream = new FileStream($"plugins/{Plugin.PluginName}/{fileName}", FileMode.Create);
                BinaryReader reader = new BinaryReader(stream);
                while (reader.PeekChar() != -1)
                {
                    fileStream.WriteByte(reader.ReadByte());
                }

                reader.Close();
                reader.Dispose();
                stream.Close();
                stream.Dispose();
                fileStream.Close();
                fileStream.Dispose();
            }
            
            if (!enabled)
                Plugin.EnablePlugin(api);
            enabled = true;
        }

        public void Reload()
        {
            Plugin.ReloadPlugin();
        }

        public void Disable()
        {
            if (enabled)
                Plugin.DisablePlugin();
            enabled = false;
        }
    }
    public class PluginManager
    {
        Server server;
        Dictionary<string, LoadedPlugin> pluginList;

        /// <summary>
        /// Create a Plugin Manager instance.
        /// </summary>
        /// <param name="server">Host server.</param>
        public PluginManager(Server server)
        {
            this.server = server;
            pluginList = new Dictionary<string, LoadedPlugin>();
        }


        public Dictionary<string, LoadedPlugin> PluginList
        {
            get { return pluginList; }
        }

        /// <summary>
        /// Enable a indicated plugin via plugin name.
        /// </summary>
        /// <param name="name">Plugin name</param>
        public void EnablePlugin(string name)
        {
            try
            {
                PluginList[name].Enable(new PluginAPI(server, name));
            }
            catch (Exception ex)
            {
                ScreenIO.Error(
                    server.Translation["server.enablePluginError"]
                    .Replace("$NAME", name)
                    .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                );
            }
        }

        /// <summary>
        /// Reload a indicated plugin via plugin name.
        /// </summary>
        /// <param name="name">Plugin name</param>
        public void ReloadPlugin(string name)
        {
            try
            {
                PluginList[name].Reload();
            }
            catch (Exception ex)
            {
                ScreenIO.Error(
                    server.Translation["server.reloadPluginError"]
                    .Replace("$NAME", name)
                    .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                );
            }
        }

        /// <summary>
        /// Reload a indicated plugin via plugin name.
        /// </summary>
        /// <param name="name">Plugin name</param>
        public void DisablePlugin(string name)
        {
            try
            {
                PluginList[name].Disable();
            }
            catch (Exception ex)
            {
                ScreenIO.Error(
                    server.Translation["server.disablePluginError"]
                    .Replace("$NAME", name)
                    .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                );
            }
        }

        /// <summary>
        /// Load plugin from indicated file.
        /// </summary>
        /// <param name="pluginPath">Plugin file path</param>
        public void LoadPlugin(string pluginPath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(pluginPath);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.GetInterface("IPlugin") != null)
                    {
                        IPlugin instance = (IPlugin)assembly.CreateInstance(type.FullName);
                        PluginList.Add(instance.PluginName, new LoadedPlugin(type.Namespace, assembly, instance));
                    }
                }
            }
            catch (Exception ex)
            {
                ScreenIO.Error(
                    server.Translation["server.failPluginLoad"]
                    .Replace("$PATH", pluginPath)
                    .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                );
            }
        }

    }
}
