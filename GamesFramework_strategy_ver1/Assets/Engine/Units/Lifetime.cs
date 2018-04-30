using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour {
    public float lifetime = 10f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);
	}
}
