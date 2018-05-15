using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put on objects that have collider.
/// </summary>
public class CollisionProxy : MonoBehaviour {
    BoxCollider source;
    private void OnTriggerEnter(Collider other) {
        GetComponentInParent<CollisionHandler>().Trigger(this, other);
    }

    internal float GetSize() {
        if (!source)
            source = GetComponent<BoxCollider>();
        return Mathf.Max(source.bounds.size.x, source.bounds.size.y, source.bounds.size.z);
    }
}
