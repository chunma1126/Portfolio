using System.Collections.Generic;
using Swift_Blade.Combat.Health;
using Swift_Blade.Pool;
using UnityEngine;

namespace Swift_Blade.Skill
{
    [CreateAssetMenu(fileName = "AttackThunderSkill", menuName = "SO/Skill/Yellow/Thunder")]
    public class AttackThunderSkill : SkillData
    {
        [Tooltip("��� ������ �����Ұ� �Ұ�����.")]  [SerializeField] private ushort attackCount = 3;
        private float attackCounter = 0;
        
        [SerializeField] private int skillDamage = 0;
                
        public override void Initialize()
        {
            if (skillParticle == null || skillParticle.GetMono == null)
            {
                Debug.LogError("SkillEffectPrefab or its MonoBehaviour is null.");
                return;
            }
            
            attackCounter = 0;
            MonoGenericPool<ThunderParticle>.Initialize(skillParticle);
        }

        public override void UseSkill(Player player, IEnumerable<Transform> targets = null)
        {
            ++attackCounter;
            
            if (attackCounter >= attackCount)
            {
                if (TryUseSkill())
                {
                    foreach (var item in targets)
                    {
                        if(item.TryGetComponent(out BaseEnemyHealth health))
                        {
                            ActionData actionData = new ActionData();
                            actionData.stun = true;
                            actionData.damageAmount = skillDamage * GetColorRatio();
                            actionData.hurtType = 1;
                            actionData.textColor = Color.yellow;
                            //FloatingTextGenerator.Instance.GenerateText(generateText,item.transform.position + new Vector3(0,0.5f,0));
                            
                            health.TakeDamage(actionData);
                            
                            ThunderParticle th = MonoGenericPool<ThunderParticle>.Pop();
                            th.transform.position = item.transform.position + new Vector3(0,1,0);
                        }
                    }
                }
                
                
                attackCounter = 0;
            }
        }
    }
}
