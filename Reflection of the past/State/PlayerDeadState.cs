public class PlayerDeadState : PlayerState
{
    
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolHash) : base(_player, _stateMachine, _animBoolHash)
    {
        
    }
        
    public override void Enter()
    {
        base.Enter();
        
        Player.StopMovement();
        PopUpManager.Instance.ShowGameOverPanel();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}