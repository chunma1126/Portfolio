using System.Collections.Generic;
using Swift_Blade.Combat.Health;
using UnityEngine;

namespace Swift_Blade.Skill
{
    [CreateAssetMenu(fileName = "HitHealSkill", menuName = "SO/Skill/Green/Heal")]
    public class HitHealSkill : SkillData
    {
        [Range(1, 10)] public int healAmount;
        private PlayerHealth playerHealth;

        public override void UseSkill(Player player, IEnumerable<Transform> targets = null)
        {
            if (playerHealth == null)
                playerHealth = player.GetPlayerHealth;

            if (TryUseSkill(Mathf.RoundToInt(GetColorRatio())))
            {
                GenerateSkillText(true);
                playerHealth.TakeHeal(healAmount);
            } 
        }
        
    }
}

