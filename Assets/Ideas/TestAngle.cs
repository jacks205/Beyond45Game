using UnityEngine;
using System.Collections;

public class TestAngle : MonoBehaviour {

    public GameObject other;

    public float angle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        angle = Mathf.Rad2Deg * Mathf.Sin((this.transform.position.y - other.transform.position.y)/(this.transform.position.x - other.transform.position.x));
	}
}
