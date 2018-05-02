using UnityEngine;
[CreateAssetMenu(fileName = "SpawnerTargetCircleMotion_", menuName = "Framework/AI/New SpawnerTargetCircleMotion", order = 1)]
public class SpawnerTargetCircleMotion : SOTreeLeaf {
    public MovementMode mode;
    public int pointsDetail = 32;
    public float size = 1;
    Vector3 axis;

    public override NodeResult Execute() {
        AITargeter aiSource = source as AITargeter;
        AITargeter s = Spawner.GetSpawnerOfSource(source as AITargeter);
        if (s)
            axis = s.transform.forward;
        else {
            Debug.LogError("Missing spawner source.");
        }
        (source as AITargeter).moving.Attach(mode,
            CircleMotion.GenerateCircleOfDirections(aiSource.transform.position, s.transform.position, size, pointsDetail, axis));
        return NodeResult.Success;
    }
}
