using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenu
{
    class FPController : FirstPersonCharacter
    {
        protected float baseWalkSpeed;
        protected float baseRunSpeed;
        protected float baseJumpHeight;
        protected float baseCrouchSpeed;
        protected float baseStrafeSpeed;
        protected float baseSwimmingSpeed;
        protected float baseGravity;
        protected float baseMaxVelocityChange;
        protected float baseMaximumVelocity;


        protected void BaseValues()
        {
            this.baseWalkSpeed = this.walkSpeed;
            this.baseRunSpeed = this.runSpeed;
            this.baseJumpHeight = this.jumpHeight;
            this.baseCrouchSpeed = this.crouchSpeed;
            this.baseStrafeSpeed = this.strafeSpeed;
            this.baseSwimmingSpeed = this.swimmingSpeed;
            this.baseMaxVelocityChange = maxVelocityChange;
            this.baseMaximumVelocity = maximumVelocity;
            this.baseGravity = this.gravity;
        }

        protected override void Start()
        {
            base.Start();
            BaseValues();
        }

        protected override void FixedUpdate()
        {
            this.walkSpeed = baseWalkSpeed * CheatMenuComponent.SpeedMultiplier;
            this.runSpeed = baseRunSpeed * CheatMenuComponent.SpeedMultiplier;
            this.jumpHeight = baseJumpHeight * CheatMenuComponent.JumpMultiplier;
            this.crouchSpeed = baseCrouchSpeed * CheatMenuComponent.SpeedMultiplier;
            this.strafeSpeed = baseStrafeSpeed * CheatMenuComponent.SpeedMultiplier;
            this.swimmingSpeed = baseSwimmingSpeed * CheatMenuComponent.SpeedMultiplier;

            if (CheatMenuComponent.FlyMode && !PushingSled)
            {
                this.rb.useGravity = false;
                if (CheatMenuComponent.NoClip)
                {
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<SphereCollider>().enabled = false;
                }
                else
                {
                    GetComponent<CapsuleCollider>().enabled = true;
                    GetComponent<SphereCollider>().enabled = true;
                }

                bool button1 = TheForest.Utils.Input.GetButton("Crouch");
                bool button2 = TheForest.Utils.Input.GetButton("Run");
                bool button3 = TheForest.Utils.Input.GetButton("Jump");
                float multiplier = baseWalkSpeed;
                this.gravity = 0f;
                if (button2) multiplier = baseRunSpeed;
                
                Vector3 vector3 = Camera.main.transform.rotation * (
                    new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"),
                    0f,
                    TheForest.Utils.Input.GetAxis("Vertical")
                ) * multiplier * CheatMenuComponent.SpeedMultiplier);
                Vector3 velocity = this.rb.velocity;
                if (button3) velocity.y -= multiplier * CheatMenuComponent.SpeedMultiplier;
                if (button1) velocity.y += multiplier * CheatMenuComponent.SpeedMultiplier;
                Vector3 force = vector3 - velocity;
                this.rb.AddForce(force, ForceMode.VelocityChange);
            }
            else
            {
                this.rb.useGravity = true;
                /*GetComponent<CapsuleCollider>().enabled = true;
                GetComponent<SphereCollider>().enabled = true;*/
                this.gravity = baseGravity;
                base.FixedUpdate();
            }
        }
    }
}