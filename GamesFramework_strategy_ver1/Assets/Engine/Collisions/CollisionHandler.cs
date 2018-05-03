using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets triggered by all proxies.
/// Put on object with rigidbody.
/// </summary>
public class CollisionHandler : CollisionProxy {
    /// <summary>
    /// Components that rely on collision.
    /// </summary>
    public CollisionUser[] users;
    public Health health;

    private void Start() {
        users = GetComponents<CollisionUser>();
        health = GetComponent<Health>();
    }

    internal void Trigger (CollisionProxy source, Collider other) {
        CollisionProxy o = other.GetComponent<CollisionProxy>();
        if (o) {
            CollisionHandler o2 = o as CollisionHandler;
            if (o2 == null)
                o2 = other.GetComponentInParent<CollisionHandler>();
            if (o2) {
                for (int i = 0; i < users.Length; i++) {
                    users[i].OnTriggered(o2);
                }
            } else {
                Debug.Log("Missing collision handler [2]", other.transform);
            }
        } else {
            Debug.Log("Missing collision handler/proxy [1]", other.transform);
        }
    }
}
