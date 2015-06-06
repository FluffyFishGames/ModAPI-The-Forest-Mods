using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RemoveBuildings
{
    
    class NewLocalPlayer : TheForest.Utils.LocalPlayer
    {
        protected List<string> buildingNames;

        protected GameObject removeClone;
        protected UITexture removeCloneTexture;
        protected bool ShowRemoveIcon = false;
        protected UILabel label;

        void Update()
        {
            try
            {
                if (buildingNames == null)
                {
                    buildingNames = new List<string>()
        {
            "FireBuilt",
            "FireStandBuilt",
            "BonFireBuilt",
            "FireBuiltRockPit",
            "LeafHutBuilt",
            "ShelterBuilt",
            "LogCabinBuilt",
            "LogCabin_Small_Built",
            "TreeHouse_Built_MP",
            "TreeHouseChalet_Built_MP",
            "Stick_HolderBuilt",
            "LogHolderBuilt",
            "MultiSledBuilt",
            "rock_HolderBuilt",
            "WeaponRack",
            "HolderExplosives_Built",
            "MedicineCabinet_Built",
            "HolderSnacks_Built",
            "WallBuilt",
            "WallBuiltDefensive",
            "WallBuilt_Doorway",
            "WallBuilt_Window",
            "StairCaseBuilt",
            "TreePlatform_Built",
            "PlatformBridgeBuilt",
            "SpikeDefenseBuilt",
            "FoundationBuilt",
            "FloorBuilt",
            "WallExBuilt",
            "StickMarkerBuilt",
            "RopeBuilt",
            "WalkwayStraightBuilt",
            "WorkBenchBuilt",
            "GazeboBuilt",
            "Trap_Rabbit",
            "Trap_TripWire_Explosive",
            "Trap_Deadfall",
            "Trap_SpikeWall",
            "Trap_RopeBuilt",
            "RabbitCageBuilt",
            "GardenBuilt",
            "DryingRackBuilt",
            "WaterCollector_Built",
            "RaftBuilt",
            "HouseBoat_Small",
            "EffigyHead",
            "EffigyBigBuilt",
            "EffigySmallBuilt",
            "EffigyRainBuilt",
            "PlatformExBuilt"
        };
                }
                if (removeClone == null)
                {
                    if (TheForest.Utils.Scene.HudGui != null && TheForest.Utils.Scene.HudGui.DestroyIcon != null && TheForest.Utils.Scene.HudGui.DestroyIcon.gameObject != null)
                    {
                        GameObject MainPanel = null;
                        for (int i = 0; i < TheForest.Utils.Scene.HudGui.PauseMenu.transform.childCount; i++)
                        {
                            Transform t = TheForest.Utils.Scene.HudGui.PauseMenu.transform.GetChild(i);
                            if (t.name == "Panel - Main")
                            {
                                MainPanel = t.gameObject;
                                break;
                            }
                        }

                        Transform window = MainPanel.transform.GetChild(0);
                        GameObject continueButton = null;
                        for (int i = 0; i < window.childCount; i++)
                        {
                            Transform t = window.GetChild(i);
                            if (t.name == "Button - Continue")
                            {
                                continueButton = t.gameObject;
                                break;
                            }
                        }

                        removeClone = NGUITools.AddChild(TheForest.Utils.Scene.HudGui.DestroyIcon.transform.parent.gameObject, TheForest.Utils.Scene.HudGui.DestroyIcon.gameObject);
                        Destroy(removeClone.transform.GetChild(0).gameObject);
                        removeClone.transform.localPosition = TheForest.Utils.Scene.HudGui.DestroyIcon.transform.localPosition;
                        removeCloneTexture = removeClone.GetComponent<UITexture>();
                        removeCloneTexture.alpha = 1f;
                        removeCloneTexture.mainTexture = ModAPI.Resources.GetTexture("RemoveBuilding.png");
                        GameObject newLabel = NGUITools.AddChild(removeClone, continueButton.transform.GetChild(0).gameObject);
                        newLabel.GetComponent<UILabel>().text = ModAPI.Input.GetKeyBindingAsString("RemoveBuilding");
                        newLabel.transform.localPosition += new Vector3(0f, -70f, 0f);
                    }
                }
                else
                {
                    Ray r = new Ray(_mainCam.transform.position, _mainCam.transform.forward);
                    RaycastHit hitInfo = new RaycastHit();
                    int mask = 0;
                    mask += 1 << 25;
                    mask += 1 << 21;
                    mask += 1 << 28;
                    if (Physics.Raycast(r, out hitInfo, 4f, mask))
                    {
                        Transform t = hitInfo.collider.transform;
                        bool found = false;
                        while (!found)
                        {
                            if (t == null) break;
                            ModAPI.Log.Write(t.name);
                            foreach (string n in buildingNames)
                            {
                        
                                if (t.name.StartsWith(n))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (found) break;
                            t = t.parent;
                        }
                        if (found)
                        {
                            ShowRemoveIcon = true;
                            removeCloneTexture.gameObject.SetActive(true);
                            if (ModAPI.Input.GetButtonDown("RemoveBuilding"))
                            {
                                Destroy(t.gameObject);
                                return;
                            }
                        }
                        else
                        {
                            ShowRemoveIcon = false;
                            removeCloneTexture.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        removeCloneTexture.gameObject.SetActive(false);
                    }
                }
            }
            catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }

        void OnGUI()
        {
            /*if (cam != null)
            {
                float x = cam.pixelWidth * 0.6f;
                float y = cam.pixelHeight * 0.7f;
                float width = cam.pixelWidth * 0.06f;
                Texture2D tex = ModAPI.Resources.GetTexture("RemoveBuilding.png");
                UnityEngine.GUI.DrawTexture(new Rect(x,y,width,width), tex);
                UnityEngine.GUI.Label(new Rect(x + width * 0.8f, y + width * 0.8f, width * 0.2f, width * 0.2f), ModAPI.Input.GetKeyBindingAsString("RemoveBuilding"));
            }*/
        }
    }
}
