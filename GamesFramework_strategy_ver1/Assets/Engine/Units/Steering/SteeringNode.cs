using UnityEngine;
[CreateAssetMenu(fileName = "_SteeringNode", menuName = "Framework/AI/Steering/New SteeringNode ", order = 1)]
public class SteeringNode : SOTreeLeaf {
    SteeringMode mode;
    public override NodeResult Execute() {
        AITargeter target = source as AITargeter;
        target.move.AttachMove(mode);
        return NodeResult.Success;
    }
}
