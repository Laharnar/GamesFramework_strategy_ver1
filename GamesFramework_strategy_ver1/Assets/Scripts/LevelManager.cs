using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float speed = 1f;

    public List<WaveSO> wavePrefs=new List<WaveSO>();
    List<Wave> waves=new List<Wave>();

	// Use this for initialization
	void Start () {
        StartCoroutine(LevelLoop());	
	}

    private IEnumerator LevelLoop() {
        // spawn waves when all enemies from current wave, whenlast wave is cleared
        while (wavePrefs.Count>0) {
            waves.Add(Wave.InitWave(wavePrefs[0]));
            while (waves[0].units.Count>0) {
                yield return null;
            }
            wavePrefs.RemoveAt(0);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update () {
        // move level left
        transform.Translate(Vector3.left*Time.deltaTime*speed);
	}

}