using System.Collections.Generic;
using Swift_Blade.Combat.Health;
using Swift_Blade.Pool;
using UnityEngine;
using System.Linq;

namespace Swift_Blade.Skill
{
    [CreateAssetMenu(fileName = "KillingPowerUpSkill", menuName = "SO/Skill/Red/KillingPowerUpSkill")]
    public class KillingPowerUpSkill : SkillData
    {
        [SerializeField] private PoolPrefabMonoBehaviourSO redCircle;
        
        [Range(0.1f, 10f)] [SerializeField] private float increaseValue;
        private float increaseAmount;
        
        public override void Initialize()
        {
            MonoGenericPool<ArrowUpParticle>.Initialize(skillParticle);
            MonoGenericPool<RedCircleParticle>.Initialize(redCircle);

            //increaseAmount = 0;
        }

        public override void UseSkill(Player player,  IEnumerable<Transform> targets = null)
        {
            if (targets.Any())
            {
                foreach (var item in targets)
                {
                    if (item.TryGetComponent(out BaseEnemyHealth health) && health.isDead)
                    {
                        ArrowUpParticle arrowUpParticle =  MonoGenericPool<ArrowUpParticle>.Pop();
                        arrowUpParticle.transform.position = player.GetPlayerTransform.position + new Vector3(0,2.5f,0);
                        arrowUpParticle.SetFollowTransform(player.GetPlayerTransform);
                        
                        RedCircleParticle redCircleParticle = MonoGenericPool<RedCircleParticle>.Pop();
                        redCircleParticle.transform.SetParent(player.GetPlayerTransform);
                        redCircleParticle.transform.position = player.GetPlayerTransform.position + new Vector3(0,0.5f,0);

                        increaseAmount += increaseValue;
                        
                        GenerateSkillText(true);
                        statCompo.RemoveModifier(statType,skillName);
                        statCompo.AddModifier(statType,skillName,increaseAmount);
                    }
                }
            }
        }
        
        public override void ResetSkill()
        {
            increaseAmount = 0;
            GenerateSkillText(false);
            statCompo.RemoveModifier(statType, skillName);
        }
        
    }
}
