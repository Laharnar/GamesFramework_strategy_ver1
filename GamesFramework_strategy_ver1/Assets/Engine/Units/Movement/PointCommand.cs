using System;
using UnityEngine;

/// <summary>
/// Stores how should this dir be used
/// </summary>
[System.Serializable]
public class PointCommand : IMoveCommand {
    [SerializeField]Vector3 pt;
    public MovementMode mode = 0; // 0: additive, 1: directive

    public PointCommand(Vector3 pt) {
        this.pt = pt;
    }

    public Vector3 GetMovePoint() {
        return pt;
    }

    /// <summary>
    /// Never done, because there is only 1 point.
    /// Use some other check over this, like range check.
    /// </summary>
    /// <returns></returns>
    public bool IsDone() {
        return true;
    }
}
