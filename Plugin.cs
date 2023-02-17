using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;
using Tesla = Exiled.API.Features.TeslaGate;
using System;

namespace BetterTeslas
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton;
        private EventHandlers Handler;

        public override string Author => "Horus";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(6, 0, 0);

        public override void OnEnabled()
        {
            Singleton = this;
            Handler = new EventHandlers(this);
            Server.WaitingForPlayers += Handler.OnWaitingForPlayers;
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
            Server.WaitingForPlayers -= Handler.OnWaitingForPlayers;
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