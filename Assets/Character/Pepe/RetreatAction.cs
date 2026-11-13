using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Retreat", story: "[Character] retreat to [Waypoints] while looking [Target]", category: "Action", id: "4d8b71d8d7dd5bae8a0837ba64c1817b")]
public partial class RetreatAction : Action
{
    [SerializeReference] public BlackboardVariable<Character> Character;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;
    [SerializeReference] public BlackboardVariable<Character> Target;

    private GameObject targetWaypoint;

    protected override Status OnStart()
    {
        if (Character.Value == null || Target.Value == null || Waypoints.Value == null || Waypoints.Value.Count == 0 || Waypoints.Value[0] == null)
        {
            return Status.Failure;
        }
        targetWaypoint = Waypoints.Value[0];
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (moveBackwardsToTarget())
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private bool moveBackwardsToTarget()
    {
        Vector2 moveDirection = new Vector2(targetWaypoint.transform.position.x, targetWaypoint.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);
        Vector2 lookDirection = new Vector2(Target.Value.transform.position.x, Target.Value.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);

        if (moveDirection.sqrMagnitude < 0.2f)
        {
            return true;
        }

        Character.Value.setMoveHorizontalDirection(moveDirection.normalized);
        Character.Value.setLookHorizontalDirection(lookDirection.normalized);

        return false;
    }
}

