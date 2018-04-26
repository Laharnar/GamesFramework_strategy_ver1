using UnityEngine;
[CreateAssetMenu(fileName = "New PointTarget", menuName = "Framework/AI/New PointTarget", order = 1)]
public class PointTarget : SOMovementBehaviour {
    public override void Execute() {
        // Point is simply data
        AITargeter target = (AITargeter)source;
        target.moving.AttachPoints(point);
    }
}

