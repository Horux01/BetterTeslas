using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Tesla = Exiled.API.Features.TeslaGate;

namespace BetterTeslas
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton;
        private EventHandlers Handler;

        public override void OnEnabled()
        {
            Singleton = this;
            Handler = new EventHandlers(this);
            Player.Spawned += Handler.OnSpawned;
            Player.ChangingItem += Handler.OnChangingItem;
            Player.DroppingItem += Handler.OnDroppingItem;
            Player.TriggeringTesla += Handler.OnTriggeringTesla;

            Tesla.IgnoredTeams.AddRange(Config.IgnoredTeams);
            Tesla.IgnoredRoles.AddRange(Config.IgnoredRoles);
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Player.Spawned -= Handler.OnSpawned;
            Player.ChangingItem -= Handler.OnChangingItem;
            Player.DroppingItem -= Handler.OnDroppingItem;
            Player.TriggeringTesla -= Handler.OnTriggeringTesla;
            Singleton = null;
            Handler = null;

            base.OnDisabled();
        }
    }
}