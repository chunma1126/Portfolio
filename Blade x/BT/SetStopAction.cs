using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetStop", story: "Set Stop [Param]", category: "Action", id: "0849a9f76965200186596f3ab10c7260")]
public partial class SetStopAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> self;
    [SerializeReference] public BlackboardVariable<bool> Param;
    
    protected override Status OnStart()
    {
        //if (self.Value.enabled == false) return Status.Failure;
        
        if(self.Value.isActiveAndEnabled)
        {
            self.Value.isStopped = Param.Value;
        }

        return Status.Success;
    }
    
}

