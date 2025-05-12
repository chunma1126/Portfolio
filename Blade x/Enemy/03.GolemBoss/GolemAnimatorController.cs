using UnityEngine.Animations.Rigging;
using Swift_Blade.Combat.Caster;
using Swift_Blade.Enemy.Throw;
using Swift_Blade.Feeling;
using Swift_Blade.Pool;
using UnityEngine;
using DG.Tweening;

namespace Swift_Blade.Enemy.Boss.Golem
{
    public class GolemAnimatorController : ThrowAnimatorController
    {
        [SerializeField] private CameraShakeType cameraShakeType;
        [SerializeField] private Rig rig;
        
        [SerializeField] private PoolPrefabMonoBehaviourSO groundCrackSO;

        [SerializeField] private Transform rightGroundCrackTrm;
        [SerializeField] private Transform leftGroundCrackTrm;
        [SerializeField] private Transform forwardGroundCrackTrm;
        
        private GolemEnemyCaster damageCaster;

        protected override void Start()
        {
            base.Start();
            damageCaster = caster as GolemEnemyCaster;
            
            MonoGenericPool<GroundCrackParticle>.Initialize(groundCrackSO);
        }
        
        public void JumpAttackCast()
        {
            damageCaster.JumpAttackCast();
        }
        
        public void StartManualLook()
        {
            DOVirtual.Float(rig.weight, 1f, 0.3f, value => { rig.weight = value; });
        }

        public void StopManualLook()
        {
            if (enemy.GetHealth().isDead)
            {
                rig.weight = 0;
            }
            else
            {
                DOVirtual.Float(rig.weight, 0f, 0.3f, value => { rig.weight = value; });
            }
        }
        
        public override void StopAllAnimationEvents()
        {
            StopManualLook();
            base.StopAllAnimationEvents();
        }
                
        public void CreateGroundCrack(int _direction)
        {
            if (_direction == 1)
            {
                var g = MonoGenericPool<GroundCrackParticle>.Pop();
                g.transform.position = rightGroundCrackTrm.position;
            }

            if (_direction == -1)
            {
                var g = MonoGenericPool<GroundCrackParticle>.Pop();
                g.transform.position = leftGroundCrackTrm.position;
            }

            if (_direction == 0)
            {
                var g = MonoGenericPool<GroundCrackParticle>.Pop();
                g.transform.position = forwardGroundCrackTrm.position;
            }
            
        }
        
        private void ShakeCam()
        {
            CameraShakeManager.Instance.DoShake(cameraShakeType);
        }
        
    }
}