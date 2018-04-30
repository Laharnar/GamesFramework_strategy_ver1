using UnityEngine;
/// <summary>
/// Allows us to distribute 1 tagged AI behaviour to 500 sub AI objects.
/// </summary>
public class AITargeter :MonoBehaviour{
    public string tagTarget;
    public Movement moving;
    public StatAccess stats;
}
