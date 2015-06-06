using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace CheatMenu
{
    public class CheatMenuComponent : MonoBehaviour
    {
        protected bool visible = false;
        public static bool GodMode = false;
        public static float SpeedMultiplier = 1f;
        public static float JumpMultiplier = 1f;
        public static bool FlyMode = false;
        public static bool NoClip = false;
        public static float TimeSpeed = 0.13f;
        public static bool InstantTree = false;
        public static bool InstantBuild = false;
        protected float a = 2f;
        protected GUIStyle labelStyle;
        protected static bool Already = false;

        [ModAPI.Attributes.ExecuteOnGameStart]
        static void AddMeToScene() 
        {
            GameObject GO = new GameObject("__CheatMenu__");
            GO.AddComponent<CheatMenuComponent>();
        }

        private void OnGUI()
        {
            if (this.visible)
            {
                GUI.skin = ModAPI.GUI.Skin;

                Matrix4x4 bkpMatrix = GUI.matrix;

                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(GUI.skin.label);
                    labelStyle.fontSize = 12;
                }

                GUI.Box(new Rect(10, 10, 400, 400), "Cheat menu", GUI.skin.window);

                float cY = 50f;
                GUI.Label(new Rect(20f, cY, 150f, 20f), "God mode:", labelStyle);
                GodMode = GUI.Toggle(new Rect(170f, cY, 20f, 30f), GodMode, "");
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Flymode:", labelStyle);
                FlyMode = GUI.Toggle(new Rect(170f, cY, 20f, 30f), FlyMode, "");
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "No clip:", labelStyle);
                NoClip = GUI.Toggle(new Rect(170f, cY, 20f, 30f), NoClip, "");
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Instant Tree:", labelStyle);
                InstantTree = GUI.Toggle(new Rect(170f, cY, 20f, 30f), InstantTree, "");
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Instant Build:", labelStyle);
                InstantBuild = GUI.Toggle(new Rect(170f, cY, 20f, 30f), InstantBuild, "");
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Speed:", labelStyle);
                SpeedMultiplier = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), SpeedMultiplier, 1f, 10f);
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Jump power:", labelStyle);
                JumpMultiplier = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), JumpMultiplier, 1f, 10f);
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Speed of time:", labelStyle);
                TheForestAtmosphere.Instance.RotationSpeed = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), TheForestAtmosphere.Instance.RotationSpeed, 0f, 10f);
                cY += 30f;
                if (GUI.Button(new Rect(280f, cY, 100f, 20f), "Reset"))
                {
                    TheForestAtmosphere.Instance.RotationSpeed = 0.13f;
                }
                cY += 30f;

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Time:", labelStyle);
                TheForestAtmosphere.Instance.TimeOfDay = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), TheForestAtmosphere.Instance.TimeOfDay, 0f, 360f);
                cY += 30f;

                GUI.matrix = bkpMatrix;
            }
        }


        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("OpenMenu"))
            //if (ModAPI.Input.GetButtonDown("Open"))
            {
                if (this.visible)
                {
                    TheForest.Utils.LocalPlayer.FpCharacter.UnLockView();
                    //Screen.lockCursor = false;
                }
                else
                {
                    TheForest.Utils.LocalPlayer.FpCharacter.LockView();
                }
                this.visible = !this.visible;
            }

        }
    }
}