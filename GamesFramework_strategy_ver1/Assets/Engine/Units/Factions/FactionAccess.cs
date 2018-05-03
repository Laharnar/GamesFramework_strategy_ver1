using UnityEngine;
public class FactionAccess:MonoBehaviour {

    public StringData faction;

    // Note: Health doesn't fit here, because different subunits can have different hp.
    // Structural Hp does.

    private void Start() {
        Faction.RegisterUnit(this);
    }

    private void OnDestroy() {
        Faction.UnregisterUnit(this);
    }
}
