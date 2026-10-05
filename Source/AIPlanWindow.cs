using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AIManager
{
    public class AIPlanWindow : Window
    {
        private string planText = "";
        private bool focused;

        public override Vector2 InitialSize => new Vector2(420f, 114f);

        public AIPlanWindow()
        {
            layer = WindowLayer.GameUI;
            doCloseX = true;
            closeOnClickedOutside = false;
            absorbInputAroundWindow = false;
            preventCameraMotion = false;
            draggable = false;
            closeOnAccept = false;
        }

        protected override void SetInitialSizeAndPosition()
        {
            Vector2 size = InitialSize;
            windowRect = new Rect(
                UI.screenWidth - size.x,
                UI.screenHeight - size.y,
                size.x,
                size.y
            );
        }

        public override void DoWindowContents(Rect inRect)
        {
            bool enter = Event.current.type == EventType.KeyDown 
                && (Event.current.keyCode == KeyCode.Return 
                    || Event.current.keyCode == KeyCode.KeypadEnter);

            Rect field = new Rect(0f, 12f, inRect.width - 90f, 66f);
            Rect send = new Rect(field.width + 6f, 12 + 30f + 6f, 84f, 30f);
            Rect options = new Rect(field.width + 6f, 12f, 84f, 30f);

            GUI.SetNextControlName("AIPlanField");

            planText = Widgets.TextArea(field, planText); 

            if (!focused){
                GUI.FocusControl("AIPlanField");
                focused = true;
            }

            if((Widgets.ButtonText(send, "Send") || enter) && planText.Trim().Length > 0)
            {
                Log.Message("[AIManager] plan: " + planText);
                planText = "";
            }

            if(Widgets.ButtonText(options, "Options"))
            {
                Log.Message("[AIManager] open options");
                Find.WindowStack.Add(new Dialog_Options());
            }
        }
    }
}