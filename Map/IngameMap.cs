using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Map
{
    public class IngameMap : MonoBehaviour
    {
        public class MarkerSetting
        {
            public string ID;
            public string Label;
            public int Texture;
            public Color Color;
            public string Category;
            public bool Selected = true;
        }

        public static Vector2 WorldToMapFactor = new Vector2(5600f, 3500f);
        public static Vector2 WorldToMapOffset = new Vector2(0f, -20f);

        public static Rect GetTextureCoords(int index)
        {
            int x = index % 6;
            int y = Mathf.FloorToInt((float)index / 6f);
            float ux = x / 6f;
            float uy = 1f - (float)(y + 1) / 6f;
            return new Rect(ux, uy, 1f / 6f, 1f / 6f);
        }

        public static Vector2 WorldToMap(Vector3 world)
        {
            return new Vector2((-world.z + WorldToMapOffset.x) / WorldToMapFactor.x, (-world.x + WorldToMapOffset.y) / WorldToMapFactor.y);
        }

        public static Dictionary<string, MarkerSetting> markerSettings = new Dictionary<string, MarkerSetting>()
        {
            {"Big tree", new MarkerSetting()
                {
                    Label = "Big tree",
                    Texture = 0,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Good spot", new MarkerSetting()
                {
                    Label = "Good spot",
                    Texture = 1,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Passenger", new MarkerSetting()
                {
                    ID = "Passenger",
                    Label = "Passenger",
                    Texture = 2,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Sharks", new MarkerSetting()
                {
                    Label = "Sharks",
                    Texture = 3,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Bedroll", new MarkerSetting()
                {
                    Label = "Bedroll",
                    Texture = 4,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Tent", new MarkerSetting()
                {
                    Label = "Tent",
                    Texture = 5,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Boat", new MarkerSetting()
                {
                    Label = "Boat",
                    Texture = 6,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Plane", new MarkerSetting()
                {
                    Label = "Plane",
                    Texture = 7,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Anchor", new MarkerSetting()
                {
                    Label = "Anchor",
                    Texture = 8,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Suitcase", new MarkerSetting()
                {
                    Label = "Suitcase",
                    Texture = 9,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Cave", new MarkerSetting()
                {
                    Label = "Cave",
                    Texture = 10,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Collectible", new MarkerSetting()
                {
                    Label = "Collectible",
                    Texture = 11,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Explodable", new MarkerSetting()
                {
                    Label = "Explodable",
                    Texture = 12,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Crate", new MarkerSetting()
                {
                    Label = "Crate",
                    Texture = 13,
                    Color = new Color(0.75f, 0.75f, 0.75f),
                    Category = "General"
                }
            },
            {"Berries", new MarkerSetting()
                {
                    Label = "Berries",
                    Texture = 14,
                    Color = new Color(0.5f,1f,0.5f),
                    Category = "Food"
                }
            },
            {"Mushrooms", new MarkerSetting()
                {
                    Label = "Mushrooms",
                    Texture = 16,
                    Color = new Color(0.5f,1f,0.5f),
                    Category = "Food"
                }
            },
            {"Fish", new MarkerSetting()
                {
                    Label = "Fish",
                    Texture = 15,
                    Color = new Color(0.5f,1f,0.5f),
                    Category = "Food"
                }
            },
            {"Medicine", new MarkerSetting()
                {
                    Label = "Medicine",
                    Texture = 17,
                    Color = new Color(0.5f,1f,0.5f),
                    Category = "Food"
                }
            },
            {"Native camp", new MarkerSetting()
                {
                    Label = "Native camp",
                    Texture = 18,
                    Color = Color.red,
                    Category = "Natives"
                }
            },
            {"Flashlight", new MarkerSetting()
                {
                    Label = "Flashlight",
                    Texture = 19,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Booze / Water", new MarkerSetting()
                {
                    Label = "Booze / Water",
                    Texture = 20,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Flare", new MarkerSetting()
                {
                    Label = "Flare",
                    Texture = 21,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Money", new MarkerSetting()
                {
                    Label = "Money",
                    Texture = 22,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Rope", new MarkerSetting()
                {
                    Label = "Rope",
                    Texture = 23,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Circuit", new MarkerSetting()
                {
                    Label = "Circuit",
                    Texture = 24,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Rebreather", new MarkerSetting()
                {
                    Label = "Rebreather",
                    Texture = 25,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Air Canister", new MarkerSetting()
                {
                    Label = "Air Canister",
                    Texture = 26,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Pot", new MarkerSetting()
                {
                    Label = "Pot",
                    Texture = 27,
                    Color = new Color(0.75f,0.75f,1f),
                    Category = "Tools"
                }
            },
            {"Stick", new MarkerSetting()
                {
                    Label = "Stick",
                    Texture = 28,
                    Color = new Color(1f, 0.6f, 0f),
                    Category = "Weapons"
                }
            },
            {"Axe", new MarkerSetting()
                {
                    Label = "Axe",
                    Texture = 29,
                    Color = new Color(1f, 0.6f, 0f),
                    Category = "Weapons"
                }
            },
            {"Gun", new MarkerSetting()
                {
                    Label = "Gun",
                    Texture = 30,
                    Color = new Color(1f, 0.6f, 0f),
                    Category = "Weapons"
                }
            }
        };
        protected bool Opened = false;
        protected Map Overworld;
        protected Map Underworld;

        protected Texture2D background;
        protected Texture2D foreground;
        protected Vector2 BaseSize = new Vector2(200f, 200f);
        protected Vector2 Position = Vector2.zero;
        protected float Zoom = 1f;
        protected int texNum = 31;

        [ModAPI.Attributes.ExecuteOnGameStart]
        public static void Init()
        {
            GameObject g = new GameObject("__Map__");
            g.AddComponent<IngameMap>();
        }

        public bool DrawMarker(Map.Marker marker, float angle = 0f, float scale = 1f)
        {
            Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f) + Position * Zoom;
            Vector2 wholeSize = new Vector2(Screen.width, Screen.height) * Zoom;
            Vector2 mapPos = WorldToMap(marker.WorldPosition);
            mapPos.x *= wholeSize.x;
            mapPos.y *= wholeSize.y;
            float markerSize = GetMarkerSize() * scale;
            Rect texCoords = GetTextureCoords(marker.Class.Texture);
            Vector2 mPos = new Vector2(center.x + mapPos.x, center.y + mapPos.y);
            Rect markerRect = new Rect(mPos.x - markerSize / 2f, mPos.y - markerSize / 2f, markerSize, markerSize);
            GUI.color = marker.Class.Color;
            if (angle != 0f)
            {
                Matrix4x4 bkpMatrix = GUI.matrix;
                GUIUtility.RotateAroundPivot(180f + TheForest.Utils.LocalPlayer.Transform.rotation.eulerAngles.y, mPos);
                GUI.DrawTextureWithTexCoords(markerRect, Markers, texCoords);
                GUI.matrix = bkpMatrix;
            }
            else
            {
                GUI.DrawTextureWithTexCoords(markerRect, Markers, texCoords);
            }
            GUI.color = Color.white;
            return markerRect.Contains(Event.current.mousePosition);
        }

        Map.Marker playerMarker;
        Map currentMap;
        void OnGUI()
        {
            try
            {
                GUI.skin = ModAPI.GUI.Skin;
                GUI.color = Color.white;
                if (Opened)
                {
                    GUI.DrawTexture(new Rect(0f, 0f, Camera.main.pixelWidth, Camera.main.pixelHeight), background);

                    if (currentMap.Textures != null && currentMap.Textures.Length > 0)
                    {
                        Vector2 wholeSize = new Vector2(Screen.width, Screen.height) * Zoom;
                        if (wholeSize.x < wholeSize.y)
                            wholeSize.y = wholeSize.x;
                        else
                            wholeSize.x = wholeSize.y;
                        Vector2 size = wholeSize / Map.SPLIT;
                        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f) + Position * Zoom;
                        Vector2 start = center - wholeSize / 2f;
                        for (int x = 0; x < Map.SPLIT; x++)
                        {
                            for (int y = 0; y < Map.SPLIT; y++)
                            {
                                int index = x + (Map.SPLIT - y - 1) * Map.SPLIT;
                                GUI.DrawTexture(new Rect(start.x + size.x * x, start.y + size.y * y, size.x, size.y), currentMap.Textures[index]);
                            }
                        }
                        GUI.DrawTexture(new Rect(0, 0, 500, 500), currentMap.Texture);

                        List<Map.Marker> tooltip = new List<Map.Marker>();
                        for (int i = 0; i < currentMap.Markers.Count; i++)
                        {
                            if (currentMap.Markers[i].Class.Selected)
                            {
                                if (DrawMarker(currentMap.Markers[i]))
                                {
                                    tooltip.Add(currentMap.Markers[i]);
                                }
                            }
                        }
                        playerMarker.WorldPosition = TheForest.Utils.LocalPlayer.Transform.position;
                        DrawMarker(playerMarker, 180f + TheForest.Utils.LocalPlayer.Transform.rotation.eulerAngles.y, 2f);
                        if (tooltip.Count > 0)
                        {
                            Vector2 tooltipStart = new Vector2(Event.current.mousePosition.x - 125f, Event.current.mousePosition.y + 5f);
                            float _height = tooltip.Count * 30f + 5f;
                            GUI.Box(new Rect(tooltipStart.x, tooltipStart.y, 120f, _height), "");
                            float ty = 0f;
                            for (int j = 0; j < tooltip.Count; j++)
                            {
                                Rect tnr = new Rect(tooltipStart.x, tooltipStart.y + ty, 120f, 30f);
                                GUI.color = new Color(tooltip[j].Class.Color.r, tooltip[j].Class.Color.g, tooltip[j].Class.Color.b, 0.2f);
                                GUI.DrawTexture(tnr, foreground);
                                GUI.color = tooltip[j].Class.Color;
                                tnr = new Rect(tooltipStart.x + 5f, tooltipStart.y + ty + 5f, 20f, 20f);
                                GUI.DrawTextureWithTexCoords(tnr, Markers, GetTextureCoords(tooltip[j].Class.Texture));
                                GUI.color = Color.white;
                                tnr = new Rect(tooltipStart.x + 30f, tooltipStart.y + ty + 5f, 90f, 30f);
                                GUI.Label(tnr, tooltip[j].Class.Label);
                                ty += 30f;
                            }
                        }
                        /*WorldToMapFactor.x = float.Parse(GUI.TextField(new Rect(10, 10, 200, 20), WorldToMapFactor.x + ""));
                        WorldToMapFactor.y = float.Parse(GUI.TextField(new Rect(10, 40, 200, 20), WorldToMapFactor.y + ""));
                        WorldToMapOffset.x = float.Parse(GUI.TextField(new Rect(10, 70, 200, 20), WorldToMapOffset.x + ""));
                        WorldToMapOffset.y = float.Parse(GUI.TextField(new Rect(10, 100, 200, 20), WorldToMapOffset.y + ""));
                        */

                        float height = 0f;
                        int nn = 0;
                        foreach (MarkerCategory category in Categories.Values)
                        {
                            height += 20f;
                            if (category.Selected)
                            {
                                nn = 0;
                                foreach (MarkerSetting setting in category.Markers)
                                {
                                    if (nn == 0)
                                        height += 20f;
                                    nn++;
                                    if (nn >= 2)
                                    {
                                        nn = 0;
                                    }
                                }
                            }
                        }
                        GUI.Box(new Rect(10, Screen.height - (height + 30f) - 10f, 200f, height + 35f), "Filter", GUI.skin.window);
                        int _y = 0;
                        int _x = 0;
                        foreach (MarkerCategory category in Categories.Values)
                        {
                            _x = 0;
                            string categoryName = category.Markers[0].Category;
                            category.Selected = GUI.Toggle(new Rect(10, Screen.height - (height) - 10f + _y, 200f, 20f), category.Selected, categoryName, GUI.skin.button);
                            _y += 20;
                            if (category.Selected)
                            {
                                foreach (MarkerSetting setting in category.Markers)
                                {
                                    Rect nr = new Rect(10 + _x, Screen.height - (height) - 10f + _y, 100f, 20f);
                                    GUI.color = new Color(category.Color.r, category.Color.g, category.Color.b, setting.Selected ? 0.2f : 0f);
                                    GUI.DrawTexture(nr, foreground);
                                    GUI.color = category.Color;
                                    nr = new Rect(10 + _x, Screen.height - (height) - 10f + _y, 20f, 20f);
                                    GUI.DrawTextureWithTexCoords(nr, Markers, GetTextureCoords(setting.Texture));
                                    GUI.color = Color.white;
                                    nr = new Rect(35 + _x, Screen.height - (height) - 10f + _y, 65f, 20f);
                                    setting.Selected = GUI.Toggle(nr, setting.Selected, setting.Label, GUI.skin.label);
                                    _x += 100;
                                    if (_x >= 200)
                                    {
                                        _x = 0;
                                        _y += 20;
                                    }
                                }
                                if (_x == 100)
                                    _y += 20;
                            }
                        }

                        if (Event.current.type == EventType.MouseDown)
                        {
                            Drag = true;
                            LastMousePos = Event.current.mousePosition;
                        }
                        else if (Event.current.type == EventType.MouseDrag)
                        {
                            Vector2 move = Event.current.mousePosition - LastMousePos;
                            float w = Mathf.Min(Screen.width, Screen.height);
                            Position += move / Zoom;
                            Position.x = Mathf.Clamp(Position.x, w / -2f, w / 2f);
                            Position.y = Mathf.Clamp(Position.y, w / -2f, w / 2f);
                            LastMousePos = Event.current.mousePosition;
                        }
                        else if (Event.current.type == EventType.MouseUp)
                        {
                            Drag = false;
                        }

                        if (Event.current.type == EventType.ScrollWheel)
                        {
                            Zoom = Mathf.Clamp(Zoom + Event.current.delta.y / -20f, 1f, 3f);
                        }

                        GUIContent con = new GUIContent("Thanks to http://theforestmap.com/ for providing the map data.");
                        Vector2 cons = GUI.skin.label.CalcSize(con);
                        GUI.color = Color.black;
                        GUI.Label(new Rect(Screen.width - 5f - cons.x, Screen.height - 25f, cons.x + 10f, cons.y + 10f), con);
                        GUI.color = Color.white;
                        GUI.Label(new Rect(Screen.width - 6f - cons.x, Screen.height - 26f, cons.x + 10f, cons.y + 10f), con);

                    }

                    if (currentMap.Loading)
                    {
                        if (currentMap.Textures == null || currentMap.Textures.Length == 0)
                        {
                            string loadingLabel = "Loading...";

                            // show big loader
                            Vector2 s = GUI.skin.label.CalcSize(new GUIContent(loadingLabel));
                            GUI.Label(new Rect(Screen.width / 2f - s.x / 2f, Screen.height / 2f - s.y - 5f, s.x + 10, s.y + 10), loadingLabel, WhiteLabel);

                            GUI.DrawTexture(new Rect(Screen.width / 4f, Screen.height / 2f + 1, (Screen.width / 2f) * currentMap.Progress, 2f), foreground);

                            string percentageLabel = currentMap.CurrentTask + ": " + Mathf.FloorToInt(currentMap.Progress * 100f) + "% (" + Mathf.FloorToInt(currentMap.BytesLoaded) + "kb / " + Mathf.FloorToInt(currentMap.BytesTotal / 1024f) + "kb)";
                            s = GUI.skin.label.CalcSize(new GUIContent(percentageLabel));
                            GUI.Label(new Rect(Screen.width / 2f - s.x / 2f, Screen.height / 2f + 2f, s.x + 10, s.y + 10), percentageLabel, WhiteLabel);
                            GUI.DrawTexture(new Rect(Screen.width / 4f, Screen.height / 2f, Screen.width / 2f, 1f), foreground);
                        }
                        else
                        {
                            string loadingLabel = "Loading...";

                            // show small loader
                            Vector2 s = GUI.skin.label.CalcSize(new GUIContent(loadingLabel));
                            GUI.Label(new Rect(Screen.width - 110f - s.x / 2f, Screen.height - 40f - s.y - 5f, s.x + 10, s.y + 10), loadingLabel, WhiteLabel);

                            GUI.DrawTexture(new Rect(Screen.width - 210f, Screen.height - 40f + 1, (200f) * currentMap.Progress, 2f), foreground);

                            string percentageLabel = currentMap.CurrentTask + ": " + Mathf.FloorToInt(currentMap.Progress * 100f) + " % (" + Mathf.FloorToInt(currentMap.BytesLoaded) + "kb / " + Mathf.FloorToInt(currentMap.BytesTotal / 1024f) + "kb)";
                            s = GUI.skin.label.CalcSize(new GUIContent(percentageLabel));
                            GUI.Label(new Rect(Screen.width - 110f - s.x / 2f, Screen.height - 40f + 2f, s.x + 10, s.y + 10), percentageLabel, WhiteLabel);
                            GUI.DrawTexture(new Rect(Screen.width - 210f, Screen.height - 40f, (200f), 1f), foreground);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }

        public float GetMarkerSize()
        {
            return 16f * Zoom;
        }

        protected bool Drag = false;
        protected Vector2 LastMousePos = Vector2.zero;

        protected Downloader overworldMapLoader;
        protected Downloader underworldMapLoader;
        protected GUIStyle WhiteLabel;
        protected float ShowPhase = 0f;
        protected Texture2D Markers;
        protected Dictionary<string, MarkerCategory> Categories;

        public class MarkerCategory
        {
            public bool Selected = false;
            public List<MarkerSetting> Markers = new List<MarkerSetting>();
            public Color Color;
        }

        void Start()
        {
            try
            {
                Categories = new Dictionary<string, MarkerCategory>();
                foreach (MarkerSetting setting in markerSettings.Values)
                {
                    if (!Categories.ContainsKey(setting.Category))
                    {
                        Categories.Add(setting.Category, new MarkerCategory());
                        Categories[setting.Category].Color = setting.Color;
                    }
                    Categories[setting.Category].Markers.Add(setting);
                }
                
                playerMarker = new Map.Marker()
                {
                    Class = new MarkerSetting()
                    {
                        ID = "Player",
                        Color = Color.white,
                        Label = "You",
                        Texture = 31,
                        Category = "Player"
                    },
                    Description = "You",
                    WorldPosition = Vector3.zero,
                };

                Markers = ModAPI.Resources.GetTexture("Markers.png");
                background = new Texture2D(2, 2);
                background.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
                background.SetPixel(1, 0, new Color(0f, 0f, 0f, 0.7f));
                background.SetPixel(0, 1, new Color(0f, 0f, 0f, 0.7f));
                background.SetPixel(1, 1, new Color(0f, 0f, 0f, 0.7f));
                background.filterMode = FilterMode.Point;
                background.Apply();

                foreground = new Texture2D(2, 2);
                foreground.SetPixel(0, 0, new Color(1f, 1f, 1f, 1f));
                foreground.SetPixel(1, 0, new Color(1f, 1f, 1f, 1f));
                foreground.SetPixel(0, 1, new Color(1f, 1f, 1f, 1f));
                foreground.SetPixel(1, 1, new Color(1f, 1f, 1f, 1f));
                foreground.filterMode = FilterMode.Point;
                foreground.Apply();

                WhiteLabel = new GUIStyle(ModAPI.GUI.Skin.label);
                WhiteLabel.normal.textColor = Color.white;

                Overworld = new Map("http://theforestmap.com/map/map-4096.jpg", "http://theforestmap.com/map/md5.php?map=forest", "http://theforestmap.com/inc/api/?json&map=forest", "Mods/Map/Cache/Overworld/map.jpg");
                Underworld = new Map("http://theforestmap.com/map/cave-4096.jpg", "http://theforestmap.com/map/md5.php?map=cave", "http://theforestmap.com/inc/api/?json&map=cave", "Mods/Map/Cache/Underworld/map.jpg");
            }
            catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }
        
        protected bool overworldParsed = false;
        protected bool underworldParsed = false;
        protected Texture2D overworldTexture;
        protected Texture2D underworldTexture;

        void Update()
        {
            try
            {
                Overworld.Update();
                Underworld.Update();
                if (ModAPI.Input.GetButtonDown("OpenMap"))
                {
                    if (TheForest.Utils.Scene.Atmosphere.InACave)
                        currentMap = Underworld;
                    else
                        currentMap = Overworld;
                    Opened = !Opened;
                    ShowPhase = 0f;
                    //Zoom = 1f;
                    //Position = Vector2.zero;
                }
                if (Opened)
                {
                    if (currentMap.TexturesLoaded)
                    {
                        if (ShowPhase < 1f)
                            ShowPhase += Time.unscaledDeltaTime;
                    }
                    TheForest.Utils.LocalPlayer.FpCharacter.LockView();
                }
                else
                {
                    if (visible)
                        TheForest.Utils.LocalPlayer.FpCharacter.UnLockView();
                }
                this.visible = this.Opened;
            }
            catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }

        protected bool visible = false;
    }
}
