using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionMask {
    All,
    OtherFactions
}
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

    // ---------- faction based -------------- export it out.
    /// <summary>
    /// Who will trigger it.
    /// </summary>
    public CollisionMask mask = CollisionMask.All;
    FactionAccess faction;

    private void Start() {
        users = GetComponents<CollisionUser>();
        health = GetComponent<Health>();

        //if (mask == CollisionMask.OtherFactions) {
            faction = GetComponentInParent<FactionAccess>();
            if (faction == null && GetComponent<TreeBehaviour>()) {
                faction = GetComponent<TreeBehaviour>().spawner.stats;
            }
       // }
    }

    internal void Trigger (CollisionProxy source, Collider other) {
        CollisionProxy otherAsProxy = other.GetComponent<CollisionProxy>();
        if (otherAsProxy == null) {
            Debug.Log("Missing collision proxy [1]. Attach proxy to obj with collider and replay.", other.transform);
            return;
        }
        CollisionHandler otherHandler = otherAsProxy as CollisionHandler;
        if (otherHandler == null)
            otherHandler = other.GetComponentInParent<CollisionHandler>();
        if (otherHandler) {
            //Debug.Log(mask + " "+otherHandler.mask);
            switch (mask) {
                // for example for meteors, that damage all factions
                case CollisionMask.All:
                    for (int i = 0; i < users.Length; i++) {
                        users[i].OnTriggered(otherHandler);
                    }
                    break;
                case CollisionMask.OtherFactions:
                    if (faction!= null && otherHandler.faction!= null && faction.faction != otherHandler.faction.faction )
                        for (int i = 0; i < users.Length; i++) {
                            users[i].OnTriggered(otherHandler);
                        }
                    break;
                default:
                    break;
            }
        } else {
            Debug.Log("Missing collision handler [2]", other.transform);
        }
    }
}
