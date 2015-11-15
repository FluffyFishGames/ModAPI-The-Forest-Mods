using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Picker : MonoBehaviour
{
    protected GUIStyle boldLabel;
    protected GUIStyle pathLabel;
    protected GUIStyle whiteLabel;
    protected GUIStyle componentLabel;
    protected GUIStyle background;
        
    [ModAPI.Attributes.ExecuteOnGameStart]
    static void Init()
    {
        GameObject g = new GameObject("__DevPicker__");
        g.AddComponent<Picker>();
    }

    protected Camera camera;
    protected bool Initialized = false;

    void Initialize()
    {
        Initialized = true;
        boldLabel = new GUIStyle(ModAPI.GUI.Skin.label);
        boldLabel.fontSize = 16;
        boldLabel.fontStyle = FontStyle.Bold;
        boldLabel.normal.textColor = Color.black;

        pathLabel = new GUIStyle(ModAPI.GUI.Skin.label);
        pathLabel.fontSize = 14;
        pathLabel.fontStyle = FontStyle.Bold;
        pathLabel.normal.textColor = Color.black;

        whiteLabel = new GUIStyle(ModAPI.GUI.Skin.label);
        whiteLabel.fontSize = 14;
        whiteLabel.fontStyle = FontStyle.Bold;
        whiteLabel.normal.textColor = Color.white;

        componentLabel = new GUIStyle(ModAPI.GUI.Skin.label);
        componentLabel.fontSize = 14;
        componentLabel.fontStyle = FontStyle.Normal;
        componentLabel.normal.textColor = Color.black;

        Texture2D t = new Texture2D(1, 1);
        t.SetPixel(0, 0, new Color(1f, 1f, 1f, 0.5f));
        t.Apply();

        background = new GUIStyle();
        background.normal.background = t;
    }

    void Update()
    {
        if (!this.Initialized)
            this.Initialize();
        if (this.camera == null)
            this.camera = TheForest.Utils.LocalPlayer.MainCam;
        if (this.camera != null)
        {
            if (ModAPI.Input.GetButton("ShowDevPicker"))
            {
                Ray r = this.camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
                // put the ray a little bit forward
                r.origin += this.camera.transform.forward * 1f;

                RaycastHit hit;

                if (Physics.Raycast(r, out hit, 100f))
                {
                    this.ScreenPosition = this.camera.WorldToScreenPoint(hit.point);
                    this.Title = hit.collider.name;
                    this.Path = "";
                    this.Layer = hit.collider.gameObject.layer + "";
                    Transform p = hit.collider.transform.parent;
                    int failsafe = 0;
                    while (p != null && failsafe < 100)
                    {
                        this.Path = p.name + " / ";
                        p = p.parent;
                        failsafe++;
                    }
                    Component[] c = hit.collider.GetComponents<Component>();
                    Components = new string[c.Length];
                    for (int i = 0; i < c.Length; i++)
                    {
                        Components[i] = c[i].GetType().Name;
                    }
                }
                else
                {
                    this.Title = "";
                }
            }
            else
            {
                this.Title = "";
            }
        }
            
    }

    protected Vector3 ScreenPosition;
    protected string Title;
    protected string Path;
    protected string Layer;
    protected string[] Components;
     
    void OnGUI()
    {
        if (this.Title != "")
        {
            float width = 300f;
            float height = 55f + Components.Length * 20f;
            float x = ScreenPosition.x - width / 2f;
            float y = ScreenPosition.y - height / 2f;
            GUI.Box(new Rect(x, y, width, height), "", this.background);
            GUI.Label(new Rect(x + 5, y - 20, width, 20), "Layer: "+this.Layer, this.whiteLabel);
            GUI.Label(new Rect(x + 5, y + 5, width, 20), this.Path, this.pathLabel);
            GUI.Label(new Rect(x + 5, y + 30, width, 20), this.Title, this.boldLabel);
            for (int i = 0; i < Components.Length; i++)
            {
                GUI.Label(new Rect(x + 5, y + 55 + 20 * i, width, 20), this.Components[i], this.componentLabel);
            }
        }
    }
}