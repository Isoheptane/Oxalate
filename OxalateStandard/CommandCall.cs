using System;
using System.Collections.Generic;
using System.Text;
using JsonSharp;

namespace Oxalate.Standard
{
    public class CommandCall
    {
        string command;
        List<string> arguments;

        public string CommandName
        {
            get { return command; }
            set { command = value; }
        }

        public List<string> Arguments
        {
            get { return arguments; }
            set { arguments = value; }
        }

        /// <summary>
        /// Create a Command instance without arguments.
        /// </summary>
        public CommandCall(string command)
        {
            this.CommandName = command;
            this.Arguments = new List<string>();
        }

        /// <summary>
        /// Create a Command instance with arguments.
        /// </summary>
        public CommandCall(string command, params string[] arguments)
        {
            this.CommandName = command;
            this.Arguments = new List<string>();
            foreach (string argument in arguments)
                this.Arguments.Add(argument);
        }

        /// <summary>
        /// Create a Command instance by resolving input.
        /// </summary>
        public static CommandCall Parse(string input)
        {
            string[] array = StringReader.ReadStringArray(input);
            CommandCall command = new CommandCall(array[0]);
            for (int i = 1; i < array.Length; i++)
                command.Arguments.Add(array[i]);
            return command;
        }

        /// <summary>
        /// Create a Command instance by resolving packet.
        /// </summary>
        public static CommandCall Parse(Packet packet)
        {
            CommandCall command = new CommandCall(packet["command"]);
            JsonArray arguments = packet["arguments"];
            foreach (var argument in arguments.elements)
                command.Arguments.Add(argument);
            return command;
        }

        /// <summary>
        /// Create a string that contains the command and the arguments.
        /// </summary>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(CommandName);
            foreach (string argument in Arguments)
                stringBuilder.Append($" \"{Reader.Escape(argument)}\"");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Create a Packet that contains the command and the arguments.
        /// </summary>
        public Packet ToPacket()
        {
            Packet packet = new Packet();
            packet["command"] = command;
            JsonArray arguments = new JsonArray();
            foreach (string argument in Arguments)
                arguments.elements.Add(argument);
            packet["arguments"] = arguments;
            return packet;
        }

    }
}
