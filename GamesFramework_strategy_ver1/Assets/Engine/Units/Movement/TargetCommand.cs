using UnityEngine;
public class TargetCommand :IMoveCommand {
    [SerializeField] Vector3 target;

    public TargetCommand(Vector3 target) {
        this.target = target;
    }

    public Vector3 GetMovePoint() {
        return target;
    }

    /// <summary>
    /// Done when target is destroyed.
    /// </summary>
    /// <returns></returns>
    public bool IsDone() {
        return target == null;
    }
}