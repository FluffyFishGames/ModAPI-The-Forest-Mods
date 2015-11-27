using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenu
{
    public class NewWeatherSystem : TheForest.World.WeatherSystem
    {
        protected float ResetCloudTime = 0f;

        protected override void TryRain()
        {
            if (CheatMenuComponent.FreezeWeather)
            {
                return;
            }
            base.TryRain();
        }

        protected override void Update()
        {
            if (ResetCloudTime > 0f)
            {
                ResetCloudTime -= UnityEngine.Time.deltaTime;
                if (ResetCloudTime <= 0f)
                {
                    CloudSmoothTime = 20f;
                }
            }
            if (CheatMenuComponent.ForceWeather >= 0)
            {
                TheForest.Utils.Scene.RainFollowGO.SetActive(false);
                TheForest.Utils.Scene.RainTypes.RainHeavy.SetActive(false);
                TheForest.Utils.Scene.RainTypes.RainMedium.SetActive(false);
                TheForest.Utils.Scene.RainTypes.RainLight.SetActive(false);
                TheForest.Utils.Scene.Clock.AfterStorm.SetActive(false);
                if (CheatMenuComponent.ForceWeather == 0)
                {
                    this.Raining = true;
                    this.RainDice = 1;
                    this.RainDiceStop = 2;
                    this.TryRain();
                    CloudSmoothTime = 1f;
                    ResetCloudTime = 2f;
                }
                if (CheatMenuComponent.ForceWeather == 1)
                {
                    this.Raining = false;
                    this.RainDice = 2;
                    this.RainDiceStop = 1;
                    this.TryRain();
                    CloudSmoothTime = 1f;
                    ResetCloudTime = 2f;
                }
                if (CheatMenuComponent.ForceWeather == 2)
                {
                    this.Raining = false;
                    this.RainDice = 3;
                    this.RainDiceStop = 1;
                    this.TryRain();
                    CloudSmoothTime = 1f;
                    ResetCloudTime = 2f;
                }
                if (CheatMenuComponent.ForceWeather == 3)
                {
                    this.Raining = false;
                    this.RainDice = 4;
                    this.RainDiceStop = 1;
                    this.TryRain();
                    CloudSmoothTime = 1f;
                    ResetCloudTime = 2f;
                }
                if (CheatMenuComponent.ForceWeather == 4)
                {
                    this.Raining = false;
                    this.RainDice = 5;
                    this.RainDiceStop = 1;
                    this.TryRain();
                    CloudSmoothTime = 1f;
                    ResetCloudTime = 2f;
                }
                CheatMenuComponent.ForceWeather = -1;
            }
            base.Update();
        }
    }
}
