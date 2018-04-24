using UnityEngine;
[CreateAssetMenu(fileName = "New string", menuName = "Framework/Data/New string", order = 1)]
public class StringData : SODataStandard {

    public string s;

    public override object GetValue() {
        return s;
    }
}
