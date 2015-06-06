using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenu
{
    class Stats : PlayerStats
    {

        [ModAPI.Attributes.Priority(1000)]
        protected override void hitFallDown()
        {
            if (!CheatMenuComponent.GodMode)
                base.hitFallDown();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFire()
        {
            if (!CheatMenuComponent.GodMode)
                base.HitFire();
        }


        [ModAPI.Attributes.Priority(1000)]
        public override void hitFromEnemy(int getDamage)
        {
            if (!CheatMenuComponent.GodMode)
                base.hitFromEnemy(getDamage);
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void HitShark(int damage)
        {
            if (!CheatMenuComponent.GodMode)
                base.HitShark(damage);
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void FallDownDead()
        {
            if (!CheatMenuComponent.GodMode)
                base.FallDownDead();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void Fell()
        {
            if (!CheatMenuComponent.GodMode)
                base.Fell();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFromPlayMaker(int damage)
        {
            if (!CheatMenuComponent.GodMode)
                base.HitFromPlayMaker(damage);
        }

        protected override void Update()
        {
            if (CheatMenuComponent.GodMode)
            {
                this.IsBloody = false;
                this.Warm = true;
                this.IsCold = false;
                this.Health = 100f;
                this.Armor = 100;
                this.Fullness = 1f;
                this.Stamina = 100f;
                this.Energy = 100f;
                this.Hunger = 0;
                this.Thirst = 0;
                this.Starvation = 0;
            }
            base.Update();
        }
    }
}