using BepInEx;
using HarmonyLib;
using UnityEngine;
using SupplementaryMines.Patches;
using DunGen;
using System;
using SupplementaryMines.config;

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

        static void PrintKeyframes(Keyframe[] keyframes)
        {
            for (int i = 0; i < keyframes.Length; i++)
            {
                Plugin.Log("PrintKeyframes: \n\tValue: " + keyframes[i].value + "\n\tTime: " + keyframes[i].time);
            }
        }

        [HarmonyPatch("SpawnMapObjects")]
        [HarmonyPrefix]
        static void Prefix(RoundManager __instance)
        {
            Plugin.Log("Prefix: Ran SpawnMapObjects");

            for (int i = 0; i < __instance.currentLevel.spawnableMapObjects.Length; i++)
            {
                PrintKeyframes(__instance.currentLevel.spawnableMapObjects[i].numberToSpawn.GetKeys());
            }

            Plugin.Log("Prefix: " + __instance.currentLevel.spawnableMapObjects);
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
                        keys.SetValue(new Keyframe(key.time, spawnableMapObject.numberToSpawn.Evaluate(key.time) * Config.landminesMultiplier), j);
                        __instance.currentLevel.spawnableMapObjects[i].numberToSpawn.SetKeys(keys);
                        Plugin.Log("Prefix: Applied MinesModifier!");
                    }
                }
            }

            for (int i = 0; i < __instance.currentLevel.spawnableMapObjects.Length; i++)
            {
                PrintKeyframes(__instance.currentLevel.spawnableMapObjects[i].numberToSpawn.GetKeys());
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