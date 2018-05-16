using UnityEngine;
public class PathCommand : IMoveCommand {
    int active = 0;
    public Path path;

    public PathCommand(Path path) {
        this.path = path;
    }

    public Vector3 GetMovePoint() {
        return path.GetNodes()[active];
    }

    public bool IsDone() {
        return active == path.GetNodes().Count-1;
    }
}