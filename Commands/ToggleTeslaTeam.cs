using CommandSystem;
using Exiled.Permissions.Extensions;
using PlayerRoles;
using System;
using Tesla = Exiled.API.Features.TeslaGate;

namespace BetterTeslas.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ToggleTeslaTeam : ICommand
    {
        public string Command => "toggleteams";
        public string[] Aliases => new string[] { "togteams" };
        public string Description => "Makes an specific team be ignored by tesla gates.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("bt.toggleteams"))
            {
                response = "You don't have the permissions to use this command. Permissions required: bt.toggleteams";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "You must provide a valid team. toggleteams [TEAM]";
                return false;
            } 
            if (!Enum.TryParse(arguments.At(0), true, out Team team))
            {
                response = "Value provided is not a valid Team type.";
                return false;
            }
            
            if (Tesla.IgnoredTeams.Contains(team))
            {
                Tesla.IgnoredTeams.Remove(team);
                response = $"Team {team} is no longer ignored by tesla gates.";
                return true;
            }
            else
            {
                Tesla.IgnoredTeams.Add(team);
                response = "";
                return true;
            }
        }
    }
}