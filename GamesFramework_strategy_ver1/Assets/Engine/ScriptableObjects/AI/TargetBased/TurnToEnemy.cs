using UnityEngine;
[CreateAssetMenu(fileName = "_TurnToEnemy", menuName = "Framework/AI/New TurnToEnemy", order = 1)]
public class TurnToEnemy : SOMovementBehaviour {

    AITargeter targeter;
    public string note = "SetToForw to rotate between 2 points";

    public override NodeResult Execute() {
        AITargeter src = source as AITargeter;
        if (FactionUnits.NoEnemies(src.stats)) {
            return NodeResult.Failure;
        }

        if (!src.movingData.IsIdle) {
            return NodeResult.Running;
        }
        targeter = src.FindClosestEnemy();
        src.AttachPathOfDirs(mode,
            GenerateDirPathToTarget(mode, src, targeter));
        return NodeResult.Success;
    }

    private Vector3[] GenerateDirPathToTarget(MovementMode mode, AITargeter source, AITargeter target) {
        if (mode == MovementMode.SetToForward) {
            return CircleMotion.GeneratePtToPtMotionOnForw(source, target, 16);
        } else if (mode == MovementMode.AxisBasedRotation) {
            return new Vector3[1] { target.transform.position - source.transform.position }; //CircleMotion.GeneratePtToPtMotionOnForw(source.GetComponent<AxisSubMovement>().yRot.GetComponent<AITargeter>(), target, 16);
        } else 
            Debug.Log("Unsupported mode. [MoveToEnemy]" + mode);
        return new Vector3[0];
    }
}