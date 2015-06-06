using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Savegames
{
    class NewTitleScreen : TitleScreen
    {
        protected GameObject loadPanel;
        protected Transform backupFrom;
        protected Transform backupTo;
        protected bool savegamesShowed = false;

        protected void ShowSavegames()
        {
            if (!savegamesShowed)
            {
                backupFrom = startTween.from;
                backupTo = startTween.to;

                startTween.from = mainTween.from;
                startTween.to = mainTween.to;
                startTween.Play(true);
                loadTween.Play(true);

                savegamesShowed = true;
            }
        }

        protected void SavegamesFinished()
        {
            if (!savegamesShowed && backupFrom != null)
            {
                startTween.from = backupFrom;
                startTween.to = backupTo;
                loadPanel.transform.localPosition = new Vector3(-1000f, 0f, 0f);
            }
        }

        protected void HideSavegames()
        {
            savegamesShowed = false;
            startTween.Play(false);
            loadTween.Play(false);

        }

        protected TweenTransform startTween;
        protected TweenTransform loadTween;
        protected TweenTransform mainTween;

        protected override void OnEnable()
        {
            base.OnEnable();
            SavegameHolder.Initialize();
            Transform menu = transform.FindChild("Menu");
            Transform panelMain = null;
            Transform panelStart = null;
            int i = 0;
            for (i = 0; i < menu.childCount; i++)
            {
                Transform c = menu.GetChild(i);
                if (c.name == "Panel - Main")
                    panelMain = c;
                if (c.name == "Panel - NewGame/Continue")
                    panelStart = c;
            }

            startTween = panelStart.GetComponent<TweenTransform>();
            mainTween = panelMain.GetComponent<TweenTransform>();
            
            loadPanel = NGUITools.AddChild(menu.gameObject, panelStart);
            loadPanel.AddComponent<UIPanel>();
            loadTween = loadPanel.AddComponent<TweenTransform>();
            loadTween.from = startTween.from;
            loadTween.to = startTween.to;
            loadTween.tweenFactor = mainTween.tweenFactor;
            loadTween.duration = mainTween.duration;
            loadTween.SetOnFinished(new EventDelegate(SavegamesFinished));
            loadTween.ResetToBeginning();
            loadTween.enabled = false;

            loadPanel.name = "Panel - Load";
            loadPanel.transform.localPosition = new Vector3(-1000f, 0f, 0f);

            Transform continueButton = null;
            for (i = 0; i < panelStart.childCount; i++)
                if (panelStart.GetChild(i).name == "Button - Continue (load)")
                    continueButton = panelStart.GetChild(i);

            UILabel l = continueButton.transform.GetChild(0).GetComponent<UILabel>();
            if (l != null)
                l.text = "Load Game";
            
            Transform backButton = panelStart.FindChild("Button - Back");
            backButton.GetComponent<UIButton>().onClick.Add(new EventDelegate(HideSavegames));
            
            for (i = loadPanel.transform.childCount - 1; i >= 0; i--)
                NGUITools.Destroy(loadPanel.transform.GetChild(i).gameObject);
            
            GameObject headLabel = NGUITools.AddChild(loadPanel, continueButton.gameObject);
            NGUITools.Destroy(headLabel.GetComponent<UIButton>());
            headLabel.GetComponentInChildren<UILabel>().text = "Choose savegame";
            headLabel.GetComponentInChildren<UILabel>().fontSize = 30;
            headLabel.GetComponentInChildren<UILabel>().fontStyle = FontStyle.Bold;
            headLabel.transform.localPosition = new Vector3(0f, 60f, 0f);

            List<string> displayNames = SavegameHolder.GetDisplayNames();
            i = 0;
            foreach (string displayName in displayNames)
            {
                GameObject newButton = NGUITools.AddChild(loadPanel, continueButton.gameObject);
                newButton.GetComponentInChildren<UILabel>().text = displayName;
                newButton.GetComponentInChildren<UILabel>().fontSize = 20;
                newButton.GetComponentInChildren<UILabel>().fontStyle = FontStyle.Normal;
                newButton.GetComponent<UIButton>().onClick.Clear();
                SavegameCallback callback = new SavegameCallback();
                callback.Receiver = this.gameObject;
                callback.CallbackName = "LoadSavegame";
                callback.DisplayName = displayName;
                newButton.GetComponent<UIButton>().onClick.Add(new EventDelegate(callback.Execute));

                newButton.transform.localPosition = new Vector3(0f, 60f - 35f * (i + 1), 0f);
                i++;
            }

            GameObject newBackButton = NGUITools.AddChild(loadPanel, backButton.gameObject);
            newBackButton.transform.localPosition = new Vector3(0f, 60f - 35f * (i + 1), 0f);
            newBackButton.GetComponent<UIButton>().onClick.Clear();
            newBackButton.GetComponent<UIButton>().onClick.Add(new EventDelegate(HideSavegames));

            UIPlayTween[] tweens = newBackButton.GetComponents<UIPlayTween>();
            for (i = 0; i < 2; i++)
            {
                if (i == 0)
                    tweens[i].tweenTarget = panelStart.gameObject;
                if (i == 1)
                    tweens[i].tweenTarget = loadPanel;
            }
        }
        
        public override void OnLoad()
        {
            ShowSavegames();
        }

        public void LoadSavegame(string name)
        {
            ModAPI.Log.Write("LOADSAVEGAME => " + name);
            SavegameHolder.Load(name);
            ModAPI.Log.Write("1LOADSAVEGAME => " + name);
            TitleScreen.StartGameSetup.Type = TitleScreen.GameSetup.InitType.Continue;
            ModAPI.Log.Write("2LOADSAVEGAME => " + name);
            LoadSave.ShouldLoad = true;
            ModAPI.Log.Write("3LOADSAVEGAME => " + name); 
            this.MyLoader.SetActive(true);
            ModAPI.Log.Write("4LOADSAVEGAME => " + name); 
            this.MenuRoot.gameObject.SetActive(false);
            ModAPI.Log.Write("5LOADSAVEGAME => " + name);
        }
    }
}
