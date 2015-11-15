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

                GUI.Box(new Rect(10, 10, 400, 430), "Cheat menu", GUI.skin.window);

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

                GUI.Label(new Rect(20f, cY, 150f, 20f), "Cave light:", labelStyle);
                CaveLight = GUI.HorizontalSlider(new Rect(170f, cY + 3f, 210f, 30f), CaveLight, 0f, 1f);
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

        private void Update()
        {
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