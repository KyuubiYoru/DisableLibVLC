using HarmonyLib; // HarmonyLib comes included with a NeosModLoader install
using NeosModLoader;
using FrooxEngine;
using BaseX;

namespace DisableLibVLC
{
    public class DisableLibVLC : NeosMod
    {
        public override string Name => "DisableLibVLC";
        public override string Author => "KyuubiYoru";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/KyuubiYoru/DisableLibVLC";

        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("DisableLibVLCHarmony");
            harmony.PatchAll();

            var engines = UnityNeos.PlaybackEngine.PlaybackEngines;
            var libVLC = engines.Find(i => i.Name == "libVLC");
            if (libVLC != default)
            {
                engines.Remove(libVLC);
                UniLog.Log("Removed libVLC from valid playback engines.");
            }
        }
        [HarmonyPatch(typeof(VideoTextureProvider))]
        class VideoTextureProviderPatch
        {
            [HarmonyPrefix]
            [HarmonyPatch("GetPlaybackEngine", typeof(string))]
            public static void GetPlaybackEnginePatch(ref string mime)
            {
                mime = null;
                UniLog.Log("Forced Unity Nativ in a video player");
            }
        }
    }
}