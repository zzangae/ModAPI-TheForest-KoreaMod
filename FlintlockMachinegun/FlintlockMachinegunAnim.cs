using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Items;
using TheForest.Utils;

namespace FlintlockMachinegun
{
    internal class FlintlockMachinegunAnim : flintLockAnimSetup
    {
        protected override void Update()
        {
            if (LocalPlayer.Inventory.Owns(this._flintAmmoId, true))
            {
                LocalPlayer.Animator.SetBool("canReload", false);
            }            
            if (!LocalPlayer.Animator)
            {
                return;
            }
            this.currState1 = LocalPlayer.Animator.GetCurrentAnimatorStateInfo(1);
            this.nextState1 = LocalPlayer.Animator.GetNextAnimatorStateInfo(1);
            this.currState2 = LocalPlayer.Animator.GetCurrentAnimatorStateInfo(2);
            if (this.currState1.shortNameHash == this.playerReloadHash && !this._net)
            {
                this._playerAnimator.SetBool("forceReload", false);
            }
            if (this.currState1.tagHash == this.knockBackHash || this.currState2.shortNameHash == this.sittingHash)
            {
                this.animator.CrossFade(this.idleHash, 0f, 0, 0f);
            }
            if (this.nextState1.shortNameHash == this.playerShootHash || nextState1.shortNameHash == playerAimShootHash)
            {
                this.animator.SetBool("shoot", true);
            }
            else
            {
                this.animator.SetBool("shoot", false);
            }
            if (this.currState1.shortNameHash == this.playerReloadHash && !this.reloadSync && !LocalPlayer.Inventory.IsLeftHandEmpty())
            {
                this.animator.CrossFade(this.reloadHash, 0f, 0, this.currState1.normalizedTime);
                this.reloadSync = false;
                this.animator.SetBool("shoot", false);
            }
            else if (this.animator.GetCurrentAnimatorStateInfo(0).shortNameHash == this.shootHash)
            {
                this.reloadSync = false;
            }
            if (this._net && this.currState1.shortNameHash == this.playerShootHash)
            {
                return;
            }
            else if (this._net && this.currState1.shortNameHash == this.playerReloadHash)
            {
                return;
            }
            if (this.nextState1.shortNameHash != this.playerIdleHash && !this._net)
            {
                LocalPlayer.Inventory.CancelReloadDelay();
            }            
            this.leftHandFull = false;
            if (this.storeReloadDelay > 0.5f)
            {
                base.Invoke("ForceReloadWeapon", 0.05f);
            }
            else if (LocalPlayer.Animator)
            {
                LocalPlayer.Animator.SetBool("forceReload", false);
            }
        }
    }    
}
