using UnityEngine;
[CreateAssetMenu(fileName = "New int", menuName = "Framework/Data/New int", order = 1)]
public class IntData : SODataStandard {

    public int f;

    public override object GetValue() {
        return f;
    }
}
