using System;
using UnityEngine;
[CreateAssetMenu(fileName = "New game obj data", menuName = "Framework/Data/New game obj data", order = 1)]
public class GameObjectData : SODataStandard {
    public GameObject goPref;

    public override object GetValue() {
        return goPref;
    }
}
