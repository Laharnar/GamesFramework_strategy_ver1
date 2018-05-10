using System;
using UnityEngine;
public class Health:MonoBehaviour {
    
    [SerializeField] int _hp=5;

    public int hp {
        get { return _hp; }
        set {
            _hp = value; if (_hp <= 0) {
                Destroy(gameObject);
            }
        }
    }
}