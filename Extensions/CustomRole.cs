using Exiled.API.Features;
using Exiled.Loader;
using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tesla = Exiled.API.Features.TeslaGate;

namespace BetterTeslas.Extensions
{
    public static class CustomRole
    {
        private static readonly Assembly Assembly = Loader.Plugins.FirstOrDefault(x => x.Name == "Exiled.CustomRoles" && x.Config.IsEnabled)?.Assembly;

        public static IEnumerator<float> CheckCustomRoles(this Player player)
        {
            yield return Timing.WaitForSeconds(0.1f);

            try
            {
                if (Assembly == null || player == null)
                    yield break;

                MethodInfo method = Assembly.GetType("Exiled.CustomRoles.API.Extensions").GetMethod("GetCustomRoles");
                foreach (var role in (IEnumerable)method.Invoke(null, new object[] { player }))
                    if (Plugin.Singleton.Config.IgnoredCustomRoles.Contains(role.GetName()))
                    {
                        if (Tesla.IgnoredPlayers.Contains(player))
                            yield break;

                        Tesla.IgnoredPlayers.Add(player);
                        EventHandlers.HasCustomRole.Add(player);
                        yield break;
                    }

                if (Tesla.IgnoredPlayers.Contains(player))
                {
                    Tesla.IgnoredPlayers.Remove(player);
                    EventHandlers.HasCustomRole.Remove(player);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            yield break;
        }

        private static string GetName(this object obj) =>
            obj.GetType().GetProperty("Name").GetValue(obj).ToString();
    }
}