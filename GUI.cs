using System;
using System.Collections.Generic;
using PulsarModLoader.CustomGUI;
using PulsarModLoader;
using UnityEngine;

namespace Whisper_Command
{
    internal class Config : ModSettingsMenu
    {
        public static SaveValue<string> TextColour = new SaveValue<string>("TextColour", "#00ffffff");
        public override string Name()
        {
            return "Whisper Customization";
        }
        public override void Draw()
        {
            Config.ColorPicker(new Rect(258f, 30f, 240f, 160f), "Text Colour", Config.TextColour);
        }
        public static void ColorPicker(Rect rect, string Name, SaveValue<string> saveValue)
        {
            Color yellow = Color.yellow;
            ColorUtility.TryParseHtmlString(saveValue.Value, out yellow);
            GUILayout.BeginArea(rect, "", "Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label(Name + " Colour");
            GUILayout.Label(saveValue.Value);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("R", new GUILayoutOption[] { GUILayout.Width(10f) });
            yellow.r = GUILayout.HorizontalSlider(yellow.r, 0f, 1f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("G", new GUILayoutOption[] { GUILayout.Width(10f) });
            yellow.g = GUILayout.HorizontalSlider(yellow.g, 0f, 1f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("B", new GUILayoutOption[] { GUILayout.Width(10f) });
            yellow.b = GUILayout.HorizontalSlider(yellow.b, 0f, 1f);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("A", new GUILayoutOption[] { GUILayout.Width(10f) });
            yellow.a = GUILayout.HorizontalSlider(yellow.a, 0f, 1f);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box", new GUILayoutOption[]
            {
                GUILayout.Width(44f),
                GUILayout.Height(44f)
            });
            GUI.color = yellow;
            saveValue.Value = "#" + ColorUtility.ToHtmlStringRGBA(yellow);
            GUILayout.Label(new Texture2D(60, 40));
            GUI.color = Color.white;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Label(string.Format("{0},{1},{2},{3}", new object[]
            {
                (int)(yellow.r * 255f),
                (int)(yellow.g * 255f),
                (int)(yellow.b * 255f),
                (int)(yellow.a * 255f)
            }));
            GUILayout.EndArea();
        }
    }
}

