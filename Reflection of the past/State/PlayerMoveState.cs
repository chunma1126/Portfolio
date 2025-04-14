using DG.Tweening;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Rigidbody Rigidbody;
    
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolHash) : base(_player, _stateMachine, _animBoolHash)
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
        Player.Flip();
        Animator.SetFloat("Speed" , Mathf.Abs(xInput));
        Player.Move(xInput * Player.MoveSpeed);

        if (yInput)
        {
            StateMachine.ChangeState(PlayerStateEnum.Jump);
        }
        
        if (xInput == 0.0f)
        {
            StateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        
    }

  
    public override void Exit()
    {
        Animator.SetFloat("Speed" , 0);
        
        base.Exit();
    }
}