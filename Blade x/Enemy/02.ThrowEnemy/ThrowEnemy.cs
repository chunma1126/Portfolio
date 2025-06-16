﻿using Swift_Blade.Combat.Health;

namespace Swift_Blade.Enemy.Throw
{
    public class ThrowEnemy : BaseEnemy
    {
        private ThrowAnimatorController _throwEnemyAnimationController;
        
        protected override void Start()
        {
            base.Start();
            
            _throwEnemyAnimationController = baseAnimationController as ThrowAnimatorController;
            _throwEnemyAnimationController.target = target;
        }

        public override BaseEnemyHealth GetHealth()
        {
            return baseHealth as ThrowEnemyHealth;
        }
        
        public override void StartKnockback(ActionData actionData)
        {
            if((baseHealth as ThrowEnemyHealth).GetChangeParry() == false)return;
            
            base.StartKnockback(actionData);
        }

        public override void DeadEvent()
        {
            _throwEnemyAnimationController.SetStone(null);
            base.DeadEvent();
        }
        
    }
}