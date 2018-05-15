using UnityEngine;
[CreateAssetMenu(fileName = "_SteeringUpdate", menuName = "Framework/AI/Steering/New SteeringUpdate", order = 1)]
public class SteeringUpdate : SOTreeLeaf {
    SteeringMode mode;
    public override NodeResult Execute() {
        AITargeter target = source as AITargeter;
        target.move.UpdateSteering();
        return NodeResult.Success;
    }
}
