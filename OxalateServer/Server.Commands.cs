using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;

namespace Oxalate.Server
{
    public partial class Server
    {
        ConcurrentDictionary<string, Command> registeredCommands;

        public ConcurrentDictionary<string, Command> Commands
        {
            get { return registeredCommands; }
        }

        /// <summary>
        /// Server execute user command.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="command"></param>
        public void ExecuteCommand(OnlineUser user, CommandCall commandCall)
        {
            try
            {
                if (Commands.ContainsKey(commandCall.CommandName))
                {
                    Command command = Commands[commandCall.CommandName];
                    switch (command.Execute(user, commandCall))
                    {
                        case CommandExecuteResult.SyntaxError:
                            SendServerMessage(
                                Translation["server.commandUsage"]
                                .Replace("$USAGE", command.Usage),
                                user
                            );
                            break;
                        case CommandExecuteResult.PermissionDenied:
                            SendServerMessage(
                                Translation["server.permissionDenied"]
                                .Replace("$COMMAND", commandCall.CommandName),
                                user
                            );
                            break;
                    }
                }
                else
                {
                    SendServerMessage(
                        Translation["server.commandNotFound"]
                        .Replace("$COMMAND", commandCall.CommandName),
                        user
                    );
                }
            }
            catch (Exception ex)
            {
                ScreenIO.Error(
                    Translation["server.commandError"]
                    .Replace("$USER", user.Username)
                    .Replace("$COMMAND", commandCall.ToString())
                    .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                );
            }
        }

    }
}
