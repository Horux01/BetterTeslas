using BetterTeslas.Extensions;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using MEC;
using System.Collections.Generic;
using System.Linq;
using Tesla = Exiled.API.Features.TeslaGate;

namespace BetterTeslas
{
    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public static List<Player> HasCustomRole = new List<Player>();

        public void OnSpawned(SpawnedEventArgs ev) => Timing.RunCoroutine(ev.Player.CheckCustomRoles());
        public void OnChangingItem(ChangingItemEventArgs ev)
        {
            if (ev.NewItem.CheckCustomItem())
            {
                if (Tesla.IgnoredPlayers.Contains(ev.Player))
                    return;

                Tesla.IgnoredPlayers.Add(ev.Player);
            }
            else if (Tesla.IgnoredPlayers.Contains(ev.Player) && !HasCustomRole.Contains(ev.Player))
                Tesla.IgnoredPlayers.Remove(ev.Player);
        }
        public void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (ev.Item.CheckCustomItem() && ev.Item == ev.Player.CurrentItem && Tesla.IgnoredPlayers.Contains(ev.Player) && !HasCustomRole.Contains(ev.Player))
                Tesla.IgnoredPlayers.Remove(ev.Player);
        }
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (plugin.Config.IgnoredItemsInInv.Any(x => ev.Player.HasItem(x)) ||
                (ev.Player.CurrentItem != null && plugin.Config.IgnoredItemsInHand.Contains(ev.Player.CurrentItem.Type)) ||
                (ev.Player.IsGodModeEnabled && plugin.Config.DisableWithGodmode) ||
                (ev.Player.Role.As<FpcRole>().IsNoclipEnabled && plugin.Config.DisableWithNoclip) ||
                (ev.Player.IsBypassModeEnabled && plugin.Config.DisableWithBypass) ||
                (ev.Player.IsEffectActive<Invisible>() && plugin.Config.DisableWithScp268))
            {
                ev.IsInIdleRange = false;
                ev.IsAllowed = false;
            }
        }
    }
}