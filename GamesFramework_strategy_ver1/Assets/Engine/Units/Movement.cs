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

    float fullMoveAmt = 0;
    Vector3 startPoint;

    DirectionCommand activeDir { get { return moveData.activeDir; } }

    public CommandData moveData;
    public AxisSubMovement axisRot;


    private void Start() {
        if (moveData == null) {
            moveData = gameObject.AddComponent<CommandData>();
        }
        /*for (int i = 0; i < directionsData.Count; i++) {
            reversedDirections.Add(new DirectionCommand(directionsData[directionsData.Count-1-i]));
        }*/
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (moveData.directions.Count > 0) {
            // move in that dir until in range
            float moveAmt = speed*Time.deltaTime;
            //fullMoveAmt += moveAmt;//
            fullMoveAmt += moveAmt;
            Move(moveAmt);
        } else {
            startPoint = transform.position;
        }
    }

    private void Move(float moveAmt) {
        if (activeDir.mode == MovementMode.AdditiveToTransform) {
            if (activeDir.dir.magnitude < fullMoveAmt + checkRange) {
                ConsumeDirection(moveAmt);
            }
            if (moveData.directions.Count > 0) {
                transform.Translate(activeDir.dir.normalized * speed * Time.deltaTime);
            }
        } else if (activeDir.mode == MovementMode.SetToUp) {
            transform.up = activeDir.dir.normalized;
            ConsumeDirection(moveAmt);

        } else if (activeDir.mode == MovementMode.SetToForward) {
            transform.forward = activeDir.dir.normalized;
            ConsumeDirection(moveAmt);
        } else if (activeDir.mode == MovementMode.AdditiveSetForward) {
            transform.forward = activeDir.dir.normalized;
            if (activeDir.dir.magnitude < fullMoveAmt + checkRange) {
                ConsumeDirection(moveAmt);
            }
            if (moveData.directions.Count > 0) {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        } else if (activeDir.mode == MovementMode.AxisBasedRotation) {
            axisRot.targetDir = activeDir.dir;
            ConsumeDirection(moveAmt);
        }
    }

    private void ConsumeDirection(float moveAmt) {
        startPoint = transform.position;//reversedDirections[reversedDirections.Count-1]- transform.position;
        moveData.RemoveLast();
        fullMoveAmt = moveAmt;
    }

    

}
/*public partial class Movement : MonoBehaviour {
    public Transform xRot, yRot, zRot;
    [HideInInspector] public Vector3 targetPoint;
    public Vector3 targetDir = Vector3.one;

    private void AxisBasedRotation() {

        Vector3 dir = targetPoint - transform.position;//targetDir;
        //targetPoint = transform.position + dir;
        if (yRot) {
            Vector3 yDir = targetPoint - yRot.position;
            //yDir.y = yRot.right.y;
            yRot.right = yDir;
            yRot.transform.localRotation = Quaternion.Euler(0, yRot.transform.localEulerAngles.y, 0);
        }
        if (xRot) {// untested
            Vector3 xDir = targetPoint - xRot.position;
            //xDir.x = xRot.right.x;
            xRot.right = xDir;
            xRot.transform.localRotation = Quaternion.Euler(xRot.transform.localEulerAngles.x, 0, 0);
        }
        if (zRot) {
            Vector3 zDir = targetPoint - zRot.position;
            //zDir.z = zRot.right.z;
            //zDir.y = zRot.parent.right.y;// zRot.right.z;
            zRot.right = zDir;
            zRot.transform.localRotation = Quaternion.Euler(0, 0, zRot.transform.localEulerAngles.z);
        }
    }
}
*/