using DG.Tweening;
using Swift_Blade.Combat.Caster;
using Swift_Blade.Audio;
using UnityEngine.AI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Swift_Blade.Enemy
{
    public class BaseEnemyAnimationController : MonoBehaviour
    {
        protected Animator Animator;
        protected NavMeshAgent NavMeshAgent;
        
        protected BaseEnemy enemy;
        protected ICasterAble caster;
        
        [SerializeField] [Range(1,60)] protected float defaultAttackMoveSpeed;
        protected float attackMoveSpeed;
        
        public float AttackMoveSpeed => attackMoveSpeed;
        
        [Space]
        public bool animationEnd;
        public bool isManualRotate;
        public bool isManualMove;
        
        public float maxAnimationSpeed = 1.3f;
        public float minAnimationSpeed = 1;
        
        protected readonly int ANIMATION_SPEED = Animator.StringToHash("AnimationSpeed");
        
        protected float originMotionSpeed;
        private float originMoveSpeed;

        private readonly float attackMoveAccelerationDuration = 0.2f;
        private Tween attackMoveTween;
        
        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            enemy = GetComponent<BaseEnemy>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            caster = GetComponentInChildren<ICasterAble>();
            
            SetRandomAttackAnimationSpeed();
            ResetAnimationSpeed();
        }
        
        #region Speed 
        private void SetRandomAttackAnimationSpeed()
        {
            originMotionSpeed = Random.Range(minAnimationSpeed, maxAnimationSpeed);
        }
        
        public void ResetAnimationSpeed()
        {
            SetAnimationSpeed(originMotionSpeed);
        }
       
        
        public void SetAnimationSpeed(float animationSpeed)
        {
            Animator.SetFloat(ANIMATION_SPEED ,animationSpeed);
        }
        
        public void MultiplyDefaultAttackMoveSpeed(float ratio)
        {
            originMoveSpeed = defaultAttackMoveSpeed;
            defaultAttackMoveSpeed *= ratio;
        }
        
        public void ResetDefaultMoveSpeed()
        {
            defaultAttackMoveSpeed = originMoveSpeed;
        }
        
        #endregion
        
        
        public void Rebind()
        {
            Animator.Rebind();
            ResetAnimationSpeed();
        }
        
        public Animator GetAnimator() => Animator;
        private void Cast() => caster.Cast();
        public virtual void SetAnimationEnd() => animationEnd = true;
        public void StopAnimationEnd() => animationEnd = false;
        public void StartManualRotate() => isManualRotate = true;
        public void StopManualRotate() => isManualRotate = false;
        public void StartApplyRootMotion() => Animator.applyRootMotion = true;
        public void StopApplyRootMotion() => Animator.applyRootMotion = false;
        
        public void StartManualMove(float _moveSpeed = 0)
        {
            float targetSpeed = _moveSpeed == 0 ? defaultAttackMoveSpeed : _moveSpeed;

            isManualMove = true;
            
            if (NavMeshAgent != null)
                NavMeshAgent.enabled = false;
            
            attackMoveTween?.Kill();
            attackMoveTween = DOVirtual.Float(
                from: 0f,
                to: targetSpeed,
                duration: attackMoveAccelerationDuration, 
                onVirtualUpdate: value => attackMoveSpeed = value
            );
        }
        public virtual void StopManualMove()
        {
            if (NavMeshAgent == null || enemy == null)
                return;

            isManualMove = false;
            
            attackMoveTween?.Kill();
            attackMoveTween = DOVirtual.Float(
                attackMoveSpeed,
                0f,
                attackMoveAccelerationDuration,
                value => attackMoveSpeed = value
            ).OnComplete(() =>
            {
                attackMoveSpeed = defaultAttackMoveSpeed;
            });

            NavMeshAgent.Warp(transform.position);
            
            var health = enemy.GetHealth();
            if (health != null)
                NavMeshAgent.enabled = !health.isDead;

        }
                
        public virtual void StopAllAnimationEvents()
        {
            StopAnimationEnd();
            StopManualMove();
            StopManualRotate();
            StopApplyRootMotion();
        }
        
        private void OnAudioPlay(AudioSO audio)
        {
            AudioManager.PlayWithInit(audio, true);
        }
        
        private void OnAudioPlayCollection(AudioCollectionSO audioCollectionSo) => OnAudioPlay(audioCollectionSo.GetRandomAudio);
        
       
    }
}
