using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheForest.Utils;
using UnityEngine;

namespace CheatMenu050
{
    internal class FPController : FirstPersonCharacter
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

        protected float baseMaxSwimVelocity;

        protected Collider[] AllChildColliders;

        protected Collider[] AllColliders;

        protected bool LastNoClip;

        protected bool LastFlyMode;

        protected void BaseValues()
        {
            this.baseMaxSwimVelocity = this.maxSwimVelocity;
            this.baseWalkSpeed = this.walkSpeed;
            this.baseRunSpeed = this.runSpeed;
            this.baseJumpHeight = this.jumpHeight;
            this.baseCrouchSpeed = this.crouchSpeed;
            this.baseStrafeSpeed = this.strafeSpeed;
            this.baseSwimmingSpeed = this.swimmingSpeed;
            this.baseMaxVelocityChange = this.maxVelocityChange;
            this.baseMaximumVelocity = this.maximumVelocity;
            this.baseGravity = this.gravity;
        }

        protected override void Start()
        {
            this.AllChildColliders = base.gameObject.GetComponentsInChildren<Collider>();
            this.AllColliders = base.gameObject.GetComponents<Collider>();
            base.Start();
            this.BaseValues();
        }

        protected override void FixedUpdate()
        {
            this.walkSpeed = this.baseWalkSpeed * CheatMenuComponent.SpeedMultiplier;
            this.runSpeed = this.baseRunSpeed * CheatMenuComponent.SpeedMultiplier;
            this.jumpHeight = this.baseJumpHeight * CheatMenuComponent.JumpMultiplier;
            this.crouchSpeed = this.baseCrouchSpeed * CheatMenuComponent.SpeedMultiplier;
            this.strafeSpeed = this.baseStrafeSpeed * CheatMenuComponent.SpeedMultiplier;
            this.swimmingSpeed = this.baseSwimmingSpeed * CheatMenuComponent.SpeedMultiplier;
            this.maxSwimVelocity = this.baseMaxSwimVelocity * CheatMenuComponent.SpeedMultiplier;
            if (!CheatMenuComponent.FreeCam)
            {
                if (CheatMenuComponent.FlyMode && !this.PushingSled)
                {
                    this.rb.useGravity = false;
                    if (CheatMenuComponent.NoClip)
                    {
                        if (!this.LastNoClip)
                        {
                            for (int i = 0; i < this.AllColliders.Length; i++)
                            {
                                this.AllColliders[i].enabled = false;
                            }
                            for (int j = 0; j < this.AllChildColliders.Length; j++)
                            {
                                this.AllChildColliders[j].enabled = false;
                            }
                            this.LastNoClip = true;
                        }
                    }
                    else if (this.LastNoClip)
                    {
                        for (int k = 0; k < this.AllColliders.Length; k++)
                        {
                            this.AllColliders[k].enabled = true;
                        }
                        for (int l = 0; l < this.AllChildColliders.Length; l++)
                        {
                            this.AllChildColliders[l].enabled = true;
                        }
                        this.LastNoClip = false;
                    }
                    bool button = TheForest.Utils.Input.GetButton("Crouch");
                    bool arg_19D_0 = TheForest.Utils.Input.GetButton("Run");
                    bool button2 = TheForest.Utils.Input.GetButton("Jump");
                    float num = this.baseWalkSpeed;
                    this.gravity = 0f;
                    if (arg_19D_0)
                    {
                        num = this.baseRunSpeed;
                    }
                    Vector3 arg_21F_0 = Camera.main.transform.rotation * (new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"), 0f, TheForest.Utils.Input.GetAxis("Vertical")) * num * CheatMenuComponent.SpeedMultiplier);
                    Vector3 velocity = this.rb.velocity;
                    if (button2)
                    {
                        velocity.y -= num * CheatMenuComponent.SpeedMultiplier;
                    }
                    if (button)
                    {
                        velocity.y += num * CheatMenuComponent.SpeedMultiplier;
                    }
                    Vector3 force = arg_21F_0 - velocity;
                    this.rb.AddForce(force, ForceMode.VelocityChange);
                    this.LastFlyMode = true;
                    return;
                }
                if (this.LastFlyMode)
                {
                    if (!this.IsInWater())
                    {
                        this.rb.useGravity = true;
                    }
                    this.gravity = this.baseGravity;
                    if (this.LastNoClip)
                    {
                        for (int m = 0; m < this.AllColliders.Length; m++)
                        {
                            this.AllColliders[m].enabled = true;
                        }
                        for (int n = 0; n < this.AllChildColliders.Length; n++)
                        {
                            this.AllChildColliders[n].enabled = true;
                        }
                        this.LastNoClip = false;
                    }
                    this.LastFlyMode = false;
                }
                base.FixedUpdate();
            }
        }
    }
}
