using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SunbaeBot
{
    class Settings
    {
        private const string path = "global.json";
        private static Settings _instance = new Settings();
        public static void Load()
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} was not found. Rename global.example.json.");
            _instance = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
        }
        public static void Save()
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var writer = new StreamWriter(stream))
                writer.Write(JsonConvert.SerializeObject(_instance, Formatting.Indented));
        }
        public class SunbaeSettings
        {
            [JsonProperty("discordToken")]
            public string Token;
        }
        [JsonProperty("Sunbae")]
        private SunbaeSettings _sunbae = new SunbaeSettings();
        public static SunbaeSettings Sunbae => _instance._sunbae;
    }
}
