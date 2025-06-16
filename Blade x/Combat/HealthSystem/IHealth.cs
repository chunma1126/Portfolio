
public interface IHealth
{
    public bool IsDead { get; }
    
    public void TakeDamage(ActionData actionData);
    public void TakeHeal(float amount);
    public void Dead();
}
