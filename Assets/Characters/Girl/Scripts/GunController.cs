using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {
	public Sprite angle45;
	public Sprite angle90;
	public Sprite angle135;
	public Sprite angle180;
	public Sprite angle225;
	public Sprite angle270;
	public Sprite angle315;
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
		Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
		Vector3 objpos = Camera.main.WorldToViewportPoint (transform.position);        //Object position on screen
		Vector2 relobjpos = new Vector2(objpos.x - 0.5f,objpos.y - 0.5f);           //Set coordinates relative to object
		Vector2 relmousepos = new Vector2 (mouse.x - 0.5f,mouse.y - 0.5f) - relobjpos;
		float angle = Vector2.Angle (Vector2.up, relmousepos);  //Angle calculation
		if (relmousepos.x > 0)
			angle = 360-angle;
		gunAngle.SetFloat ("MouseAngle", angle);
		Debug.Log (angle);
//		if (angle <= 360 && angle > 300) {
//			spriteRend.sprite = angle45;
//			Debug.Log (spriteRend.sprite.name);
//		} else if (angle < 300 && angle >= 240) {
//			spriteRend.sprite = angle90;
//			Debug.Log (spriteRend.sprite.name);
//		}
	}

}
