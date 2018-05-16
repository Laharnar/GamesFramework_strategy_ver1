using UnityEngine;
[CreateAssetMenu(fileName = "_MoveToEnemy", menuName = "Framework/AI/New MoveToEnemy", order = 1)]
public class MoveToEnemy : SOMovementBehaviour {

    AITargeter targeter;
    public string note = "SetToForw to rotate between 2 points";

    public override NodeResult Execute() {
        AITargeter src = source as AITargeter;
        if (FactionUnits.NoEnemies(src.stats)) {
            return NodeResult.Failure;
        }

        if (!src.movingData.IsIdle) {
            return NodeResult.Failure;
        }
        targeter = src.FindClosestEnemy();
        src.AttachPathOfDirs(mode,
            GenerateDirPathToTarget(mode, src, targeter));
        return NodeResult.Success;
    }

    private Vector3[] GenerateDirPathToTarget(MovementMode mode, AITargeter source, AITargeter target) {
        if (mode == MovementMode.AdditiveSetForward)
            return new Vector3[1] { (target.transform.position - source.transform.position) / 10 };
        else if (mode == MovementMode.AdditiveToTransform)
            return new Vector3[1] { source.transform.InverseTransformDirection(target.transform.position - source.transform.position) / 10 };
        else if (mode == MovementMode.SetToForward) {
            return CircleMotion.GeneratePtToPtMotionOnForw(source, target, 16);
        }
        else Debug.Log("Unsupported mode. [PathfindToTargetMotion]"+mode);
        return new Vector3[0];
    }
}
