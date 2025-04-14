using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private readonly Rigidbody Rigidbody;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolHash) : base(_player, _stateMachine, _animBoolHash)
    {
        Rigidbody = _player.Rigidbody;
    }

    public override void Enter()
    {
        base.Enter();
        
        
        Player.Jump(Player.JumpForce);
    }
    
    public override void Update()
    {
        base.Update();
        
        if (Rigidbody.velocity.y < 0)
        {
            StateMachine.ChangeState(PlayerStateEnum.Falling);
        }
        
        Player.Move(xInput * Player.MoveSpeed/2);
    }

    public override void Exit()
    {
        base.Exit();
    }
}