using BepInEx;
using HarmonyLib;
using SupplementaryMines.Patches;
using DunGen;

namespace SupplementaryMines.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {
        public static RoundManagerPatch Instance = null;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        [HarmonyPatch("SpawnMapObjects")]
        [HarmonyPrefix]
        static void Prefix(RoundManager __instance)
        {
            Plugin.Log("Prefix: Ran SpawnMapObjects");
        }

        [HarmonyPatch("SpawnMapObjects")]
        [HarmonyPostfix]
        static void Postfix(RoundManager __instance)
        {
            Plugin.Log("Postfix: Ran SpawnMapObjects");
        }
    }
}