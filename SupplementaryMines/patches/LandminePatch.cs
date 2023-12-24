using HarmonyLib;
using SupplementaryMines.config;
using SupplementaryMines.Patches;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace SupplementaryMines.patches
{
    [HarmonyPatch(typeof(Landmine))]
    internal class LandminePatch
    {
        public static LandminePatch Instance = null;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Prefix_Start(Landmine __instance) 
        {
            // Find the collider and set it to `Math.max(default_collider_size, Config.hellminesDetectionRange)`
        }
    }
}
