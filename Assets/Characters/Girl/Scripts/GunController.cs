using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {
	public static int BULLET_DAMAGE = 100;

	public Sprite angleDef;
	public Sprite angle45;
	public Sprite angle90;
	public Sprite angle135;
	public Sprite angle180; 	
	public Sprite angle360;

	public Transform fireAngle90;
	public Transform fireAngle45;
	public Transform fireAngle360;
	public Transform fireAngle135;
	public Transform fireAngle180;

	public Transform bullet;

	public float bulletSpeed = 2f;
	public float shootingRate = 0.25f;
	Transform currentSelectedFiringPosition;
	SpriteRenderer spriteRend;
	Animator gunAngle;
	float shootCooldown;

	// Use this for initialization
	void Start () {
		spriteRend = GetComponent<SpriteRenderer> ();
		gunAngle = GetComponent<Animator> ();
		currentSelectedFiringPosition = fireAngle90;
		shootCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		FollowJoystick ();
	}


	void FollowJoystick(){
		float joystick4thAxis = Input.GetAxis ("Mouse X");
		float joystick5thAxis = Input.GetAxis ("Mouse Y");
		//		Debug.Log ("X: " + joystick5thAxis + " Y: " + joystick6thAxis);
		if (joystick4thAxis <= 0.009 && joystick5thAxis <= 0.009) {
			spriteRend.sprite = angleDef;
			HeroController.MaxSpeed = 3f;
			currentSelectedFiringPosition = fireAngle90;
			CheckToShoot (90, 0);
		} 
		else if (HeroController.FacingRight && ((joystick4thAxis <= 0.09 && joystick4thAxis > 0.05) && (joystick5thAxis <= -0.05 && joystick5thAxis > -0.09))) {
			spriteRend.sprite = angle45;
			HeroController.MaxSpeed = 1.5f;
			currentSelectedFiringPosition = fireAngle45;
			CheckToShoot (45, 35);
		} 
		else if (HeroController.FacingRight && ((joystick4thAxis > 0.08) && (joystick5thAxis <= 0.05 && joystick5thAxis > -0.05))) {
			spriteRend.sprite = angle90;
			HeroController.MaxSpeed = 1.5f;
			currentSelectedFiringPosition = fireAngle90;
			CheckToShoot (90, 0);
		} else if (HeroController.FacingRight && ((joystick4thAxis <= 0.05 && joystick4thAxis > -0.05) && (joystick5thAxis < -0.08))) {
			spriteRend.sprite = angle360;
			HeroController.MaxSpeed = 1.5f;
			currentSelectedFiringPosition = fireAngle360;
			CheckToShoot (360, 90);
		}else if (HeroController.FacingRight && ((joystick4thAxis <= 0.09 && joystick4thAxis > 0.05) && (joystick5thAxis <= 0.09 && joystick5thAxis > 0.05))) {
			spriteRend.sprite = angle135;
			HeroController.MaxSpeed = 1.5f;
			currentSelectedFiringPosition = fireAngle135;
			CheckToShoot (135,-35);
		}else if (HeroController.FacingRight && ((joystick4thAxis <= 0.05 && joystick4thAxis > -0.05) && (joystick5thAxis > 0.08))) {
			spriteRend.sprite = angle180;
			HeroController.MaxSpeed = 1.5f;
			currentSelectedFiringPosition = fireAngle180;
			CheckToShoot (180,-90);
		}

	}

	void CheckToShoot(float spriteAngle, float bulletAngle){
		float fire1 = Input.GetAxis ("Fire1");
		if (CanAttack && fire1 == 1) {
			shootCooldown = shootingRate;
			Fire (spriteAngle, bulletAngle);
		}
	}

	void Fire(float spriteAngle, float bulletAngle){
		if(currentSelectedFiringPosition != null){
			if(HeroController.FacingRight)
			{
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Transform bulletInstance = Instantiate(bullet, currentSelectedFiringPosition.position, Quaternion.Euler(new Vector3(0,0,-spriteAngle))) as Transform;
				bulletInstance.rigidbody2D.velocity = new Vector2(bulletSpeed, bulletAngle);
			}
			else
			{ 
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Transform bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,spriteAngle))) as Transform;
				bulletInstance.rigidbody2D.velocity = new Vector2(-bulletSpeed, bulletAngle);
			}
		}
	}

	void FollowMouse(){
		Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
		Vector3 objpos = Camera.main.WorldToViewportPoint (transform.position);        //Object position on screen
		Vector2 relobjpos = new Vector2(objpos.x - 0.5f,objpos.y - 0.5f);           //Set coordinates relative to object
		Vector2 relmousepos = new Vector2 (mouse.x - 0.5f,mouse.y - 0.5f) - relobjpos;
		float angle = Vector2.Angle (Vector2.up, relmousepos);  //Angle calculation
		if (relmousepos.x > 0)
			angle = 360-angle;
		//		gunAngle.SetFloat ("MouseAngle", angle);
		Debug.Log (angle);
		if (angle <= 325 && angle > 305) {
			spriteRend.sprite = angle45;
		} else if (angle < 280 && angle >= 260) {
			spriteRend.sprite = angle90;
		} else if (angle > 350) {
			spriteRend.sprite = angle360;
		}else if (angle < 235 && angle >= 215) {
			spriteRend.sprite = angle135;
		}else if (angle < 190 && angle >= 170) {
			spriteRend.sprite = angle180;
		}
	}

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}

}
