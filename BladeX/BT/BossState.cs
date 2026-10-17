using Unity.Behavior;

[BlackboardEnum]
public enum BossState
{
    Idle,
    Recovery,
	Move,
	Attack,
	Parry,
	Hit,
	Step,
	Dead
}
