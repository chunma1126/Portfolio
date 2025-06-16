using System.Collections.Generic;
using Swift_Blade.Combat.Health;
using Swift_Blade.Pool;
using System.Linq;
using UnityEngine;

namespace Swift_Blade.Skill
{
    [CreateAssetMenu(fileName = "MaxHealthKnockDownSkill", menuName = "SO/Skill/Green/MaxHealthKnockDownSkill")]
    public class MaxHealthKnockDownSkill : SkillData
    {
        public override void Initialize()
        {
            MonoGenericPool<ImpactDirtParticle>.Initialize(skillParticle);
        }
        
        public override void UseSkill(Player player,  IEnumerable<Transform> targets = null)
        {
            if(targets == null || !targets.Any())return;
            if(player.GetPlayerHealth.IsFullHealth == false)return;
            
            
            int value = Mathf.RoundToInt(GetColorRatio());
            if(TryUseSkill(value) == false)return;
            
            GenerateSkillText(true);
            MonoGenericPool<ImpactDirtParticle>.Pop().transform.position = targets.First().transform.position + new Vector3(0,1,0);
            
            foreach (var item in targets)
            {
                if (item.TryGetComponent(out BaseEnemyHealth enemyHealth))
                {
                    ActionData actionData = new ActionData
                    {
                        stun = true
                    };
                    enemyHealth.TakeDamage(actionData);
                }
            }
            
        }
    }
}
