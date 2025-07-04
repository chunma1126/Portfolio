namespace Swift_Blade.Combat.Health
{
    public class GoblinBossHealth : BossHealth
    {
        private readonly string PARRY_TYPE_HASH = "ParryType";
        
        public override void TakeDamage(ActionData actionData)
        {
            animationController.GetAnimator().SetInteger(PARRY_TYPE_HASH , actionData.ParryType);
            base.TakeDamage(actionData);
        }
                
    }
}
