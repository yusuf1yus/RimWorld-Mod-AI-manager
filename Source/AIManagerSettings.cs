using HarmonyLib;
using UnityEngine;
using Verse;
using RimWorld;

namespace AIManager{
    class AIManagerSettings : ModSettings
    {
        public string userAPI = "";

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.userAPI, "userAPI", defaultValue: "");
        }
    }

    class AIManagerMod : Mod
    {
        public static AIManagerSettings settings;

        public AIManagerMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AIManagerSettings>();
        }

        public override string SettingsCategory() => "AIManagerSettingsCategoryLabel".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
       }
    }
}