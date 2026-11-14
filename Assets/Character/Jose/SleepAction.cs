using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Sleep", story: "[Character] sleeps for [Time]", category: "Action", id: "5ecbbe249061d373a361c6c7cc36d595")]
public partial class SleepAction : Action
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
        Character.Value.setMessage("(Durmiendo)");
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

