using UnityEngine;
[UnityEngine.CreateAssetMenu(fileName = "New InTargetRange", menuName = "Framework/AI/New InTargetRange", order = 1)]
public class InTargetRange : SOTreeLeaf {
    AITargeter targeter;
    public float range= 1;
    public override NodeResult Execute() {
        AITargeter aiSource = source as AITargeter;
        if (Faction.NoEnemies((source as AITargeter).stats))
            return NodeResult.Failure;
        targeter = Faction.FindClosestEnemy(source as AITargeter);
        if (Vector3.Distance(targeter.transform.position, aiSource.transform.position) < range) {
            return NodeResult.Success;
        }
        return NodeResult.Failure;
    }

    private Vector3[] GenerateDirPathToTarget(AITargeter source, AITargeter target) {
        return new Vector3[1] { (target.transform.position - source.transform.position) / 10 };
    }
}
