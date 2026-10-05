using RimWorld;
using HarmonyLib;
using Verse;

namespace AIManager
{
    [HarmonyPatch(typeof(PlaySettings), "DoMapControls")]
    public static class Patch_DoMapControls
    {
        public static void Postfix(WidgetRow row)
        {
            if (row.ButtonIcon(TexButton.Search, "Open AI Plan input"))
            {
                Find.WindowStack.Add(new AIPlanWindow());
            }
        }
    }
}