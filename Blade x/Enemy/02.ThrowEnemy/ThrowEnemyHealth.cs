using Swift_Blade.Enemy.Throw;

namespace Swift_Blade.Combat.Health
{
    public class ThrowEnemyHealth : BaseEnemyHealth
    {
        private bool canChangeParry;
        private ThrowAnimatorController throwAnimatorController;

        protected override void Start()
        {
            base.Start();
            throwAnimatorController = animationController as ThrowAnimatorController;
        }
                
        public override void TriggerState(BossState state)
        {
            if(canChangeParry == false)return;
            
            throwAnimatorController.SetStone(null);
            base.TriggerState(state);
        }
        public void SetCanChangeParry(bool _canChangeParry)
        {
            canChangeParry = _canChangeParry;
        }

        public bool GetChangeParry()
        {
            return canChangeParry;
        }
        
    }
}
