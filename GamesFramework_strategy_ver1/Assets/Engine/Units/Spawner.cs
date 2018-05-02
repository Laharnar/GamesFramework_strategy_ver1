
// This approach isn't the best, would be better to keep 1 instance in library for every spawn object ever,
// instead of mono reference.
public static class Spawner {

    /// <summary>
    /// Gets who spawned this object.
    /// Null if none or destroyed.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    internal static AITargeter GetSpawnerOfSource(AITargeter source) { 
        // get tree beh on self and it should have prestored spawner.
        return source.GetComponentInParent<TreeBehaviour>().spawner;
    }
}
