using System;
using UnityEngine;
[CreateAssetMenu(fileName = "New game obj data", menuName = "Framework/Data/New game obj data", order = 1)]
public class GameObjectData : SODataStandard {
    public GameObject goPref;

    public override object GetValue() {
        return goPref;
    }
}

[CreateAssetMenu(fileName = "New transform data", menuName = "Framework/Data/New transform data", order = 1)]
public class TransformData : SODataStandard {
    public Transform transformPref;

    public override object GetValue() {
        return transformPref;
    }
}
