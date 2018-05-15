﻿using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New PointTarget", menuName = "Framework/AI/New PointTarget", order = 1)]
public class PointTarget : SOMovementBehaviour {
    public override NodeResult Execute() {
        // Point is simply data
        AITargeter target = (AITargeter)source;
        target.movingData.AttachPoints(mode, point);
        return NodeResult.Success;
    }
}
