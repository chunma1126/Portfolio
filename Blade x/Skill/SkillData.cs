using System.Collections.Generic;
using Swift_Blade.Pool;
using UnityEngine;

namespace Swift_Blade.Skill
{
    public abstract class SkillData : ScriptableObject
    {
        public string skillName;
        public Sprite skillIcon;
        [Space(30)]
        public SkillType skillType;
        public StatType statType;
        public ColorType colorType;
        [Tooltip("���� ������ ������ �󸶳� ������")] public float colorRatio;
        [Tooltip("���� Ȯ��")][Range(1,100)] public int random;
                
        [TextArea] public string skillDescription;
        
        [Space(40)]
        public PoolPrefabMonoBehaviourSO skillParticle;
        
        public virtual void Initialize(){}
        
        public virtual void ResetSkill(){}
        
        public virtual void Render(){}
        
        public virtual void SkillUpdate(Player player, IEnumerable<Transform> targets = null){}
        
        public abstract void UseSkill(Player player, IEnumerable<Transform> targets = null);
        
        protected bool TryUseSkill()
        {
            return Random.Range(0, 100) <= random;
        }
        
    }
}