using RimWorld;
using HarmonyLib;
using Verse;
using UnityEngine;

namespace AIManager
{
    [HarmonyPatch(typeof(PlaySettings), "DoMapControls")]
    public static class Patch_DoMapControls
    {
        public static void Postfix(WidgetRow row)
        {
            row.Gap(row.CellGap);

            Texture2D AIManagerIcon = ContentFinder<Texture2D>.Get("Icon/AIManager");
            
            if (row.ButtonIcon(AIManagerIcon, "Open AI Plan input"))
            {
                Find.WindowStack.Add(new AIPlanWindow());
            }
        }
    }
}