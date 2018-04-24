using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New float", menuName = "Framework/Data/New float", order = 1)]
public class FloatData:SODataStandard {

    public float f;

    public override object GetValue() {
        return f;
    }
}
