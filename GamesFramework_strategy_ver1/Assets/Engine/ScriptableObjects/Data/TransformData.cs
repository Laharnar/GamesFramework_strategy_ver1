using UnityEngine;
[CreateAssetMenu(fileName = "New transform data", menuName = "Framework/Data/New transform data", order = 1)]
public class TransformData : SODataStandard {
    public Transform transformPref;

    public override object GetValue() {
        return transformPref;
    }
}
