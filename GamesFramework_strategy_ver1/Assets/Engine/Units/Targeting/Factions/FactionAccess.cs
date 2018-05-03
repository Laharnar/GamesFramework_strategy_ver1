﻿using UnityEngine;

/// <summary>
/// One per unit root.
/// </summary>
public class FactionAccess:MonoBehaviour {

    public StringData faction;

    // Note: Health doesn't fit here, because different subunits can have different hp.
    // Structural Hp does.

    private void Start() {
        FactionUnits.RegisterUnit(this);
    }

    private void OnDestroy() {
        FactionUnits.UnregisterUnit(this);
    }
}
