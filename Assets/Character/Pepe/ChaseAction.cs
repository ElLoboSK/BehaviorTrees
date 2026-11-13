using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[Character] chase [Target] until in [Range]", category: "Action", id: "5ab16abcedba76a55359cae6184ba127")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<Character> Character;
    [SerializeReference] public BlackboardVariable<Character> Target;
    [SerializeReference] public BlackboardVariable<float> Range;
    protected override Status OnStart()
    {
        if (Character.Value == null || Target.Value == null)
        {
            return Status.Failure;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (targetInRange())
        {
            return Status.Success;
        }
        navigateToTarget();

        return Status.Running;
    }

    protected override void OnEnd()
    {
        Character.Value.setMoveHorizontalDirection(Vector2.zero);
    }

    private void navigateToTarget()
    {
        Vector2 offset = new Vector2(Target.Value.transform.position.x, Target.Value.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);
        Character.Value.setMoveHorizontalDirection(offset.normalized);
        Character.Value.setLookHorizontalDirection(offset.normalized);
    }

    private bool targetInRange()
    {
        Vector2 offset = new Vector2(Target.Value.transform.position.x, Target.Value.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);
        return offset.sqrMagnitude <= Range.Value * Range.Value;
    }
}

