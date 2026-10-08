using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AIManager
{
    public class Dialog_Options : Window
    {
        private string userAPI;
        private const float CategoryListWidth = 160f;
        private const float CategoryRowHeight = 48f;
        private const float CategoryRowSpacing = 2f;
        private const float CategoryBottomRowHeight = 50f;

        public override Vector2 InitialSize => new Vector2(650f, 600f);

        public Dialog_Options(){
            doCloseX = true;
            layer = WindowLayer.GameUI;
            closeOnClickedOutside = false;
            absorbInputAroundWindow = true;
            draggable = false;
            forcePause = true;
            closeOnAccept = false;
        }

        public override void DoWindowContents(Rect inRect)
        {
            DoAPIRow(InitialPosition: new Vector2(0, 0), inRect: inRect);
        }

        private void DoAPIRow(Vector2 InitialPosition, Rect inRect)
        {
            Rect fieldAPI = new Rect(InitialPosition.x, InitialPosition.y, inRect.width - 84f, 30f);
            Rect saveAPI = new Rect(InitialPosition.x + fieldAPI.width + 6f, InitialPosition.y, inRect.width - fieldAPI.width - 6f, 30f);

            GUI.SetNextControlName("AIPlanField");
            userAPI = Widgets.TextField(fieldAPI, userAPI);
            
            if(Widgets.ButtonText(saveAPI, "Save"))
            {
                Log.Message("[AIManager] user saved new API: " + userAPI);
                AIManagerMod.settings.userAPI = userAPI;
                AIManagerMod.settings.Write();
                GUI.FocusControl("");
            }
        }
    }
}