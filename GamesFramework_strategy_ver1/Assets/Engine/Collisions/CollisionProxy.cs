using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put on objects that have collider.
/// </summary>
public class CollisionProxy : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        GetComponentInParent<CollisionHandler>().Trigger(this, other);
    }
}
