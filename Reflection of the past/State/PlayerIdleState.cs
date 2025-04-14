public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolHash) : base(_player, _stateMachine, _animBoolHash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        if (xInput != 0)
        {
            StateMachine.ChangeState(PlayerStateEnum.Run);
        }
        
        if (yInput)
        {
            StateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}