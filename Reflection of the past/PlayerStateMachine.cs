using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;

public enum PlayerStateEnum
{
    Idle,
    Run,
    Jump,
    Falling,
    Dead
}

public class PlayerStateMachine
{
    public PlayerState currentState;
    public Dictionary<PlayerStateEnum, PlayerState> PlayerStates = new Dictionary<PlayerStateEnum, PlayerState>();


    public void Init(PlayerStateEnum playerStateEnum)
    {
        currentState = PlayerStates[playerStateEnum];
        currentState.Enter();
    }
    
    public void ChangeState(PlayerStateEnum changeState)
    {
        currentState.Exit();
        currentState = PlayerStates[changeState];
        currentState.Enter();
    }

    public void AddState(PlayerStateEnum stateEnum , PlayerState playerState)
    {
        PlayerStates.Add(stateEnum , playerState);
    }
}