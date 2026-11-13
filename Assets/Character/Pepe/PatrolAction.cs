using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.InputSystem.Interactions;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Character] patrols along [Waypoints] until detects [Target] in [Range]", category: "Action", id: "febe83dfa6d9f6c8d4fb5acca8b46644")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<Character> Character;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;
    [SerializeReference] public BlackboardVariable<Character> Target;
    [SerializeReference] public BlackboardVariable<float> Range;
    private int pointIndex;

    protected override Status OnStart()
    {
        if (Character.Value == null || Waypoints.Value == null || Waypoints.Value.Count == 0)
        {
            return Status.Failure;
        }

        pointIndex = 0;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (detectTarget())
        {
            return Status.Success;
        }

        navigateWaypoints();
        return Status.Running;
    }

    protected override void OnEnd()
    {
        Character.Value.setMoveHorizontalDirection(Vector2.zero);
    }

    private void navigateWaypoints()
    {
        GameObject target = Waypoints.Value[pointIndex];

        Vector2 offset = new Vector2(target.transform.position.x, target.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);

        if (offset.magnitude < 0.2f)
        {
            pointIndex = (pointIndex + 1) % Waypoints.Value.Count;
            target = Waypoints.Value[pointIndex];
            offset = new Vector2(target.transform.position.x, target.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);
        }

        Character.Value.setMoveHorizontalDirection(offset.normalized);
        Character.Value.setLookHorizontalDirection(offset.normalized);
    }

    private bool detectTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(Character.Value.transform.position, Range.Value);

        Character closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            Character candidate = collider.GetComponentInParent<Character>();

            if (candidate == null || candidate == Character.Value)
            {
                continue;                
            }

            float distance = Vector3.Distance(Character.Value.transform.position, candidate.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = candidate;
            }
        }

        Target.Value = closestTarget;

        return closestTarget != null ? true : false;
    }
}

