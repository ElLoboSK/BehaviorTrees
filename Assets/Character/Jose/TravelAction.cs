using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Travel", story: "[Character] travels to [Location]", category: "Action", id: "578ce0fcab2c0e131f3efc5b5e8180ac")]
public partial class TravelAction : Action
{
    [SerializeReference] public BlackboardVariable<Character> Character;
    [SerializeReference] public BlackboardVariable<GameObject> Location;
    protected override Status OnStart()
    {
        if (Character.Value == null || Location.Value == null)
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (navigateToLocation())
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        Character.Value.setMoveHorizontalDirection(Vector2.zero);
    }

    private bool navigateToLocation()
    {
        Vector2 offset = new Vector2(Location.Value.transform.position.x, Location.Value.transform.position.z) - new Vector2(Character.Value.transform.position.x, Character.Value.transform.position.z);
        Character.Value.setMoveHorizontalDirection(offset.normalized);
        Character.Value.setLookHorizontalDirection(offset.normalized);

        if (offset.magnitude < 0.2f)
        {
            return true;
        }
        return false;
    }
}
