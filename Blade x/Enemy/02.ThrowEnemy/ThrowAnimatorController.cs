using Swift_Blade.Combat.Projectile;
using Swift_Blade.Combat.Health;
using DG.Tweening;
using UnityEngine;

namespace Swift_Blade.Enemy.Throw
{
    public class ThrowAnimatorController : BaseEnemyAnimationController 
    {
        public Transform target;
        public Transform throwHolder;

        public Collider bodyCollider;
        public Collider originCollider;
        
        private BaseThrow throwThing;
        
        private ThrowEnemy throwEnemy;
        private ThrowEnemyHealth throwEnemyHealth;
        private float originAttackDistance;
                
        protected virtual void Start()
        {
            throwEnemy = enemy as ThrowEnemy;
            throwEnemyHealth = enemy.GetHealth() as ThrowEnemyHealth;
        }
                        
        public void SetStone(BaseThrow stone)
        {
            if (stone == null)
            {
                if (throwThing != null)
                {
                    throwThing.transform.SetParent(null);
                    throwThing.SetPhysicsState(false);
                }
            }
            
            throwEnemyHealth.SetCanChangeParry(true);
            throwThing = stone;
        }
        
        public void CatchStone()
        {
            throwEnemyHealth.SetCanChangeParry(false);
            throwThing.SetPhysicsState(true);
            
            throwThing.transform.SetParent(throwHolder);
            throwThing.transform.localEulerAngles = Vector3.zero;
            throwThing.transform.localPosition = Vector3.zero;
        }
        
        public void ThrowStone()
        {
            throwEnemyHealth.SetCanChangeParry(true);
            
            var direction = (target.position - transform.position).normalized;
            
            throwThing.SetDirection(direction);
            throwThing = null;
        }
        
        public void StartManualCollider()
        {
            originAttackDistance = enemy.StopDistance;
            enemy.StopDistance = -1;
            
            bodyCollider.enabled = true;
            originCollider.excludeLayers |= 1 << LayerMask.NameToLayer("Player");
        }
        
        public void StopManualCollider()
        {
            enemy.StopDistance = originAttackDistance;
            
            bodyCollider.enabled = false;
            originCollider.excludeLayers &= ~(1 << LayerMask.NameToLayer("Player"));
        }

        public override void StopManualMove()
        {
            if (enemy.GetHealth().isDead)
            {
                base.StopManualMove();
            }
            else
            {
                DOVirtual.Float(attackMoveSpeed, throwEnemy.GetMoveSpeed(), 0.7f, x =>
                {
                    attackMoveSpeed = x;
                }).OnComplete(() =>
                {
                    NavMeshAgent.Warp(transform.position);
                    isManualMove = false;
                    NavMeshAgent.enabled = true;
                });
            }
            
        }
        
        public override void StopAllAnimationEvents()
        {
            SetStone(null);
            StopManualCollider();
            base.StopAllAnimationEvents();
        }
        
    }
}
