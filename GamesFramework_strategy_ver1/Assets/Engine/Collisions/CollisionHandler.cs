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

    private void Start() {
        users = GetComponents<CollisionUser>();
    }

    internal void Trigger (CollisionProxy source, Collider other) {
        CollisionHandler o = other.GetComponent<CollisionHandler>();
        if (o)
        for (int i = 0; i < users.Length; i++) {
            users[i].OnTriggered(o);
        } else {
            Debug.Log("Missing collision handler/proxy", other.transform);
        }
    }
}
