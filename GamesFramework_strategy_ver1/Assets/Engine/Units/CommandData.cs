using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stadard format for executing moves.
/// </summary>
public class CommandData : MonoBehaviour {

    [SerializeField] internal List<IMoveCommand> points = new List<IMoveCommand>();

    public bool IsIdle { get { return points.Count == 0; } }

    public bool IsAlmostIdle { get { return points.Count == 1; } }

    public IMoveCommand activePoint { get { return points[0]; } }

    /// <summary>
    /// Sums some number of directions already saved for final vector.
    /// UNTESTED.
    /// </summary>
    /// <param name="count">How many should be summed. -1:all</param>
    public Vector3 SumDirection(int count = -1) {
        if (count == -1) {
            count = points.Count;
        }
        Vector3 sum = Vector3.zero;
        for (int i = 0; i < count; i++) {
            sum += points[count].GetMovePoint();
        }
        return sum;
    }

    private void OnDrawGizmosSelected() {
        Vector3 start = transform.position;
        for (int i = 0; i < points.Count; i++) {
            Gizmos.DrawLine(start, points[i].GetMovePoint());
            start = points[i].GetMovePoint();
        }
    }

    /// <summary>
    /// Fixes current path with new one.
    /// </summary>
    /// <param name=""></param>
    public void Fix(MovementMode nMode, Vector3[] path, int num = 1) {
        for (int i = 0; i < num && points.Count > 0; i++) {
            points.RemoveAt(0);
        }
        for (int i = 0; i < path.Length; i++) {
            points.Add(new PointCommand(path[i]) { mode = nMode });
        }
    }

    internal void RemoveLast() {
        points.RemoveAt(points.Count-1);
    }

    /// <summary>
    /// Convert to directions and add them.
    /// </summary>
    /// <param name="points"></param>
    public void AttachPoints(MovementMode nMode, params Vector3[] points) {
        if (points.Length > 0) {
            // note: removed connection between cur pos and final point.
            for (int i = 0; i < points.Length; i++) {
                this.points.Add(new PointCommand(points[i]) { mode = nMode });
            }
        }
    }

    public void AttachCommand(IMoveCommand command) {
        points.Add(command);
    }
}