using BepInEx;
using HarmonyLib;
using UnityEngine;
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

            Plugin.Log("Prefix: Finished SpawnMapObjects");
        }

        [HarmonyPatch("SpawnMapObjects")]
        [HarmonyPostfix]
        static void Postfix(RoundManager __instance)
        {
            Plugin.Log("Postfix: Ran SpawnMapObjects");

            for (int i = 0; i < __instance.currentLevel.spawnableMapObjects.Length; i++)
            {
                SpawnableMapObject spawnableMapObject = __instance.currentLevel.spawnableMapObjects[i];

                Plugin.Log("Postfix: Found SpawnableMapObject prefab named " + "\"" + spawnableMapObject.prefabToSpawn.GetType().Name + "\"");
                if (spawnableMapObject.prefabToSpawn.name == "Landmine")
                {
                    Plugin.Log("Postfix: Found Landmine in \"currentLevel.spawnableMapObjects\"!");

                    Keyframe[] keys = spawnableMapObject.numberToSpawn.GetKeys();
                    for (int j = 0; j < keys.Length; j++)
                    {
                        float val = (float)keys.GetValue(j);
                        keys.SetValue(val * Plugin.MinesMultiplier, j);
                        Plugin.Log("Postfix: Applied MinesModifier!");
                    }
                }
            }
            
            Plugin.Log("Postfix: Finished SpawnMapObjects");
        }
    }
}