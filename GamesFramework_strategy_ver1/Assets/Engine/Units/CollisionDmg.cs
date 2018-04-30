using System;

public class CollisionDmg : CollisionUser {
    public int damage=1;
    public override void OnTriggered(CollisionHandler other) {
        Health hpSc = other.GetComponent<Health>();
        if (hpSc) {
            hpSc.hp -= damage;
            Destroy(gameObject);
        }
    }
}