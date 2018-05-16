using UnityEngine;

[CreateAssetMenu(fileName = "New RandomPointInArea", menuName = "Framework/AI/New RandomPointInArea", order = 1)]
public class RandomPointInArea : SOMovementBehaviour {
    public float w, l;
    public override NodeResult Execute() {
        point = new Vector3(
            UnityEngine.Random.Range(-w, w), 
            UnityEngine.Random.Range(-l, l), 0);
        (source as AITargeter).AttachPoint(mode, point);
        return NodeResult.Success;
    }
}

