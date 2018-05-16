using System;
using UnityEngine;
public interface IMoveCommand {
    Vector3 GetMovePoint();
    /// <summary>
    /// For things like paths, it's done when last point is active.
    /// For object targets, when they are null.
    /// For single points always done.
    /// </summary>
    /// <returns></returns>
    bool IsDone();
}
