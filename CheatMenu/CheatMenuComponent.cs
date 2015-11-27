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
        public static float CaveLight = 0f;
        public static int ForceWeather = -1;
        public static bool FreezeWeather = false;

        protected float a = 2f;
        protected GUIStyle labelStyle;
        protected static bool Already = false;

        [ModAPI.Attributes.ExecuteOnGameStart]
        static void AddMeToScene()
        {
            GameObject GO = new GameObject("__CheatMenu__");
            GO.AddComponent<CheatMenuComponent>();
        }

        protected int Tab = 0;

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

                GUI.Box(new Rect(10, 10, 400, 430), "", GUI.skin.window);
                this.Tab = GUI.Toolbar(new Rect(10, 10, 400, 30), this.Tab, new GUIContent[] { new GUIContent("Cheats"), new GUIContent("Environment"), new GUIContent("Player"), new GUIContent("Other") }, GUI.skin.GetStyle("Tabs"));
                /*this.Tab = GUI.Toggle(new Rect(10, 10, 100, 30), this.Tab == 0, "Cheats", GUI.skin.button) ? 0 : this.Tab;
                this.Tab = GUI.Toggle(new Rect(110, 10, 100, 30), this.Tab == 1, "Environment", GUI.skin.button) ? 1 : this.Tab;
                this.Tab = GUI.Toggle(new Rect(210, 10, 100, 30), this.Tab == 2, "Player", GUI.skin.button) ? 2 : this.Tab;
                this.Tab = GUI.Toggle(new Rect(310, 10, 100, 30), this.Tab == 3, "Other", GUI.skin.button) ? 3 : this.Tab;*/
                float cY = 50f;

                if (Tab == 0)
                {
                    GUI.Label(new Rect(20f, cY, 150f, 20f), "God mode:", labelStyle);
                    GodMode = GUI.Toggle(new Rect(170f, cY, 20f, 30f), GodMode, "");
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Flymode:", labelStyle);
                    FlyMode = GUI.Toggle(new Rect(170f, cY, 20f, 30f), FlyMode, "");
                    cY += 30f;

                    if (FlyMode)
                    {
                        GUI.Label(new Rect(20f, cY, 150f, 20f), "No clip:", labelStyle);
                        NoClip = GUI.Toggle(new Rect(170f, cY, 20f, 30f), NoClip, "");
                        cY += 30f;
                    }

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "InstaTree:", labelStyle);
                    InstantTree = GUI.Toggle(new Rect(170f, cY, 20f, 30f), InstantTree, "");
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "InstaBuild:", labelStyle);
                    InstantBuild = GUI.Toggle(new Rect(170f, cY, 20f, 30f), InstantBuild, "");
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "InstaKill:", labelStyle);
                    InstaKill = GUI.Toggle(new Rect(170f, cY, 20f, 30f), InstaKill, "");
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Speed:", labelStyle);
                    SpeedMultiplier = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), SpeedMultiplier, 1f, 10f);
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Jump power:", labelStyle);
                    JumpMultiplier = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), JumpMultiplier, 1f, 10f);
                    cY += 30f;
                }

                if (Tab == 1)
                {
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

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Cave light:", labelStyle);
                    CaveLight = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), CaveLight, 0f, 1f);
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Weather", labelStyle);
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Freeze:", labelStyle);
                    FreezeWeather = GUI.Toggle(new Rect(170f, cY, 20f, 30f), FreezeWeather, "");
                    cY += 30f;

                    if (GUI.Button(new Rect(20f, cY, 180f, 20f), "Clear Weather"))
                    {
                        ForceWeather = 0;
                    }
                    cY += 30f;

                    if (GUI.Button(new Rect(20f, cY, 180f, 20f), "Cloudy"))
                    {
                        ForceWeather = 4;
                    }
                    cY += 30f;

                    if (GUI.Button(new Rect(20f, cY, 180f, 20f), "Light rain"))
                    {
                        ForceWeather = 1;
                    }
                    cY += 30f;

                    if (GUI.Button(new Rect(20f, cY, 180f, 20f), "Medium rain"))
                    {
                        ForceWeather = 2;
                    }
                    cY += 30f;

                    if (GUI.Button(new Rect(20f, cY, 180f, 20f), "Heavy rain"))
                    {
                        ForceWeather = 3;
                    }
                    cY += 30f;
                }

                if (Tab == 2)
                {
                    GUI.Label(new Rect(370f, cY, 150f, 20f), "Fix", labelStyle);
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Health:", labelStyle);
                    if (!FixHealth)
                        TheForest.Utils.LocalPlayer.Stats.Health = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.Health, 0f, 100f);
                    else
                        FixedHealth = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedHealth, 0f, 100f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.Health * 10) / 10f) + "");
                    FixHealth = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixHealth, "");
                    if (FixHealth)
                    { 
                        if (FixedHealth == -1f)
                            FixedHealth = TheForest.Utils.LocalPlayer.Stats.Health;
                    }
                    else
                    {
                        FixedHealth = -1f;
                    }
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Battery charge:", labelStyle);
                    if (!FixBatteryCharge)
                        TheForest.Utils.LocalPlayer.Stats.BatteryCharge = (int)GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.BatteryCharge, 0f, 100f);
                    else
                        FixedBatteryCharge = (int)GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedBatteryCharge, 0f, 100f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.BatteryCharge * 10) / 10f) + "");
                    FixBatteryCharge = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixBatteryCharge, "");
                    if (FixBatteryCharge)
                    {
                        if (FixedBatteryCharge == -1f)
                            FixedBatteryCharge = TheForest.Utils.LocalPlayer.Stats.BatteryCharge;
                    }
                    else
                    {
                        FixedBatteryCharge = -1f;
                    }
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Fullness:", labelStyle);
                    if (!FixFullness)
                        TheForest.Utils.LocalPlayer.Stats.Fullness = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.Fullness, 0f, 1f);
                    else
                        FixedFullness = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedFullness, 0f, 1f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.Fullness * 10) / 10f) + "");
                    FixFullness = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixFullness, "");
                    if (FixFullness)
                    {
                        if (FixedFullness == -1f)
                            FixedFullness = TheForest.Utils.LocalPlayer.Stats.Fullness;
                    }
                    else
                    {
                        FixedFullness = -1f;
                    }
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Stamina:", labelStyle);
                    if (!FixStamina)
                        TheForest.Utils.LocalPlayer.Stats.Stamina = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.Stamina, 0f, 100f);
                    else 
                        FixedStamina = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedStamina, 0f, 100f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.Stamina * 10) / 10f) + "");
                    FixStamina = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixStamina, "");
                    if (FixStamina)
                    {
                        if (FixedStamina == -1f)
                            FixedStamina = TheForest.Utils.LocalPlayer.Stats.Stamina;
                    }
                    else
                    {
                        FixedStamina = -1f;
                    }
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Energy:", labelStyle);
                    if (!FixEnergy)
                        TheForest.Utils.LocalPlayer.Stats.Energy = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.Energy, 0f, 100f);
                    else
                        FixedEnergy = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedEnergy, 0f, 100f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.Energy * 10) / 10f) + "");
                    FixEnergy = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixEnergy, "");
                    if (FixEnergy)
                    {
                        if (FixedEnergy == -1f)
                            FixedEnergy = TheForest.Utils.LocalPlayer.Stats.Energy;
                    }
                    else
                    {
                        FixedEnergy = -1f;
                    }
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Thirst:", labelStyle);
                    if (!FixThirst)
                        TheForest.Utils.LocalPlayer.Stats.Thirst = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.Thirst, 0f, 1f);
                    else
                        FixedThirst = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedThirst, 0f, 1f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.Thirst * 10) / 10f) + "");
                    FixThirst = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixThirst, "");
                    if (FixThirst)
                    {
                        if (FixedThirst == -1f)
                            FixedThirst = TheForest.Utils.LocalPlayer.Stats.Thirst;
                    }
                    else
                    {
                        FixedThirst = -1f;
                    }
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Starvation:", labelStyle);
                    if (!FixStarvation)
                        TheForest.Utils.LocalPlayer.Stats.Starvation = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.Starvation, 0f, 1f);
                    else
                        FixedStarvation = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedStarvation, 0f, 1f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.Starvation * 10) / 10f) + "");
                    FixStarvation = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixStarvation, "");
                    if (FixStarvation)
                    {
                        if (FixedStarvation == -1f)
                            FixedStarvation = TheForest.Utils.LocalPlayer.Stats.Starvation;
                    }
                    else
                    {
                        FixedStarvation = -1f;
                    }
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Body Temp:", labelStyle);
                    if (!FixBodyTemp)
                        TheForest.Utils.LocalPlayer.Stats.BodyTemp = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), TheForest.Utils.LocalPlayer.Stats.BodyTemp, 10f, 60f);
                    else
                        FixedBodyTemp = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 160f, 30f), FixedBodyTemp, 10f, 60f);
                    GUI.Label(new Rect(340f, cY, 40f, 20f), (Mathf.RoundToInt(TheForest.Utils.LocalPlayer.Stats.BodyTemp * 10) / 10f) + "");
                    FixBodyTemp = GUI.Toggle(new Rect(370f, cY, 20f, 20f), FixBodyTemp, "");
                    if (FixBodyTemp)
                    {
                        if (FixedBodyTemp == -1f)
                            FixedBodyTemp = TheForest.Utils.LocalPlayer.Stats.BodyTemp;
                    }
                    else
                    {
                        FixedBodyTemp = -1f;
                    }
                    cY += 30f;
                    
                }

                if (Tab == 3)
                {
                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Free cam:", labelStyle);
                    FreeCam = GUI.Toggle(new Rect(170f, cY, 20f, 30f), FreeCam, "");
                    cY += 30f;

                    GUI.Label(new Rect(20f, cY, 150f, 20f), "Freeze time:", labelStyle);
                    FreezeTime = GUI.Toggle(new Rect(170f, cY, 20f, 30f), FreezeTime, "");
                    cY += 30f;
                    GUI.matrix = bkpMatrix;
                }
            }
        }

        public static bool InstaKill = false;

        protected static bool FixHealth = false;
        protected static bool FixBatteryCharge = false;
        protected static bool FixFullness = false;
        protected static bool FixStamina = false;
        protected static bool FixEnergy = false;
        protected static bool FixThirst = false;
        protected static bool FixStarvation = false;
        protected static bool FixBodyTemp = false;

        protected static float FixedHealth = -1f;
        protected static float FixedBatteryCharge = -1f;
        protected static float FixedFullness = -1f;
        protected static float FixedStamina = -1f;
        protected static float FixedEnergy = -1f;
        protected static float FixedThirst = -1f;
        protected static float FixedStarvation = -1f;
        protected static float FixedBodyTemp = -1f;

        public static bool FreeCam = false;
        public static bool FreezeTime = false;

        protected GameObject Sphere;
        void Start()
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            MeshFilter mf = go.GetComponent<MeshFilter>();
            Mesh mesh = mf.sharedMesh;

            GameObject goNew = new GameObject();
            goNew.name = "Inverted Sphere";
            MeshFilter mfNew = goNew.AddComponent<MeshFilter>();
            mfNew.sharedMesh = new Mesh();

            //Scale the vertices;
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
                vertices[i] = vertices[i];
            mfNew.sharedMesh.vertices = vertices;

            // Reverse the triangles
            int[] triangles = mesh.triangles;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int t = triangles[i];
                triangles[i] = triangles[i + 2];
                triangles[i + 2] = t;
            }
            mfNew.sharedMesh.triangles = triangles;

            // Reverse the normals;
            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
                normals[i] = -normals[i];
            mfNew.sharedMesh.normals = normals;


            mfNew.sharedMesh.uv = mesh.uv;
            mfNew.sharedMesh.uv2 = mesh.uv2;
            mfNew.sharedMesh.RecalculateBounds();


            DestroyImmediate(go);

            this.Sphere = goNew;
            this.Sphere.AddComponent<MeshRenderer>();
            Sphere.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
            Sphere.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1f, 0f, 0f, 0.9f));
            Sphere.GetComponent<MeshRenderer>().enabled = false;
            Sphere.GetComponent<Collider>().enabled = false;
        }

        protected float massTreeRadius = 10f;
        protected bool DestroyTree = false;
        protected bool LastFreezeTime = false;
        protected bool LastFreeCam = false;
        protected float rotationY = 0f;

        private void Update()
        {
            if (FreezeTime && !LastFreezeTime)
            {
                UnityEngine.Time.timeScale = 0f;
                LastFreezeTime = true;
            }
            if (!FreezeTime && LastFreezeTime)
            {
                UnityEngine.Time.timeScale = 1f;
                LastFreezeTime = false;
            }

            if (FreeCam && !LastFreeCam)
            {
                TheForest.Utils.LocalPlayer.CamFollowHead.enabled = false;
                TheForest.Utils.LocalPlayer.CamRotator.enabled = false;
                TheForest.Utils.LocalPlayer.MainRotator.enabled = false;
                TheForest.Utils.LocalPlayer.FpCharacter.enabled = false;
                LastFreeCam = true;
            }
            if (!FreeCam && LastFreeCam)
            {
                TheForest.Utils.LocalPlayer.CamFollowHead.enabled = true;
                TheForest.Utils.LocalPlayer.CamRotator.enabled = true;
                TheForest.Utils.LocalPlayer.MainRotator.enabled = true;
                TheForest.Utils.LocalPlayer.FpCharacter.enabled = true;
                LastFreeCam = false;
            }

            if (FreeCam)
            {
                bool button1 = TheForest.Utils.Input.GetButton("Crouch");
                bool button2 = TheForest.Utils.Input.GetButton("Run");
                bool button3 = TheForest.Utils.Input.GetButton("Jump");
                float multiplier = 0.1f;
                if (button2) multiplier = 2f;

                Vector3 vector3 = Camera.main.transform.rotation * (
                    new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"),
                    0f,
                    TheForest.Utils.Input.GetAxis("Vertical")
                ) * multiplier);
                if (button3) vector3.y += multiplier;
                if (button1) vector3.y -= multiplier;
                Camera.main.transform.position += vector3;

                float rotationX = Camera.main.transform.localEulerAngles.y + TheForest.Utils.Input.GetAxis("Mouse X") * 15f;
                rotationY += TheForest.Utils.Input.GetAxis("Mouse Y") * 15f;
                rotationY = Mathf.Clamp(rotationY, -80f, 80f);
                Camera.main.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }

            if (ModAPI.Input.GetButtonDown("FreezeTime"))
            {
                FreezeTime = !FreezeTime;
            }

            if (ModAPI.Input.GetButtonDown("FreeCam"))
            {
                FreeCam = !FreeCam;
            }
            if (ModAPI.Input.GetButton("MassTree"))
            {
                if (Input.mouseScrollDelta != Vector2.zero)
                {
                    massTreeRadius = Mathf.Clamp(massTreeRadius + Input.mouseScrollDelta.y, 20f, 100f);
                }
                Sphere.GetComponent<MeshRenderer>().enabled = true;
                Sphere.transform.position = TheForest.Utils.LocalPlayer.Transform.position;
                Sphere.transform.localScale = new Vector3(massTreeRadius * 2f, massTreeRadius * 2f, massTreeRadius * 2f);
                DestroyTree = true;
            }
            else
            {
                if (DestroyTree)
                {
                    RaycastHit[] hits = Physics.SphereCastAll(Sphere.transform.position, massTreeRadius, new Vector3(1f, 0f, 0f));
                    foreach (RaycastHit hit in hits)
                    {
                        TreeHealth h = hit.collider.GetComponent<TreeHealth>();
                        if (h != null)
                        {
                            hit.collider.gameObject.SendMessage("Explosion", 100f);
                        }
                    }
                    DestroyTree = false;
                }
                Sphere.GetComponent<MeshRenderer>().enabled = false;
            }
            if (FixBodyTemp)
                TheForest.Utils.LocalPlayer.Stats.BodyTemp = FixedBodyTemp;
            if (FixBatteryCharge)
                TheForest.Utils.LocalPlayer.Stats.BatteryCharge = FixedBatteryCharge;
            if (FixEnergy)
                TheForest.Utils.LocalPlayer.Stats.Energy = FixedEnergy;
            if (FixHealth)
                TheForest.Utils.LocalPlayer.Stats.Health = FixedHealth;
            if (FixStamina)
                TheForest.Utils.LocalPlayer.Stats.Stamina = FixedStamina;
            if (FixFullness)
                TheForest.Utils.LocalPlayer.Stats.Fullness = FixedFullness;
            if (FixStarvation)
                TheForest.Utils.LocalPlayer.Stats.Starvation = FixedStarvation;
            if (FixThirst)
                TheForest.Utils.LocalPlayer.Stats.Thirst = FixedThirst;
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