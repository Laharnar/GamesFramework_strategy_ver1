using UnityEngine;
[CreateAssetMenu(fileName = "New MoveInDir", menuName = "Framework/AI/New MoveInDir", order = 1)]
public class MoveInDir:SOMovementBehaviour {
    public override NodeResult Execute() {
        //Vector3 dir = point;
        // Point is simply data
        AITargeter target = (AITargeter)source;
        target.moving.Attach(mode, point);
        return NodeResult.Success;
    }
}