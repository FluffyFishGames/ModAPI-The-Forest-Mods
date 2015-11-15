using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
    
public class RemoveBuildingsBehaviour : MonoBehaviour
{
    protected List<string> buildingNames;

    protected GameObject removeClone;
    protected UITexture removeCloneTexture;
    protected bool ShowRemoveIcon = false;
    protected UILabel label;
    protected bool Initialized = false;
    protected Camera camera;

    [ModAPI.Attributes.ExecuteOnGameStart]
    public static void Init()
    {
        GameObject g = new GameObject("__RemoveBuildings__");
        g.AddComponent<RemoveBuildingsBehaviour>();
    }
        
    void Initialize()
    {
        Initialized = true;
        buildingNames = new List<string>()
        {
            "Ex_EffigyBuilt",
            "Trap_SpikeWall",
            "Trap_Deadfall",
            "Trap_TripWire_Explosive",
            "Trap_Rabbit",
            "Ex_RockFenceChunkBuilt",
            "Bed_Built",
            "WalkwayStraightBuilt",
            "Ex_RoofBuilt",
            "TreesapCollectorBuilt",
            "Target_Built",
            "Ex_FloorBuilt",
            "Ex_FoundationBuilt",
            "Ex_StairsBuilt",
            "Ex_PlatformBuilt",
            "Ex_WallDefensiveChunkBuilt",
            "Ex_WallChunkBuilt",
            "Ex_StickFenceChunkBuilt",
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

    void Update()
    {
        if (!Initialized)
            Initialize();
        if (this.camera == null)
            this.camera = TheForest.Utils.LocalPlayer.MainCam;
        if (this.camera != null)
        {
            try
            {
                Ray r = new Ray(camera.transform.position + camera.transform.forward * 1f, camera.transform.forward);
                RaycastHit[] hits = Physics.RaycastAll(r, 5f);
                if (hits.Length == 0)
                    removeCloneTexture.gameObject.SetActive(false);

                foreach (RaycastHit hitInfo in hits)
                {
                    Transform t = hitInfo.collider.transform;
                    bool found = false;
                    while (!found) 
                    {
                        if (t == null) break;
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
                        break;
                    }
                    else
                    {
                        ShowRemoveIcon = false;
                        removeCloneTexture.gameObject.SetActive(false);
                    }
                }
            }
            catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }
    }
}