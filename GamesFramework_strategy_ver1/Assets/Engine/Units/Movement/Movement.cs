using UnityEngine;

/// <summary>
/// Stores a list of directions for us from other scripts.
/// </summary>
public partial class Movement : MonoBehaviour {
    // btw saving points, would take same space than as directions. that adding and removing.
    //public List<Vector3> directionsData = new List<Vector3>();

    // Directions are taken from back for removing performance. Note: is same, since we insert at front.
    /*FloatData speedImport = 1f;*/
    public float speed = 1f;
    public float checkRange = 0.1f;

    IMoveCommand activeMove { get { return movementCommands.activePoint; } }

    public CommandData movementCommands;
    public AxisSubMovement axisRot;

    private void Start() {
        if (movementCommands == null) {
            movementCommands = gameObject.AddComponent<CommandData>();
        }
    }

    // Update is called once per frame
    void Update () {
        if (movementCommands.points.Count > 0) {
            Move();
        }
    }

    private void Move() {
        PointCommand activeDir = activeMove as PointCommand;
        if (activeDir.mode == MovementMode.AdditiveToTransform) {
            Vector3 dir = activeDir.GetMovePoint()-transform.position;
            if (Vector3.Distance(activeDir.GetMovePoint(), transform.position)<= checkRange) {
                ConsumeDirection();
            }
            if (movementCommands.points.Count > 0) {
                transform.Translate(dir.normalized * speed * Time.deltaTime);
            }
        } else if (activeDir.mode == MovementMode.SetToUp) {
            transform.up = activeDir.GetMovePoint() - transform.position;
            ConsumeDirection();

        } else if (activeDir.mode == MovementMode.SetToForward) {
            transform.forward = activeDir.GetMovePoint() - transform.position;
            ConsumeDirection();
        } else if (activeDir.mode == MovementMode.AdditiveSetForward) {
            transform.forward = activeDir.GetMovePoint() - transform.position;
            if (Vector3.Distance(activeDir.GetMovePoint(), transform.position) <= checkRange) {
                ConsumeDirection();
            }
            if (movementCommands.points.Count > 0) {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        } else if (activeDir.mode == MovementMode.AxisBasedRotation) {
            axisRot.targetDir = activeDir.GetMovePoint() - transform.position;
            ConsumeDirection();
        }
    }

    private void ConsumeDirection() {
        movementCommands.RemoveLast();
    }
}
