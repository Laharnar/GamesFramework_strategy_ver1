using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Root of wave.
/// </summary>
public class Wave :MonoBehaviour{
    internal List<WaveUnit> units=new List<WaveUnit>();
    static Vector3 defaultStartPos = Vector3.zero;

    /// <summary>
    /// Automatically adds components for the wave.
    /// Unit's don't need any additional setup.
    /// </summary>
    /// <param name="so"></param>
    public static Wave InitWave(WaveSO so) {
        Transform t = Instantiate(so.wavePref, defaultStartPos, new Quaternion());
        Wave w = t.gameObject.AddComponent<Wave>();
        w.units = new List<WaveUnit>();
        for (int i = 0; i < t.childCount; i++) {
            t.GetChild(i).gameObject.AddComponent<WaveUnit>();
        }
        return w;
    }
}
