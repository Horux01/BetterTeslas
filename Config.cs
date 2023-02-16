using Exiled.API.Interfaces;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace BetterTeslas
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Which teams won't be triggering tesla gates.")]
        public List<Team> IgnoredTeams { get; set; } = new List<Team>();

        [Description("Which roles won't be triggering tesla gates.")]
        public List<RoleTypeId> IgnoredRoles { get; set; } = new List<RoleTypeId>()
        {
            RoleTypeId.Tutorial
        };

        [Description("Which items won't be triggering tesla gates when you have them in the inventory.")]
        public List<ItemType> IgnoredItemsInInv { get; set; } = new List<ItemType>()
        {
            ItemType.KeycardO5
        };

        [Description("Which items won't be triggering tesla gates when you have them in your hand.")]
        public List<ItemType> IgnoredItemsInHand { get; set; } = new List<ItemType>()
        {
            ItemType.Coin
        };

        [Description("Whether tesla gates should ignore players with godmode.")]
        public bool DisableWithGodmode { get; set; } = true;

        [Description("Whether tesla gates should ignore players with noclip.")]
        public bool DisableWithNoclip { get; set; } = false;

        [Description("Whether tesla gates should ignore players with bypass.")]
        public bool DisableWithBypass { get; set; } = false;

        [Description("Whether tesla gates should ignore players with SCP-268 active.")]
        public bool DisableWithScp268 { get; set; } = true;

        [Description("Which custom roles won't be triggering tesla gates.")]
        public List<string> IgnoredCustomRoles { get; set; } = new List<string>();

        [Description("Which custom items won't be triggering tesla gates.")]
        public List<string> IgnoredCustomItems { get; set; } = new List<string>();

        [Description("Wheter debug messages should be shown in the console.")]
        public bool Debug { get; set; } = false;
    }
}