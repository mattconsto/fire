using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public int count = 0;
    public GameObject text;

    public AudioSource airSource;
    public float airTarget = 0;
    public AudioSource crackleSource;
    public float crackleTarget = 0;

    public void Increment() {
        text.GetComponent<Text>().text = "" + ++count;
    }

    public void Update() {
        airSource.volume = Mathf.Lerp(airSource.volume, airTarget, 0.2f);
        crackleSource.volume = Mathf.Lerp(crackleSource.volume, crackleTarget, 0.2f);
    }
}
