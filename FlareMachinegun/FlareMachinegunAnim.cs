using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TheForest.Items;
using TheForest.Utils;

namespace FlintlockMachinegun
{
    internal class FlareGunAnim : flareGunAnimSetup
    {
        protected override void Update()
        {
            if (LocalPlayer.Inventory.Owns(_ammoId, true))
            {
                LocalPlayer.Animator.SetBool("canReload", false);
            }
            if (!LocalPlayer.Animator)
            {
                return;
            }
            AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            currState1 = _playerAnimator.GetCurrentAnimatorStateInfo(1);
            nextState1 = _playerAnimator.GetNextAnimatorStateInfo(1);
            currState2 = _playerAnimator.GetCurrentAnimatorStateInfo(2);
            if (currState1.shortNameHash == playerReloadHash && !_net)
            {
                _playerAnimator.SetBool("forceReload", false);
            }
            if (currState1.tagHash == knockBackHash || currState2.shortNameHash == sittingHash)
            {
                _animator.CrossFade(idleHash, 0f, 0, 0f);
            }
            if (nextState1.shortNameHash == playerShootHash || nextState1.shortNameHash == playerAimShootHash)
            {
                if (!_net)
                {
                    doWeaponNoise();
                }
                _animator.SetBool("shoot", true);
            }
            else
            {
                _animator.SetBool("shoot", false);
            }
            if (currState1.shortNameHash == playerReloadHash && !reloadSync)
            {
                _animator.CrossFade(reloadHash, 0f, 0, currState1.normalizedTime);
                reloadSync = false;
                _animator.SetBool("shoot", false);
            }
            else if (currentAnimatorStateInfo.shortNameHash == shootHash)
            {
                reloadSync = false;
            }
            if (_net && currState1.shortNameHash == playerShootHash)
            {
                _animator.Play(shootHash, 0, currState1.normalizedTime);
            }
            else if (_net && currState1.shortNameHash == playerReloadHash)
            {
                _animator.Play(reloadHash, 0, currState1.normalizedTime);
            }
            if (nextState1.shortNameHash != playerIdleHash && !_net)
            {
                LocalPlayer.Inventory.CancelReloadDelay();
            }
            if (currentAnimatorStateInfo.shortNameHash == reloadHash)
            {
                if (currentAnimatorStateInfo.normalizedTime < 0.1f)
                {
                    _ammoEmpty.SetActive(false);
                    _ammoFull.SetActive(false);
                }
                else if (currentAnimatorStateInfo.normalizedTime < 0.306f)
                {
                    _ammoEmpty.SetActive(true);
                    _ammoFull.SetActive(true);
                }
                else if (currentAnimatorStateInfo.normalizedTime < 0.73f)
                {
                    _ammoEmpty.SetActive(false);
                    _ammoFull.SetActive(true);
                }
                else
                {
                    _ammoEmpty.SetActive(false);
                    _ammoFull.SetActive(false);
                }
            }
            else if (currentAnimatorStateInfo.shortNameHash == idleHash)
            {
                if (_ammoEmpty.activeSelf)
                {
                    _ammoEmpty.SetActive(false);
                }
                if (_ammoFull.activeSelf)
                {
                    _ammoFull.SetActive(false);
                }
            }
            _ammoEmpty.SetActive(false);
            _ammoFull.SetActive(false);
            blockAmmoSpawn = false;
            leftHandFull = false;
            if (storeReloadDelay > 0.5f)
            {
                base.Invoke("ForceReloadWeapon", 0.05f);
            }
            else if (!_net)
            {
                _playerAnimator.SetBool("forceReload", false);
            }
        }
    }
}
