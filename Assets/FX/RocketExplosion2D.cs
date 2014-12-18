using UnityEngine;
using System.Collections;

public class RocketExplosion2D : MonoBehaviour {

    public float explosionRate = 0.5f;
    public float explosionMaxSize = 1f; 
    public float explosionSpeed = 2f;
    public float currentRadius = 0f;
    public float explosionPower = 200f;
    public float explosionVerticalMultiplier = 10f;
    public int explosionDamage = 100;

    bool exploded = false;
    CircleCollider2D explosionRadius;
	// Use this for initialization
	void Start () {
        explosionRadius = GetComponent<CircleCollider2D>();
        explosionDamage = Random.Range(explosionDamage - 20, explosionDamage);
	}
	
	// Update is called once per frame
	void Update () {
	
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
                    if (enemyHealth != null)
                    {
                        Vector2 target = other.gameObject.transform.position;
                        Vector2 grenade = gameObject.transform.position;
                        Vector2 direction = explosionPower * (target - grenade);
                        direction.y *= explosionVerticalMultiplier;
                        other.gameObject.rigidbody2D.AddForce(direction);
                        enemyHealth.TakeDamage((float)explosionDamage);

                    }
                    
                }
            }
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Sandbag"))
        {
            Destroy(other.gameObject.GetComponent("BoxCollider2D"));
        }
    }

    public void ExplodeCollider()
    {
        exploded = true;
    }
}
