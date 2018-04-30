using UnityEngine;
/// <summary>
/// Allows us to distribute 1 tagged AI behaviour to 500 sub AI objects.
/// </summary>
public class AITargeter :MonoBehaviour{

    public string tagTarget;
    public Movement moving;

    [SerializeField]FactionAccess _stats;

    public FactionAccess stats {
        get {
            if (_stats != null)
                return _stats;
            else {
                Debug.Log("Trying to reassign StatAcess to parent.");
                return _stats = GetComponentInParent<FactionAccess>();
            }
        }
    }
}
