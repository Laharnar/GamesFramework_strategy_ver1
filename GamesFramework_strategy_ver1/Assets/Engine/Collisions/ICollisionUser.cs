using UnityEngine;
/// <summary>
/// Use this class on classes that need collision.
/// </summary>
public abstract class CollisionUser :MonoBehaviour{
    public abstract void OnTriggered(CollisionHandler other);
}