public class WaveUnit : CollisionUser {

    private void Start() {
        TreeBehaviour[] s = GetComponents<TreeBehaviour>();
        foreach (var item in s) {
            item.enabled = true;
        }
    }

    public override void OnTriggered(CollisionHandler other) {
        TreeBehaviour[] s = GetComponents<TreeBehaviour>();
        foreach (var item in s) {
            item.enabled = false;
        }
    }
}