using System.Collections;
using Swift_Blade.Enemy;
using Swift_Blade.Pool;
using UnityEngine.AI;
using Unity.Behavior;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Swift_Blade.Combat.Health
{
    public class BaseEnemyHealth : BaseEntityHealth
    {
        public event Action<float> OnChangeHealthEvent; 
        public float currentHealth;
        
        [Space]
        [SerializeField] protected BehaviorGraphAgent BehaviorGraphAgent;
        [SerializeField] protected ChangeBossState changeBossState;
        
        protected BaseEnemyAnimationController animationController;

        [Header("EXP")] 
        [SerializeField] private int minExp;
        [SerializeField] private int maxExp;
        
        private const float DAMAGE_INTERVAL = 0.25f;
        protected float lastDamageTime;
        
        protected virtual void Start()
        {
            BehaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
            animationController = GetComponentInChildren<BaseEnemyAnimationController>();
            
            OnHitEvent.AddListener(GeneratorText);
            OnDeadEvent.AddListener(AddExp);

            BehaviorGraphAgent.GetVariable("ChangeBossState", out BlackboardVariable<ChangeBossState> state);
            {
                Debug.Assert(state != null, "Enemy has Not State Change");
                changeBossState = state;
            }
        }
        
        private void OnDestroy()
        {
            OnHitEvent.RemoveListener(GeneratorText);
            OnDeadEvent.RemoveListener(AddExp);
        }
        
        private void AddExp()
        {
            Player.level.AddExp(Random.Range(minExp , maxExp));
        }
        
        private void GeneratorText(ActionData actionData)
        {
            Vector3 textPosition = actionData.hitPoint;
                        
            FloatingTextGenerator.Instance.GenerateText
            (
                Mathf.CeilToInt(actionData.damageAmount).ToString(),
                textPosition,
                actionData.textColor == default ? Color.white : actionData.textColor);
        }
        
        public override void TakeDamage(ActionData actionData)
        {
            if((isDead || !IsDamageTime()))return;
            
            lastDamageTime = Time.time; 
            
            currentHealth -= actionData.damageAmount;
            OnChangeHealthEvent?.Invoke(GetHealthPercent());
            
            if(actionData.stun)
                TriggerParry();
            
            OnHitEvent?.Invoke(actionData);
            
            if (currentHealth <= 0)
            {
                TriggerState(BossState.Dead);
                Dead();
            }
            
        }

        protected bool IsDamageTime()
        {
            return Time.time > lastDamageTime + DAMAGE_INTERVAL;
        }
        
        public void AddMaxHealth(float currentIndex)
        {
            maxHealth += currentIndex * 1.7f;
            currentHealth = maxHealth;
        }
        
        public override void TakeHeal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth , maxHealth);
        }
                
        public virtual void TriggerState(BossState state)
        {
            if(isDead)return;
            animationController.StopAllAnimationEvents();
            
            BehaviorGraphAgent.SetVariableValue("BossState", state);
            changeBossState.SendEventMessage(state);
        }
        
        public void TriggerHit()
        {
            TriggerState(BossState.Hit);
        }
        
        public void TriggerParry()
        {
            TriggerState(BossState.Parry);
        }
        
        private float GetHealthPercent()
        {
            return currentHealth / maxHealth;
        }
                                
        
    }
}
