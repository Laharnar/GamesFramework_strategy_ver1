using UnityEngine;

public interface IAISource {

}
/// <summary>
/// Allows us to distribute 1 tagged AI behaviour to 500 sub AI objects.
/// 
/// CANNOT BE ON ROOT. Use Tree behaviour instead.
/// </summary>
public class AITargeter : MonoBehaviour, IAISource{
    public SteeringMovement move;

    public string tagTarget;
    public CommandData _movingData;
    public CommandData movingData {
        get {
            if (_movingData == null) {
                _movingData = GetComponent<CommandData>();
                Debug.Log("Looking for missing component: CommandData", gameObject);
                if (_movingData == null) {
                    _movingData = gameObject.AddComponent<CommandData>();
                    Debug.Log("Adding missing component: CommandData", gameObject);
                }
            }
            return _movingData;
        }
    }

    /// <summary>
    /// Who spawned this object.
    /// It's only saved on root.
    /// </summary>
    public AITargeter spawner;


    [SerializeField] FiringSystems _firing;

    public FiringSystems Firing {
        get {
            if (_firing == null) {
                Debug.Log("Missing firing system. Add it to this object and link it to AiTargeter script.", this);
            }
            return _firing;
        }
    }

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
