﻿using System;
[System.Serializable]
[UnityEngine.CreateAssetMenu(fileName = "New IsIdleCond", menuName = "Framework/AI/New IsIdleCond", order = 1)]
public class IsIdleCond : SOParentNode ,ISOTagNode {
    public string _tag;
    public string tag {
        get {
            return _tag;
        }

        set {
            _tag = value;
        }
    }

    public override void Execute() {
        if ((source as AITargeter).moving.IsIdle) {
            child.StandardNodeExecute();
        }
    }
}
