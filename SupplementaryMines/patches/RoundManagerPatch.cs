using BepInEx;
using HarmonyLib;
using UnityEngine;
using SupplementaryMines.Patches;
using DunGen;
using System;

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


            for (int i = 0; i < __instance.currentLevel.spawnableMapObjects.Length; i++)
            {
                SpawnableMapObject spawnableMapObject = __instance.currentLevel.spawnableMapObjects[i];

                Plugin.Log("Prefix: Found SpawnableMapObject prefab named " + "\"" + spawnableMapObject.prefabToSpawn.GetType().Name + "\"");
                if (spawnableMapObject.prefabToSpawn.name == "Landmine")
                {
                    Plugin.Log("Prefix: Found Landmine in \"currentLevel.spawnableMapObjects\"!");

                    Keyframe[] keys = spawnableMapObject.numberToSpawn.GetKeys();
                    for (int j = 0; j < keys.Length; j++)
                    {
                        Keyframe key = keys[j];
                        keys[j] = new(key.time, spawnableMapObject.numberToSpawn.Evaluate(key.time) * Plugin.MinesMultiplier);
                        Plugin.Log("Prefix: Applied MinesModifier!");
                    }
                }
            }

            Plugin.Log("Prefix: Finished SpawnMapObjects");
        }

        [HarmonyPatch("SpawnMapObjects")]
        [HarmonyPostfix]
        static void Postfix(RoundManager __instance)
        {
            Plugin.Log("Postfix: Ran SpawnMapObjects");

            
            Plugin.Log("Postfix: Finished SpawnMapObjects");
        }
    }
}