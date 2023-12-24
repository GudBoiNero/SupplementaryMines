using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SupplementaryMines.config;
using SupplementaryMines.patches;
using SupplementaryMines.Patches;
using System.Linq;
using UnityEngine;

namespace SupplementaryMines
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin instance = null;
        Harmony harmony = new(PluginInfo.PLUGIN_GUID);

        private void Awake()
        {
            instance = this;
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            config.Config.Init();
            config.Config.PrintConfig();

            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(RoundManagerPatch));
            harmony.PatchAll(typeof(LandminePatch));
        }

        public static void Log(string message)
        {
            instance.Logger.LogInfo((object)message);
        }
    }
}
