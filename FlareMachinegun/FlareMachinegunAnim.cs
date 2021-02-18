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
        private AnimatorStateInfo _currState;
        private AnimatorStateInfo _nextState;

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
            AnimatorStateInfo _currAniStateinfo = _animator.GetCurrentAnimatorStateInfo(0);
            _currState = _playerAnimator.GetCurrentAnimatorStateInfo(1);
            _nextState = _playerAnimator.GetNextAnimatorStateInfo(1);            
            if (_currState.shortNameHash == playerReloadHash && !reloadSync)
            {
                _animator.CrossFade(reloadHash, 0f, 0, _currState.normalizedTime);
                reloadSync = false;
                _animator.SetBool("shoot", false);
            }
            else if (_currAniStateinfo.shortNameHash == shootHash)
            {
                reloadSync = false;
            }
            if (_nextState.shortNameHash != playerIdleHash && !_net)
            {
                LocalPlayer.Inventory.CancelReloadDelay();
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
