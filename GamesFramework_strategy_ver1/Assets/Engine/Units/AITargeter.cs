using UnityEngine;
/// <summary>
/// Allows us to distribute 1 tagged AI behaviour to 500 sub AI objects.
/// 
/// CANNOT BE ON ROOT. Use Tree behaviour instead.
/// </summary>
public class AITargeter :MonoBehaviour{

    public string tagTarget;
    public Movement moving;

    /// <summary>
    /// Who spawned this object.
    /// It's only saved on root.
    /// </summary>
    public AITargeter spawner;

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

    public void OnSpawned(AITargeter other) {
        spawner = other;
        if (_stats)
        _stats.OnSpawned(other.transform.root.GetComponent<FactionAccess>().faction);
    }

}
