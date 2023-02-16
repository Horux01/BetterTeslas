using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Loader;
using System;
using System.Linq;
using System.Reflection;

namespace BetterTeslas.Extensions
{
    public static class CustomItem
    {
        private static readonly Assembly Assembly = Loader.Plugins.FirstOrDefault(x => x.Name == "Exiled.CustomItems" && x.Config.IsEnabled)?.Assembly;

        public static bool CheckCustomItem(this Item item)
        {
            try
            {
                if (Assembly == null || item == null)
                    return false;

                Type customItem = Assembly.GetType("Exiled.CustomItems.API.Features.CustomItem");
                MethodInfo method = customItem.GetMethod("TryGet", new Type[] { typeof(Item), customItem.MakeByRefType() });

                object[] args = new object[] { item, null };
                if ((bool)method.Invoke(null, args))
                    if (Plugin.Singleton.Config.IgnoredCustomItems.Contains(args.GetName()))
                        return true;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            return false;
        }

        private static string GetName(this object[] obj) =>
            obj[1].GetType().GetProperty("Name").GetValue(obj[1]).ToString();
    }
}