using System;
using Swift_Blade.Feeling;
using UnityEngine;

namespace Swift_Blade.Enemy.Sword
{
    public class SwordBossAnimatorController : BaseEnemyAnimationController
    {
        [SerializeField] private CameraShakeType cameraShakeType;
        
        private void Update()
        {
            if (Animator.GetFloat(ANIMATION_SPEED) != originMotionSpeed)
            {
                Animator.SetFloat(ANIMATION_SPEED,originMotionSpeed);                
            }
        }
        
        public void CamShake()
        {
            CameraShakeManager.Instance.DoShake(cameraShakeType);    
        }
        
        
    }
}
