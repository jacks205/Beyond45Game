using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {
	public Sprite angleDef;
	public Sprite angle45;
	public Sprite angle90;
	public Sprite angle135;
	public Sprite angle180; 	
	public Sprite angle360;
	SpriteRenderer spriteRend;
	Animator gunAngle;


	// Use this for initialization
	void Start () {
		spriteRend = GetComponent<SpriteRenderer> ();
		gunAngle = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float joystick4thAxis = Input.GetAxis ("Mouse X");
		float joystick5thAxis = Input.GetAxis ("Mouse Y");
//		Debug.Log ("X: " + joystick5thAxis + " Y: " + joystick6thAxis);
		if (joystick4thAxis <= 0.009 && joystick5thAxis <= 0.009) {
			spriteRend.sprite = angleDef;
		} 
		else if ((joystick4thAxis <= 0.09 && joystick4thAxis > 0.05) && (joystick5thAxis <= -0.05 && joystick5thAxis > -0.09)) {
			spriteRend.sprite = angle45;
			Debug.Log ("Angle45");
		} 
		else if ((joystick4thAxis > 0.08) && (joystick5thAxis <= 0.05 && joystick5thAxis > -0.05)) {
			spriteRend.sprite = angle90;
			Debug.Log ("Angle90");
		} else if ((joystick4thAxis <= 0.05 && joystick4thAxis > -0.05) && (joystick5thAxis < -0.08)) {
			spriteRend.sprite = angle360;
			Debug.Log ("Angle360");
		}else if ((joystick4thAxis <= 0.09 && joystick4thAxis > 0.05) && (joystick5thAxis <= 0.09 && joystick5thAxis > 0.05)) {
			spriteRend.sprite = angle135;
			Debug.Log ("Angle135");
		}else if ((joystick4thAxis <= 0.05 && joystick4thAxis > -0.05) && (joystick5thAxis > 0.08)) {
			spriteRend.sprite = angle180;
			Debug.Log ("Angle180");
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

}
