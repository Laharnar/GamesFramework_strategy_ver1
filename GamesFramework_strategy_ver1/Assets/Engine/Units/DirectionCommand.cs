using UnityEngine;

/// <summary>
/// Stores how should this dir be used
/// </summary>
[System.Serializable]
public class DirectionCommand {
    public Vector3 dir;
    public MovementMode mode = 0; // 0: additive, 1: directive

    public DirectionCommand(Vector3 dir) {
        this.dir = dir;
    }
}
