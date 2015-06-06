using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Savegames
{
    class NewHudGui : HudGui
    {
        public static bool SaveOpened = false;
        protected GameObject PausePanel;
        protected GameObject OverwritePanel;
        protected GameObject DescriptionLabel;
        protected GameObject MainPanel;
        protected GameObject BackButton;
        protected GameObject CreateButton;
        protected GameObject continueButton;
        protected GameObject input;
        protected GameObject warning;
        
        protected int c = 0;
        
        protected override void Start()
        {
            base.Start();
            SavegameHolder.OnSavegameAdded += Create;
            
            for (int i = 0; i < this.PauseMenu.transform.childCount; i++)
            {
                Transform t = this.PauseMenu.transform.GetChild(i);
                if (t.name == "Panel - Main")
                {
                    MainPanel = t.gameObject;
                    break;
                }
            }

            Transform window = MainPanel.transform.GetChild(0);
            continueButton = null;
            for (int i = 0; i < window.childCount; i++)
            {
                Transform t = window.GetChild(i);
                if (t.name == "Button - Continue")
                {
                    continueButton = t.gameObject;
                    break;
                }
            }
            
            PausePanel = NGUITools.AddChild(MainPanel.transform.parent.gameObject, MainPanel);
            PausePanel.name = "SavePanel";
            PausePanel.AddComponent<UIWidget>().pivot = UIWidget.Pivot.Left;
            PausePanel.transform.localPosition = new Vector3(-600f, 200f, 0f);
            NGUITools.Destroy(PausePanel.GetComponent<TweenTransform>());
            Destroy(PausePanel.GetComponent<MenuMain>());
            
            Transform PauseWindow = PausePanel.transform;
            
            for (int i = PauseWindow.childCount - 1; i >= 0; i--)
                Destroy(PauseWindow.GetChild(i).gameObject);
            
            GameObject HeaderLabel = NGUITools.AddChild(PauseWindow.gameObject, continueButton.transform.GetChild(0).gameObject);
            HeaderLabel.GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            HeaderLabel.GetComponent<UILabel>().text = "Save game";
            HeaderLabel.transform.localPosition = new Vector3(9.1f, 28.3f, 0f);

            List<string> displayNames = SavegameHolder.GetDisplayNames();
            c = 0;
            foreach (string displayName in displayNames)
            {
                GameObject savegame = NGUITools.AddChild(PauseWindow.gameObject, continueButton);
                UIWidget w = savegame.gameObject.AddComponent<UIWidget>();
                savegame.GetComponent<BoxCollider>().size = new Vector3(8000f, 15f, 1f);
                w.pivot = UIWidget.Pivot.Left;
                w.autoResizeBoxCollider = true;
                savegame.GetComponent<UIButton>().onClick.Clear();
                SavegameCallback callback = new SavegameCallback() {
                    Receiver = this.gameObject,
                    CallbackName = "Save",
                    DisplayName = displayName
                };
                savegame.GetComponent<UIButton>().onClick.Add(new EventDelegate(callback.Execute));
                savegame.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
                savegame.transform.GetChild(0).GetComponent<UILabel>().fontSize = 20;
                savegame.transform.GetChild(0).GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
                savegame.transform.GetChild(0).GetComponent<UILabel>().alignment = NGUIText.Alignment.Automatic;
                savegame.transform.GetChild(0).GetComponent<UILabel>().text = displayName;
                savegame.transform.GetChild(0).GetComponent<UILabel>().autoResizeBoxCollider = true;
                
                savegame.transform.localPosition = new Vector3(9.1f, 28.3f - (c + 1) * 30f, 0f);
                savegame.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
                c++;
            }

            input = NGUITools.AddChild(PauseWindow.gameObject);
            GameObject inputLabel = NGUITools.AddChild(input, continueButton.transform.GetChild(0).gameObject);
            
            UIInput inputField = input.AddComponent<UIInput>();
            inputField.onChange.Add(new EventDelegate(CheckName));
            UIWidget sprite = input.AddComponent<UIWidget>();
            sprite.autoResizeBoxCollider = true;
            BoxCollider collider = input.AddComponent<BoxCollider>();
            collider.size = new Vector3(8000f, 15f, 1f);
            inputField.inputType = UIInput.InputType.Standard;
            inputField.defaultText = "New savegame name";
            inputField.label = inputLabel.GetComponent<UILabel>();
            inputField.label.fontSize = 20;
            inputField.label.text = "New savegame name";
            inputField.label.pivot = UIWidget.Pivot.Left;
            inputField.label.autoResizeBoxCollider = true;
            inputField.transform.localPosition = new Vector3(9.1f, 28.3f - (c + 1) * 30f, 0f);
            inputField.label.transform.localPosition = new Vector3(0f, 0f, 0f);

            warning = NGUITools.AddChild(PauseWindow.gameObject, continueButton.transform.GetChild(0).gameObject);
            warning.GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            warning.GetComponent<UILabel>().color = new Color(255, 122, 79);
            warning.GetComponent<UILabel>().effectColor = new Color(255, 122, 79);
            warning.GetComponent<UILabel>().fontSize = 20;
            warning.GetComponent<UILabel>().text = "Only alphanumeric characters are allowed!";
            warning.transform.localPosition = new Vector3(9.1f, 28.3f - (c + 2) * 30f, 0f);
            warning.SetActive(false);

            BackButton = NGUITools.AddChild(PauseWindow.gameObject, continueButton);
            BackButton.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f); 
            BackButton.transform.GetChild(0).GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            BackButton.transform.GetChild(0).GetComponent<UILabel>().text = "Back";
            BackButton.transform.GetChild(0).GetComponent<UILabel>().autoResizeBoxCollider = true;
            BackButton.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
            BackButton.GetComponent<UIButton>().onClick.Clear();
            BackButton.GetComponent<UIButton>().onClick.Add(new EventDelegate(CloseSave));
            
            CreateButton = NGUITools.AddChild(PauseWindow.gameObject, continueButton);
            CreateButton.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f); 
            CreateButton.transform.GetChild(0).GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            CreateButton.transform.GetChild(0).GetComponent<UILabel>().text = "Create";
            CreateButton.transform.GetChild(0).GetComponent<UILabel>().autoResizeBoxCollider = true;
            CreateButton.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
            BackButton.transform.localPosition = new Vector3(9.1f, 28.3f - (c + 2) * 30f, 0f);
            
            CreateButton.transform.localPosition = new Vector3(200f, 28.3f - (c + 2) * 30f, 0f);
            BackButton.GetComponent<BoxCollider>().size = new Vector3(100f, 20f, 1f); 
            CreateButton.GetComponent<BoxCollider>().size = new Vector3(100f, 20f, 1f);
            CreateButton.GetComponent<UIButton>().onClick.Clear();
            CreateButton.GetComponent<UIButton>().onClick.Add(new EventDelegate(SaveName));
            CreateButton.SetActive(false);
            NGUITools.SetActive(PausePanel, false);
            NGUITools.SetDirty(PausePanel);

            OverwritePanel = NGUITools.AddChild(MainPanel.transform.parent.gameObject, MainPanel);
            OverwritePanel.AddComponent<UIWidget>().pivot = UIWidget.Pivot.Left;
            OverwritePanel.transform.localPosition = new Vector3(-600f, 0f, 0f);
            NGUITools.Destroy(OverwritePanel.GetComponent<TweenTransform>());
            Destroy(OverwritePanel.GetComponent<MenuMain>());
            
            for (int i = OverwritePanel.transform.childCount - 1; i >= 0; i--)
                Destroy(OverwritePanel.transform.GetChild(i).gameObject);
            GameObject TitleLabel = NGUITools.AddChild(OverwritePanel, continueButton.transform.GetChild(0).gameObject);
            TitleLabel.GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            TitleLabel.GetComponent<UILabel>().text = "Overwrite savegame?";
            TitleLabel.transform.localPosition = new Vector3(9.1f, 28.3f, 0f);

            DescriptionLabel = NGUITools.AddChild(OverwritePanel, continueButton.transform.GetChild(0).gameObject);
            DescriptionLabel.GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            DescriptionLabel.GetComponent<UILabel>().text = "";
            DescriptionLabel.GetComponent<UILabel>().fontSize = 20;
            DescriptionLabel.transform.localPosition = new Vector3(9.1f, -1.7f, 0f);

            GameObject BackButton2 = NGUITools.AddChild(OverwritePanel, continueButton);
            BackButton2.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            BackButton2.transform.GetChild(0).GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            BackButton2.transform.GetChild(0).GetComponent<UILabel>().text = "No";
            BackButton2.transform.GetChild(0).GetComponent<UILabel>().autoResizeBoxCollider = true;
            BackButton2.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);

            GameObject CreateButton2 = NGUITools.AddChild(OverwritePanel, continueButton);
            CreateButton2.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
            CreateButton2.transform.GetChild(0).GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
            CreateButton2.transform.GetChild(0).GetComponent<UILabel>().text = "Yes";
            CreateButton2.transform.GetChild(0).GetComponent<UILabel>().autoResizeBoxCollider = true;
            CreateButton2.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
            BackButton2.transform.localPosition = new Vector3(9.1f, -31.7f, 0f);
            CreateButton2.transform.localPosition = new Vector3(200f, -31.7f, 0f);

            CreateButton2.GetComponent<UIButton>().onClick.Clear();
            CreateButton2.GetComponent<UIButton>().onClick.Add(new EventDelegate(Overwrite));
            BackButton2.GetComponent<UIButton>().onClick.Clear();
            BackButton2.GetComponent<UIButton>().onClick.Add(new EventDelegate(OpenPause));
            BackButton2.GetComponent<BoxCollider>().size = new Vector3(100f, 20f, 1f);
            CreateButton2.GetComponent<BoxCollider>().size = new Vector3(100f, 20f, 1f);
            OverwritePanel.SetActive(false);
            Reorder();
        }
        
        void Overwrite()
        {
            DoOverwrite = true;
            Save(this.OverwriteName);
        }

        void CheckName()
        {
            string name = this.input.GetComponent<UIInput>().value;
            Regex reg = new Regex("^[a-zA-Z0-9 ]+$");
            if (reg.IsMatch(name))
            {
                warning.SetActive(false);
                CreateButton.SetActive(true);
            }
            else
            {
                if (name == "")
                    warning.SetActive(false);
                else 
                    warning.SetActive(true);
                CreateButton.SetActive(false);
            }
            Reorder();
        }

        void Reorder()
        {
            int k = warning.activeSelf?1:0;
            
            BackButton.transform.localPosition = new Vector3(9.1f, 20f - (c + 2 + k) * 30f, 0f);
            CreateButton.transform.localPosition = new Vector3(200f, 20f - (c + 2 + k) * 30f, 0f);
            warning.transform.localPosition = new Vector3(9.1f, 20f - (c + 2) * 30f, 0f); 
            input.transform.localPosition = new Vector3(9.1f, 20f - (c + 1) * 30f, 0f);
            

        }

        void SaveName()
        {
            string nameText = input.GetComponent<UIInput>().value;
            Save(nameText);
        }

        void Save(string name)
        {
            if (name != "")
            {
                List<string> displayNames = SavegameHolder.GetDisplayNames();
                bool already = false;
                for (int i = 0; i < displayNames.Count; i++)
                    if (displayNames[i].ToLower() == name.ToLower())
                        already = true;
                if (already)
                {
                    if (DoOverwrite)
                    {
                        DoOverwrite = false;
                        OverwriteName = "";
                        TheForest.Utils.LocalPlayer.Inventory.TogglePauseMenu();
                        SavegameHolder.Save(name);
                    }
                    else
                    {
                        ShowOverwritePrompt(name);
                    }
                }
                else
                {
                    SavegameHolder.Save(name);
                }
            }
        }

        protected string OverwriteName = "";
        protected bool DoOverwrite = false;

        void ShowOverwritePrompt(string name)
        {
            OverwriteName = name;
            OverwritePanel.SetActive(true);
            DescriptionLabel.GetComponent<UILabel>().text = "Do you really want to overwrite the savegame \"" + name + "\"?";
            PausePanel.SetActive(false);
        }

        protected static string CreateNewEntry = "";
        public void Create(string name)
        {
            CreateNewEntry = name;
            TheForest.Utils.Scene.HudGui.enabled = true;
        }

        protected override void Update()
        {
            this.TimmyStomach.gameObject.SetActive(true);
            this.TimmyStomachOutline.SetActive(true);
            if (CreateNewEntry != "")
            {
                string name = CreateNewEntry;
                if (continueButton == null)
                {
                    if (MainPanel == null)
                    {
                        for (int i = 0; i < TheForest.Utils.Scene.HudGui.PauseMenu.transform.childCount; i++)
                        {
                            Transform t = TheForest.Utils.Scene.HudGui.PauseMenu.transform.GetChild(i);
                            if (t.name == "Panel - Main")
                            {
                                MainPanel = t.gameObject;
                            }
                            if (t.name == "SavePanel")
                                PausePanel = t.gameObject;
                        }
                    }

                    Transform window = MainPanel.transform.GetChild(0);

                    for (int i = 0; i < window.childCount; i++)
                    {
                        Transform t = window.GetChild(i);
                        if (t.name == "Button - Continue")
                        {
                            continueButton = t.gameObject;
                            break;
                        }
                    }
                }
                GameObject savegame = NGUITools.AddChild(PausePanel, continueButton);
                UIWidget w = savegame.AddComponent<UIWidget>();
                savegame.GetComponent<BoxCollider>().size = new Vector3(8000f, 15f, 1f);
                w.pivot = UIWidget.Pivot.Left;
                w.autoResizeBoxCollider = true;

                savegame.GetComponent<UIButton>().onClick.Clear();
                SavegameCallback callback = new SavegameCallback()
                {
                    Receiver = this.gameObject,
                    CallbackName = "Save",
                    DisplayName = name
                };
                savegame.GetComponent<UIButton>().onClick.Add(new EventDelegate(callback.Execute));

                savegame.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
                savegame.transform.GetChild(0).GetComponent<UILabel>().fontSize = 20;
                savegame.transform.GetChild(0).GetComponent<UILabel>().pivot = UIWidget.Pivot.Left;
                savegame.transform.GetChild(0).GetComponent<UILabel>().alignment = NGUIText.Alignment.Automatic;
                savegame.transform.GetChild(0).GetComponent<UILabel>().text = name;
                savegame.transform.GetChild(0).GetComponent<UILabel>().autoResizeBoxCollider = true;

                savegame.transform.localPosition = new Vector3(9.1f, 28.3f - (c + 1) * 30f, 0f);
                savegame.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);

                c++;
                Reorder();
                CreateNewEntry = "";
            }
            base.Update();
        }

        public void CloseSave()
        {
            SaveOpened = false; 
            MainPanel.SetActive(true);
            PausePanel.SetActive(false);
            OverwritePanel.SetActive(false);
            TheForest.Utils.LocalPlayer.Inventory.TogglePauseMenu();
        }

        public void OpenPause()
        {
            this.gameObject.SetActive(true);
            SaveOpened = true;
            MainPanel.SetActive(false);
            PausePanel.SetActive(true);
            OverwritePanel.SetActive(false);
            CreateButton.SetActive(false);
            input.GetComponent<UIInput>().value = "";
            input.GetComponent<UIInput>().label.text = "New savegame name";
        }
    }
}
