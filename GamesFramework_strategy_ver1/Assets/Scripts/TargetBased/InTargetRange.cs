using UnityEngine;
[UnityEngine.CreateAssetMenu(fileName = "New InTargetRange", menuName = "Framework/AI/New InTargetRange", order = 1)]
public class InTargetRange : SOTreeLeaf{
    AITargeter targeter;
    public float range= 1;
    public override NodeResult Execute() {
        AITargeter aiSource = source as AITargeter;
        if (FactionUnits.NoEnemies((source as AITargeter).stats))
            return NodeResult.Failure;
        targeter = FactionUnits.FindClosestEnemy(source as AITargeter);
        if (Vector3.Distance(targeter.transform.position, aiSource.transform.position) < range) {
            return NodeResult.Success;
        }
        return NodeResult.Failure;
    }

    public string _tag;
    public string tag {
        get {
            return _tag;
        }

        set {
            _tag = value;
        }
    }
}
