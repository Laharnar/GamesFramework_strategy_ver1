using UnityEngine;
[CreateAssetMenu(fileName = "New SOClass", menuName = "Framework/ScriptableObjects/New SOClass", order = 1)]

public class SOClass : ScriptableObject {

    public static string[] allClasses;
    public int selected = 0;
    public object element;
}
