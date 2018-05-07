using System;
using System.Collections.Generic;
using UnityEngine;

public class FiringSystems : MonoBehaviour {
    public int limit = -1;
    List<Transform> existingBullets = new List<Transform>();
    [HideInInspector] public bool reachedLimitLastFrame;

    internal Transform Fire(TransformData bullet, Transform sourceThatFired) {
        
        Transform tr = Instantiate((Transform)bullet.GetValue(), sourceThatFired.position, sourceThatFired.rotation);
        tr.GetComponent<AITargeter>().OnSpawned(sourceThatFired.GetComponentInParent<TreeBehaviour>());
        existingBullets.Add(tr);
        
        if (existingBullets.Count == limit){
            reachedLimitLastFrame = true;
        }
        return tr;
    }
    public bool IsLimited() {
        for (int i = 0; i < existingBullets.Count; i++) {
            if (existingBullets[i] == null) {
                existingBullets.RemoveAt(i);
                i--;
            }
        }
        if (limit == -1 || existingBullets.Count < limit) {
            return false;
        }
        return true;
    }
}
/// <summary>
/// Allows us to distribute 1 tagged AI behaviour to 500 sub AI objects.
/// 
/// CANNOT BE ON ROOT. Use Tree behaviour instead.
/// </summary>
public class AITargeter :MonoBehaviour{

    public string tagTarget;
    public Movement moving;
    public FiringSystems firing;

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
