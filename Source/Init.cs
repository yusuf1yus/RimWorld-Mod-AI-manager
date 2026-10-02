using HarmonyLib;
using Verse;

namespace AIManager
{
    [StaticConstructorOnStartup]
    public static class Init
    {
        static Init()
        {
            Log.Message("Hello from AI Manager!");
            new Harmony("yfyusuf.aimanager").PatchAll();
        }
    }
}