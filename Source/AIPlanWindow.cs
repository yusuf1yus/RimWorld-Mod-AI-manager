using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
/*
YF_YUSUF's plan to mod's logic 

1. сделать плашку что будет работать(похожа на функцию поиска предемтов на карте)
2. получить текст который юзщер пишет в эту плашку
3. сформировать промт для ИИ
    3.1 собрать весь контекст о карте
        3.1.1 строения на карте
        3.1.2 комнаты 
        3.1.3 информация о пешках
        3.1.4 ивенты на карте (рейд, осада, торговцы)
        3.1.5 ...
    3.2 прикрепить промт юзера
    3.3 ...
4. получить ответ от ИИ 
5. дать задания пешкам

*/
namespace AIManager
{
    public class AIPlanWindow : Window
    {
        private string planText = "";
        private bool focused;

        public override Vector2 InitialSize => new Vector2(420f, 102f);

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

            Rect field = new Rect(0f, inRect.height - 66f, inRect.width - 90f, 66f);
            Rect send = new Rect(field.width + 6f, inRect.height - 30f, 84f, 30f);
            Rect options = new Rect(field.width + 6f, 0, 84f, 30f);

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
                Close();
            }

            if(Widgets.ButtonText(options, "Options"))
            {
                // Здесь открывается отдельное окно с настройками  
            }
        }
    }
    
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