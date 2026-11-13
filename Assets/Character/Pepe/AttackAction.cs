using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using TMPro;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Character] attacks [Target] for [Time] with [Message]", category: "Action", id: "cf931abd38e6902a9985af04d1ac6c8f")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Character> Character;
    [SerializeReference] public BlackboardVariable<Character> Target;
    [SerializeReference] public BlackboardVariable<float> Time;
    [SerializeReference] public BlackboardVariable<string> Message;
    private float timer;

    protected override Status OnStart()
    {
        
        if (Character.Value == null || Target.Value == null)
        {
            return Status.Failure;
        }
        timer = 0f;
        lookTarget();
        Character.Value.setMessage(Message);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        lookTarget();
        timer += UnityEngine.Time.deltaTime;
        if (timer >= Time.Value)
        {
            Character.Value.setMessage("");
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private void lookTarget()
    {
        Vector2 offset = new Vector2(Target.Value.transform.position.x, Target.Value.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);
        Character.Value.setLookHorizontalDirection(offset.normalized);
    }
}

