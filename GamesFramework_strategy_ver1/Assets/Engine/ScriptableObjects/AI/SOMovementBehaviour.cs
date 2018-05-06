using UnityEngine;

/// <summary>
/// Standard leaf node used for generating movement behaviours.
/// Result is an aim point and mode of movement.
/// </summary>
[System.Serializable]
public abstract class SOMovementBehaviour : SOTreeLeaf {
    public Vector3 point;
    public MovementMode mode = 0;
}

