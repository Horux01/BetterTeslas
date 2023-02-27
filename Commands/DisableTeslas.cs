using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace BetterTeslas.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class DisableTeslas : ICommand
    {
        public string Command => "toggleteslas";
        public string[] Aliases => new string[] { "tt", "togteslas" };
        public string Description => "Toggle between enabling and disabling tesla gates.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("bt.disable"))
            {
                response = "You don't have the permissions to use this command. Permissions required: bt.disable";
                return false;
            }

            EventHandlers.AreTeslasEnabled = !EventHandlers.AreTeslasEnabled;
            response = $"Tesla gates have been {(EventHandlers.AreTeslasEnabled ? "enabled" : "disabled")}.";
            return true;
        }
    }
}