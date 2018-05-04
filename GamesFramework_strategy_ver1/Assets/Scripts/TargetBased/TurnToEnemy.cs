using UnityEngine;
[CreateAssetMenu(fileName = "_TurnToEnemy", menuName = "Framework/AI/New TurnToEnemy", order = 1)]
public class TurnToEnemy : SOMovementBehaviour {

    AITargeter targeter;
    public string note = "SetToForw to rotate between 2 points";

    public override NodeResult Execute() {
        if (FactionUnits.NoEnemies((source as AITargeter).stats)) {
            return NodeResult.Failure;
        }

        if (!(source as AITargeter).moving.IsIdle) {
            return NodeResult.Failure;
        }
        targeter = FactionUnits.FindClosestEnemy(source as AITargeter);
        (source as AITargeter).moving.Attach(mode,
            GenerateDirPathToTarget(mode, (source as AITargeter), targeter));
        return NodeResult.Success;
    }

    private Vector3[] GenerateDirPathToTarget(MovementMode mode, AITargeter source, AITargeter target) {
         if (mode == MovementMode.SetToForward) {
            return CircleMotion.GeneratePtToPtMotionOnForw(source, target, 16);
        } else Debug.Log("Unsupported mode. [MoveToEnemy]" + mode);
        return new Vector3[0];
    }
}