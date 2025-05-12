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
        
        private BaseThrow _throw;
        
        private IGetMoveSpeedAble getMoveSpeedAble;
        private ThrowEnemyHealth throwEnemyHealth;

        private float originAttackDistance;
        
        protected override void Start()
        {
            base.Start();
            enemy = enemy as ThrowEnemy;
            getMoveSpeedAble = enemy as IGetMoveSpeedAble;
            throwEnemyHealth = enemy.GetHealth() as ThrowEnemyHealth;
        }
        
        public void SetStone(BaseThrow stone)
        {
            if (stone == null)
            {
                if (_throw != null)
                {
                    _throw.transform.SetParent(null);
                    _throw.SetPhysicsState(false);
                }
            }
            
            throwEnemyHealth.SetCanChangeParry(true);
            _throw = stone;
        }
        
        public void CatchStone()
        {
            throwEnemyHealth.SetCanChangeParry(false);
            _throw.SetPhysicsState(true);
                        
            _throw.transform.SetParent(throwHolder);
            _throw.transform.localEulerAngles = Vector3.zero;
            _throw.transform.localPosition = Vector3.zero;
        }
        
        public void ThrowStone()
        {
            throwEnemyHealth.SetCanChangeParry(true);
            
            var direction = (target.position - transform.position).normalized;
            
            _throw.SetDirection(direction);
            _throw = null;
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
                DOVirtual.Float(attackMoveSpeed, getMoveSpeedAble.GetMoveSpeed(), 0.7f, x =>
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
