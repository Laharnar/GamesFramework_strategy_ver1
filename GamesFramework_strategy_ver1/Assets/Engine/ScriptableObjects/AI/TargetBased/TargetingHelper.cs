public static class TargetingHelper {
    public static AITargeter FindClosestEnemy(this AITargeter source) {
        return FactionUnits.FindClosestEnemy(source);
    }
}