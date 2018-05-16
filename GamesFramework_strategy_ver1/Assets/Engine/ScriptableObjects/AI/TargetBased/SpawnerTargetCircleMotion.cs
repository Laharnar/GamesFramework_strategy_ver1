using UnityEngine;
[CreateAssetMenu(fileName = "SpawnerTargetCircleMotion_", menuName = "Framework/AI/New SpawnerTargetCircleMotion", order = 1)]
public class SpawnerTargetCircleMotion : SOTreeLeaf {
    public MovementMode mode;
    public int pointsDetail = 32;
    public float size = 1;
    Vector3 axis;

    public override NodeResult Execute() {
        AITargeter aiSource = source as AITargeter;
        AITargeter spawnedBy = Spawner.GetSpawnerOfSource(source as AITargeter);
        if (spawnedBy)
            axis = spawnedBy.transform.forward;
        else {
            Debug.LogError("Missing spawner source. Ignoring node.", aiSource);
            return NodeResult.Failure;
        }
        aiSource.AttachPathOfDirs(mode,
            CircleMotion.GenerateCircleOfDirections(aiSource.transform.position, spawnedBy.transform.position, size, pointsDetail, axis));
        return NodeResult.Success;
    }
}
