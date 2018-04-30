using UnityEngine;
/// <summary>
/// Standard nested pref solution: build object out of empty GO targets.
/// How to use: In play mode, create the object. Then create empty child GO's on positions where nested pref should be 
/// instantiated. Then add this script to it and link it to pref.
/// </summary>
public class NestedPrefabLoader:MonoBehaviour {
    public Transform prefTarget;
    private void Awake() {
        Instantiate(prefTarget, transform.position, transform.rotation, transform.parent);
    }
}