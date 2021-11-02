using System;
using System.Collections.Generic;
using System.Text;
using Oxalate.Server;
using Oxalate.Standard;
using JsonSharp;

namespace OxalateCore
{
    class SayCommand : Command
    {
        public SayCommand(IPlugin plugin, string name, string usage, JsonObject config) : base(plugin, name, usage, config) {}

        public override CommandExecuteResult Execute(OnlineUser sender, CommandCall call)
        {
            if (sender.BackendUser.PermissionLevel < Config["permissionLevel"])
                return CommandExecuteResult.PermissionDenied;
            if (call.Arguments.Count == 0)
                return CommandExecuteResult.SyntaxError;

            string message = call.Arguments[0];
            Packet messagePacket = new Packet();
            messagePacket["messageType"] = "chat";
            messagePacket["username"] = sender.Username;
            messagePacket["nickname"] = sender.Nickname;
            messagePacket["message"] = message;
            Plugin.API.BroadcastPacket(messagePacket);
            ScreenIO.Info(
                Plugin.Translation["display.chat"]
                .Replace("$USER", sender.Username)
                .Replace("$NICK", sender.Nickname)
                .Replace("$MESSAGE", message)
            );

            return CommandExecuteResult.Succeed;
        }
    }

    public class TellCommand : Command
    {
        public TellCommand(IPlugin plugin, string name, string usage, JsonObject config) : base(plugin, name, usage, config) { }

        public override CommandExecuteResult Execute(OnlineUser sender, CommandCall call)
        {
            if (sender.BackendUser.PermissionLevel < Config["permissionLevel"])
                return CommandExecuteResult.PermissionDenied;
            if (call.Arguments.Count != 2)
                return CommandExecuteResult.SyntaxError;

            string target = call.Arguments[0];
            string message = call.Arguments[1];

            if (!Plugin.API.OnlineUsers.ContainsKey(target))
            {
                Plugin.API.SendServerMessage (
                    Plugin.Translation["whisper.userNotOnline"]
                    .Replace("$USER", target),
                    sender
                );
                return CommandExecuteResult.Succeed;
            }

            OnlineUser targetUser = Plugin.API.OnlineUsers[target];

            Packet messagePacket = new Packet();
            messagePacket["messageType"] = "whisper";
            messagePacket["username"] = sender.Username;
            messagePacket["nickname"] = sender.Nickname;
            messagePacket["message"] = message;
            Plugin.API.SendPacket(messagePacket, targetUser);
            ScreenIO.Info(
                Plugin.Translation["display.whisper"]
                .Replace("$SENDER.USER", sender.Username)
                .Replace("$SENDER.NICK", sender.Nickname)
                .Replace("$RECEIVER.USER", targetUser.Username)
                .Replace("$RECEIVER.NICK", targetUser.Nickname)
                .Replace("$MESSAGE", message)
            );

            return CommandExecuteResult.Succeed;
        }
    }

    class ActionCommand : Command
    {
        public ActionCommand(IPlugin plugin, string name, string usage, JsonObject config) : base(plugin, name, usage, config) { }

        public override CommandExecuteResult Execute(OnlineUser sender, CommandCall call)
        {
            if (sender.BackendUser.PermissionLevel < Config["permissionLevel"])
                return CommandExecuteResult.PermissionDenied;
            if (call.Arguments.Count == 0)
                return CommandExecuteResult.SyntaxError;

            string message = call.Arguments[0];
            Packet messagePacket = new Packet();
            messagePacket["messageType"] = "action";
            messagePacket["username"] = sender.Username;
            messagePacket["nickname"] = sender.Nickname;
            messagePacket["message"] = message;
            Plugin.API.BroadcastPacket(messagePacket);
            ScreenIO.Info(
                Plugin.Translation["display.action"]
                .Replace("$USER", sender.Username)
                .Replace("$NICK", sender.Nickname)
                .Replace("$MESSAGE", message)
            );

            return CommandExecuteResult.Succeed;
        }
    }
}
