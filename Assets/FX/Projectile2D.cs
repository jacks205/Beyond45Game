using UnityEngine;
using System.Collections;

public class Projectile2D : MonoBehaviour {

    public CircleCollider2D circleCollider;

    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    public Vector2 movement;

    public bool isEnemyShot = false;
    public float damage = 25f;


	// Use this for initialization
	protected virtual void Start () {
        circleCollider = GetComponent<CircleCollider2D>();
        Destroy(this.gameObject, 7f);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y);
	}

	protected virtual void FixedUpdate(){
        rigidbody2D.velocity = movement;
    }
}
