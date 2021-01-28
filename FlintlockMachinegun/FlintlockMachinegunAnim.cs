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
            if (LocalPlayer.Inventory.Owns(_flintAmmoId, true))
            {
                LocalPlayer.Animator.SetBool("canReload", false);
            }            
            if (!LocalPlayer.Animator)
            {
                return;
            }
            currState1 = LocalPlayer.Animator.GetCurrentAnimatorStateInfo(1);
            nextState1 = LocalPlayer.Animator.GetNextAnimatorStateInfo(1);
            currState2 = LocalPlayer.Animator.GetCurrentAnimatorStateInfo(2);
            if (currState1.shortNameHash == playerReloadHash && !_net)
            {
                _playerAnimator.SetBool("forceReload", false);
            }
            if (currState1.tagHash == knockBackHash || currState2.shortNameHash == sittingHash)
            {
                animator.CrossFade(idleHash, 0f, 0, 0f);
            }
            if (nextState1.shortNameHash == playerShootHash || nextState1.shortNameHash == playerAimShootHash)
            {
                animator.SetBool("shoot", true);
            }
            else
            {
                animator.SetBool("shoot", false);
            }
            if (currState1.shortNameHash == playerReloadHash && !reloadSync && !LocalPlayer.Inventory.IsLeftHandEmpty())
            {
                animator.CrossFade(reloadHash, 0f, 0, currState1.normalizedTime);
                reloadSync = false;
                animator.SetBool("shoot", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == shootHash)
            {
                reloadSync = false;
            }
            if (_net && currState1.shortNameHash == playerShootHash)
            {
                return;
            }
            else if (_net && currState1.shortNameHash == playerReloadHash)
            {
                return;
            }
            if (nextState1.shortNameHash != playerIdleHash && !_net)
            {
                LocalPlayer.Inventory.CancelReloadDelay();
            }            
            leftHandFull = false;
            if (storeReloadDelay > 0.5f)
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
