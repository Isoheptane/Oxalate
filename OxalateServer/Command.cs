using System;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;
using JsonSharp;

namespace Oxalate.Server
{
    public enum CommandExecuteResult
    {
        Succeed = 0,
        SyntaxError = 1,
        PermissionDenied = 2
    }

    public abstract class Command
    {
        /// <summary>
        /// Plugin
        /// </summary>
        public IPlugin Plugin { get; }

        /// <summary>
        /// The name of this command.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The usage of this command.
        /// </summary>
        public string Usage { get; }

        /// <summary>
        /// The config object of this command.
        /// </summary>
        public JsonObject Config { get; }

        public Command(IPlugin plugin, string name, string usage, JsonObject config)
        {
            Plugin = plugin;
            Name = name;
            Usage = usage;
            Config = config;
        }

        /// <summary>
        /// Execute command.
        /// </summary>
        /// <returns>If the command's syntax is correct.</returns>
        public abstract CommandExecuteResult Execute(OnlineUser sender, CommandCall call);
    }
}
