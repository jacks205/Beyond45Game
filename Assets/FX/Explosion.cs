using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Explostion");
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
