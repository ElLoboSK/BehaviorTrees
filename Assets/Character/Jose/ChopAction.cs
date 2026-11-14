using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chop", story: "[Character] chops wood for [Time]", category: "Action", id: "da83c1a6b7cfb3cad94af074d9c39a28")]
public partial class ChopAction : Action
{
    [SerializeReference] public BlackboardVariable<Character> Character;
    [SerializeReference] public BlackboardVariable<float> Time;
    private float timer;

    protected override Status OnStart()
    {
        timer = 0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer += UnityEngine.Time.deltaTime;
        if (timer >= Time.Value)
        {
            Character.Value.setMessage("");
            return Status.Success;
        }
        Character.Value.setMessage("(Cortando madera)");
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

