using UnityEngine;
[CreateAssetMenu(fileName = "_SteeringFindTarget", menuName = "Framework/AI/Steering/New SteeringFindTarget", order = 1)]
public class SteeringFindTarget : SOTreeLeaf {
    SteeringMode mode;
    public override NodeResult Execute() {
        AITargeter target = source as AITargeter;
        target.move.target = target.FindClosestEnemy();
        return NodeResult.Success;
    }
}
