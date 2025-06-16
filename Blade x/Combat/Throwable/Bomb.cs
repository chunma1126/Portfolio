using Swift_Blade.Audio;
using Swift_Blade.Combat.Health;
using Swift_Blade.Feeling;
using Swift_Blade.Pool;
using UnityEngine;

namespace Swift_Blade.Combat.Projectile
{
    public class Bomb : BaseThrow
    {
        [SerializeField] private LayerMask whatIsTarget;
                
        [SerializeField] private CameraShakeType shakeType;
        [SerializeField] private PoolPrefabMonoBehaviourSO explosionSO;

        [SerializeField] private float explosionRadius;
        [SerializeField] private int enemyDamage = 5;
        
        private bool canExplosion;
        private bool hasExploded; // 무한루프 방지용 플래그
        private readonly Collider[] targets = new Collider[10];
        
        
        [Space]
        [SerializeField] private AudioCollectionSO explosionSound;
        
        protected override void Start()
        {
            base.Start();
            MonoGenericPool<ExplosionParticle>.Initialize(explosionSO);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (canExplosion)
            {
                Explosion(other.GetContact(0).point);
            }
        }
        private void Explosion(Vector3 explosionPoint)
        {
            if (canExplosion == false || hasExploded) 
                return;
            
            AudioManager.PlayWithInit(explosionSound.GetRandomAudio, true);
            
            canExplosion = false;
            hasExploded = true;
            
            int counts = Physics.OverlapSphereNonAlloc(explosionPoint, explosionRadius, targets, whatIsTarget);
            
            for (int i = 0; i < counts; i++)
            {
                var targetObject = targets[i];
                var position = targetObject.transform.position + new Vector3(0, 0.25f, 0);

                // 1. IHealth 처리
                if (targetObject.TryGetComponent<IHealth>(out IHealth health))
                {
                    var isEnemy = health is BaseEnemyHealth;
                    var actionData = new ActionData
                    {
                        damageAmount = isEnemy ? enemyDamage : 1,
                        hitPoint = position,
                        textColor = Color.yellow,
                        stun = true,
                        ParryType = 1
                    };
                    
                    health.TakeDamage(actionData);
                    continue;
                }

                if (targetObject.TryGetComponent(out Bomb bomb))
                {
                    bomb.SetPhysicsState(false);
                    bomb.SetDirection(Vector3.zero);
                    bomb.Explosion(bomb.transform.position);
                    continue;
                }
                
                if (targetObject.TryGetComponent(out DestructibleObject destructible))
                {
                    destructible.TakeDamage(new ActionData());
                }
            }

            CameraShakeManager.Instance.DoShake(shakeType);

            var e = MonoGenericPool<ExplosionParticle>.Pop();
            e.transform.position = explosionPoint;
            
            Destroy(gameObject);
        }

        public override void SetPhysicsState(bool isActive)
        {
            base.SetPhysicsState(isActive);
            if (isActive == false)
                canExplosion = true;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
