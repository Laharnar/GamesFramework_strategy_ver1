using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DirectionCommand {
    public Vector3 dir;
    public MovementMode mode = 0; // 0: additive, 1: directive

    public DirectionCommand(Vector3 dir) {
        this.dir = dir;
    }
}

/// <summary>
/// Executes a list of directions.
/// </summary>
public class Movement : MonoBehaviour {

    public List<Vector3> directionsData = new List<Vector3>();

    // Directions are taken from back for performance.
    [SerializeField] List<DirectionCommand> reversedDirections = new List<DirectionCommand>();
    /*FloatData speedImport = 1f;*/
    public float speed = 1f;
    public float checkRange = 0.1f;

    float fullMoveAmt = 0;
    Vector3 startPoint;

    public bool IsIdle { get { return reversedDirections.Count == 0; } }

    private void Start() {
        for (int i = 0; i < directionsData.Count; i++) {
            reversedDirections.Add(new DirectionCommand(directionsData[directionsData.Count-1-i]));
        }
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (reversedDirections.Count > 0) {
            // move in that dir until in range
            float moveAmt = speed*Time.deltaTime;
            fullMoveAmt += moveAmt;
            int id = reversedDirections.Count - 1;
            
            id = Move(moveAmt, id);
        } else {
            startPoint = transform.position;
        }
    }

    private int Move(float moveAmt, int id) {
        if (reversedDirections[id].mode == MovementMode.AdditiveToTransform) {
            if (reversedDirections[id].dir.magnitude < fullMoveAmt + checkRange) {
                startPoint = transform.position;//reversedDirections[reversedDirections.Count-1]- transform.position;
                reversedDirections.RemoveAt(id);
                id--;
                fullMoveAmt = moveAmt;
            }
            if (reversedDirections.Count > 0) {
                transform.Translate(reversedDirections[id].dir.normalized * speed * Time.deltaTime);
            }
        } else if (reversedDirections[id].mode == MovementMode.SetToUp) {
            transform.up = reversedDirections[id].dir.normalized;

            startPoint = transform.position;//reversedDirections[reversedDirections.Count-1]- transform.position;
            reversedDirections.RemoveAt(id);
            id--;
            fullMoveAmt = moveAmt;
        } else if (reversedDirections[id].mode == MovementMode.SetToForward) {
            transform.forward = reversedDirections[id].dir.normalized;

            startPoint = transform.position;//reversedDirections[reversedDirections.Count-1]- transform.position;
            reversedDirections.RemoveAt(id);
            id--;
            fullMoveAmt = moveAmt;
        }

        return id;
    }

    /// <summary>
    /// Sums some number of directions already saved.
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
    public void AttachPoints(params Vector3[] points) {
        if (points.Length > 0) {
            Vector3 finalPoint = SumDirection() + startPoint;
            Attach(points[0]-finalPoint);
            for (int i = 1; i < points.Length; i++) {
                Attach(points[i]- points[i - 1]);
            }
        }
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

    public void Attach(params Vector3[] directions) {
        for (int i = 0; i < directions.Length; i++) {
            reversedDirections.Insert(0, new DirectionCommand(directions[i]));
        }
    }

    public void Attach(MovementMode nMode, params Vector3[] directions) {
        for (int i = 0; i < directions.Length; i++) {
            reversedDirections.Insert(0, new DirectionCommand(directions[i]) { mode=nMode });
        }
    }
}
