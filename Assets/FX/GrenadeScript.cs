using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour {

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask  whatIsGround;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
	}
}
