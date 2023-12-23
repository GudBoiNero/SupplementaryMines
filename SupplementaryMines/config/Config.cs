using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SupplementaryMines.config
{
    internal static class Config
    {
        private const string CONFIG_FILE_NAME = "supplementary_mines.cfg";

        private static ConfigFile _config;
        private static ConfigEntry<bool> _consistentLandmines;
        private static ConfigEntry<float> _landminesMultiplier;
        private static ConfigEntry<float> _landminesMinimum;
        private static ConfigEntry<bool> _hellmines;
        private static ConfigEntry<float> _hellminesDetectionRange;



        public static void Init()
        {
            Plugin.Log("Initializing config...");

            var filePath = Path.Combine(Paths.ConfigPath, CONFIG_FILE_NAME);
            _config = new ConfigFile(filePath, true);

            _consistentLandmines = _config.Bind("Config", "Consistent Landmines", false, "Every map will have landmines in their spawn table.");
            _landminesMultiplier = _config.Bind("Config", "Landmines Multiplier", 1.0f, "Multiplies the default amount of landmines to spawn.");
            _landminesMinimum = _config.Bind("Config", "Landmines Minimum", 1.0f, "[Only applies with Consistent Landmines enabled] The minimum amount of landmines to spawn.");
            _hellmines = _config.Bind("Config", "Hellmines", false, "Landmines are now bigger and can detect you without being stepped on...");
            _hellminesDetectionRange = _config.Bind("Config", "Hellmines", 0f, "[Only applies with Hellmines enabled] The hellmines' detection range.");

            Plugin.Log("Config initialized!");
        }

        public static void PrintConfig()
        {
            Plugin.Log($"Consistent Landmines: {consistentLandmines}");
            Plugin.Log($"Landmines Multiplier: {landminesMultiplier}");
            Plugin.Log($"Landmines Multiplier: {landminesMinimum}");
            Plugin.Log($"Hellmines: {hellmines}");
            Plugin.Log($"Hellmines Detection Range: {hellminesDetectionRange}");
        }

        public static bool consistentLandmines => _consistentLandmines.Value;
        public static float landminesMultiplier => _landminesMultiplier.Value;
        public static float landminesMinimum => _landminesMinimum.Value;
        public static bool hellmines => _hellmines.Value;
        public static float hellminesDetectionRange => _hellminesDetectionRange.Value;
    }
}
