using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Executes a list of directions.
/// </summary>
public partial class Movement : MonoBehaviour {
    // btw saving points, would take less space than all that adding and removing.
    //public List<Vector3> directionsData = new List<Vector3>();

    // Directions are taken from back for removing performance. Note: is same, since we insert at front.
    [SerializeField] List<DirectionCommand> reversedDirections = new List<DirectionCommand>();
    /*FloatData speedImport = 1f;*/
    public float speed = 1f;
    public float checkRange = 0.1f;

    float fullMoveAmt = 0;
    Vector3 startPoint;

    public bool IsIdle { get { return reversedDirections.Count == 0; } }

    public bool IsAlmostIdle { get { return reversedDirections.Count == 1; } }

    DirectionCommand activeDir { get { return reversedDirections[reversedDirections.Count - 1]; } }

    public AxisSubMovement axisRot;

    private void Start() {
        /*for (int i = 0; i < directionsData.Count; i++) {
            reversedDirections.Add(new DirectionCommand(directionsData[directionsData.Count-1-i]));
        }*/
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (reversedDirections.Count > 0) {
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
            if (reversedDirections.Count > 0) {
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
            if (reversedDirections.Count > 0) {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        } else if (activeDir.mode == MovementMode.AxisBasedRotation) {
            axisRot.targetDir = activeDir.dir;
            ConsumeDirection(moveAmt);
        }
    }

    private void ConsumeDirection(float moveAmt) {
        startPoint = transform.position;//reversedDirections[reversedDirections.Count-1]- transform.position;
        reversedDirections.RemoveAt(reversedDirections.Count-1);
        fullMoveAmt = moveAmt;
    }

    /// <summary>
    /// Sums some number of directions already saved for final vector.
    /// UNTESTED.
    /// </summary>
    /// <param name="count">How many should be summed. -1:all</param>
    public Vector3 SumDirection(int count = -1) {
        if (count == -1) {
            count = reversedDirections.Count;
        }
        Vector3 sum = Vector3.zero;
        while (count > 0) {
            count--;
            sum += reversedDirections[count].dir;
        }
        return sum;
    }
    
    /// <summary>
    /// Convert to directions and add them.
    /// </summary>
    /// <param name="points"></param>
    public void AttachPoints(MovementMode nMode, params Vector3[] points) {
        if (points.Length > 0) {
            Vector3 finalPoint = SumDirection() + startPoint;
            Attach(nMode, points[0] - finalPoint);
            for (int i = 1; i < points.Length; i++) {
                Attach(nMode, points[i] - points[i - 1]);
            }
        }
    }

    /// <summary>
    /// Add directions.
    /// </summary>
    /// <param name="nMode"></param>
    /// <param name="directions"></param>
    public void Attach(MovementMode nMode, params Vector3[] directions) {
        for (int i = 0; i < directions.Length; i++) {
            //reversedDirections.Insert(0, new DirectionCommand(directions[i]) { mode = nMode });
            Vector3[] dirs = Chop(directions[i]);
            for (int j = 0; j < dirs.Length; j++) {
                reversedDirections.Insert(0, new DirectionCommand(dirs[j]) { mode = nMode });
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dirs"></param>
    /// <returns>Long vector chopped into short ones. </returns>
    /// <remarks>Might leave a small error because of division.</remarks>
    public Vector3[] Chop(Vector3 dirs) {
        List<Vector3> r = new List<Vector3>();
        float sqrMag = dirs.sqrMagnitude;
        if (sqrMag > 400) {
            float len = dirs.magnitude;
            float steps = len / 20f;
            for (int i = 0; i < steps; i++) {
                r.Add(dirs/steps);
            }
        } else {
            return new Vector3[1] { dirs };
        }
        return r.ToArray();
    }

    private void OnDrawGizmosSelected() {
        Vector3 start = transform.position;
        for (int i = 0; i < reversedDirections.Count; i++) {
            Gizmos.DrawRay(start, reversedDirections[reversedDirections.Count - 1-i].dir);
            start += reversedDirections[reversedDirections.Count - 1 - i].dir;
        }
    }

    /// <summary>
    /// Fixes current path with new one.
    /// </summary>
    /// <param name=""></param>
    public void Fix(MovementMode nMode, Vector3[] path, int num=1) {
        for (int i = 0; i < num && reversedDirections.Count > 0; i++) {
            reversedDirections.RemoveAt(reversedDirections.Count-1);
        }
        for (int i = 0; i < path.Length; i++) {
            reversedDirections.Add(new DirectionCommand(path[path.Length-1-i]) { mode = nMode });
        }
    }
}
public partial class Movement : MonoBehaviour {
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
