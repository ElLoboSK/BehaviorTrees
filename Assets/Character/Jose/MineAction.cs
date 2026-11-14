using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Mine", story: "[Character] mines stone for [Time]", category: "Action", id: "1d5dba7ed6ebd9b9c9c751d7ae67a3b1")]
public partial class MineAction : Action
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
        Character.Value.setMessage("(Minando piedra)");
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

