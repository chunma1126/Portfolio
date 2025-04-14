using UnityEngine;

public class PlayerFallingState : PlayerState
{
    private Rigidbody Rigidbody;
    public PlayerFallingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolHash) : base(_player, _stateMachine, _animBoolHash)
    {
        Rigidbody = Player.Rigidbody;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        if (Player.CheckGround())
        {
            StateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        
        Player.Move(xInput * Player.MoveSpeed/2);
    }

    public override void Exit()
    {
        base.Exit();
    }
}