using UnityEngine;
[CreateAssetMenu(fileName = "New PathfindToTargetMotion", menuName = "Framework/AI/New PathfindToTargetMotion", order = 1)]
public class PathfindToTargetMotion : SOMovementBehaviour {

    AITargeter targeter;

    public override NodeResult Execute() {

        if (Faction.NoEnemies((source as AITargeter).stats)) {
            Debug.Log("no Enemies");
            return NodeResult.Failure;
        }
        targeter = Faction.FindClosestEnemy(source as AITargeter);
        (source as AITargeter).moving.Attach(mode,
            GenerateDirPathToTarget((source as AITargeter), targeter));
        return NodeResult.Success;
    }

    private Vector3[] GenerateDirPathToTarget(AITargeter source, AITargeter target) {
        return new Vector3[1] { source.transform.InverseTransformDirection(target.transform.position-source.transform.position) /10};
    }
}
