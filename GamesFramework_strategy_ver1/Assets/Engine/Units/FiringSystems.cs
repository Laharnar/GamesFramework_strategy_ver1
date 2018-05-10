using System.Collections.Generic;
using UnityEngine;
public class FiringSystems : MonoBehaviour {
    /// <summary>
    /// Use limit for things like hangars.
    /// For example set limit to 4 to allow only 4 units per hangar.
    /// </summary>
    public int limit = -1;
    [HideInInspector] public bool reachedLimitLastFrame;

    List<Transform> existingBullets = new List<Transform>();

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
