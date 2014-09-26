using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public static float MaxSpeed = 5;
	public Animator legAnimator = null;
	public Animator upperBodyAnimator = null;
	public static bool FacingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask  whatIsGround;
	public float jumpForce = 150;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
//		anim.SetBool ("Ground", grounded);
//		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		float move = Input.GetAxis ("Horizontal");
		SetRunningSpeed(Mathf.Abs (move));
		rigidbody2D.velocity = new Vector2 (move * MaxSpeed, rigidbody2D.velocity.y); 
		if (move > 0 && !FacingRight) {
			Flip ();
		} else if (move < 0 && FacingRight) {
			Flip ();
		}

		Physics2D.IgnoreLayerCollision( LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"),!grounded || rigidbody2D.velocity.y > 0);

		float vertical = Input.GetAxis ("Vertical");
		if (grounded && vertical < -0.05) {
			Physics2D.IgnoreLayerCollision( LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), vertical < -0.05);
		}
	}

	void Update(){
		if (grounded && Input.GetAxis ("Jump") > 0) {
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
		}
	}

	void Flip(){
		FacingRight = !FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void SetRunningSpeed(float speed){
		legAnimator.SetFloat ("Speed", speed);
		upperBodyAnimator.SetFloat ("Speed", speed);
	}

}
