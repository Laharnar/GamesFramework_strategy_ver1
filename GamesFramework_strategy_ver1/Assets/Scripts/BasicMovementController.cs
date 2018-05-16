using System.Collections.Generic;
using UnityEngine;

public class Formation {
    public List<Vector3> shape = new List<Vector3>();
    public List<BasicMovementController> members = new List<BasicMovementController>();
}

public class BasicMovementController : MonoBehaviour {
    // Ally simple
    void Seek(Vector3 point) { }
    void Arrive(Vector3 point) { }
    void Flee(Vector3 point) { }
    void Wander() { }
    void PathFollow(Path path) { }
    void Encircle(Vector3 point, float keepRange, Vector3 axisWeights) { }
    // target based
    void Pursuit(AITargeter target) { }
    void Evade(AITargeter target) { }
    void Encircle(AITargeter target, float keepRange, Vector3 axisWeights) { }
    // target based 
    bool TargetExists() { return true; }
    bool InRangeToTarget(int range) { return true; }
    void SaveNearestTarget() { }
    // Ally group based
    void FollowLeader() { }
    void Rally() { }
    void Rally(float allyDectectionRange) { }
    void Rally(float allyDectectionRange, int maxAllies) { }
    void Rally(float allyDectectionRange, Vector3 point, int maxAllies) { }
    // Ally formation based(specific shape of points)
    void CreateFormation(Formation formation) { }
    void JoinFormation(Formation formation) { }
    void DetachFromFormation() { }
}
