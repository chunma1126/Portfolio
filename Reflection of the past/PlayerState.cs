using UnityEngine;
using UnityEngine.Rendering;

public class PlayerState
{
    protected Player Player;
    protected PlayerStateMachine StateMachine;
    protected int animBoolHash;
    
    protected Animator Animator;
    protected bool AnimationEndTrigger;

    protected float xInput;
    protected bool yInput;
    
    public PlayerState(Player _player, PlayerStateMachine _stateMachine,string _animBoolHash)
    {
        Player = _player;
        StateMachine = _stateMachine;
        animBoolHash = Animator.StringToHash(_animBoolHash);

        Animator = _player.Animator;
    }
    
    public virtual void Enter()
    {
        Animator.SetBool(animBoolHash,true);
        AnimationEndTrigger = false;
    }

    public virtual void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        xInput *= -1;
        
        yInput = Input.GetKeyDown(KeyCode.Space);
    }
        
    public virtual void Exit()
    {
        Animator.SetBool(animBoolHash,false); 
    }

    public void AnimationEnd()
    {
        AnimationEndTrigger = true;
    }
    
}