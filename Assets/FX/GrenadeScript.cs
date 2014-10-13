using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour {

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask  whatIsGround;

    public float explosionDelay = 1f;
    public float explosionRate = 0.5f;
    public float explosionMaxSize = 1f;
    public float explosionSpeed = 2f;
    public float currentRadius = 0f;
    public float explosionPower = 200f;
    public float explosionVerticalMultiplier = 10f;
    public float explosionDamage = 100f;

    bool exploded = false;
    CircleCollider2D explosionRadius;
	// Use this for initialization
	void Start () {
        Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Grenade"), LayerMask.NameToLayer("Default"));
        Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Grenade"), LayerMask.NameToLayer("Bullet"));
        explosionRadius = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
        explosionDelay -= Time.deltaTime;
        if (explosionDelay < 0)
        {
            exploded = true;
        }
	}

    void FixedUpdate(){
        if (exploded)
        {
            if(currentRadius < explosionMaxSize){

                currentRadius += explosionRate;
               
            }else{
                Destroy(this.gameObject.transform.parent.gameObject);
            }
            explosionRadius.radius = currentRadius;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (exploded)
            {
                if (other.gameObject.rigidbody2D != null)
                {
                    EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                    if(enemyHealth != null){
                        Vector2 target = other.gameObject.transform.position;
                        Vector2 grenade = gameObject.transform.position;
                        Vector2 direction = explosionPower * (target - grenade);
                        direction.y *= explosionVerticalMultiplier;
                        other.gameObject.rigidbody2D.AddForce(direction);
                        enemyHealth.TakeDamage(explosionDamage);
                    }
                   
                }
            }
        }
    }
}
